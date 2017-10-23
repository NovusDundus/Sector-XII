﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 23.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public GameObject _TeleportPartner;
    public Transform _TeleportPosition;

    /// Private
    private int _CooldownTime;
    private float _Cooldown = 0f;
    private bool _CanUse = true;
    private bool _Phase1Enabled = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start () {

        // Initialize properties based off the game manager
        _CooldownTime = DeviceManager._pInstance._TeleportCooldownTime;
        _Phase1Enabled = DeviceManager._pInstance._CanBeUsedInPhase1;
    }
    
    //--------------------------------------------------------------
    // *** FRAME ***
    
    public void FixedUpdate () {
		
        // Cooldown not complete
        if (_Cooldown > 0f) {

            // Deduct 1 second per second
            _Cooldown -= Time.fixedDeltaTime;

            // Clamp to 0f
            if (_Cooldown < 0f) {

                _Cooldown = 0f;
            }
        }

        _CanUse = _Cooldown == 0f;
    }

    public void OnTriggerEnter(Collider other) {
        
        // Can the teleporter be used in phase 1?
        if (_Phase1Enabled == true) {

            // If its a player controlled character thats been collided with the trigger
            if (other.GetComponent<Player>() != null) {

                if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase1) {

                    if (_Phase1Enabled == true) {

                        // If cooldown is complete
                        if (_CanUse == true) {

                            Teleport(other);
                        }
                    }
                }

                else if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

                    // If cooldown is complete
                    if (_CanUse == true) {

                        Teleport(other);
                    }

                }
            }
        }
    }

    public void Teleport(Collider other) {

        // Teleport to partnering position
        other.transform.position = _TeleportPosition.position;

        // Disable teleporters
        ResetCooldown();
        _TeleportPartner.GetComponent<TeleportGate>().ResetCooldown();
    }

    public void ResetCooldown() {

        // Reset cooldown
        _Cooldown = _CooldownTime;
    }
    
}