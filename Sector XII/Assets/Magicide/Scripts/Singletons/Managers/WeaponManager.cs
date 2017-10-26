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
    [Header("---------------------------------------------------------------------------")]
    [Header(" *** ORB FIREBALL ***")]
    [Header("- Firing Delay")]
    [Tooltip("Minimum time in seconds allowed between each projectile that is fired.")]
    [Range(0f, 1f)]
    public float _OrbFireDelay = 0.6f;
    [Header("- Heat")]
    [Tooltip("Percentage of heat generated for each projectile fired.")]
    public float _FireballHeatCost = 0.1f;
    [Tooltip("Percentage of heat lost each second when the weapon is still in a stable state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    public float _OrbCooldownRateStable = 0.01f;
    [Tooltip("Percentage of heat lost each second when the weapon is in an overheated state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    public float _OrbCooldownRateOverheated = 0.005f;
    [Header("- Damage")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    public int _FireballImpactDamage = 100;
    [Header("- Movement")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    public float _FireballSpeed = 20f;
    [Tooltip("Maximum distance a fireball can travel (will detonate when range is hit).")]
    public float _FireballRange = 50f;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** FLAMETHROWER ***")]
    [Header("- Firing Delay")]
    [Tooltip("Minimum time in seconds allowed between each projectile that is fired.")]
    [Range(0f, 1f)]
    public float _FlameFireDelay = 0.05f;
    [Header("- Heat")]
    [Tooltip("Percentage of heat generated for each projectile fired.")]
    public float _FlameHeatCost = 0.05f;
    [Tooltip("Percentage of heat lost each second when the weapon is still in a stable state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    public float _FlameCooldownRateStable = 0.01f;
    [Tooltip("Percentage of heat lost each second when the weapon is in an overheated state. (MAKE SURE THIS IS LESS THAN THE COST)")]
    public float _FlameCooldownRateOverheated = 0.01f;
    [Header("- Damage")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    public int _FlameDamage = 10;
    [Header("- Movement")]
    [Tooltip("Amount of impact damage applied to the object when collided with a fireball.")]
    public float _FlameSpeed = 10f;
    [Tooltip("Maximum distance a fireball can travel (will detonate when range is hit).")]
    public float _FlameRange = 20f;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** SHIELD MINIONS ***")]
    [Header("- Health")]
    public int _MinionHealth = 100;
    [Header("- Size")]
    [Tooltip("Maximum amount of minions allowed to compose the meat shield.")]
    public int _MaxSize = 14;
    [Tooltip("The amount of spacing that spans out the corpses from the center.")]
    [Range(0f, 4f)]
    public float _MinionSpacing = 1.5f;
    [Header("- Movement")]
    [Tooltip("The rate of rotation that the each individual minion will rotate locally within the shield.")]
    public float _MinionSpinSpeed = 4f;
    [Tooltip("The units of measurement that each individual minion will bob up & down.")]
    public float _MinionBobHeight = 0.5f;
    [Tooltip("The rate of speed that each individual minion will bob up & down.")]
    public float _MinionBobSpeed = 0.5f;
    [Header("- Shield Rotation")]
    [Tooltip("Whether the shield automatically rotates on its own")]
    public bool _AutoRotateShield = false;
    [Tooltip("Sets whether the shield will rotate either clockwise or counter clockwise (AUTO ROTATE NEEDS TO BE ENABLED)")]
    public bool _ShieldOrbitClockwise = true;
    [Tooltip("The speed of rotation it takes for the 'Meat' to orbit the character")]
    public float _ShieldOrbitSpeed = 3f;

    /// Public (internal)
    [HideInInspector]
    public static WeaponManager _pInstance;                         // This is a singleton script, Initialized in Startup().

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

}