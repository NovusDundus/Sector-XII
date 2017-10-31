using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behaviour_Seek : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Callan Mitchel
    /// Created on: 30.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Private
    private NavMeshAgent _agent;
    private SphereCollider _RetargetCollision;
    private GameObject _Target;
    private LinearGoToTarget _GoToTarget;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        _agent = GetComponent<NavMeshAgent>();
        _RetargetCollision = GetComponent<SphereCollider>();
        _GoToTarget = GetComponent<LinearGoToTarget>();
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {

        if (_agent != null && _Target != null) {

            // Continuously seek towards the target
            _agent.destination = _Target.transform.position;
            _GoToTarget.enabled = false;
        }

        else if (_Target != null) {

            ///_GoToTarget.enabled = true;
        }
	}

    private void OnTriggerEnter(Collider other) {

        // Check if other object is a player
        if (other.tag == "P1_Character" || other.tag == "P2_Character" || other.tag == "P3_Character" || other.tag  == "P4_Character") {

            // Set gameobject to be the new seek target
            _Target = other.gameObject;
        }
    }

}