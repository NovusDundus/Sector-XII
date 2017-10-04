using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (Designers)
    [Header(" *** ORB WEAPON ***")]
    [Header("- Firing Delay")]
    [Tooltip("Minimum time in seconds allowed between each projectile that is fired.")]
    ///[Range(0f, 3f)]
    public float _OrbFireDelay = 0.6f;
    [Header("- Heat")]
    [Tooltip("Percentage of heat generated for each projectile fired.")]
    ///[Range(0f,1f)]
    public float _FireballHeatCost = 0.1f;
    [Tooltip("Percentage of heat lost each second when the weapon is still in a stable state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    ///[Range(0f, 0.1f)]
    public float _OrbCooldownRateStable = 0.01f;
    [Tooltip("Percentage of heat lost each second when the weapon is in an overheated state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    ///[Range(0f, 0.1f)]
    public float _OrbCooldownRateOverheated = 0.005f;
    [Header("- Damage")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    ///[Range(0f, 200f)]
    public int _ImpactDamage = 100;
    [Header("- Movement")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    ///[Range(0f, 200f)]
    public float _FireballSpeed = 20f;
    [Tooltip("Maximum distance a fireball can travel (will detonate when range is hit).")]
    ///[Range(0f, 200f)]
    public float _FireballRange = 50f;
    [Header("")]
    [Header(" *** MEAT SHIELD ***")]
    [Header("- Size")]
    [Tooltip("Maximum amount of minions allowed to compose the meat shield.")]
    ///[Range(10, 18)]
    public int _MaxSize = 14;

    /// Public (internal)
    [HideInInspector]
    public static WeaponManager _pInstance;                         // This is a singleton script, Initialized in Startup().

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }
}
