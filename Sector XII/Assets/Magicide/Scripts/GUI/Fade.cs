using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 8.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public GameObject UI_Panel;

    /// Public (internal)
    [HideInInspector]
    public static Fade _pInstance;                                  // This is a singleton script, Initialized in Startup().
    public enum FadeStates {

        idle,
        fadeIn,
        fadeOut
    }

    /// Private
    private Image _Image;
    private float _FadeRate = 0f;
    private FadeStates _FadeState = FadeStates.idle;
    private bool _Fading = false;

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

    public void Start () {
        
        // Get reference to panel's image component
        _Image = GetComponent<Image>();
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update () {
		
	}

    public void FixedUpdate() {

        // Precaution
        if (UI_Panel != null) {

            switch (_FadeState) {

                case FadeStates.idle: {

                        // Hide panel
                        UI_Panel.SetActive(false);
                        _Fading = false;
                        break;
                    }
                case FadeStates.fadeIn: {

                        // Show panel
                        UI_Panel.SetActive(true);

                        // Fade screen into COLOUR
                        if (_Image.color.a < 1f) {

                            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, _Image.color.a + _FadeRate);
                            _Fading = true;
                        }

                        // Fade complete
                        else {

                            _FadeState = FadeStates.idle;
                        }
                        break;
                    }
                case FadeStates.fadeOut: {

                        // Show panel
                        UI_Panel.SetActive(true);

                        // Fade screen from COLOUR
                        if (_Image.color.a > 0f) {

                            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, _Image.color.a - _FadeRate);
                            _Fading = true;
                        }

                        // Fade complete
                        else {

                            _FadeState = FadeStates.idle;
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
    // *** FADE ***

    public void StartFade(FadeStates state, Color colour, float rate) {

        // Set fade colour
        _Image.color = colour;

        // Set fading rate
        _FadeRate = rate;

        // Begin fade
        _FadeState = state;
    }

    public bool IsFadeComplete() {

        return !_Fading;
    }

}