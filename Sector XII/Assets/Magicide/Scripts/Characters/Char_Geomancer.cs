using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Geomancer : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Private
    private XboxCtrlrInput.XboxButton _DashInputButton = XboxCtrlrInput.XboxButton.B;
    private float _DashDistance;
    private float _DashCooldown;
    private float _CurrentDashCooldown = 0f;

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

        // Set dash properties
        _DashInputButton = PlayerManager._pInstance.DashButton;
        _DashDistance = PlayerManager._pInstance.DashDistance;
        _DashCooldown = PlayerManager._pInstance.DashCooldown;

        // Create players's primary weapon (orb)
        _WeaponPrimary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_PrimaryWeapon").GetComponent<Weapon>();
        _WeaponPrimary.SetOwner(this);
        _WeaponPrimary.Init(); /// Create fireball object pool (inactive projectiles)

        // Create player's secondary weapon (shield)
        _WeaponSecondary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_SecondaryWeapon").GetComponent<Weapon>();
        _WeaponSecondary.SetOwner(this);
        _WeaponSecondary.Init(); /// Create minion object pool (empty)

        // Can be controlled by player / ai controller
        SetActive(true);

        // Add the gameObject associated to this script to the playermanager object pools
        PlayerManager._pInstance.GetAliveNecromancers().Add(this);
        PlayerManager._pInstance.GetAllPlayers().Add(this);
    }

    //--------------------------------------------------------------
    // *** FRAME ***
    
    public override void FixedUpdate() {

        // If in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Only proceed if the character is actively being possessed by a controller (player OR ai)
            if (_Active == true) {

                // ************************
                //   MOVEMENT CONTROLLER 
                // ************************
                
                // Placeholder movement controller
                if (_Player.GetRotationInput != new Vector3(0, 90, 0)) {

                    // Get directional input (movement)
                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * _MovementSpeed * Time.fixedDeltaTime, Quaternion.Euler(_Player.GetRotationInput));
                }

                else { /// GetRotationInput == new Vector3(0, 0, 0)

                    // Get directional input (movement)
                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * _MovementSpeed * Time.fixedDeltaTime, transform.rotation);
                }

                // ************************
                //    COMBAT CONTROLLER   
                // ************************

                // Detect firing input
                if (_Player.GetFireInput) { 
                 
                    // Fire primary weapon (orb)
                    _WeaponPrimary.Fire();
                }

                // Detect dash ability input
                switch (_DashInputButton) {

                    // Face button bottom (A)
                    case XboxCtrlrInput.XboxButton.A: { 

                            if (_Player.GetFaceBottomInput) {

                                Dash();
                            }
                            break;
                        }

                    // Face button right (B)
                    case XboxCtrlrInput.XboxButton.B: {
                            
                            if (_Player.GetFaceRightInput) {

                                Dash();
                            }
                            break;
                        }

                    // Face button left (X)
                    case XboxCtrlrInput.XboxButton.X: {

                            if (_Player.GetFaceLeftInput) {

                                Dash();
                            }
                            break;
                        }

                    // Face button top (Y)
                    case XboxCtrlrInput.XboxButton.Y: {

                            if (_Player.GetFaceTopInput) {

                                Dash();
                            }
                            break;
                        }

                    default: {

                            break;
                        }
                }              
                
                // Deduct dash cooldown
                if (_CurrentDashCooldown > 0f) {

                    _CurrentDashCooldown -= Time.fixedDeltaTime;

                    // Clamp to 0f
                    if (_CurrentDashCooldown < 0f) {

                        _CurrentDashCooldown = 0f;
                    }
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void Damage(int amount) {

        // Only damage character if match is in phase2
        if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

            // Damage character based on amount passed through
            _Health -= amount;

            // Returns TRUE if character has no health
            if (_Health <= 0) {

                // Character has died
                OnDeath();
            }
        }
    }

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // Kill all the minions associated with the character
   ///     Wep_Shield shield = _WeaponSecondary.GetComponent<Wep_Shield>();
   ///     foreach (var minion in shield.GetMeatMinionPool())
   ///     {
   ///         minion.GetComponent<Proj_ShieldMinion>().ForceDeath();
   ///     }
        // Reset score to 0
        ///_Player.SetScore(0);

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

    //--------------------------------------------------------------
    // *** ABILITIES ***

    public void Dash() {

        // If dash cooldown is complete
        if (_CurrentDashCooldown <= 0f) {

            // Determine how far the character can teleport
            Vector3 DashPos = transform.position;
            Vector3 DashDirection = _Player.GetMovementInput;
            DashPos += DashDirection * _DashDistance;

            // Perform dash
            gameObject.transform.position = DashPos;

            // Reset cooldown
            _CurrentDashCooldown = _DashCooldown;
        }
    }
}