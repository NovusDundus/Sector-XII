using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Dash : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 31.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public Char_Geomancer _CharacterAssociated;

    /// Private
    private Image _DashBar;
    private float _DashMaxCooldown;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get reference to the flamethrower meter components of the ui panel
        _DashBar = GetComponentInChildren<Image>();
        _DashMaxCooldown = PlayerManager._pInstance.DashCooldown;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update() {

        if (_CharacterAssociated != null && _DashBar != null) {

            // Set the flamethrower bar fill to match the
            float percent = ((float)_CharacterAssociated.GetDashCooldown() / _DashMaxCooldown);
            _DashBar.fillAmount = percent;

        }
    }

}