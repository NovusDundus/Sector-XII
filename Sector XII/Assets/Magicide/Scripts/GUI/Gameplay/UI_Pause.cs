using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES **

    /// Public (designers)
    public Character _PlayerAttachedTo;

    /// Private
    private bool PanelShowing = true;
    private float _TimerActive = 0f;
    private float _TimerInactive = 0f;

    //--------------------------------------------------------------
    // *** FRAME ***

    void FixedUpdate() {

        if (PanelShowing == true) {

            _TimerActive += Time.deltaTime;
            _TimerInactive = 0f;
        }
        else {

            _TimerInactive += Time.deltaTime;
            _TimerActive = 0f;
        }

        if (_PlayerAttachedTo != null) {

            // If the gamepad associated with the attached pressed the special left button
            if (_PlayerAttachedTo._Player.GetSpecialRightButton == true) {

                // And the panel was currently showing and had been on screen for enough time
                if (PanelShowing == true && _TimerActive > 0.5f) {

                    // Hide the panel
                    HUD._pInstance._UIPause.SetActive(false);
                    PanelShowing = false;

                    // Resume gameplay
                }

                // And the panel wasnt on screen and had been inactive for enough time
                else if (_TimerInactive > 0.5f) {

                    // Show the panel
                    HUD._pInstance._UIPause.SetActive(true);
                    PanelShowing = true;
                }
            }
        }
    }
}
