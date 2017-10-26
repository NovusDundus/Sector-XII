using System.Collections;
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
    }

    //--------------------------------------------------------------
    // *** FRAME ***
    
    public void FixedUpdate() {

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
                        
                        foreach (var player in PlayerManager._pInstance.GetAliveNecromancers()) {

                            // Add 1 second per second to the alive time
                            player._Player.AddTimeAlive(Time.deltaTime);
                        }

                        // Once phase1 timer is complete
                        if (_TimerPhase1 <= 0f) {

                            // Initiate phase2
                            _GameState = GameState.Phase2;
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


                            foreach (var player in PlayerManager._pInstance.GetAliveNecromancers()) {

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
                    }
                    break;
                }

            // ****** MATCH COMPLETE ******
            case GameState.Gameover: {

                    _CinematicPlaying = true;

                    // Disable pausing (and force unpause)
                    SetPause(false);
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
        if (PlayerManager._pInstance.GetAliveNecromancers().Count <= 1) {

            // Match completed
            MatchCompleted();
        }
    }

    //--------------------------------------------------------------
    // *** MATCH ***

    public void Introduction() {

        // Set gameState
        _GameState = GameState.Intro;

        // Hide hud
        HUD._pInstance.ShowHUD(false);

        // Fade in from black
        Fade._pInstance.StartFade(Fade.FadeStates.fadeOut, Color.black, 0.005f);
    }

    public void MatchSetup() {

        // Determine how many controllers are connected
        ///int size = Input.GetJoystickNames().Length / 2;

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
    }

    public void MatchCompleted() {

        // Set gamestate
        _GameState = GameState.Gameover;

        // Show scoreboard
        if (HUD._pInstance._UIScoreboard != null) {

            HUD._pInstance._UIScoreboard.SetActive(true);
            HUD._pInstance._UIScoreboard.GetComponent<PostMatch_Scoreboard>().ResetScoreboard();
        }

        // Show cinematic bars
        CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Enter, 4f);
    }

    //--------------------------------------------------------------
    // *** GAMESTATES ***

    public void SetPause(bool pause) {

        // Attempting to UNPAUSE the game
        if (pause == false) {
            
            // Unpause the game
            _GamePaused = false;

            // Exit cinematic bars
            ///CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Exit, 4f);
        }

        // Attemtping to PAUSE the game
        else { /// pause == true

            // Pause the game
            _GamePaused = true;

            // Show cinematic bars
            ///CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Enter, 4f);
        }
    }

    public bool GetPaused() {

        // Returns TRUE if the game is paused
        return _GamePaused;
    }

    public bool GetGameplay() {

        // Gameplay is true if the game isnt paused AND NOT playing a cinematic
        return _Gameplay;
    }

    public GameState GetGameState() {

        return _GameState;
    }

    public float GetPhase1Timer() {

        return _TimerPhase1;
    }

    public float GetPhase2Timer() {

        return _TimerPhase2;
    }

    public bool GetMaxMatchTime() {

        return _MaxMatchTimer;
    }

}