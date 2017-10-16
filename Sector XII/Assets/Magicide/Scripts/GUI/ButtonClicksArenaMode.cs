using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicksArenaMode : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 
    
    public GameObject _LoadingScreen;

    //--------------------------------------------------------------
    // *** BUTTON CLICKS *** 

    public void OnClick_Restart() {

        // Reset everything & start a new match
        ///MatchManager._pInstance.Start();

        // Hide scoreboard panel
        HUD._pInstance._UIScoreboard.SetActive(false);

        // Load main menu level
        _LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(1);

        // Show loading screen panel
        _LoadingScreen.SetActive(true);
    }

    public void OnClick_MainMenu() {

        // Load main menu level
        _LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(0);

        // Show loading screen panel
        _LoadingScreen.SetActive(true);
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    void Update () {
		
	}

}