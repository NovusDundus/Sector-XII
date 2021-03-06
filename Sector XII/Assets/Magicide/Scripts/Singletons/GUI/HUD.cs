﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 7.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (Exposed)
    [Header("---------------------------------------------------------------------------")]
    [Header("*** MATCH USER INTERFACE ***")]
    [Tooltip("Reference to the match timer text that is displayed in the HUD.")]
    public Text _MatchTimerText;                                    // Reference to the match timer text that is displayed in the HUD.
    public Color _TimeLowColour = Color.red;
    public Color _TimeMediumColour = Color.yellow;
    public Color _TimeOkayColour = Color.white;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** PANEL SCREENS ***")]
    [Tooltip("Reference to the gui pause screen panel.")]
    public GameObject _UIPause;                                     // Reference to the gui pause screen panel.
    [Tooltip("Reference to the gui Scoreboard panel.")]
    public GameObject _UIScoreboard;                                // Reference to the gui Scoreboard panel.
    [Tooltip("Reference to the gui HUD panel.")]
    public GameObject _UIHud;                                       // Reference to the gui HUD panel.

    [Header("---------------------------------------------------------------------------")]
    [Header("*** MESSAGE WIDGETS ***")]
    public GameObject _GetReadyWidget;
    public GameObject _CollectCrystalsWidget;
    public GameObject _EliminatePlayersWidget;

    /// Public (Internal)
    [HideInInspector]
    public static HUD _pInstance;                                   // This is a singleton script, Initialized in Startup().
    [HideInInspector]
    public bool _DisplayHUD = false;                                // Returns TRUE if the HUD is being displayed on screen.

    /// Private
    private int _Phase1Medium;
    private int _Phase1Low;
    private int _Phase2Medium;
    private int _Phase2Low;
    
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

        _Phase1Medium = MatchManager._pInstance._Phase1Length / 2;
        _Phase1Low = 5;
        _Phase2Medium = MatchManager._pInstance._Phase2Length / 2;
        _Phase2Low = 10;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        // Only display HUD if its allowed to
        if (_DisplayHUD == true) {

            // Phase1 in session
            if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase1) {

                // Update HUD
                _MatchTimerText.text = MatchManager._pInstance.GetPhase1Timer().ToString("00");
                
                // Set text colour low
                if (MatchManager._pInstance.GetPhase1Timer() <= _Phase1Low) {

                    _MatchTimerText.color = HUD._pInstance._TimeLowColour;
                }

                else {

                    // Set text colour medium
                    if (MatchManager._pInstance.GetPhase1Timer() <= _Phase1Medium) {

                        _MatchTimerText.color = HUD._pInstance._TimeMediumColour;
                    }

                    // Set text colour okay
                    else {

                        _MatchTimerText.color = HUD._pInstance._TimeOkayColour;
                    }
                }
            }

            // Phase2 in session
            if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

                // Phase2 has a time limit
                if (MatchManager._pInstance.GetMaxMatchTime() == true) {

                    // Update HUD
                    _MatchTimerText.text = MatchManager._pInstance.GetPhase2Timer().ToString("00");
                }

                // Phase2 has NO time limit
                else {

                    // Hide match timer text
                    _MatchTimerText.gameObject.SetActive(false);
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** HUD ***

    public void ShowHUD(bool value) { _DisplayHUD = value; }

}