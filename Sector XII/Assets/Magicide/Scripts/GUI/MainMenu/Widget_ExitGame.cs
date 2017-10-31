using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget_ExitGame : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public UnityEngine.UI.Button _ConfirmButton;
    public UnityEngine.UI.Button _CancelButton;

    /// Private
    private int _ButtonIndex = 0;
    private Player _PlayerController;
    private ButtonClicksMainMenu _ButtonClicks;
    private bool _ResetUpInput = false;
    private bool _ResetDownInput = false;
    private bool _ResetFaceRightInput = false;
    private bool _ResetFaceDownInput = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get references
        _PlayerController = MainMenu._pInstance.GetComponent<Player>();
        _ButtonClicks = GetComponentInParent<ButtonClicksMainMenu>();
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
        ///    Face button RIGHT
        /// 
        /// ***************************        
        if (_ResetFaceRightInput == false) {

            // On button down
            if (_PlayerController.GetFaceRightInput) {

                // Show MAIN MENU widget
                _ButtonClicks.OnClick_bCancelExitGame();
                _ResetFaceRightInput = true;
            }
        }

        else { /// _ResetFaceRightInput == true

            // On button release
            if (_PlayerController.GetFaceRightInput == false) {

                _ResetFaceRightInput = false;
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

                    // Confirm Quit
                    case 0: {

                            _ButtonClicks.OnClick_bConfirmExitGame();
                            break;
                        }

                    // Cancel quit
                    case 1: {

                            _ButtonClicks.OnClick_bCancelExitGame();
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

            // Confirm quit
            case 0: {

                    if (_ConfirmButton != null) {

                        // Highlight
                        _ConfirmButton.Select();
                    }
                    break;
                }

            // Cancel quit
            case 1: {

                    if (_CancelButton != null) {

                        // Highlight
                        _CancelButton.Select();
                    }
                    break;
                }

            default: {

                    break;
                }
        }
    }
}
