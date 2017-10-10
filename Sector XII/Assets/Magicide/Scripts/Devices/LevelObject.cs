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
    public int _Health = 100;
    public bool _destroyed = false;

    /// Private
    private Collider _Collision;
    private bool _CanBeDamaged = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start () {

        // Get reference to collision
        _Collision = GameObject.FindGameObjectWithTag("Collision").GetComponent<Collider>();

        _CanBeDamaged = !_StaticObject;

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
    // *** FRAME ***

    public void Update () {

    }

    public void FixedUpdate() {
        
        // If the object is dynamic
        if (_CanBeDamaged == true) {

        }
    }

    //--------------------------------------------------------------
    // *** HEALTH ***

    public Collider GetCollision() {

        return _Collision;
    }

    public void Damage(int damage) {

        // Only damage the object if its dynamic
        if (_CanBeDamaged == true) {

            // Apply damage to health
            _Health -= damage;

            // Check if dead
            if (_Health <= 0) {

                _Health = 0;

                // Set object to destroyed
                _destroyed = true;
            }
        }
    }
}
