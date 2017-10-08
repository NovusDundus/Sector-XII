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

    /// Public (designers)
    public float _RotationSpeed = 2f;
    public float _BobHeight = 1f;
    public float _BobSpeed = 1f;

    /// Private
    private bool _Active = false;
    private Collider _Collision;
    private Char_Wyrm Wyrm;
    private float _Min;
    private float _Max;
    private bool _MovingUp = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Get reference to the collision
        _Collision = GetComponent<Collider>();

        // Set min & max bob positions
        _Min = transform.position.y;
        _Max = _Min + _BobHeight;
    }

    public void Init(Char_Wyrm Char) {

        // Set reference to the wyrm to be used in the meat shield
        Wyrm = Char;
        
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
                ///transform.Translate(new Vector3(transform.position.x, transform.position.y + _BobSpeed * Time.deltaTime, transform.position.z));
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
                ///transform.Translate(new Vector3(transform.position.x, transform.position.y - _BobSpeed * Time.deltaTime, transform.position.z));
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
                    OnPickup(necromancer.GetComponent<Char_Necromancer>());
                    break;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** PICKUP ***

    public void OnPickup(Char_Necromancer Necromancer) {

        // Determine if whether the necromancer can be picked up or not
        // Get minion count
        int minionCount =  Necromancer.GetSecondaryWeapon().GetComponent<Wep_Shield>().GetMinionCount();

        // Check against max size
        int MaxSize = Necromancer.GetSecondaryWeapon().GetComponent<Wep_Shield>().GetMaxMinions();
        if (minionCount < MaxSize) {

            // Add to meat shield
            Necromancer.GetSecondaryWeapon().GetComponent<Wep_Shield>().AddMinion(Wyrm);

            // Destroy tag
            Destroy(gameObject);
        }
    }

}