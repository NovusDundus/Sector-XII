using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    [Header("---------------------------------------------------------------------------")]
    [Header(" *** GEOMANCER CHARACTER ***")]
    [Header("- Respawning")]
    [Tooltip("The maximum amount of respawns associated to each player.")]
    public int _Respawns = 3;                                       
    [Tooltip("The amount of time between a player's death and their respawn.")]
    public int _RespawnTime = 3;                                    
    [Tooltip("List of references to the North, South, East & West player respawning triggers.")]
    public List<Collider> _RespawnTriggers;                         
    [Header("- Health")]
    [Tooltip("Starting health of the necromancers that are possessed by the players.")]
    public int _GeomancerStartingHealth = 100;                      
    [Tooltip("The particle effect that is played when the character dies.")]
    public GameObject _OnDeathEffect;                               
    [Tooltip("")]
    public Material _InvincibleMaterial;
    [Header("- Movement")]
    [Tooltip("Movement speed of the necromancer character.")]
    public float _GeomancerMovementSpeed = 10f;                     
    [Header(" *** ABILITIES ***")]
    [Header("- Dash")]
    public bool _DashEnabled = true;
    [Tooltip("Input button tied to the player's dash ability.")]
    public XboxCtrlrInput.XboxButton _DashButton = XboxCtrlrInput.XboxButton.B;  
    [Tooltip("")]
    public float _DashDistance = 5f;                                
    [Tooltip("Amount of time between reallowing the dash ability.")]
    public float DashCooldown = 5f;                                 
    [Header("- Knockback")]
    public bool _KnockbackEnabled = false;
    [Tooltip("Input button tied to the player's knockback ability.")]
    public XboxCtrlrInput.XboxButton _KnockbackButton = XboxCtrlrInput.XboxButton.Y;  
    [Tooltip("Amount of force applied to the target that is being knockbacked against.")]
    [Range(10000, 100000)]
    public int _KnockbackForceNormal = 100000;                      
    [Tooltip("Amount of force applied to the target that is being knockbacked against (After a dash was just performed).")]
    [Range(10000, 100000)]
    public int KnockbackForceDash = 100000;                         
    [Tooltip("Amount of time between reallowing the knockback ability.")]
    public float _KnockbackCooldown = 5f;                           
    [Header("- Weapons")]
    [Tooltip("The primary weapon associated with a player on startup.")]
    public WeaponList _PrimaryWeapon = WeaponList.Orb;
    [Tooltip("Sets whether the player will have a secondary weapon attached on startup.")]
    public bool _SecondaryWeaponEnabled = false;
    [Tooltip("Input button tied to the player's weapon swap ability.")]
    public XboxCtrlrInput.XboxButton _WeaponSwapButton = XboxCtrlrInput.XboxButton.LeftBumper;  

    /// Public (Internal)
    [HideInInspector]
    public static PlayerManager _pInstance;                         
   
    /// Private
    private List<Character> _POOL_ACTIVE_NECROMANCERS;              
    private List<Character> _POOL_ELIMINATED_NECROMANCERS;          
    private List<Character> _AllPlayers;                              

    public enum WeaponList {

        Orb,
        Flamethrower
    }

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // if the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;

        // Create object pools
        _POOL_ACTIVE_NECROMANCERS = new List<Character>();
        _POOL_ELIMINATED_NECROMANCERS = new List<Character>();
        _AllPlayers = new List<Character>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update() {

        // If match is in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Add 1 second of alive time to all alive players
            foreach (Character player in _POOL_ACTIVE_NECROMANCERS) {

                if (player.GetHealth() > 0) {

                    player._Player.AddTimeAlive(Time.deltaTime);
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** OBJECT POOLS ***

    public List<Character> GetActiveNecromancers() { return _POOL_ACTIVE_NECROMANCERS; }

    public List<Character> GetEliminatedNecromancers() { return _POOL_ELIMINATED_NECROMANCERS; }

    public List<Character> GetAllPlayers() { return _AllPlayers; }

}