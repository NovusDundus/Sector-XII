using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Alessandro Baiocchi
    /// Created on: 14.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Publ;ic
    public float _Speed = 15.0f;

    //--------------------------------------------------------------
    // *** FRAME ***
    
    private void Update () {

        transform.Rotate(new Vector3(0f, Time.deltaTime * _Speed, 0f), Space.World);
	}

}