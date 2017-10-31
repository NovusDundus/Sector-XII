using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Flamethrower : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public Wep_Flamethrower _FlamethrowerAssociated;

    /// Private
    private Image _FlamethrowerBar;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        // Get reference to the flamethrower meter components of the ui panel
        _FlamethrowerBar = GetComponentInChildren<Image>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update() {

        if (_FlamethrowerAssociated != null && _FlamethrowerBar != null) {

            // Set the flamethrower bar fill to match the
            float percent = ((float)_FlamethrowerAssociated.GetCurrentHeat() / 1f);
            _FlamethrowerBar.fillAmount = 1f - percent;

        }
    }
}
