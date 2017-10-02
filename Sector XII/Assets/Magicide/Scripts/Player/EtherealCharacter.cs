using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealCharacter : PlayerCharacter {

    //--------------------------------------
    // VARIABLES

    

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Stores reference to the player controller
        _Player = GetComponent<Player>();

        // Set character's movement speed
        _MovementSpeed = PlayerManager._pInstance._pEtherealSpeed;
        _RotationSpeed = PlayerManager._pInstance._pEtherealRotationRate;

        // Set character's health
        _Health = PlayerManager._pInstance._pEtherealStartingHealth;

        // Create character's weapon
        _Weapon = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_AuraPool").GetComponent<Weapon>();
        _Weapon.SetOwner(this);
        _Weapon.Init(); /// Create minions

        // Inactive by default
        SetActive(true);
    }

    public override void Update() {

        base.Update();
    }

    public override void FixedUpdate() {

        // Detect firing input
        base.FixedUpdate();

        // Only proceed if the character is actively being controller in the world
        if (_pActive == true) {
                        
            // Placeholder movement controller (DOESNT RELY ON SPEED, JUST PURE CONTROLLER INPUT
            if (GetRotationInput != new Vector3(0, 0, 0)) {

                transform.SetPositionAndRotation(transform.position + GetMovementInput / 4, Quaternion.Euler(GetRotationInput));
            }

            else { // GetRotationInput == new Vector3(0, 0, 0)

                transform.SetPositionAndRotation(transform.position + GetMovementInput / 4, transform.rotation);
            }
        }
    }

    public override void OnDeath() {

        // Do default death related things
        base.OnDeath();
    }

    public override void FireInput() {

        // Only initiate if there is a valid weapon attached to the character
        if (_Weapon != null) {

            // Shoot weapon
            _Weapon.Fire();
        }
    }
}
