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

    /// Private
    private float _SpinSpeed = 4f;
    ///private 

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get referenece to projectile collision
        base.Start();
    }

    public void AddToPool(Wep_Shield weapon) {

        weapon.GetMeatMinionPool().Add(this.gameObject);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously rotate the minion on the spot 
        transform.Rotate(0f, transform.rotation.y + _SpinSpeed, 0f);
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Collision") {

        }
    }

    //--------------------------------------------------------------
    // *** HEALTH ***

    public void Damage(int amount) {

        // Damage character based on amount passed through
        _Health -= amount;

        // Check if minion character has no health
        if (_Health <= 0) {

            // Clamp health to 0
            _Health = 0;

            // Detach & hide minion (NOT DELETED)
            transform.parent = null;
            transform.position = new Vector3(1000, 0, 1000);
                                               
            // Deduct from weapon's minion count thats associated with this minion
            GetComponent<Renderer>().enabled = false;
            Wep_Shield wep = GameObject.FindGameObjectWithTag(string.Concat("P" + GetOwner().GetOwner()._Player._pPlayerID + "_SecondaryWeapon")).GetComponent<Wep_Shield>();
            wep.SetMinionCount(wep.GetMinionCount() - 1);

            // Set player associated with the minion's score
            wep.GetOwner()._Player.SetScore(wep.GetMinionCount());

            Debug.Log(wep.GetMinionCount());

            // Remove from weapon pool
            wep.GetMeatMinionPool().Remove(this.gameObject);
        }
    }

    public void ForceDeath()
    {
        Damage(10000);
    }

}