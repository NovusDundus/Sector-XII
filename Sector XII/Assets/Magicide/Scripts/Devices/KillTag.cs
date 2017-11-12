using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTag : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 30.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Public (internal)
    public enum PickupType {

        AddToShield,
        Healthpack,
        SpeedBoost
    }

    /// Private
    private bool _Active = false;
    private Collider _Collision;
    private Char_Crystal _Crystal;
    private float _Min;
    private float _Max;
    private bool _MovingUp = true;
    private PickupType _Type = PickupType.AddToShield;
    private float _RotationSpeed = 2f;
    private float _BobHeight = 1f;
    private float _BobSpeed = 1f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Get reference to the collision
        _Collision = GetComponent<Collider>();

        // Set min & max bob positions
        _Min = transform.position.y;
        _Max = _Min + _BobHeight;
    }

    public void Init(Char_Crystal Char, PickupType type) {

        // Set OnPickupType
        _Type = type;
        switch (_Type) {

            case PickupType.AddToShield: {

                    GetComponent<MeshRenderer>().material = DeviceManager._pInstance._AddShieldTypeMaterial;
                    _RotationSpeed = DeviceManager._pInstance._AddShieldRotationSpeed;
                    _BobHeight = DeviceManager._pInstance._AddShieldBobHeight;
                    _BobSpeed = DeviceManager._pInstance._AddShieldBobSpeed;
                    break;                                     
                }                                              

            case PickupType.SpeedBoost: { 

                    GetComponent<MeshRenderer>().material = DeviceManager._pInstance._SpeedBoostTypeMaterial;
                    _RotationSpeed = DeviceManager._pInstance._SpeedBoostRotationSpeed;
                    _BobHeight = DeviceManager._pInstance._SpeedBoostBobHeight;
                    _BobSpeed = DeviceManager._pInstance._SpeedBoostBobSpeed;
                    break;
                }          

            case PickupType.Healthpack: {

                    GetComponent<MeshRenderer>().material = DeviceManager._pInstance._mHealthpackTypeMaterial;
                    _RotationSpeed = DeviceManager._pInstance._HealthpackRotationSpeed;
                    _BobHeight = DeviceManager._pInstance._HealthpackBobHeight;
                    _BobSpeed = DeviceManager._pInstance._HealthpackBobSpeed;
                    break;
                }        

            default: {

                    break;
                }
        }

        // Set reference to the wyrm to be used in the meat shield
        _Crystal = Char;
        
        // Tag is now active in the world
        _Active = true;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        CollisionChecks();

        // Continuously spin the object
        transform.Rotate(0f, transform.rotation.y + _RotationSpeed, 0f);

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

    public void CollisionChecks() {

        // Precautions
        if (_Collision != null && _Active == true) {

            // Test against all alive necromancers in the game
            foreach (var necromancer in PlayerManager._pInstance.GetAliveNecromancers()) {

                // Once collision against the necro has happened
                if (_Collision.bounds.Intersects(necromancer.GetCollider().bounds)) {

                    // Pickup minion check
                    OnPickup(necromancer.GetComponent<Char_Geomancer>());

                    switch (_Crystal.GetVariantType()) {

                        case Char_Crystal.CrystalType.Minor: {
                                
                                if (AiManager._pInstance._CrystalMinorSpawningBehaviour == AiManager.AiSpawningBehaviour.RandomSpawning) {

                                    // Spawn at random position within the arena bounds
                                    AiManager._pInstance.OnRespawnMinorRandom();
                                }

                                if (AiManager._pInstance._CrystalMinorSpawningBehaviour == AiManager.AiSpawningBehaviour.TeleportingGates) {

                                    // Spawn behind a teleporter gate and move into the gameplay area
                                    AiManager._pInstance.OnRespawnMinorTeleporter();
                                }

                                break;
                            }
                        case Char_Crystal.CrystalType.Major: {

                                if (AiManager._pInstance._CrystalMajorSpawningBehaviour == AiManager.AiSpawningBehaviour.RandomSpawning) {

                                    // Spawn at random position within the arena bounds
                                    AiManager._pInstance.OnRespawnMajorRandom();
                                }

                                if (AiManager._pInstance._CrystalMajorSpawningBehaviour == AiManager.AiSpawningBehaviour.TeleportingGates) {

                                    // Spawn behind a teleporter gate and move into the gameplay area
                                    AiManager._pInstance.OnRespawnMajorTeleporter();
                                }
                                break;
                            }
                        case Char_Crystal.CrystalType.Cursed: {

                                if (AiManager._pInstance._CrystalCursedSpawningBehaviour == AiManager.AiSpawningBehaviour.RandomSpawning) {

                                    // Spawn at random position within the arena bounds
                                    AiManager._pInstance.OnRespawnCursedRandom();
                                }

                                if (AiManager._pInstance._CrystalCursedSpawningBehaviour == AiManager.AiSpawningBehaviour.TeleportingGates) {

                                    // Spawn behind a teleporter gate and move into the gameplay area
                                    AiManager._pInstance.OnRespawnCursedTeleporter();
                                }
                                break;
                            }
                        default: {
                                break;
                            }
                    }
                    break;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** PICKUP ***

    public void OnPickup(Char_Geomancer Necromancer) {

        switch (_Type) {

            // MINION SHIELD
            case PickupType.AddToShield: {

                    AddToShield(Necromancer);
                    break;
                }

            // HEALTHPACK
            case PickupType.Healthpack: {

                    Healthpack(Necromancer);
                    break;
                }
            
            // SPEED BOOST
            case PickupType.SpeedBoost: {

                    SpeedBoost(Necromancer);
                    break;
                }

            default: {

                    break;
                }
        }

        // Destroy tag
        ///Destroy(gameObject);
    }

    public void AddToShield(Char_Geomancer Geomancer) {

    // Determine if whether the necromancer can be picked up or not
    // Get minion count
    int minionCount = Geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMinionCount();

    // Check against max size
    float MaxSize = Geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMaxMinions() * 0.65f;
        if (minionCount < MaxSize) {

            // Add to meat shield
            Geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().AddMinion(_Crystal);

            // Destroy tag
            Destroy(gameObject);
        }
    }

    public void Healthpack(Char_Geomancer Geomancer) {

        // Determine if whether the tag can be picked up or not
        // Check if there's any missing health
        if (Geomancer.GetHealth() < Geomancer.GetStartingHealth()) {

            // Add health to necromancer
            Geomancer.AddHealth(DeviceManager._pInstance._HealthAddAmount);

            // Destroy tag
            Destroy(gameObject);
        }
    }

    public void SpeedBoost(Char_Geomancer Geomancer) {

        // Determine if whether the tag can be picked up or not.
        // Check if character is already using a speed boost
        if (Geomancer.IsSpeedBoost() != true) {

            // Activate speed boost
            Geomancer.ActivateSpeedBoost(DeviceManager._pInstance._SpeedBoostModifier, DeviceManager._pInstance._SpeedBoostTime);

            // Destroy tag
            Destroy(gameObject);
        }
    }

}