using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Flamethrower : Weapon {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 25.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    [Tooltip("Empty gameObject placed where the left muzzle launch point will be")]
    public Transform _MuzzlePointLeft;
    [Tooltip("Empty gameObject placed where the right muzzle launch point will be")]
    public Transform _MuzzlePointRight;
    [Tooltip("gameobject that contains the Flamethrower projectile script")]
    public GameObject _ProjectilePrefab;

    /// Public (Internal)
    [HideInInspector]
    public int _ActiveProjectiles = 0;

    /// Private
    private int _POOL_SIZE = 40;                                    // Instance amount required for the object pool to function.
    List<GameObject> _POOL_FIREBALL_INACTIVE;                       // Object pool of all inactive projectiles.
    List<GameObject> _POOL_FIREBALL_ACTIVE;                         // Object pool of all active projectiles in the world.
    private bool _FiredFromLeftMuzzle = false;                      // Returns TRUE if the last projectile was fired from the LEFT muzzle launch point.
    private bool _Firing;
    private ParticleSystem _FiringEffect;
    private bool _DrainFuel = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Precausions
        base.Start();

        // Set firing rate
        _FiringRate = WeaponManager._pInstance._FlameFireDelay;

        // Set overheating properties
        _HeatedWeapon = true;
        _FiringHeatCost = WeaponManager._pInstance._FlameHeatCost;
        _CooldownRateStable = WeaponManager._pInstance._FlameCooldownRateStable;
        _CooldownRateOverheated = WeaponManager._pInstance._FlameCooldownRateOverheated;

        // Set firing effect
        _FiringEffect = Instantiate(WeaponManager._pInstance._FlamethrowerEffect, transform.position, transform.rotation);
    }

    public override void Init() {

        // Create object pool for projectiles
        _POOL_FIREBALL_INACTIVE = new List<GameObject>();
        _POOL_FIREBALL_ACTIVE = new List<GameObject>();

        // create inactive projectile pool by the defined size
        for (int i = 0; i < _POOL_SIZE; i++) {

            // If the designers do their job
            if (_ProjectilePrefab != null) {

                // instantiate projectile
                var proj = Instantiate(_ProjectilePrefab, new Vector3(), Quaternion.identity);
                proj.GetComponent<Proj_Flame>().Start();
                proj.GetComponent<Projectile>().SetOwner(this);

                // Add projectile to the end of the inactive object pool
                _POOL_FIREBALL_INACTIVE.Add(proj);
            }
        }
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

        // Update the position and rotation to match the owning character's transform
        if (_Owner != null) {

            transform.position = new Vector3(_Owner.transform.position.x, transform.position.y, _Owner.transform.position.z);
            transform.rotation = _Owner.transform.rotation;
        }

        // Check if fire delay allows the firing sequence to be initiated
        // Apply cooldown to weapon heat based on overheat status
        base.Update();

        // Always hide inactive projectiles
        if (_POOL_FIREBALL_INACTIVE != null && _POOL_FIREBALL_INACTIVE.Count > 0) {

            foreach (var projectile in _POOL_FIREBALL_INACTIVE) {

                projectile.GetComponent<Renderer>().enabled = false;
            }
        }

        // Always render active projectiles
        if (_POOL_FIREBALL_ACTIVE != null && _POOL_FIREBALL_ACTIVE.Count > 0) {

            foreach (var projectile in _POOL_FIREBALL_ACTIVE) {

                projectile.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }

        // Play the flamethrower particle effect when firing
        if (_FiringEffect != null) {

            // Set the flamethrower particle system to match the transform of the owner that is firing it
            _FiringEffect.transform.position = new Vector3(GetOwner().transform.position.x, GetOwner().transform.position.y + 2f, GetOwner().transform.position.z);
            _FiringEffect.transform.position = _FiringEffect.transform.position + _FiringEffect.transform.forward * 1;
            _FiringEffect.transform.rotation = GetOwner().transform.rotation;
        }

        // Continuously drain fuel from the weapon once it is fired
        if (_DrainFuel == true) {

            // Keep shooting
            if (_CurrentHeat < 1f) {

                Fire();
            }
        }

        // Stop trying to fire the weapon now that it is overheated
        if (_Overheated == true) {

            _DrainFuel = false;
        }
    }

    // -------------------------------------------------------------
    // *** FIRING ***

    public override void Fire() {

        // If NOT overheated and firing delay is completed
        if (_CanFire == true) {

            // Last shot came from left side
            if (_FiredFromLeftMuzzle == true) {

                // If the designers did their job
                if (_MuzzlePointRight != null) {

                    // If the designers did their job
                    if (_ProjectilePrefab != null) {

                        // Get fireball projectile and move it through the pools
                        GameObject proj = GetInactiveProjectile();

                        if (proj != null) {

                            _POOL_FIREBALL_ACTIVE.Add(proj);
                            _POOL_FIREBALL_INACTIVE.RemoveAt(_POOL_FIREBALL_INACTIVE.Count - 1);

                            // Initialize the flame
                            proj.GetComponent<Projectile>().Init();
                            proj.GetComponent<Projectile>().SetOwner(this);
                            proj.transform.position = _MuzzlePointLeft.position;
                            proj.transform.rotation = transform.rotation;

                            if (proj != null) {

                                _ActiveProjectiles += 1;

                                // Set last muzzle used to RIGHT
                                _FiredFromLeftMuzzle = false;
                            }
                        }
                    }
                }
            }

            // Last shot came from right side
            else { ///_FiredFromLeftMuzzle == false

                // If the designers did their job
                if (_ProjectilePrefab != null) {

                    // Get fireball projectile and move it through the pools
                    GameObject proj = GetInactiveProjectile();

                    if (proj != null) {

                        _POOL_FIREBALL_ACTIVE.Add(proj);
                        _POOL_FIREBALL_INACTIVE.RemoveAt(_POOL_FIREBALL_INACTIVE.Count - 1);

                        // Initialize the flame
                        proj.GetComponent<Projectile>().Init();
                        proj.GetComponent<Projectile>().SetOwner(this);
                        proj.transform.position = _MuzzlePointRight.position;
                        proj.transform.rotation = transform.rotation;

                        if (proj != null) {

                            _ActiveProjectiles += 1;

                            // Set last muzzle used to LEFT
                            _FiredFromLeftMuzzle = true;
                        }
                    }
                }
            }

            // Add heat from firing
            _CurrentHeat += _FiringHeatCost;

            // Reset firing delay
            base.Fire();
            _Firing = true;

            // Play flamethrower firing effect
            if (_FiringEffect != null) {
                
                if (_FiringEffect.isPlaying == false) {

                    _FiringEffect.Play();

                    // Set looping to true
                    ///ParticleSystem.MainModule mainModule = _FiringEffect.main;
                    ///mainModule.loop = true;
                }                
            }

            _DrainFuel = true;

            // Play firing sound
            SoundManager._pInstance.PlayFlamethrowerAttack();          
        }
    }
    
    public void SetFiring(bool value) {

        _Firing = value;
    }

    //--------------------------------------------------------------
    // *** OBJECT POOLS ***

    public GameObject GetInactiveProjectile() {

        if (_POOL_FIREBALL_INACTIVE.Count > 0) {

            // Get the projectile from the end of the list
            GameObject proj = _POOL_FIREBALL_INACTIVE[_POOL_FIREBALL_INACTIVE.Count - 1];

            return proj;
        }

        // Empty pool >> returns NULL
        else {

            return null;
        }
    }

    public GameObject GetActiveProjectile() {

        if (_POOL_FIREBALL_ACTIVE.Count > 0) {

            // Get the projectile from the end of the list
            GameObject proj = _POOL_FIREBALL_ACTIVE[_POOL_FIREBALL_ACTIVE.Count - 1];

            return proj;
        }

        // Empty pool >> returns NULL
        else {

            return null;
        }
    }

    public int GetPoolSize() { return _POOL_SIZE; }

    public int GetPoolInactiveCount() { return _POOL_FIREBALL_INACTIVE.Count; }

    public int GetPoolActiveCount() { return _POOL_FIREBALL_ACTIVE.Count; }

    public List<GameObject> GetActivePool() { return _POOL_FIREBALL_ACTIVE; }

    public List<GameObject> GetInactivePool() { return _POOL_FIREBALL_INACTIVE; }

}