using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingMessage : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 27.1.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    public Image _Outline;
    public Image _Text;
    public float _FadeInRate = 1f;
    public float _FadeOutRate = 1f;
    public float _ShowTime = 2f;

    /// Private
    private bool _FadingIn = false;
    private bool _Active = false;
    private float _Alpha = 0f;
    private float _ShowTimer = 0f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***
    
    void Start () {
		
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
        if (_Active == true) {

            // Fading widget into the screen
            if (_FadingIn == true) {

                // Lerp alpha till its fully visible
                _Alpha += _FadeInRate * Time.deltaTime;
                _Outline.color = new Color(_Outline.color.r, _Outline.color.g, _Outline.color.b, _Alpha);
                _Text.color = new Color(_Outline.color.r, _Outline.color.g, _Outline.color.b, _Alpha);

                // Lerp complete
                if (_Alpha >= 1f) {

                    _Alpha = 1f;

                    // Wait until the show timer is complete
                    _ShowTimer += Time.deltaTime;

                    // Timer complete
                    if (_ShowTimer >= _ShowTime) {

                        Exit();
                    }
                }
            }

            // Fading widget out of the screen
            else { /// _FadingIn == false

                // Lerp alpha till its not visible anymore
                _Alpha -= _FadeOutRate * Time.deltaTime;
                _Outline.color = new Color(_Outline.color.r, _Outline.color.g, _Outline.color.b, _Alpha);
                _Text.color = new Color(_Outline.color.r, _Outline.color.g, _Outline.color.b, _Alpha);

                // Lerp complete
                if (_Alpha <= 0f) {

                    _Alpha = 0f;
                    _Active = false;
                }
            }
        }
	}

    //--------------------------------------------------------------
    // *** FADE ***

    public void Enter() {

        _ShowTimer = 0f;
        _Alpha = 0f;
        _FadingIn = true;
        _Active = true;
    }

    public void Exit() {

        _Alpha = 1f;
        _FadingIn = false;
        _Active = true;
    }

}