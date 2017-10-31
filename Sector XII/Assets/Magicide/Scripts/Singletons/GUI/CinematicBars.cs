using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBars : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public GameObject _BottomBar;                                   // Reference to the cinematic bar on the bottom of the screen.
    public GameObject _TopBar;                                      // Reference to the cinematic bar on the top of the screen.

    /// Public (internal)
    [HideInInspector]
    public static CinematicBars _pInstance;                         // This is a singleton script, Initialized in Awake().
    public enum BarDirection {

        Enter,
        Exit
    }

    /// Private
    private bool _AnimationComplete = true;
    private float _AnimationSpeed;
    private BarDirection _BarDirection;

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
    
    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {

        if (_AnimationComplete == false) {

            switch (_BarDirection) {

                case BarDirection.Enter: {

                        // Move cinematic bars inwards
                        if (_BottomBar != null && _TopBar != null) {

                            if (_BottomBar.transform.position.y < 0) {

                                // Move topbar down into the screen
                                _TopBar.transform.position = new Vector3(_TopBar.transform.position.x, _TopBar.transform.position.y - _AnimationSpeed, _TopBar.transform.position.z);

                                // Move bottom upwards into the screen
                                _BottomBar.transform.position = new Vector3(_BottomBar.transform.position.x, _BottomBar.transform.position.y + _AnimationSpeed, _BottomBar.transform.position.z);
                            }
                        }
                        break;
                    }

                case BarDirection.Exit: {

                        // Move cinematic bars outwards
                        if (_BottomBar != null && _TopBar != null) {

                            if (_BottomBar.transform.position.y > -100) {

                                // Move topbar upwards off the screen
                                _TopBar.transform.position = new Vector3(_TopBar.transform.position.x, _TopBar.transform.position.y + _AnimationSpeed, _TopBar.transform.position.z);

                                // Move bottom down off the screen
                                _BottomBar.transform.position = new Vector3(_BottomBar.transform.position.x, _BottomBar.transform.position.y - _AnimationSpeed, _BottomBar.transform.position.z);
                            }
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
    // *** CINEMATIC BARS ***

    public void StartAnimation(BarDirection direction, float rate) {

        // Set fading rate & direction
        _AnimationSpeed = rate;
        _BarDirection = direction;

        // Begin animation
        _AnimationComplete = false;
    }

    public bool IsPlayingAnimation() {

        return _AnimationComplete;
    } 

}