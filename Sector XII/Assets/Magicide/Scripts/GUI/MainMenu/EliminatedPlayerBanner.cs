using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminatedPlayerBanner : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 22.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed) 
    public float _ShowTime = 3f;
    public float _ShowRate = 50f;

    /// Public (Internal)
    public enum States {

        Enter,
        Idle,
        Exit
    }

    /// Private
    private RectTransform _Rect;
    private float _Timer;
    private bool _Active = false;
    private States _CurrentState = States.Enter;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Get references
        _Rect = GetComponent<RectTransform>();
    }

    //--------------------------------------------------------------
    // *** MISC ***

    public void StartPopup() { _Active = true; }

    public bool GetIsActive() { return _Active; }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update () {

        // Only change states when the popup is active
        if (_Active == true) {

            switch (_CurrentState) {

                case States.Enter: { 

                    // Bring the banner into the screen bounds
                    if (_Rect.position.y < 40) {
                                               
                        
                        _Rect.transform.position = new Vector3(_Rect.transform.position.x, _Rect.transform.position.y + Time.deltaTime * _ShowRate, 0f);
                    }

                    // Go to new state
                    else { /// _Rect.position.y => 40

                        _CurrentState = States.Idle;
                    }
                    break;
                }

                case States.Idle: {
                    
                    // Stay on screen for a moment
                    if (_Timer < _ShowTime) {

                        _Timer += Time.deltaTime;
                    }

                    // Go to new state
                    else { /// _Timer => _ShowTime

                        _CurrentState = States.Exit;
                    }
                    break;
                }

                case States.Exit: {

                    // Bring the banner off the screen bounds
                    if (_Rect.position.y > -50) {

                        _Rect.transform.position = new Vector3(_Rect.transform.position.x, _Rect.transform.position.y - Time.deltaTime * _ShowRate, 0f);
                    }

                    // No longer active on screen
                    else { /// _Rect.position.y < -50

                        _Active = false;
                    }
                    break;
                }

                default: {

                        break;
                    }
            }
        }

        else { /// _Active == false

            // Keep the timer at zero
            _Timer = 0f;

            // Store the panel out of the screen bounds
            _Rect.position = new Vector3(_Rect.position.x, -50, 0);

            _CurrentState = States.Enter;
        }
    }
    
}