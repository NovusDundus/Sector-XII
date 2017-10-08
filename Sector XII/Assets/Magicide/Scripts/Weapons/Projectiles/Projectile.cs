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

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public virtual void Start() {

    }

    public virtual void Init() {

        // Reinitialize
        Start();
    }

    public void SetOwner(Weapon a_Owner) {

        // Set the new owner for this projectile
        _Owner = a_Owner;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

    }

    public virtual void OnImpact() {

    }

}