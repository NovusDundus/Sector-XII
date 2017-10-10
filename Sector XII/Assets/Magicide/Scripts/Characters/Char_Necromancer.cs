using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Necromancer : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***



    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Stores reference to the player associated with its
        _Player = GetComponent<Player>();

        // Set character's health & get collision reference
        _StartingHealth = PlayerManager._pInstance._NecromancerStartingHealth;
        base.Start();

        // Set character's speed
        _MovementSpeed = PlayerManager._pInstance._NecromancerMovementSpeed;
        _RotationSpeed = PlayerManager._pInstance._NecromancerRotationSpeed;
        
        // Create players's primary weapon (orb)
        _WeaponPrimary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_PrimaryWeapon").GetComponent<Weapon>();
        _WeaponPrimary.SetOwner(this);
        _WeaponPrimary.Init(); /// Create fireball object pool (full of inactive fireballs)

        // Create player's secondary weapon (shield)
        _WeaponSecondary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_SecondaryWeapon").GetComponent<Weapon>();
        _WeaponSecondary.SetOwner(this);
        _WeaponSecondary.Init(); /// Create minion object pool (empty)

        // Can be controlled by player / ai controller
        SetActive(true);

        // Add the gameObject associated to this script to the playermanager object pool
        PlayerManager._pInstance.GetAliveNecromancers().Add(this);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {

        // If in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Only proceed if the character is actively being possessed by a controller (player OR ai)
            if (_Active == true) {

                // ************************
                //   MOVEMENT CONTROLLER 
                // ************************

                // Placeholder movement controller (DOESNT RELY ON SPEED, JUST PURE CONTROLLER INPUT)
                if (_Player.GetRotationInput != new Vector3(0, 90, 0)) {

                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * _MovementSpeed * Time.fixedDeltaTime, Quaternion.Euler(_Player.GetRotationInput));
                }

                else { /// GetRotationInput == new Vector3(0, 0, 0)

                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * _MovementSpeed * Time.fixedDeltaTime/*_Player.GetMovementInput / 4*/, transform.rotation);
                }

                // ************************
                //    COMBAT CONTROLLER   
                // ************************

                // Detect firing input
                if (_Player.GetFireInput) { 
                 
                    // Fire primary weapon (orb)
                    _WeaponPrimary.Fire();
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // hide character & move out of playable space
        GetComponentInChildren<Renderer>().enabled = false;
        transform.position = new Vector3(1000, 0, 1000);

        // Find self in active pool
        foreach (var necromancer in PlayerManager._pInstance.GetAliveNecromancers()) {
            
            // Once we have found ourself in the pool
            if (necromancer == this) {

                // Move to inactive pool
                PlayerManager._pInstance.GetDeadNecromancers().Add(necromancer);
                PlayerManager._pInstance.GetAliveNecromancers().Remove(necromancer);
                break;
            }
        }
    }
}