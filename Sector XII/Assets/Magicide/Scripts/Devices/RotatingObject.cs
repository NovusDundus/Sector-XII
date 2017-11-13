using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Alessandro Baiocchi
    /// Created on: 14.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Private
    private float degrees = 15.0f;

    //--------------------------------------------------------------
    // *** FRAME ***
    
    private void Update () {

        transform.Rotate(new Vector3(0f, Time.deltaTime * degrees, 0f), Space.World);
	}

}