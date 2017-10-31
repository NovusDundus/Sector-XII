using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget_Scoreboard : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public UnityEngine.UI.Button _RestartButton;
    public UnityEngine.UI.Button _QuitButton;

    /// Private
    private int _ButtonIndex = 0;
    private Player _PlayerController;
    private ButtonClicksArenaMode _ButtonClicks;
    private bool _ResetDownInput = false;
    private bool _ResetUpInput = false;
    private bool _ResetFaceDownInput = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get references
        _PlayerController = ArenaMode._pInstance.GetComponent<Player>();
        _ButtonClicks = GetComponentInParent<ButtonClicksArenaMode>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update() {

        /// ***************************
        /// 
        ///    Left thumbstick UP
        /// 
        /// ***************************
        if (_ResetUpInput == false) {

            // On axis change
            if (_PlayerController.GetLeftAxisUpInput) {

                if (_ButtonIndex > 0) {

                    // Highlight the button above the currently highlighted button
                    _ButtonIndex -= 1;
                    _ResetUpInput = true;
                }

                else { /// _ButtonIndex <= 0

                    // Highlight the lowest button
                    _ButtonIndex = 1;
                    _ResetUpInput = true;
                }
            }
        }

        else { /// _ResetUpInput == true

            // On axis release
            if (_PlayerController.GetLeftAxisUpInput == false) {

                // Reset button input check
                _ResetUpInput = false;
            }
        }

        /// ***************************
        /// 
        ///    Left thumbstick DOWN
        /// 
        /// ***************************
        if (_ResetDownInput == false) {

            // On axis change
            if (_PlayerController.GetLeftAxisDownInput) {

                if (_ButtonIndex < 1) {

                    // Highlight the button above the currently highlighted button
                    _ButtonIndex += 1;
                    _ResetDownInput = true;
                }

                else { /// _ButtonIndex >= 3

                    // Highlight the lowest button
                    _ButtonIndex = 0;
                    _ResetDownInput = true;
                }
            }
        }

        else { /// _ResetDownInput == true

            // On axis release
            if (_PlayerController.GetLeftAxisDownInput == false) {

                // Reset button input check
                _ResetDownInput = false;
            }
        }

        /// ***************************
        /// 
        ///    Face button DOWN
        /// 
        /// ***************************        
        if (_ResetFaceDownInput == false) {

            // On button down
            if (_PlayerController.GetFaceBottomInput) {

                _ResetFaceDownInput = true;

                // Proceed to next widget based on current index
                switch (_ButtonIndex) {

                    // Restart gameplay
                    case 0: {

                            _ButtonClicks.OnClick_Restart();
                            break;
                        }

                    // Quit match
                    case 1: {

                            _ButtonClicks.OnClick_MainMenu();
                            break;
                        }

                    default: {

                            break;
                        }
                }
            }
        }

        else { /// _ResetFaceDownInput == true

            // On button release
            if (_PlayerController.GetFaceBottomInput == false) {

                _ResetFaceDownInput = false;
            }
        }

        /// ***************************
        /// 
        ///    Button highlights
        /// 
        /// ***************************      
        switch (_ButtonIndex) {

            // Restart gameplay
            case 0: {

                    if (_RestartButton != null) {

                        // Highlight
                        _RestartButton.Select();
                    }
                    break;
                }

            // Exit match
            case 1: {

                    if (_QuitButton != null) {

                        // Highlight
                        _QuitButton.Select();
                    }
                    break;
                }

            default: {

                    break;
                }
        }
    }

}