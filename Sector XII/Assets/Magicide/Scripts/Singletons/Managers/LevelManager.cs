using System.Collections;
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
    private List<LevelObject> _StaticObjects;
    private List<LevelObject> _DynamicObjects;

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

        // Create object pools
        _StaticObjects = new List<LevelObject>();
        _DynamicObjects = new List<LevelObject>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
	}

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // *** OBJECT POOLS ***

    public List<LevelObject> GetStaticObjects() {

        return _StaticObjects;
    }

    public List<LevelObject> GetDynamicObjects() {

        return _DynamicObjects;
    }
}
