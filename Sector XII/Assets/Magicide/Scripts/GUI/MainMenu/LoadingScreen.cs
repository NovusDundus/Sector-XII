using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 
    
    public Slider _LoadSlider;                                      // Reference to the "s_ProgressBar" widget.
    public Text _MessageText;                                       // Reference to the "t_Message" text in the panel..
    public RawImage _GamepadContinueIcon;                           // Reference to the "i_GamepadContinue" image widget.

    private int _LevelIndex;                                        // Build level index for the level to load.
    private eState _currentState = eState.Intro;                    // Current state of the UI.
    private float _LoadingTextAlpha = 0f;                           // Current alpha colour for "t_Loading" text.

    private enum eState {

        Intro,
        Loading,
        Exit
    }

    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public void Start() {

        if (_MessageText != null) {

            // Set the text's colour to full transparency
            _MessageText.color = Color.clear;
        }
        if (_GamepadContinueIcon != null) {

            // Hide the gamepad icon
            _GamepadContinueIcon.color = Color.clear;
        }
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void Update () {

        if (_LoadSlider != null && _MessageText != null && _GamepadContinueIcon != null) {

            switch (_currentState) {

                case eState.Intro: {

                        // Fade in "t_Loading" text from transparent.
                        if (_MessageText.color.a < 1f) {

                            _LoadingTextAlpha += Time.deltaTime;

                            _MessageText.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
                            ///_LoadSlider.image.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
                        }

                        // Text has completed its fade in
                        else {

                            // Start background loading of the new scene
                            Loading._pInstance.LoadLevel(_LevelIndex);
                            _currentState = eState.Loading;
                        }
                        break;
                    }

                case eState.Loading: {

                        // Once scene loading is complete (last 0.1 represents the level activation)
                        if (Loading._pInstance.Async.progress >= 0.9f) {

                            // Prompt the user to continue to the next level
                            _LoadSlider.value = 1f;
                            _MessageText.text = "PRESS      TO CONTINUE";
                            _GamepadContinueIcon.color = Color.white;

                            // Main menu to arena
                            if (MainMenu._pInstance != null) {

                                // Once any player presses the A button
                                if (MainMenu._pInstance.GetComponent<Player>().GetFaceBottomInput) {

                                    // Proceed to scene activation
                                    _currentState = eState.Exit;
                                }
                            }

                            // Arena to main menu
                            else if (ArenaMode._pInstance != null) {

                                // Once any player presses the A button
                                if (ArenaMode._pInstance.GetComponent<Player>().GetFaceBottomInput) {

                                    // Proceed to scene activation
                                    _currentState = eState.Exit;
                                }
                            }
                        }

                        else { // Loading._pInstance.Async.progress < 0.9f

                            // Track the level loading progress
                            _LoadSlider.value = Loading._pInstance.GetSceneLoadProgress();
                            _GamepadContinueIcon.color = Color.clear;
                        }

                        break;
                    }

                case eState.Exit: {

                        // Fade out widgets to full transparency
                        if (_MessageText.color.a > 0f) {

                            _LoadingTextAlpha -= Time.deltaTime;

                            _MessageText.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
                            _GamepadContinueIcon.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
                            ///_LoadSlider.image.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
                        }

                        // Loading screen has completed its cycle
                        else {

                            // Bring new scene to the front
                            Loading._pInstance.ActivateLevel();
                        }

                        break;
                    }

                default: {

                        break;
                    }
            }
        }
    }
    
    //--------------------------------------------------------------
    // *** LOADING *** 

    public void SetLevelIndex(int index) {

        // Set the level index for the loading
        _LevelIndex = index;
    }

}