﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (designers)
    

    /// Public (internal)
    [HideInInspector]
    public static SoundManager _pInstance;                          // This is a singleton script, Initialized in Startup().

    /// Private

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }
}
