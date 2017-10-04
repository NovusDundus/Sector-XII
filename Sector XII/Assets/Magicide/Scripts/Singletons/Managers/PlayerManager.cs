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
    public float _NecromancerMovementSpeed = 10f;
    public float _NecromancerRotationSpeed = 2f;

    /// Public (internal)
    [HideInInspector]
    public static PlayerManager _pInstance;                         // This is a singleton script, Initialized in Startup().
   
    /// Private
    private List<GameObject> _POOL_ALIVE_NECROMANCERS;              // Object pool of all inactive Player necromancers.
    private List<GameObject> _POOL_DEAD_NECROMANCERS;               // Object pool of all active Player necromancers.
    private List<GameObject> _POOL_ALIVE_WYRMS;                     // Object pool of all inactive AI wyrms.
    private List<GameObject> _POOL_DEAD_WYRMS;                      // Object pool of all active AI wyrms.

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;

        // Create object pools
        _POOL_ALIVE_NECROMANCERS = new List<GameObject>();
        _POOL_DEAD_NECROMANCERS = new List<GameObject>();
        _POOL_ALIVE_WYRMS = new List<GameObject>();
        _POOL_DEAD_WYRMS = new List<GameObject>();
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public List<GameObject> GetAliveNecromancers() {

        return _POOL_ALIVE_NECROMANCERS;
    }

    public List<GameObject> GetDeadNecromancers() {

        return _POOL_DEAD_NECROMANCERS;
    }

    public List<GameObject> GetAliveWyrms() {

        return _POOL_ALIVE_WYRMS;
    }

    public List<GameObject> GetDeadWyrms() {

        return _POOL_DEAD_WYRMS;
    }
}
