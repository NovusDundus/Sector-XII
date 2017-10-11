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
    [Header("- Phases")]
    [Tooltip("Time in seconds to complete the first phase of the match.(Meat grab)")]
    [Range(1, 120)]
    public int _Phase1Length = 30;
    [Tooltip("Time in seconds to complete the second phase of the match.(Kill players)")]
    public bool _Phase2Timer = false;
    [Range(1, 600)]
    public int _Phase2Length = 120;

    /// Public (internal)
    [HideInInspector]
    public static MatchManager _pInstance;                          // This is a singleton script, Initialized in Startup().

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
    private float _MinimumPauseTime = 10f;                          // Minimum amount of time required for the game to be in a paused state before returning to unpaused.
    private float _TimePaused = 0f;
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

    public void Update() {

    }

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

                        // Reset pause time counter
                        _TimePaused = 0f;

                        // Deduct from timer 1 second per second
                        _TimerPhase1 -= Time.deltaTime;

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
                        
                        // Reset pause time counter
                        _TimePaused = 0f;

                        // If phase2 runs on a timer
                        if (_MaxMatchTimer == true) {

                            // Deduct from timer 1 second per second
                            _TimerPhase2 -= Time.deltaTime;
   
                            // Once phase2 timer is complete
                            if (_TimerPhase2 <= 0f) {

                                // Stop phase2 (GAME OVER)
                                _GameState = GameState.Gameover;
                            }
                        }
                    }
                    break;
                }

            // ****** MATCH COMPLETE ******
            case GameState.Gameover: {

                    _CinematicPlaying = true;

                    // Disable pausing (and force unpause)
                    SetPause(false);
                    break;
                }

            default: {

                    break;
                }
        }

        // If the game is PAUSED
        if (_GamePaused == true) {

            // Add to pause time counter
            _TimePaused += Time.deltaTime;

            ///Debug.Log("GAME PAUSED");
        }

        // If the game is UNPAUSED
        else { /// _GamePaused == false

            ///Debug.Log("GAME UNPAUSED");
        }

        // Gameplay is true if the game isnt paused AND NOT playing a cinematic
        _Gameplay = !_GamePaused && !_CinematicPlaying;
    }

    //--------------------------------------------------------------
    // *** MATCH ***

    public void Introduction() {

        // Set gameState
        _GameState = GameState.Intro;

        // Hide hud
        HUD._pInstance.ShowHUD(false);

        // Fade in from black
        Fade._pInstance.StartFade(Fade.FadeStates.fadeOut, Color.black, 0.01f);
    }

    public void MatchSetup() {

        /* Reset all match settings */

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

        // Show hud
        HUD._pInstance.ShowHUD(true);

        // Start camera
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ///cam.GetComponent<DynamicCamera>().Init();

        // Initiate phase1
        _GameState = GameState.Phase1;
    }

    public void MatchCompleted() {

    }

    //--------------------------------------------------------------
    // *** GAMESTATES ***

    public void SetPause(bool pause) {

        // Attempting to UNPAUSE the game
        if (pause == false) {

            // If time paused has reached minimum time required
            if (_TimePaused >= _MinimumPauseTime) {

                // Unpause the game
                _GamePaused = false;
            }
        }

        // Attemtping to PAUSE the game
        else { /// pause == true

            // Pause the game
            _GamePaused = true;
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