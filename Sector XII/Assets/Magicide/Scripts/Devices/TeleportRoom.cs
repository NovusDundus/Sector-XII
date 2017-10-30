using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRoom : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 30.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Private
    private List<GameObject> _AiList;
    private Collider _Collision;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        // Create arrays
        _AiList = new List<GameObject>();

        // Get references
        _Collision = GetComponent<Collider>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
        // For all Ai in the room

        // Force them out through the gates via LinearGoToTarget
	}

    private void OnTriggerEnter(Collider other) {

        // If the object isnt already in the array

        // Add it to the AiList
    }

}