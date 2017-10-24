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
    
    /// Private
    private int _Health;                                            // Current health associated to the minion.
    private float _SpinSpeed;
    private float _BobHeight;
    private float _BobSpeed;
    private float _Min;
    private float _Max;
    private bool _MovingUp = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get referenece to projectile collision
        base.Start();

        // Set min & max bob positions
        _Min = transform.position.y;
        _Max = _Min + _BobHeight;

        // Get stats based off weapon manager
        _Health = WeaponManager._pInstance._MinionHealth;
        _SpinSpeed = WeaponManager._pInstance._MinionSpinSpeed;
        _BobHeight = WeaponManager._pInstance._MinionBobHeight;
        _BobSpeed = WeaponManager._pInstance._MinionBobSpeed;
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
        
        // Bob the minion up & down
        if (_MovingUp == true) {

            // Havent reached max yet
            if (transform.position.y < _Max) {

                // Move up
                transform.position = new Vector3(transform.position.x, transform.position.y + _BobSpeed * Time.deltaTime, transform.position.z);
            }

            // Minimum bob has been reached
            else {

                _MovingUp = false;
            }
        }

        else { /// _MovingUp == false

            // Havent reached min yet
            if (transform.position.y > _Min) {

                // Move down
                transform.position = new Vector3(transform.position.x, transform.position.y - _BobSpeed * Time.deltaTime, transform.position.z);
            }

            // Minimum bob has been reached
            else {

                _MovingUp = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Collision") {

            // Lock shield weapon rotation
            _Owner.GetComponent<Wep_Shield>().SetCanRotate(false);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision collision) {

        if (collision.gameObject.tag == "Collision") {

            // Unlock shield weapon rotation
            _Owner.GetComponent<Wep_Shield>().SetCanRotate(true);
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    //--------------------------------------------------------------
    // *** HEALTH ***

    public void Damage(float amount) {

        // Damage character based on amount passed through
        _Health -= (int)amount;

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

    public int GetHealth() {

        return _Health;
    }

}