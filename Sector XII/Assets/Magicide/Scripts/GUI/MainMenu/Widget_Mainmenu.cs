using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Widget_Mainmenu : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public UnityEngine.UI.Button _PlayButton;
    public UnityEngine.UI.Button _CreditsButton;
    public UnityEngine.UI.Button _ExitGameButton;
    
    /// Private
    private int _ButtonIndex = 0;
    private Player _PlayerController;
    private ButtonClicksMainMenu _ButtonClicks;
    private bool _ResetDownInput = false;
    private bool _ResetUpInput = false;
    private bool _ResetFaceRightInput = false;
    private bool _ResetFaceDownInput = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

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

                else { /// _ButtonIndex >= 3

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

                // Show EXIT GAME widget
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

                    // Play game
                    case 0: {

                        _ButtonClicks.OnClick_Play();
                        _ButtonClicks.ui_LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(1);
                        break;
                    }

                    // View credits
                    case 1: {

                        _ButtonClicks.OnClick_Credits();
                        break;
                    }
                    
                    // Exit game popup screen
                    case 2: {

                        _ButtonClicks.OnClick_ExitGame();
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
        ///    Button highlights
        /// 
        /// ***************************      
        switch (_ButtonIndex) {

            // Play game
            case 0: {

                if (_PlayButton != null) {

                        // Highlight
                        _PlayButton.Select();
                    }
                break;
            }

            // View credits
            case 1: {

                if (_CreditsButton != null) {

                    // Highlight
                    _CreditsButton.Select();
                }
                break;
            }

            // Exit game popup screen
            case 2: {

                if (_ExitGameButton != null) {

                    // Highlight
                    _ExitGameButton.Select();
                }    
                break;
            }

            default: {

                    break;
            }
        }
    }
    
}