using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 27.11.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    public float _DestroyAfter = 1f;

    /// Private
    private GameObject _particle;
    private float _Timer = 0f;

    // -------------------------------------------------------------
    // *** DESTROY ***

    public void Start() {

        // Get references
        _particle = GetComponent<GameObject>();
    }

    void Update () {

        // Wait until timer is complete
        if (_Timer <= _DestroyAfter) {

            _Timer += Time.deltaTime;
        }

        // Timer is complete
        else { /// _Timer > _DestroyAfter

            Destroy(gameObject);
        }
	}

}