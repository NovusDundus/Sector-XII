using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget_Credits : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Private
    private Player _PlayerController;
    private ButtonClicksMainMenu _ButtonClicks;
    private bool _ResetFaceRightInput = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get references
        _PlayerController = MainMenu._pInstance.GetComponent<Player>();
        _ButtonClicks = GetComponentInParent<ButtonClicksMainMenu>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {

        /// ***************************
        /// 
        ///    Face button RIGHT
        /// 
        /// ***************************        
        if (_ResetFaceRightInput == false) {

            // On button down
            if (_PlayerController.GetFaceRightInput) {

                _ResetFaceRightInput = true;

                // Return to main menu
                if (_ButtonClicks != null) {

                    _ButtonClicks.OnClick_bGoBack_Credits();
                }
            }
        }

        else { /// _ResetFaceRightInput == true

            // On button release
            if (_PlayerController.GetFaceRightInput == false) {

                _ResetFaceRightInput = false;
            }
        }
    }

}