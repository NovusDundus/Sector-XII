using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Orb : Weapon {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // VARIABLES

    /// Public (designers)
    [Tooltip("Empty gameObject placed where the left muzzle launch point will be")]
    public Transform _MuzzlePointLeft;
    [Tooltip("Empty gameObject placed where the right muzzle launch point will be")]
    public Transform _MuzzlePointRight;
    [Tooltip("gameobject that contains the Fireball script")]
    public GameObject _ProjectilePrefab;

    /// Public (internal)
    [HideInInspector]
    public int _ActiveProjectiles = 0;

    /// Private
    private int _POOL_SIZE = 40;                                 // Instance amount required for the object pool to function.
    List<GameObject> _POOL_FIREBALL_INACTIVE;                    // Object pool of all inactive projectiles.
    List<GameObject> _POOL_FIREBALL_ACTIVE;                      // Object pool of all active projectiles in the world.
    private bool _FiredFromLeftMuzzle = false;


    //--------------------------------------------------------------
    // CONSTRUCTORS

    public override void Start() {

        // Set firing rate
        _FiringRate = WeaponManager._pInstance._OrbFireDelay;

        // Set overheating properties
        _HeatedWeapon = true;
        _FiringHeatCost = WeaponManager._pInstance._FireballHeatCost;
        _CooldownRateStable = WeaponManager._pInstance._OrbCooldownRateStable;
        _CooldownRateOverheated = WeaponManager._pInstance._OrbCooldownRateOverheated;

        // Create object pools
        _POOL_FIREBALL_INACTIVE = new List<GameObject>();
        _POOL_FIREBALL_ACTIVE = new List<GameObject>();
    }

    public override void Init() {

        // create inactive projectile pool by the defined size
   //     for (int i = 0; i < _POOL_SIZE; ++i) {
   //
   //         // If the designers do their job
   //         if (_ProjectilePrefab != null) {
   //
   //             // instantiate projectile
   //             var proj = Instantiate(_ProjectilePrefab.GetComponent<Proj_Fireball>(), new Vector3(), Quaternion.identity);
   //             proj.GetComponent<Proj_Fireball>().Start();
   //             proj.GetComponent<Projectile>().SetOwner(this);
   //             
   //             // Add projectile to the end of the inactive object pool
   //             _POOL_FIREBALL_INACTIVE.Add(proj.gameObject);
   //        }
   //    }
    }

    //--------------------------------------------------------------
    // FRAME

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Update the position and rotation to match the owning character's transform
        if (_Owner != null) {

            transform.position = new Vector3(_Owner.transform.position.x, transform.position.y, _Owner.transform.position.z);
            transform.rotation = _Owner.transform.rotation;
        }

        // Check if fire delay allows the firing sequence to be initiated
        // Apply cooldown to weapon heat based on overheat status
        base.FixedUpdate();

        // Always hide inactive projectiles
        if (_POOL_FIREBALL_INACTIVE != null) {

            foreach (var projectile in _POOL_FIREBALL_INACTIVE) {

                projectile.GetComponent<Renderer>().enabled = false;
            }
        }

        // Always render active projectiles
        if (_POOL_FIREBALL_ACTIVE != null) {

            foreach (var projectile in _POOL_FIREBALL_ACTIVE) {

                projectile.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    // -------------------------------------------------------------
    // FIRING

    public override void Fire() {
        
        // If NOT overheated and firing delay is completed
        if (_CanFire == true) {

            // Last shot came from left side
            if (_FiredFromLeftMuzzle == true) {

                // If the designers did their job
                if (_MuzzlePointRight != null) {

                    // If the designers did their job
                    if (_ProjectilePrefab != null) {

                        // Create projectile on RIGHT
                        var proj = Instantiate(_ProjectilePrefab, _MuzzlePointLeft.position, transform.rotation);
                        proj.GetComponent<Projectile>().Init();
                        proj.GetComponent<Projectile>().SetOwner(this);
                        _ActiveProjectiles += 1;

                        // Set last muzzle used to RIGHT
                        _FiredFromLeftMuzzle = false;
                    }
                }
            }

            // Last shot came from right side
            else { ///_FiredFromLeftMuzzle == false

                // If the designers did their job
                if (_ProjectilePrefab != null) {

                    // Create projectile on LEFT
                    var proj = Instantiate(_ProjectilePrefab, _MuzzlePointRight.position, transform.rotation);
                    proj.GetComponent<Projectile>().Init();
                    proj.GetComponent<Projectile>().SetOwner(this);
                    _ActiveProjectiles += 1;

                    // Set last muzzle used to LEFT
                    _FiredFromLeftMuzzle = true;
                }
            }
       
            // Add heat from firing
            _CurrentHeat += _FiringHeatCost;

            // Reset firing delay
            base.Fire();
        }
    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public Proj_Fireball GetInactiveProjectile() {

        if (_POOL_FIREBALL_INACTIVE.Count > 0) {

            // Get the projectile from the end of the list
            ///Projectile proj = _POOL_INACTIVE.RemoveLast();

            return null;
        }

        // Empty pool >> returns NULL
        else {

            return null;
        }
    }

    public Proj_Fireball GetActiveProjectile() {

        return null;
    }

    public void SetInactive(Proj_Fireball proj) {

    }

    public void SetActive(Proj_Fireball proj) {

    }

    public int GetPoolSize() {

        return _POOL_SIZE;
    }

    public int GetPoolInactiveCount() {

        return GetPoolSize() - _ActiveProjectiles;
        ///return _POOL_FIREBALL_INACTIVE.Count;
    }

    public int GetPoolActiveCount() {

        return _ActiveProjectiles;
        ///return _POOL_FIREBALL_ACTIVE.Count;
    }

}