using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public GameObject _GameTitleImage;                              // Reference to the Game Title Image.

    /// Public (internal)
    [HideInInspector]
    public static MainMenu _pInstance;                              // This is a singleton script, Initialized in Awake().
    public enum MenuState {

        SplashScreen,
        MainMenu,
        Credits,
        ExitGame,
        LoadingScreen
    }                                       // Enumerator list of Menu States

    private MenuState _State = MenuState.SplashScreen;              // Current state/page of the main menu.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    public void Start() {
        
        // Fade in from black
        Fade._pInstance.StartFade(Fade.FadeStates.fadeOut, Color.black, 0.01f);
    }

    public void FixedUpdate() {

        switch (_State) {

            case MenuState.SplashScreen: {

                    

                    break;
                }

            case MenuState.MainMenu: {

                    break;
                }

            case MenuState.Credits: {

                    break;
                }

            case MenuState.ExitGame: {

                    break;
                }

            case MenuState.LoadingScreen: {

                    break;
                }

            default: {

                    break;
                }
        }
    }

}