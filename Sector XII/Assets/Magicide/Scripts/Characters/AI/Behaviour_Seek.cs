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

    public Transform _Target;

    /// Private
    NavMeshAgent _agent;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        _agent = GetComponent<NavMeshAgent>();
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {

        if (_agent != null) {

            // Continuously seek towards the target
            _agent.destination = _Target.transform.position;
        }
	}

}