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
    [Header(" *** CRYSTAL CHARACTER ***")]
    [Header("- Health")]
    public int _CrystalStartingHealth = 100;                        // Starting health of the wyrms when spawning.
    [Header("- Movement")]
    public float _CrystalMovementSpeed = 5f;                        // Movement speed of the crystal character.
    [Header("- AI Respawning")]
    public List<Transform> _SpawnPositions;                         // Array of spawn points
    public int _MaxLives = 10;

    /// Public (internal)
    [HideInInspector]
    public static AiManager _pInstance;                             // This is a singleton script, Initialized in Startup().
    
    /// Private
    private List<GameObject> _POOL_ALIVE_MINIONS;                   // Object pool of all ALIVE minions in the scene
    private List<GameObject> _POOL_DEAD_MINIONS;                    // Object pool of all DEAD minions in the scene
    
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

        // Create vector arrays
        _POOL_ALIVE_MINIONS = new List<GameObject>();
        _POOL_DEAD_MINIONS = new List<GameObject>();

        // Get all startup ai
        GameObject[] startupAi = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var ai in startupAi) {
    
            // Add to alive pool
            _POOL_ALIVE_MINIONS.Add(ai.gameObject);
        }
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    public void FixedUpdate() {

    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public void OnRespawn() {
    
        if (_POOL_DEAD_MINIONS.Count > 0 && _MaxLives > 0)
        {
            // Get the character from the end of the dead array
            GameObject newAi = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1];

            // Get random int (min = 0, max = vector array.size -1)
            int randIndex = Random.Range(0, _SpawnPositions.Count - 1);

            // Get spawn position from spawnPoints [ randIndex ].position (newAI)
            // Set ai transform's to the random spawn's position
            newAi.transform.position = _SpawnPositions[randIndex].position;

            // Add ai (newAI variable) to active minion array
           _POOL_ALIVE_MINIONS.Add(newAi);

            // Remove ai (new AI variable) from dead minion array
            _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

            // -1 from ai lives cap
            _MaxLives -= 1;
        }        
    }

    public List<GameObject> GetActiveMinions() {

        // Returns the contiguous array of all alive minions
        return _POOL_ALIVE_MINIONS;
    }

    public List<GameObject> GetInactiveMinions() {

        // Returns the contiguous array of all dead/despawned minions
        return _POOL_DEAD_MINIONS;
    }
}