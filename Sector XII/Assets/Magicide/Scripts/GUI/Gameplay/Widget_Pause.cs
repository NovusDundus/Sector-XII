using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget_Pause : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public UnityEngine.UI.Button _ResumeButton;
    public UnityEngine.UI.Button _RestartButton;
    public UnityEngine.UI.Button _ExitMatchButton;

    /// Private
    private int _ButtonIndex = 0;
    private Player _PlayerController;
    private ButtonClicksArenaMode _ButtonClicks;
    private bool _ResetDownInput = false;
    private bool _ResetUpInput = false;
    private bool _ResetFaceRightInput = false;
    private bool _ResetFaceDownInput = false;
    private bool _ResetSpecialDownInput = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get references
        _PlayerController = ArenaMode._pInstance.GetComponent<Player>();
        _ButtonClicks = GetComponentInParent<ButtonClicksArenaMode>();
    }

    public void SetButtonIndex(int value) { _ButtonIndex = value; }

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
                    _ButtonIndex = 2;
                    _ResetUpInput = true;
                }

                // Play button hover sound
                SoundManager._pInstance.PlayButtonHover();
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

                if (_ButtonIndex < 2) {

                    // Highlight the button above the currently highlighted button
                    _ButtonIndex += 1;
                    _ResetDownInput = true;
                }

                else { /// _ButtonIndex >= 2

                    // Highlight the lowest button
                    _ButtonIndex = 0;
                    _ResetDownInput = true;
                }

                // Play button hover sound
                SoundManager._pInstance.PlayButtonHover();
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

                // Resume gameplay
                _ButtonClicks.OnClick_Resume();
                _ResetFaceRightInput = true;

                // Play button go back sound
                SoundManager._pInstance.PlayButtonGoBack();
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

                    // Resume gameplay
                    case 0: {
                            
                            _ButtonClicks.OnClick_Resume();
                            break;
                        }

                    // Restart match
                    case 1: {

                            _ButtonClicks.OnClick_Restart();
                            break;
                        }

                    // Exit match
                    case 2: {

                            _ButtonClicks.OnClick_MainMenu();
                            enabled = false;
                            break;
                        }

                    default: {

                            break;
                        }
                }

                // Play button click sound
                SoundManager._pInstance.PlayButtonClick();
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
        ///    Special button RIGHT
        /// 
        /// ***************************        
        if (_ResetSpecialDownInput == false) {

            // On button down
            if (_PlayerController.GetSpecialRightButton) {

                // Resume gameplay
                _ButtonClicks.OnClick_Resume();
                _ResetSpecialDownInput = true;

                // Play button go back sound
                SoundManager._pInstance.PlayButtonGoBack();
            }
        }

        else { /// _ResetSpecialDownInput == true

            // On button release
            if (_PlayerController.GetSpecialRightButton == false) {

                _ResetSpecialDownInput = false;
            }
        }

        /// ***************************
        /// 
        ///    Button highlights
        /// 
        /// ***************************      
        switch (_ButtonIndex) {

            // Resume gameplay
            case 0: {

                if (_ResumeButton != null) {

                    // Highlight
                    _ResumeButton.Select();
                }
                break;
            }

            // Restart match
            case 1: {

                if (_RestartButton != null) {
                
                    // Highlight
                    _RestartButton.Select();
                }
                break;
            }

            // Exit match
            case 2: {

                if (_ExitMatchButton != null) {

                    // Highlight
                    _ExitMatchButton.Select();
                }
                break;
            }

            default: {

                break;
            }
        }
    }

}