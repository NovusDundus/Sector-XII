using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicksMainMenu : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    public GameObject ui_MainMenu;
    public GameObject ui_LoadingScreen;
    public GameObject ui_Credits;
    public GameObject ui_ExitGameConfirm;

    private bool _ClosingApplication = false;

    //--------------------------------------------------------------
    // *** BUTTON CLICKS *** 

    public void OnClick_Play() {

        // Start game (go to loading screen)
        if (ui_LoadingScreen != null && ui_MainMenu != null) {

            // Load arena mode level
            ui_LoadingScreen.GetComponent<LoadingScreen>().SetLevelIndex(1);

            // Show loading screen
            ui_LoadingScreen.SetActive(true);

            // Hide main menu
            ui_MainMenu.SetActive(false);
        }
    }

    public void OnClick_Credits() {

        // Transition to credits menu
        if (ui_Credits != null && ui_MainMenu != null) {

            // Reset credits reel
            ui_Credits.GetComponent<CreditsReel>().ResetReel();

            // Show credits
            ui_Credits.SetActive(true);

            // Hide main menu
            ui_MainMenu.SetActive(false);
        }
    }

    public void OnClick_ExitGame() {

        // Show quit game popup
        if (ui_ExitGameConfirm != null) {
            
            ui_ExitGameConfirm.SetActive(true);
        }
    }

    public void OnClick_bGoBack_Credits() {

        // Transition back to main menu
        if (ui_Credits != null && ui_MainMenu != null) {

            if (MainMenu._pInstance._GameTitleImage != null) {

                // Show game title image
                MainMenu._pInstance._GameTitleImage.SetActive(true);
            }

            // Show main menu
            ui_MainMenu.SetActive(true);

            // Hide credits
            ui_Credits.SetActive(false);
        }
    }

    public void OnClick_bCancelExitGame() {
    
        // Hide quit game popup
        if (ui_ExitGameConfirm != null) {
            
            ui_ExitGameConfirm.SetActive(false);
        }
    }

    public void OnClick_bConfirmExitGame() {
                
        // Fade into black
        Fade._pInstance.StartFade(Fade.FadeStates.fadeIn, Color.black, 0.04f);

        _ClosingApplication = true;
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public void FixedUpdate() {

        // Once the fade after confirming the exit game button is complete
        if (_ClosingApplication == true && Fade._pInstance.IsFadeComplete() == true) {
            
            // Close the application
            Application.Quit();
        }
    }

}