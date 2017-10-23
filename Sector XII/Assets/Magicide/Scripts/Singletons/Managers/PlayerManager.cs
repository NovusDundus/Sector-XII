using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (Designers)
    [Header(" *** GEOMANCER CHARACTER ***")]
    [Header("- Health")]
    [Tooltip("Starting health of the necromancers that are possessed by the players.")]
    public int _NecromancerStartingHealth = 100;                    // Starting health of the necromancers that are possessed by the players.
    [Header("- Movement")]
    [Tooltip("Movement speed of the necromancer character.")]
    public float _NecromancerMovementSpeed = 10f;                   // Movement speed of the necromancer character.
    [Header("- Dash")]
    public bool DashEnabled = true;
    [Tooltip("Input button tied to the player's dash ability.")]
    public XboxCtrlrInput.XboxButton DashButton = XboxCtrlrInput.XboxButton.B;  // Input button tied to the player's dash ability.
    [Tooltip("")]
    public float DashDistance = 5f;                                 // Units of distance that the player's character will teleport when performed.
    [Tooltip("Amount of time between reallowing the dash ability.")]
    public int DashCooldown = 5;                                    // Amount of time between reallowing the dash ability.
    [Header("- Knockback")]
    public bool KnockbackEnabled = false;
    [Tooltip("Input button tied to the player's knockback ability.")]
    public XboxCtrlrInput.XboxButton KnockbackButton = XboxCtrlrInput.XboxButton.Y;  // Input button tied to the player's knockback ability.
    [Tooltip("Amount of force applied to the target that is being knockbacked against.")]
    [Range(10000, 100000)]
    public int KnockbackForceNormal = 100000;                       // Amount of force applied to the target that is being knockbacked against.
    [Tooltip("Amount of force applied to the target that is being knockbacked against (After a dash was just performed).")]
    [Range(10000, 100000)]
    public int KnockbackForceDash = 100000;                         // Amount of force applied to the target that is being knockbacked against (After a dash was just performed).
    [Tooltip("Amount of time between reallowing the knockback ability.")]
    public int KnockbackCooldown = 5;                               // Amount of time between reallowing the knockback ability.

    /// Public (internal)
    [HideInInspector]
    public static PlayerManager _pInstance;                         // This is a singleton script, Initialized in Startup().
   
    /// Private
    private List<Character> _POOL_ALIVE_NECROMANCERS;               // Object pool of all ALIVE Player necromancers.
    private List<Character> _POOL_DEAD_NECROMANCERS;                // Object pool of all DEAD Player necromancers.
    private List<Character> _AllPlayers;

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // if the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;

        // Create object pools
        _POOL_ALIVE_NECROMANCERS = new List<Character>();
        _POOL_DEAD_NECROMANCERS = new List<Character>();
        _AllPlayers = new List<Character>();
    }

    //--------------------------------------------------------------
    // FRAME
    
    public void FixedUpdate() {

        // If match is in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Add 1 second of alive time to all alive players
            foreach (var player in _POOL_ALIVE_NECROMANCERS) {

                player._Player.AddTimeAlive(Time.deltaTime);
            }
        }
    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public List<Character> GetAliveNecromancers() {

        return _POOL_ALIVE_NECROMANCERS;
    }

    public List<Character> GetDeadNecromancers() {

        return _POOL_DEAD_NECROMANCERS;
    }

    public List<Character> GetAllPlayers() {

        return _AllPlayers;
    }
}
