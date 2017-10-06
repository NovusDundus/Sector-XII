using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (Designers)
    [Header(" *** WYRM CHARACTER ***")]
    [Header("- Health")]
    public int _WyrmStartingHealth = 100;                           // Starting health of the wyrms when spawning.
    [Header("- Movement")]
    public float _WyrmMovementSpeed = 5f;                           // Movement speed of the wyrm character.

    /// Public (internal)
    [HideInInspector]
    public static AiManager _pInstance;                             // This is a singleton script, Initialized in Startup().

    /// Private
    private List<Character> _POOL_ALIVE_MINIONS;                    // Object pool of all ALIVE minions in the scene
    private List<Character> _POOL_DEAD_MINIONS;                     // Object pool of all DEAD minions in the scene

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public void Awake() {

        // If the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;

        // Create vector arrays
        _POOL_ALIVE_MINIONS = new List<Character>();
        _POOL_DEAD_MINIONS = new List<Character>();
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public List<Character> GetActiveMinions() {

        // Returns the contiguous array of all alive minions
        return _POOL_ALIVE_MINIONS;
    }

    public List<Character> GetInactiveMinions() {

        // Returns the contiguous array of all dead/despawned minions
        return _POOL_DEAD_MINIONS;
    }
}