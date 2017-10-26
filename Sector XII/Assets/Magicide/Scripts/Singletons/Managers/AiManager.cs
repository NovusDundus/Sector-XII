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
    [Header("---------------------------------------------------------------------------")]
    [Header(" *** MINOR VARIANT ***")]
    [Header("- Movement")]
    [Tooltip("Movement speed of the minor crystal variant.")]
    public float _CrystalMinorMovementSpeed = 5f;                   // Movement speed of the minor crystal variant.
    [Header("- Spawning")]
    [Tooltip("Array of spawn points for the minor crystal variant.")]
    public List<Transform> _MinorSpawnPositions;                    // Array of spawn points for the minor crystal variant.
    [Tooltip("")]
    public int _MinorLives = 10;
    [Tooltip("Starting health of the minor crystal variant when spawning.")]
    public int _CrystalMinorStartingHealth = 100;                   // Starting health of the minor crystal variant when spawning.
    public KillTag.PickupType _MinorTagType = KillTag.PickupType.AddToShield;
    [Header("- Appearance")]
    public Material _MinorTypeMaterial;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** MAJOR VARIANT ***")]
    [Header("- Movement")]
    [Tooltip("Movement speed of the major crystal variant.")]
    public float _CrystalMajorMovementSpeed = 5f;                   // Movement speed of the major crystal variant.
    [Header("- Spawning")]
    [Tooltip("Array of spawn points for the major crystal variant.")]
    public List<Transform> _MajorSpawnPositions;                    // Array of spawn points for the major crystal variant.
    [Tooltip("Starting health of the major crystal variant when spawning.")]
    public int _CrystalMajorStartingHealth = 100;                   // Starting health of the major crystal variant when spawning.
    public KillTag.PickupType _MajorTagType = KillTag.PickupType.SpeedBoost;
    [Header("- Appearance")]
    public Material _MajorTypeMaterial;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** CURSED VARIANT***")]
    [Header("- Movement")]
    [Tooltip("Movement speed of the cursed crystal variant.")]
    public float _CrystalCursedMovementSpeed = 5f;                  // Movement speed of the cursed crystal variant.
    [Header("- Spawning")]
    [Tooltip("Array of spawn points for the cursed crystal variant.")]
    public List<Transform> _CursedSpawnPositions;                   // Array of spawn points for the cursed crystal variant.
    [Tooltip("Starting health of the cursed crystal variant when spawning.")]
    public int _CrystalCursedStartingHealth = 100;                  // Starting health of the cursed crystal variant when spawning.
    public KillTag.PickupType _CursedTagType = KillTag.PickupType.Healthpack;
    [Header("- Appearance")]
    public Material _CursedTypeMaterial;

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
    
        if (_POOL_DEAD_MINIONS.Count > 0 && _MinorLives > 0)
        {
            // Get the character from the end of the dead array
            GameObject newAi = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;

            // Get random int (min = 0, max = vector array.size -1)
            int randIndex = Random.Range(0, _MinorSpawnPositions.Count - 1);

            // Get spawn position from spawnPoints [ randIndex ].position (newAI)
            // Set ai transform's to the random spawn's position
            newAi.transform.position = _MinorSpawnPositions[randIndex].position;

            // Show the ai's mesh renderer
            newAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

            // Add ai (newAI variable) to active minion array
            _POOL_ALIVE_MINIONS.Add(newAi.gameObject);

            // Remove ai (new AI variable) from dead minion array
            _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

            // -1 from ai lives cap
            _MinorLives -= 1;
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