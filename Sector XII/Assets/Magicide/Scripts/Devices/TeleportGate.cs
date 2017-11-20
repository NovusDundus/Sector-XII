using System.Collections;
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
    public GameObject _Rune;
    public Transform _TeleportPosition;
    public Material _InactiveMaterial;
    public Material _ActiveMaterial;

    /// Private
    private int _CooldownTime;
    private float _Cooldown = 0f;
    private bool _CanUse = true;
    private bool _Phase1Enabled = false;
    private MeshRenderer meshRenderer;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Initialize properties based off the game manager
        _CooldownTime = DeviceManager._pInstance._TeleportCooldownTime;
        _Phase1Enabled = DeviceManager._pInstance._UsedInPhase1;

        // Get reference to mesh renderer to change the material
        if (_Rune != null) {

            meshRenderer = _Rune.GetComponent<MeshRenderer>();
        }
    }
    
    //--------------------------------------------------------------
    // *** FRAME ***
    
    public void Update () {
		
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

        // Set material of the plane
        if (_CanUse == true) {

            // If the teleport can be used in phase 1
            if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase1 && _Phase1Enabled == true) {

                // Active teleport
                meshRenderer.material = _ActiveMaterial;
            }

            else {

                // If the match's gamestate is in phase 2
                if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

                    // Active teleport
                    meshRenderer.material = _ActiveMaterial;

                    // Lerp to active teleport
                }

                else { /// MatchManager._pInstance.GetGameState() != MatchManager.GameState.Phase2

                    // Inactive teleport
                    meshRenderer.material = _InactiveMaterial;
                }
            }
        }

        else { /// _CanUse == false

            // Inactive teleport
            meshRenderer.material = _InactiveMaterial;
        }
    }

    public void OnTriggerEnter(Collider other) {
        
        // If its a player controlled character thats been collided with the trigger
        if (other.GetComponent<Player>() != null && other.GetComponent<Char_Geomancer>().GetActive() == true) {

            if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase1) {

                // Can the teleporter be used in phase 1?
                if (_Phase1Enabled == true) {

                    // If cooldown is complete
                    if (_CanUse == true) {

                        // Teleport character
                        Teleport(other);
                    }
                }
            }

            else if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

                // If cooldown is complete
                if (_CanUse == true) {

                    // Teleport character
                    Teleport(other);
                }
            }
        }        
    }

    public void Teleport(Collider other) {

        // Teleport to partnering position
        other.transform.position = new Vector3(_TeleportPosition.position.x, other.transform.position.y, _TeleportPosition.position.z);

        // Disable teleporters
        ResetCooldown();
        _TeleportPartner.GetComponent<TeleportGate>().ResetCooldown();

        // Play teleport enter sound
        SoundManager._pInstance.PlayTeleport();
    }

    public void ResetCooldown() {

        // Reset cooldown
        _Cooldown = _CooldownTime;
    }
    
}