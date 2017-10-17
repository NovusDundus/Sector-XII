using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Protected
    protected Weapon _Owner;                                        // Reference to the weapon that owns/fires this projectile.
    protected Collider _Collision;                                  // Reference to the projectile's collision.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public virtual void Start() {

        // Get reference to collision collider
        _Collision = GetComponent<Collider>();
    }

    public virtual void Init() {

        // Reinitialize
        Start();
    }

    public void SetOwner(Weapon a_Owner) {

        // Set the new owner for this projectile
        _Owner = a_Owner;
    }

    public Weapon GetOwner() {

        return _Owner;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // *** COLLISION ***

    public virtual void OnImpact() {

    }

    public Collider GetCollision() {

        return _Collision;
    }

}