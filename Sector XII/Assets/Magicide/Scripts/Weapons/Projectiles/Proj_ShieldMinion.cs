using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_ShieldMinion : Projectile {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public int _Health = 100;                                      // Current health associated to the minion.

    private float _SpinSpeed = 4f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get referenece to projectile collision
        base.Start();
    }

    public void AddToPool(Wep_Shield weapon) {

        weapon.GetMeatMinionPool().Add(this);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously rotate the minion on the spot 
        transform.Rotate(0f, transform.rotation.y + _SpinSpeed, 0f);
    }

    //--------------------------------------------------------------
    // *** HEALTH ***

    public void Damage(int amount) {

        // Damage character based on amount passed through
        _Health -= amount;

        // Check if minion character has no health
        if (_Health <= 0) {
            
            // Detach & hide minion (NOT DELETED)
            GetComponent<Renderer>().enabled = false;
            transform.parent = null;
            transform.position = new Vector3(1000, 0, 1000);
        }
    }

}