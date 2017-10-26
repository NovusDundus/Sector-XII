using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTag : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: FIRSTNAME LASTNAME
    /// Created on: DAY.MONTH.YEAR
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Public (internal)
    public enum PickupType {

        AddToShield,
        Healthpack,
        SpeedBoost,
        Dash
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

                    GetComponent<MeshRenderer>().material = DeviceManager._pInstance._HealthpackTypeMaterial;
                    _RotationSpeed = DeviceManager._pInstance._HealthpackRotationSpeed;
                    _BobHeight = DeviceManager._pInstance._HealthpackBobHeight;
                    _BobSpeed = DeviceManager._pInstance._HealthpackBobSpeed;
                    break;
                }          

            case PickupType.Dash: { 

                    GetComponent<MeshRenderer>().material = DeviceManager._pInstance._HealthpackTypeMaterial;
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

    }

    public void FixedUpdate() {

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

                    // Respawn ai (if possible)
                    AiManager._pInstance.OnRespawn();
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
            
            // DASH 1 TIME USE
            case PickupType.Dash: {

                    Dash(Necromancer);
                    break;
                }

            default: {

                    break;
                }
        }

        // hide tag & move out of playable space
        GetComponentInChildren<Renderer>().enabled = false;
        transform.position = new Vector3(1000, 0, 1000);

        // Destroy tag
        Destroy(gameObject);        
    }

    public void AddToShield(Char_Geomancer Necromancer) {

    // Determine if whether the necromancer can be picked up or not
    // Get minion count
    int minionCount = Necromancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMinionCount();

    // Check against max size
    int MaxSize = Necromancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMaxMinions();
        if (minionCount < MaxSize) {

            // Add to meat shield
            Necromancer.GetSpecialWeapon().GetComponent<Wep_Shield>().AddMinion(_Crystal);
        }
    }

    public void Healthpack(Char_Geomancer Necromancer) {

        // Determine if whether the tag can be picked up or not

    }

    public void SpeedBoost(Char_Geomancer Necromancer) {

        // Determine if whether the tag can be picked up or not

    }

    public void Dash(Char_Geomancer Necromancer) {

        // Determine if whether the tag can be picked up or not

    }
}