﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    [Header("---------------------------------------------------------------------------")]
    [Header(" *** TIMERS ***")]
    [Header("- Phase One")]
    [Tooltip("Time in seconds to complete the first phase of the match.(Meat grab)")]
    [Range(1, 120)]
    public int _Phase1Length = 30;
    [Header("- Phase Two")]
    public bool _Phase2Timer = false;
    [Tooltip("Time in seconds to complete the second phase of the match.(Kill players)")]
    [Range(1, 600)]
    public int _Phase2Length = 120;
    [Tooltip("When there are 2 player's left in the match, remove all lives so that the one who survives for the longest wins.")]
    public bool _SuddenDeath = false;
    [Tooltip("")]
    public EliminatedPlayerBanner _PlayerEliminatedPanel;
    [Tooltip("")]
    public Text _PlayerEliminatedText;

    /// Public (internal)
    [HideInInspector]
    public static MatchManager _pInstance;                          // This is a singleton script, Initialized in Awake().

    public enum GameState {                                         // Enumerator for match states.

        Intro,
        Phase1,
        Phase2,
        Gameover
    }                                       

    /// Private 
    private bool _GamePaused = false;                               // Returns TRUE if the game is currently paused.
    private bool _CinematicPlaying = true;                          // Returns TRUE if the game is currently playing a cinematic.
    private bool _Gameplay;                                         // Returns TRUE if the game is currently in a gameplay state.
    private float _TimerPhase1 = 0f;
    private float _TimerPhase2 = 0f;
    private bool _MaxMatchTimer = false;                            // Returns TRUE if the game has a maximum time limit for gameplay.
    private GameState _GameState = GameState.Intro;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    public void Start() {

        // Setup match
        MatchSetup();

        // Start match intro cinematic
        Introduction();

        // Start gameplay music & ambience
        SoundManager._pInstance.PlayMusicGameplay();
        SoundManager._pInstance.PlayAmbienceGameplay();
    }

    //--------------------------------------------------------------
    // *** FRAME ***
    
    public void Update() {

        switch (_GameState) {

            // ****** OPENING CINEMATIC ******
            case GameState.Intro: {

                    _CinematicPlaying = true;

                    // Disable pausing (and force unpause)
                    SetPause(false);

                    // Once fade in has completed
                    if (Fade._pInstance.IsFadeComplete() == true) {

                        // Cinematic complete so start the gameplay
                        MatchStart();
                    }
                    break;
                }

            // ****** PHASE ONE ******
            case GameState.Phase1: {

                    _CinematicPlaying = false;

                    // Game is currently unpaused
                    if (_GamePaused == false) {

                        // Deduct from timer 1 second per second
                        _TimerPhase1 -= Time.deltaTime;
                        
                        foreach (var player in PlayerManager._pInstance.GetActiveNecromancers()) {

                            // Add 1 second per second to the alive time
                            player._Player.AddTimeAlive(Time.deltaTime);
                        }

                        // Once phase1 timer is complete
                        if (_TimerPhase1 <= 0f) {

                            // Initiate phase2
                            _GameState = GameState.Phase2;
                            AiManager._pInstance.OnPhase2Start();

                            // Fade in message widget
                            HUD._pInstance._EliminatePlayersWidget.GetComponent<FadingMessage>().Enter();

                            // Play announcer sound
                            if (SoundManager._pInstance._EnableAnnouncer == true) {

                                SoundManager._pInstance._Announcer.PlayPhaseTwoStart();
                            }

                            // Play phase transition sound
                            SoundManager._pInstance.PlayPhaseTransition();
                        }
                    }
                    break;
                }

            // ****** PHASE TWO ******
            case GameState.Phase2: {

                    _CinematicPlaying = false;

                    // Game is currently unpaused
                    if (_GamePaused == false) {

                        // If phase2 runs on a timer
                        if (_MaxMatchTimer == true) {

                            // Deduct from timer 1 second per second
                            _TimerPhase2 -= Time.deltaTime;

                            foreach (var player in PlayerManager._pInstance.GetActiveNecromancers()) {

                                // Add 1 second per second to the alive time
                                player._Player.AddTimeAlive(Time.deltaTime);
                            }

                            // Check for premature game over
                            LastManStandingChecks();

                            // Once phase2 timer is complete
                            if (_TimerPhase2 <= 0f) {

                                // Stop phase2 (GAME OVER)
                                MatchCompleted();
                            }
                        }

                        // Phase 2 has no time limit
                        else { /// _MaxMatchTimer == false

                            // Check for premature game over
                            LastManStandingChecks();
                        }

                        // Check for sudden death event
                        SuddenDeathChecks();
                    }
                    break;
                }

            // ****** MATCH COMPLETE ******
            case GameState.Gameover: {

                    _CinematicPlaying = true;
                    MatchCompleted();
                    break;
                }

            default: {

                    break;
                }
        }

        // Gameplay is true if the game isnt paused AND NOT playing a cinematic
        _Gameplay = _GamePaused == false && _CinematicPlaying == false;
    }

    public void LastManStandingChecks() {

        // There is now only 1 alive player in the match
        if (PlayerManager._pInstance.GetActiveNecromancers().Count <= 1) {

            // Match completed
            MatchCompleted();
        }
    }

    public void SuddenDeathChecks() {

        // Only check if sudden death is enabled
        if (_SuddenDeath == true) {

            // If there are only 2 players left in the match
            if (PlayerManager._pInstance.GetActiveNecromancers().Count == 2) {

                // Destroy all their respawns so that both players only have 1 life left
                foreach (Character charPlyr in PlayerManager._pInstance.GetActiveNecromancers()) {

                    charPlyr._Player.SetRespawnsLeft(0);
                }

                // Play announcer sound
                if (SoundManager._pInstance._EnableAnnouncer == true) {

                    SoundManager._pInstance._Announcer.PlaySuddenDeath();
                }

                // Set sudden death to false to avoid having the function being called the next frame
                _SuddenDeath = false;
            }
        }
    }

    //--------------------------------------------------------------
    // *** MATCH ***

    public void Introduction() {

        // Set gameState
        _GameState = GameState.Intro;

        // Hide hud
        HUD._pInstance.ShowHUD(false);

        // Fade in message widget
        HUD._pInstance._GetReadyWidget.GetComponent<FadingMessage>().Enter();

        // Fade in from black
        Fade._pInstance.StartFade(Fade.FadeStates.fadeOut, Color.black, 0.005f);
        
        // Play announcer sound
        if (SoundManager._pInstance._EnableAnnouncer == true) {

            SoundManager._pInstance._Announcer.PlayGetReady();
        }
    }

    public void MatchSetup() {

        // Set phase1 time
        _TimerPhase1 = _Phase1Length;

        // Set match time limit
        if (_Phase2Timer == true) {

            // Set maximum match time limit
            _TimerPhase2 = _Phase2Length;

            // Set match to allow having a time cap
            _MaxMatchTimer = true;
        }

        // Unlimited match time
        else {

            _MaxMatchTimer = false;
        }
    }

    public void MatchStart() {

        // Exit cinematic bars
        CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Exit, 4f);

        // Show hud
        HUD._pInstance.ShowHUD(true);

        // Start dynamic camera
        ///Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ///cam.GetComponent<DynamicCamera>().Init();

        // Initiate phase1
        _GameState = GameState.Phase1;

        // Fade in message widget
        HUD._pInstance._CollectCrystalsWidget.GetComponent<FadingMessage>().Enter();

        // Play announcer sound
        if (SoundManager._pInstance._EnableAnnouncer == true) {

            SoundManager._pInstance._Announcer.PlayPhaseOneStart();
        }

        // Play phase transition sound
        SoundManager._pInstance.PlayPhaseTransition();
    }

    public void MatchCompleted() {

        // Set gamestate
        _GameState = GameState.Gameover;

        // Force pause
        SetPause(true);
        
        // Show scoreboard
        if (HUD._pInstance._UIScoreboard != null) {

            HUD._pInstance._UIScoreboard.SetActive(true);
            ///HUD._pInstance._UIScoreboard.GetComponent<PostMatch_Scoreboard>().ResetScoreboard();
        }

        // Show cinematic bars
        CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Enter, 4f);

        // Play announcer sound
        if (SoundManager._pInstance._EnableAnnouncer == true) {

            SoundManager._pInstance._Announcer.PlayGameOver();
        }
    }

    public void OnPlayerEliminated(Player plyr) {

        // Precautions
        if (_PlayerEliminatedPanel != null && _PlayerEliminatedText != null) {

            // Set text
            _PlayerEliminatedText.text = string.Concat("Player " + plyr._pPlayerID + " Eliminated!");
            _PlayerEliminatedText.color = plyr._PlayerColour;

            _PlayerEliminatedPanel.ResetPopopTimer();
            
            // Wasnt the 2nd last player that was removed from the game
            if (PlayerManager._pInstance.GetActiveNecromancers().Count > 2) {

                // Start popup animation
                _PlayerEliminatedPanel.StartPopup();
            }
            
            // Play announcer sound
            if (SoundManager._pInstance._EnableAnnouncer == true) {

                SoundManager._pInstance._Announcer.PlayPlayerEliminated();
            }
        }
    }

    //--------------------------------------------------------------
    // *** GAMESTATES ***

    public void SetPause(bool pause) {

        // Attempting to UNPAUSE the game
        if (pause == false) {
            
            // Unpause the game
            _GamePaused = false;
            Time.timeScale = 1f;

            // Hide pause screen
            HUD._pInstance._UIPause.SetActive(false);

            // Exit cinematic bars
            ///CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Exit, 4f);
        }

        // Attemtping to PAUSE the game
        else { /// pause == true

            // Pause the game
            _GamePaused = true;
            Time.timeScale = 0.000001f;

            // Show pause screen
            HUD._pInstance._UIPause.SetActive(true);
            HUD._pInstance._UIPause.GetComponent<Widget_Pause>().SetButtonIndex(0);

            // Show cinematic bars
            ///CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Enter, 4f);
        }
    }

    public bool GetPaused() { return _GamePaused; }

    public bool GetGameplay() { return _Gameplay; }

    public GameState GetGameState() { return _GameState; }

    public float GetPhase1Timer() { return _TimerPhase1; }

    public float GetPhase2Timer() { return _TimerPhase2; }

    public bool GetMaxMatchTime() { return _MaxMatchTimer; }

}