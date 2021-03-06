﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 10.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES
    
    /// Public (internal)
    [HideInInspector]
    public static LevelManager _pInstance;                          // This is a singleton script, Initialized in Startup().

    /// Private
    private List<GameObject> _StaticObjects;
    private List<GameObject> _DynamicObjects;

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

        // Create object pools
        _StaticObjects = new List<GameObject>();
        _DynamicObjects = new List<GameObject>();
    }
    
    //--------------------------------------------------------------
    // *** OBJECT POOLS ***

    public List<GameObject> GetStaticObjects() { return _StaticObjects; }

    public List<GameObject> GetDynamicObjects() { return _DynamicObjects; }
}
