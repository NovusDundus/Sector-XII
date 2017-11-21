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
    
    /// Public (Exposed)
    public RectTransform _Panel;                                    // Reference to the 'CreditsReel' panel.
    public float _ReelSpeed = 0.5f;                                 // The speed that the credits reel will move across the screen.
    public float _ReturnMenuDelay = 10f;

    /// Private
    private float _ReturnMenuTimer = 0f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public void Start () {

        ResetReel();
    }
    
    public void ResetReel() {

        // Move the credits reel's rect transform back to the starting position
        _Panel.localPosition = new Vector3(50, -500);

        // Reset timer
        _ReturnMenuTimer = 0f;

        if (MainMenu._pInstance._GameTitleImage != null) {

            // Hide game title image
            MainMenu._pInstance._GameTitleImage.SetActive(false);
        }
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void Update () {
		
        if (_Panel != null) {

            // Move upwards across the screen
            _Panel.localPosition = new Vector3(_Panel.localPosition.x, _Panel.localPosition.y + _ReelSpeed, _Panel.localPosition.z);

            // Auto credits reel timer is not complete
            if (_ReturnMenuTimer < _ReturnMenuDelay) {

                _ReturnMenuTimer += Time.deltaTime;
            }

            else { /// _ReturnMenuTimer >= _ReturnMenuDelay

                // Return to main menu
                GetComponentInParent<ButtonClicksMainMenu>().OnClick_bGoBack_Credits();
            }
        }
	}

}