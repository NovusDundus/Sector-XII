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
    // VARIABLES

    /// Public
    public GameObject uiPause; 

    //--------------------------------------------------------------
    // CONSTRUCTORS
    
    void Start () {

        // Get reference to the panel
        ///uiPause = GetComponent<GameObject>();
	}

    //--------------------------------------------------------------
    // FRAME

    void Update () {

        // Game is PAUSED
        if (MatchManager._pInstance.GetPaused() == true) {

            uiPause.SetActive(true);
        }

        // Game is UNPAUSED
        else { /// MatchManager._pInstance.GetPaused() == false

            uiPause.SetActive(false);
        }
    }
}
