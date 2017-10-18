using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 10.10.2017  
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public bool _Destroyable = true;
    public int _Health = 100;
    public DamagedState _DamagedState = DamagedState.New;
    public GameObject NewMesh;
    public GameObject DamagedMesh;
    public GameObject DeastroyedMesh;

    public enum DamagedState {

        New,
        Damaged,
        Destroyed
    }

    /// Private
    private Collider _Collision;
    private bool _CanBeDamaged = false;
    private int _StartingHealth;
    private int _DamagedHealth;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start () {

        // Get reference to collision
        _Collision = GameObject.FindGameObjectWithTag("Collision").GetComponent<Collider>();

        // Set if destroyable
        _CanBeDamaged = !_Destroyable;
        // Set starting health
        _StartingHealth = _Health;
        // Set damage threshold
        _DamagedHealth = _StartingHealth / 2;

        // Add to object pool
        if (_Destroyable == true) {

            // Static object pool
            LevelManager._pInstance.GetStaticObjects().Add(this.gameObject);
        }

        else { /// _Destroyable == false

            // Dynamic object pool
            LevelManager._pInstance.GetDynamicObjects().Add(this.gameObject);
        }
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update () {

    }

    public void FixedUpdate() {
        
        // If the object is dynamic
        if (_CanBeDamaged == true) {

            switch (_DamagedState) {

                case DamagedState.New: { 

                    break;
                }

                case DamagedState.Damaged: { 

                    break;
                }

                case DamagedState.Destroyed: { 

                    break;
                }

                default: { 
                    break;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** HEALTH ***

    public Collider GetCollision() {

        return _Collision;
    }

    public void Damage(int damage) {

        // Only damage the object if its dynamic
        if (_CanBeDamaged == true) {

            // Apply damage to health
            _Health -= damage;

            // Check if damaged
            if (_Health <= _DamagedHealth && _Health > 0) {

                // Set object to damaged
                _DamagedState = DamagedState.Damaged;
            }

            // Check if dead
            if (_Health <= 0) {

                // Clamp to 0
                _Health = 0;

                // Set object to destroyed
                _DamagedState = DamagedState.Destroyed;
            }
        }
    }
}
