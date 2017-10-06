using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Flee : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: FIRSTNAME LASTNAME
    /// Created on: DATE HERE
    ///--------------------------------------///

    //--------------------------------------
    // VARIABLES

    /// Public (designers)
    public Character _FleeTarget;

    /// Public (internal)
    public Character _Agent;
    public float _Speed;
    
    /// Private
    private Vector3 _TargetPosition;
    private Vector3 _FleeDirection;

    //--------------------------------------
    // FUNCTIONS

    void Start() {

    }

    void Update() {

        // If a valid agent & flee target has been set
        if (_FleeTarget != null && _Agent != null) {

            _Speed = _Agent.GetMovementSpeed();

            // Get target's position to determine flee direction
            _TargetPosition = _FleeTarget.transform.position;
            _FleeDirection = _Agent.transform.position - _TargetPosition;
            _FleeDirection.Normalize();
            _FleeDirection = _FleeDirection * _Speed;

            // Set facing direction away from target's currect position
            _Agent.transform.forward = _FleeDirection.normalized;
        }
    }
}
