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
        Fade._pInstance.StartFade(Fade.FadeStates.fadeOut, Color.black, 0.02f);

        // Cinematic bars EXIT
        CinematicBars._pInstance.StartAnimation(CinematicBars.BarDirection.Exit, 4f);

        // Play music
        SoundManager._pInstance.PlayMusicMainMenu();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        // Hide the mouse cursor
        Cursor.visible = false;
    }

}