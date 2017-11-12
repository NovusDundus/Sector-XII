using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 9.11.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public string[] _Tags;
    
    //--------------------------------------------------------------
    // *** TAGS ***

    public bool ContainsTag(string value) {

        // Loops through the object's tag array and checks to see if the value exists in the array
        foreach (string tag in _Tags) {

            if (tag == value)
                return true;
        }
        return false;
    }

}