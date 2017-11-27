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
        SpeedBoost,
        Invincibility,
        Random
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

        // Check if random
        if (type == PickupType.Random && DeviceManager._pInstance.GetRandomKilltagList().Count >= 1) {

            // Generate a random pickup type
            int i;
            ///i = Random.Range(0, System.Enum.GetValues(typeof(PickupType)).Length - 1);
            i = Random.Range(0, DeviceManager._pInstance.GetRandomKilltagList().Count);
            _Type = DeviceManager._pInstance.GetRandomKilltagList()[i];
        }

        else if (type == PickupType.Random && DeviceManager._pInstance.GetRandomKilltagList().Count < 1) {

            // Default to the first in the enum list
            _Type = (PickupType)0;
        }

        // Set OnPickupType
        switch (_Type) {

            // SHIELD MINION PICKUP
            case PickupType.AddToShield: {

                // Set movement speed and apply material
                GetComponent<MeshRenderer>().material = DeviceManager._pInstance._AddShieldTypeMaterial;
                _RotationSpeed = DeviceManager._pInstance._AddShieldRotationSpeed;
                _BobHeight = DeviceManager._pInstance._AddShieldBobHeight;
                _BobSpeed = DeviceManager._pInstance._AddShieldBobSpeed;
                break;                                     
            }                                              

            // SPEED BOOST
            case PickupType.SpeedBoost: {

                // Set movement speed and apply material
                GetComponent<MeshRenderer>().material = DeviceManager._pInstance._SpeedBoostTypeMaterial;
                _RotationSpeed = DeviceManager._pInstance._SpeedBoostRotationSpeed;
                _BobHeight = DeviceManager._pInstance._SpeedBoostBobHeight;
                _BobSpeed = DeviceManager._pInstance._SpeedBoostBobSpeed;
                break;
            }          

            // HEALTHPACK
            case PickupType.Healthpack: {

                // Set movement speed and apply material
                GetComponent<MeshRenderer>().material = DeviceManager._pInstance._HealthpackTypeMaterial;
                _RotationSpeed = DeviceManager._pInstance._HealthpackRotationSpeed;
                _BobHeight = DeviceManager._pInstance._HealthpackBobHeight;
                _BobSpeed = DeviceManager._pInstance._HealthpackBobSpeed;
                break;
            }

            // INVINCIBILITY
            case PickupType.Invincibility: {

                // Set movement speed and apply material
                GetComponent<MeshRenderer>().material = DeviceManager._pInstance._InvincibilityTypeMaterial;
                _RotationSpeed = DeviceManager._pInstance._InvincibilityRotationSpeed;
                _BobHeight = DeviceManager._pInstance._InvincibilityRotationSpeed;
                _BobSpeed = DeviceManager._pInstance._InvincibilityBobSpeed;
                break;
            }

            default: {

                    break;
            }
        }

        // Set reference to the crystal to be used in the meat shield
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
            foreach (var necromancer in PlayerManager._pInstance.GetActiveNecromancers()) {

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

    public void OnPickup(Char_Geomancer necromancer) {

        switch (_Type) {

            // MINION SHIELD
            case PickupType.AddToShield: {

                AddToShield(necromancer);
                break;
            }

            // HEALTHPACK
            case PickupType.Healthpack: {

                Healthpack(necromancer);
                break;
            }
            
            // SPEED BOOST
            case PickupType.SpeedBoost: {

                SpeedBoost(necromancer);
                break;
            }

            // INVINCIBILITY
            case PickupType.Invincibility: {

                Invincibility(necromancer);
                break;
            }

            default: {

                    break;
            }
        }
    }

    public void AddToShield(Char_Geomancer geomancer) {

    // Determine if whether the necromancer can be picked up or not
    // Get minion count
    int minionCount = geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMinionCount();

    // Check against max size
    float MaxSize = geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMaxMinions() * 0.65f;
        if (minionCount < MaxSize) {

            // Add to meat shield
            geomancer.GetSpecialWeapon().GetComponent<Wep_Shield>().AddMinion(_Crystal);

            // Destroy tag
            Destroy(gameObject);
        }
    }

    public void Healthpack(Char_Geomancer geomancer) {

        // Determine if whether the tag can be picked up or not
        // Check if there's any missing health
        if (geomancer.GetHealth() < geomancer.GetStartingHealth()) {

            // Add health to necromancer
            geomancer.AddHealth(DeviceManager._pInstance._HealthAddAmount);
        }

        // Destroy tag
        Destroy(gameObject);
    }

    public void SpeedBoost(Char_Geomancer geomancer) {

        // Determine if whether the tag can be picked up or not.
        // Check if character is already using a speed boost
        ///if (geomancer.IsSpeedBoost() != true) {

            // Activate speed boost
            geomancer.ActivateSpeedBoost(DeviceManager._pInstance._SpeedBoostModifier, DeviceManager._pInstance._SpeedBoostTime);

            // Destroy tag
            Destroy(gameObject);
        ///}
    }

    public void Invincibility(Char_Geomancer geomancer) {
        
        // Activate invincibility (or reset the timer if already invincible)
        geomancer.ActivateInvincibility();

        // Destroy tag
        Destroy(gameObject);        
    }

}