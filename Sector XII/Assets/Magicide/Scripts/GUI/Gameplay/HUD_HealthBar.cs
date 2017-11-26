using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_HealthBar : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 24.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public Character _CharacterAssociated;
    public RawImage _DeadCross;

    /// Private
    private Image _HealthBar;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get reference to the health bar components of the ui panel
        _HealthBar = GetComponentInChildren<Image>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update() {
        
        if (_CharacterAssociated != null && _HealthBar != null) {

            // Set the health bar fill to match the player's current health / starting health (0.0 - 1.0)
            float percent = ((float)_CharacterAssociated.GetHealth() / (float)_CharacterAssociated.GetStartingHealth());
            _HealthBar.fillAmount = /*1f -*/ percent;

            // Valid dead image?
            if (_DeadCross != null) {

                // If the player's character is dead & out of lives
                if (_CharacterAssociated.GetHealth() <= 0 && _CharacterAssociated._Player.GetRespawnsLeft() < 0) {

                    _DeadCross.enabled = true;
                }

                // The player's character is still alive
                else { /// CharacterAssociated.GetHealth() > 0

                    _DeadCross.enabled = false;
                }
            }
        }
    }

}