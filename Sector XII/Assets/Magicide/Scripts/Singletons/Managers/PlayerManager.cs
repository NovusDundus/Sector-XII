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
    [Header(" *** NECROMANCER CHARACTER ***")]
    [Header("- Health")]
    public int _NecromancerStartingHealth = 100;                    // Starting health of the necromancers that are possessed by the players.
    [Header("- Movement")]
    public float _NecromancerMovementSpeed = 10f;                   // Movement speed of the necromancer character.
    public float _NecromancerRotationSpeed = 2f;

    /// Public (internal)
    [HideInInspector]
    public static PlayerManager _pInstance;                         // This is a singleton script, Initialized in Startup().
   
    /// Private
    private List<Character> _POOL_ALIVE_NECROMANCERS;               // Object pool of all ALIVE Player necromancers.
    private List<Character> _POOL_DEAD_NECROMANCERS;                // Object pool of all DEAD Player necromancers.
    
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
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public List<Character> GetAliveNecromancers() {

        return _POOL_ALIVE_NECROMANCERS;
    }

    public List<Character> GetDeadNecromancers() {

        return _POOL_DEAD_NECROMANCERS;
    }
}
