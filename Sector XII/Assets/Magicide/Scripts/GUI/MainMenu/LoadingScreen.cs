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

    public int _MinimumLoadingTime = 10;                            // Minimum loading time for the level.
    public Slider _LoadSlider;                                      // Reference to the s_Loadbar widget.

    private int _LevelIndex;                                        // Build level index for the level to load.
    private eState _currentState = eState.Intro;                    // Current state of the UI.
    private float _TimerLoadingScreen = 0f;                         // Timer for the scene load.
    private Text _LoadingText;                                      // Reference to the "t_Loading" text in the panel.
    private float _LoadingTextAlpha = 0f;                           // Current alpha colour for "t_Loading" text.

    private enum eState {

        Intro,
        Loading,
        Exit
    }

    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public void Start () {

        // Get reference to 't_Loading'
        _LoadingText = GetComponentInChildren<Text>();

        // Store load slider max value
        _LoadSlider.maxValue = _MinimumLoadingTime;
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void Update () {

        switch (_currentState) {

            case eState.Intro: {

                    // Fade in "t_Loading" text from transparent.
                    if (_LoadingText.color.a < 1f) {
                        _LoadingTextAlpha += Time.deltaTime * 0.5f;
                        _LoadingText.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
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

                    // Main load slider
                    if (_LoadSlider.value < (_LoadSlider.maxValue / 1.1f)) {

                        // Add to load slider's current value
                        _LoadSlider.value += Time.deltaTime * 1.5f;
                    }

                    // Minimum timer counter for the scene loading.
                    if (_TimerLoadingScreen < _MinimumLoadingTime) {

                        _TimerLoadingScreen += Time.deltaTime;
                    }

                    // Once minimum timer has reached threshold
                    else {

                        // If the background loading has finished
                        if (Loading._pInstance.Async.progress >= 0.9f) {

                            // Add to load slider the last bit to complete the loading
                            _LoadSlider.value += Time.deltaTime * 2f;
                        }

                        // Once the slider has complete
                        if (_LoadSlider.value == _LoadSlider.maxValue) {

                            _currentState = eState.Exit;
                        }
                    }

                    break;
            }

            case eState.Exit: {

                    // Fade out loading text to full transparency.
                    if (_LoadingText.color.a > 0f) {

                        _LoadingTextAlpha -= Time.deltaTime / 2f;
                        _LoadingText.color = Color.Lerp(Color.clear, Color.white, _LoadingTextAlpha);
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
    
    //--------------------------------------------------------------
    // *** LOADING *** 

    public void SetLevelIndex(int index) {

        // Set the level index for the loading
        _LevelIndex = index;
    }

}