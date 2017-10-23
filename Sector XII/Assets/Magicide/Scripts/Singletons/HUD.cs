using System.Collections;
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

    /// Public (designers)
    [Header("*** MATCH USER INTERFACE ***")]
    [Tooltip("Reference to the match timer text that is displayed in the HUD.")]
    public Text _MatchTimerText;                                    // Reference to the match timer text that is displayed in the HUD.
    public Color _TimeLowColour = Color.red;
    public Color _TimeMediumColour = Color.yellow;
    public Color _TimeOkayColour = Color.white;
    [Header("*** PANEL SCREENS ***")]
    [Tooltip("Reference to the gui pause screen panel.")]
    public GameObject _UIPause;                                     // Reference to the gui pause screen panel.
    [Tooltip("Reference to the gui Scoreboard panel.")]
    public GameObject _UIScoreboard;                                // Reference to the gui Scoreboard panel.
    [Header(" *** PLAYERS ***")]
    [Tooltip("Reference to player 1's character.")]
    public Char_Geomancer PlayerOne;                                // Reference to player 1's character.
    [Tooltip("Player 1's HUD colour pallet.")]
    public Color _PlayerOneColour = Color.blue;                     // Player 1's HUD colour pallet.
    [Tooltip("Reference to player 2's character.")]
    public Char_Geomancer PlayerTwoCharacter;                       // Reference to player 2's character.
    [Tooltip("Player 2's HUD colour pallet.")]
    public Color _PlayerTwoColour = Color.green;                    // Player 2's HUD colour pallet.
    [Tooltip("Reference to player 3's character.")]
    public Char_Geomancer PlayerThree;                              // Reference to player 3's character.
    [Tooltip("Player 3's HUD colour pallet.")]
    public Color _PlayerThreeColour = Color.magenta;                // Player 3's HUD colour pallet.
    [Tooltip("Reference to player 4's character.")]
    public Char_Geomancer PlayerFour;                               // Reference to player 4's character.
    [Tooltip("Player 4's HUD colour pallet.")]
    public Color _PlayerFourColour = Color.yellow;                  // Player 4's HUD colour pallet.

    /// Public (internal)
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
        _Phase1Low = 3;
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

    public void ShowHUD(bool display) {

        // Show or hide the hud
        _DisplayHUD = display;
    }

}