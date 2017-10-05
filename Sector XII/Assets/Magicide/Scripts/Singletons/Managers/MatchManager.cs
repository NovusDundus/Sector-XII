using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (designers)
    [Header(" *** TIMERS ***")]
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

    /// Private 
    private bool _GamePaused = false;                               // Returns TRUE if the game is currently paused.
    private float _MinimumPauseTime = 100f;                         // Minimum amount of time required for the game to be in a paused state before returning to unpaused.
    private float _TimePaused = 0f;

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // If the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

        // If the game is PAUSED
        if (_GamePaused == true) {

            // Add to pause time counter
            _TimePaused += Time.deltaTime;
            
            Debug.Log("GAME PAUSED");
        }

        // If the game is UNPAUSED
        else { /// _GamePaused == false

            // Reset pause time counter
            _TimePaused = 0f;

            Debug.Log("UNPAUSED");
        }   
    }

    //--------------------------------------------------------------
    // INTRO / OUTRO

    public void Introduction() {

    }

    public void MatchStart() {

    }

    public void MatchCompleted() {

    }

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
}
