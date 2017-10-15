using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMode : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 12.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (internal)
    [HideInInspector]
    public static ArenaMode _pInstance;                              // This is a singleton script, Initialized in Awake().

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

    }

    //--------------------------------------------------------------
    // *** FRAME ***

}