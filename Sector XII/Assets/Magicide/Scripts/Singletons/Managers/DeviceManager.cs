using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 23.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    [Header("---------------------------------------------------------------------------")]
    [Header("*** Kill Tags ***")]
    [Header("- Add Shield Variant")]
    public Material _AddShieldTypeMaterial;
    public float _AddShieldRotationSpeed = 2f;
    public float _AddShieldBobHeight = 1f;
    public float _AddShieldBobSpeed = 1f;
   
    [Header("- Speed Boost Variant")]
    public Material _SpeedBoostTypeMaterial;
    public float _SpeedBoostRotationSpeed = 1f;
    public float _SpeedBoostBobHeight = 1f;
    public float _SpeedBoostBobSpeed = 0.5f;
    public float _SpeedBoostModifier = 2f;
    public float _SpeedBoostTime = 5f;

    [Header("- Health Pack Variant")]
    public Material _HealthpackTypeMaterial;
    public float _HealthpackRotationSpeed = 3f;
    public float _HealthpackBobHeight = 1f;
    public float _HealthpackBobSpeed = 2f;
    public int _HealthAddAmount = 50;

    [Header("- Invincibility Variant")]
    public Material _InvincibilityTypeMaterial;
    public float _InvincibilityRotationSpeed = 3f;
    public float _InvincibilityBobHeight = 1f;
    public float _InvincibilityBobSpeed = 2f;
    public float _InvincibilityTime = 5f;

    [Header("- Random Variant")]
    public bool _AddShield = false;
    public bool _SpeedBoost = false;
    public bool _Healthpack = false;
    public bool _Invincibility = false;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** Teleporters ***")]
    [Tooltip("Can the teleporters be used in phase 1?")]
    public bool _UsedInPhase1 = true;
    [Header("- Cooldown")]
    [Tooltip("Time in seconds that allows reusing of the teleporters once they has been activated.")]
    public int _TeleportCooldownTime = 10;
    
    [Header("---------------------------------------------------------------------------")]
    [Header("*** Glowing Floor ***")]
    public float _GlowTime = 1f;
    public Material _FloorMaterial;
    public Color _GlowFloorColourMin;
    public Color _GlowFloorColourMax;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** Face Tree ***")]
    public List<FaceTree> _FaceTrees;

    /// Public (internal)
    [HideInInspector]
    public static DeviceManager _pInstance;

    /// Private
    private List<KillTag.PickupType> _RandomKilltagPool;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    public void Start() {

        _RandomKilltagPool = new List<KillTag.PickupType>();

        // If minion shield variant is enabled - add it to the pool
        if (_AddShield == true) 
            _RandomKilltagPool.Add(KillTag.PickupType.AddToShield);

        // If speed boost variant is enabled - add it to the pool
        if (_SpeedBoost == true)
            _RandomKilltagPool.Add(KillTag.PickupType.SpeedBoost);

        // If health pack variant is enabled - add it to the pool
        if (_Healthpack == true)
            _RandomKilltagPool.Add(KillTag.PickupType.Healthpack);

        // If invincibility variant is enabled - add it to the pool
        if (_Invincibility == true)
            _RandomKilltagPool.Add(KillTag.PickupType.Invincibility);
    }

    //--------------------------------------------------------------
    // *** GETTERS ***

    public List<KillTag.PickupType> GetRandomKilltagList() { return _RandomKilltagPool; }

}