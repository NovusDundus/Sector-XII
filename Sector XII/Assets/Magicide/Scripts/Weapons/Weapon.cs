using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    // Protected
    protected Character _Owner;                                     // Reference to the current owner assosiated with the weapon.
    protected float _FiringRate = 1;                                // The amount of time between shots.
    protected float _FiringDelay = 0;                               // Current amount of time left till it can fire a projectile.
    protected bool _CanFire = true;                                 // Returns TRUE if the weapon is allowed to successfully fire a projectile.
    protected bool _HeatedWeapon = false;                           // Can the weapon overheat from firing too much?
    protected bool _Overheated = false;                             // Returns TRUE if the weapon is currently in an overheated state.
    protected bool _CoolingDown;                                    // Returns TRUE if the weapon is currently venting cooldown. (Cannot fire during cooldown phase)
    protected float _CurrentHeat = 0f;                              // The current percentage of heat that the weapon has.
    protected float _FiringHeatCost;                                // The heat cost of each successful firing of the weapon.
    protected float _CooldownRateStable;                            // The amount of heat that is removed each frame when the weapon is in a 'stable' state.
    protected float _CooldownRateOverheated;                        // The amount of heat that is removed each frame when the weapon is in an 'overheated' state.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS *** 

    public virtual void Start() {

        // If the weapon has overheating functionality
        if (_HeatedWeapon == true) {

            // Precausion so that when the weapon overheats, it will cooldown
            if (_CooldownRateOverheated == 0f) {

                _CooldownRateOverheated = 0.1f;
            }
        }

        //  Precausion so that the firing rate isnt instant
        if (_FiringRate == 0f) {

            _FiringRate = 0.1f;
        }
    }

    public virtual void Init() {

    }

    public void SetOwner(Character a_Owner) {

        // Set the new owner for this weapon
        _Owner = a_Owner;
    }

    //--------------------------------------------------------------
    // *** FRAME *** 

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

        // Incomplete firing delay sequence
        if (_FiringDelay > 0f) {

            // Deduct from the firing delay timer
            _FiringDelay -= Time.deltaTime;

            // Clamp delay to 0.0
            if (_FiringDelay < 0f) {

                _FiringDelay = 0f;
            }
        }

        if (_HeatedWeapon == true) {

            // Hasnt overheated yet
            if (_CoolingDown == false) {

                // Weapon HAS hit max heat capacity
                if (_CurrentHeat >= 1f) {

                    // Weapon is now overheated and must completely cooldown before it can be fired again
                    _Overheated = true;
                    _CoolingDown = true;

                    // Clamp max heat to 1
                    _CurrentHeat = 1;
                }

                // Weapon has NOT hit max heat capacity
                else { /* _CurrentHeat < 1f */

                    // Clamp minimum heat to 0
                    if (_CurrentHeat > 0f) {

                        // Deduct stable cooldown amount
                        _CurrentHeat -= _CooldownRateStable;

                        // Clamp heat to 0
                        if (_CurrentHeat < 0f) {

                            _CurrentHeat = 0f;
                        }
                    }
                }
            }

            // Weapon has overheated & it now locked until completely cooled down
            else { /// _CoolingDown == true

                // Weapon has not finished cooling down
                if (_CurrentHeat > 0f) {

                    // Deduct overheated cooldown amount
                    _CurrentHeat -= _CooldownRateOverheated;

                    // Clamp heat to 0
                    if (_CurrentHeat < 0f) {

                        _CurrentHeat = 0f;
                    }
                }
                
                // Weapon has finished cooling down
                else { /*/ _CurrentHeat <= 0f   */

                    // Cooldown phase complete
                    _CoolingDown = false;
                    _Overheated = false;
                }
            }

            // enable OR disable firing sequence (based on firing delay and if the weapon currently ISNT overheated)
            _CanFire = _FiringDelay <= 0f && !_CoolingDown && !_Overheated;
        }

        else { ///_HeatedWeapon == false

            // enable OR disable firing sequence based on firing delay
            _CanFire = _FiringDelay <= 0f;
        }
    }

    // -------------------------------------------------------------
    // *** FIRING ***

    public virtual void Fire() {

        // Reset firing delay (only executes after a successful firing sequence)
        _FiringDelay = _FiringRate;
    }

    public float GetFireDelay() {

        return _FiringDelay;
    }

    //--------------------------------------------------------------
    // *** HEAT ***

    public float GetCurrentHeat() {

        return _CurrentHeat;
    }

    public bool GetOverheatedStatus() {

        return _Overheated;
    }
}