using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 10.10.2017  
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public bool _StaticObject = true;

    /// Private
    private Collider _Collision;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***
    
    void Start () {

        // Get reference to collision
        _Collision = GameObject.FindGameObjectWithTag("Collision").GetComponent<Collider>();

        // Add to object pool
        if (_StaticObject == true) {

            // Static object pool
            LevelManager._pInstance.GetStaticObjects().Add(this.gameObject);
        }

        else { /// _StaticObject == false

            // Dynamic object pool
            LevelManager._pInstance.GetDynamicObjects().Add(this.gameObject);
        }
	}

    //--------------------------------------------------------------
    // *** MISC ***

    void Update () {
		
	}

    public Collider GetCollision() {

        return _Collision;
    }
}
