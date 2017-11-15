using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_Text : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 16.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    /// Public (designers)
    public MatchManager.GameState ShowInState;

    /// Private
    private Image _TitleText;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public void Start() {
        
        // Get reference to text component
        _TitleText = GetComponent<Image>();
    }   

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void FixedUpdate () {
		
        // If gameplay is in the correct phase
        if (MatchManager._pInstance.GetGameState().Equals(ShowInState)) { 

            // Show text
            _TitleText.enabled = true;
        }

        // If gameplay isnt in the correct phase
        else { /// MatchManager._pInstance.GetGameState() != (ShowInState)

            // Hide text
            _TitleText.enabled = false;
        }
    }

}