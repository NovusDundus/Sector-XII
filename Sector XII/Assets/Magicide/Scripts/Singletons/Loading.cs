using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    public AsyncOperation Async { get; private set; }

    /// Public (internal)
    [HideInInspector]
    public static Loading _pInstance;                               // This is a singleton script, Initialized in Startup().

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

    //--------------------------------------------------------------
    // *** LOADING *** 

    public void LoadLevel(int index) {

        Async = SceneManager.LoadSceneAsync(index);
        Async.allowSceneActivation = false;
    }

    public void ActivateLevel() {

        Async.allowSceneActivation = true;
    }

    public bool LoadComplete() {

        return Async.isDone;
    }

}