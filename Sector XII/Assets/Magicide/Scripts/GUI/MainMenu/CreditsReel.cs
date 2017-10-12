using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsReel : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    public RectTransform _Panel;                                    // Reference to the 'CreditsReel' panel.
    public float _ReelSpeed = 0.5f;                                 // The speed that the credits reel will move across the screen.
    
    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public void Start () {
        
    }
    
    public void ResetReel() {

        // Move the credits reel's rect transform back to the starting position
        _Panel.localPosition = new Vector3(50, -300);
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void Update () {
		
        if (_Panel != null) {

            // Move upwards across the screen
            _Panel.localPosition = new Vector3(_Panel.localPosition.x, _Panel.localPosition.y + _ReelSpeed, _Panel.localPosition.z);
        }
	}

}