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
    public static MatchManager _pInstance;                             // This is a singleton script, Initialized in Startup().

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // INTRO / OUTRO

    public void Introduction() {

    }

    public void MatchStart() {

    }

    public void MatchCompleted() {

    }
}
