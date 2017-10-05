using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_ShieldMinion : Projectile {

    //--------------------------------------
    // VARIABLES

    private float _SpinSpeed = 4f;
    private int _Health = 100;

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // add to object pool
        ///PlayerManager._pInstance.GetActiveAuraMinions().Add(this.gameObject);
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously rotate the minion on the spot 
        transform.Rotate(0f, transform.rotation.y + _SpinSpeed, 0f);
    }

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
