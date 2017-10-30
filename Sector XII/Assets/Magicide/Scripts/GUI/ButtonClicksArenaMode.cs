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
    
    /// Public (designers)
    [Header("LOADING SCREENS")]
    public GameObject _LoadingScreen;
    public GameObject _RestartScreen;

    //--------------------------------------------------------------
    // *** BUTTON CLICKS *** 

    public void OnClick_Restart() {

        // Hide scoreboard panel
        HUD._pInstance._UIScoreboard.SetActive(false);

        // Reload the arena mode level
        _LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(1);

        // Show loading screen panel
        _LoadingScreen.SetActive(true);
        _RestartScreen.SetActive(true);
    }

    public void OnClick_MainMenu() {

        // Load main menu level
        _LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(0);

        // Show loading screen panel
        _LoadingScreen.SetActive(true);
    }

    public void OnClick_Resume() {

        // Hide pause screen panel
        HUD._pInstance._UIPause.SetActive(false);

        // Resume gameplay
        MatchManager._pInstance.SetPause(false);
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    void Update () {
		
	}

}