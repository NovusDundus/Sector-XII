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
    public Character CharacterAssociated;
    public RawImage DeadCross;

    /// Private
    private Image HealthBar;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get reference to the health bar components of the ui panel
        HealthBar = GetComponentInChildren<Image>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update() {
        
        if (CharacterAssociated != null && HealthBar != null) {

            // Set the health bar fill to match the player's current health / starting health (0.0 - 1.0)
            float percent = ((float)CharacterAssociated.GetHealth() / (float)CharacterAssociated.GetStartingHealth());
            HealthBar.fillAmount = percent;

            // Valid dead image?
            if (DeadCross != null) {

                // If the player's character is dead
                if (CharacterAssociated.GetHealth() <= 0) {

                    DeadCross.enabled = true;
                }

                // The player's character is still alive
                else { /// CharacterAssociated.GetHealth() > 0

                    DeadCross.enabled = false;
                }
            }
        }
    }

}