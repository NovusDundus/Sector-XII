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
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalMinorBehaviourType = AiBehaviourType.Wander;
    [Header("- Movement")]
    [Tooltip("Movement speed of the minor crystal variant.")]
    public float _CrystalMinorMovementSpeed = 5f;                   // Movement speed of the minor crystal variant.
    [Header("- Spawning")]
    [Tooltip("The amount of respawns allowed for the minor crystal variant.")]
    public int _CrystalMinorLives = 10;                             // The amount of respawns allowed for the minor crystal variant.
    [Tooltip("Array of spawn points for the minor crystal variant.")]
    public List<Transform> _CrystalMinorSpawnPositions;             // Array of spawn points for the minor crystal variant.
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalMinorSpawningBehaviour = AiSpawningBehaviour.MatchStart;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalMinorSpawnTime = 50;
    [Tooltip("Starting health of the minor crystal variant when spawning.")]
    public int _CrystalMinorStartingHealth = 100;                   // Starting health of the minor crystal variant when spawning.
    public KillTag.PickupType _CrystalMinorTagType = KillTag.PickupType.AddToShield;
    [Header("- Appearance")]
    public Material _CrystalMinorTypeMaterial;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** MAJOR VARIANT ***")]
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalMajorBehaviourType = AiBehaviourType.Flee;
    [Header("- Movement")]
    [Tooltip("Movement speed of the major crystal variant.")]
    public float _CrystalMajorMovementSpeed = 5f;                   // Movement speed of the major crystal variant.
    [Header("- Spawning")]
    [Tooltip("The amount of respawns allowed for the major crystal variant.")]
    public int _CrystalMajorLives = 5;                              // The amount of respawns allowed for the major crystal variant.
    [Tooltip("Array of spawn points for the major crystal variant.")]
    public List<Transform> _CrystalMajorSpawnPositions;             // Array of spawn points for the major crystal variant.
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalMajorSpawningBehaviour = AiSpawningBehaviour.Phase2Start;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalMajorSpawnTime = 50;
    [Tooltip("Starting health of the major crystal variant when spawning.")]
    public int _CrystalMajorStartingHealth = 100;                   // Starting health of the major crystal variant when spawning.
    public KillTag.PickupType _CrystalMajorTagType = KillTag.PickupType.SpeedBoost;
    [Header("- Appearance")]
    public Material _CrystalMajorTypeMaterial;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** CURSED VARIANT***")]
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalCursedBehaviourType = AiBehaviourType.Seek;
    [Header("- Movement")]
    [Tooltip("Movement speed of the cursed crystal variant.")]
    public float _CrystalCursedMovementSpeed = 5f;                  // Movement speed of the cursed crystal variant.
    [Header("- Spawning")]
    [Tooltip("The amount of respawns allowed for the cursed crystal variant.")]
    public int _CrystalCursedLives = 2;                             // The amount of respawns allowed for the cursed crystal variant.
    [Tooltip("Array of spawn points for the cursed crystal variant.")]
    public List<Transform> _CrystalCursedSpawnPositions;            // Array of spawn points for the cursed crystal variant.
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalCursedSpawningBehaviour = AiSpawningBehaviour.AtSpecificTime;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalCursedSpawnTime = 50;
    [Tooltip("Starting health of the cursed crystal variant when spawning.")]
    public int _CrystalCursedStartingHealth = 100;                  // Starting health of the cursed crystal variant when spawning.
    public KillTag.PickupType _CrystalCursedTagType = KillTag.PickupType.Healthpack;
    [Header("- Appearance")]
    public Material _CrystalCursedTypeMaterial;

    /// Public (internal)
    [HideInInspector]
    public static AiManager _pInstance;                             // This is a singleton script, Initialized in Startup().
    public enum AiBehaviourType {

        Wander,
        Flee,
        Seek,
        Mixed
    }
    public enum AiSpawningBehaviour {

        MatchStart,
        Phase2Start,
        AtSpecificTime
    }
    
    /// Private
    private List<GameObject> _POOL_ALIVE_MINIONS;                   // Object pool of all ALIVE minions in the scene
    private List<GameObject> _POOL_DEAD_MINIONS;                    // Object pool of all DEAD minions in the scene
    private List<GameObject> _POOL_MINOR_MINIONS;
    private List<GameObject> _POOL_MAJOR_MINIONS;
    private List<GameObject> _POOL_CURSED_MINIONS;

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
        _POOL_MINOR_MINIONS = new List<GameObject>();
        _POOL_MAJOR_MINIONS = new List<GameObject>();
        _POOL_CURSED_MINIONS = new List<GameObject>();

        // Get all ai prefabs in the scene
        GameObject[] startupAi = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var ai in startupAi) {

            // If the AI is meant to be active at the start of the match
            if (ai.GetComponent<Char_Crystal>().GetSpawningBehaviour() == AiSpawningBehaviour.MatchStart) {
                
                // Add to alive pool
                _POOL_ALIVE_MINIONS.Add(ai.gameObject);
            }

            // Allocate minor types to their pool
            if (ai.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Add to minor pool
                _POOL_MINOR_MINIONS.Add(ai.gameObject);
            }

            // Allocate major types to their pool
            if (ai.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Add to major pool
                _POOL_MAJOR_MINIONS.Add(ai.gameObject);
            }

            // Allocate cursed types to their pool
            if (ai.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Add to cursed pool
                _POOL_CURSED_MINIONS.Add(ai.gameObject);
            }
        }
    }

    //--------------------------------------------------------------
    // FRAME

    public void Update() {

    }

    //--------------------------------------------------------------
    // SPAWNING

    public void OnPhase2Start() {

        // MINOR VARIANT
        if (_CrystalMinorSpawningBehaviour == AiSpawningBehaviour.Phase2Start) {

            // Call the respawning event for the minor variant X amount of times (depending on how many exist in the pool)
            foreach (var minor in _POOL_MINOR_MINIONS) {

                OnRespawnMinor();
            }
        }

        // MAJOR VARIANT
        if (_CrystalMajorSpawningBehaviour == AiSpawningBehaviour.Phase2Start) {

            // Call the respawning event for the major variant X amount of times (depending on how many exist in the pool)
            foreach (var major in _POOL_MAJOR_MINIONS) {

                OnRespawnMajor();
            }
        }

        // CURSED VARIANT
        if (_CrystalCursedSpawningBehaviour == AiSpawningBehaviour.Phase2Start) {

            // Call the respawning event for the cursed variant X amount of times (depending on how many exist in the pool)
            foreach (var cursed in _POOL_CURSED_MINIONS) {

                OnRespawnCursed();
            }
        }
    }

    //--------------------------------------------------------------
    // OBJECT POOLS

    public void OnRespawnMinor() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMinorLives > 0) {

            // Get the character from the end of the dead array
            GameObject newAi = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;

            if (newAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _CrystalMinorSpawnPositions.Count - 1);

                // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                // Set ai transform's to the random spawn's position
                newAi.transform.position = _CrystalMinorSpawnPositions[randIndex].position;

                // Show the ai's mesh renderer
                newAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                // Add ai (newAI variable) to active minion array
                _POOL_ALIVE_MINIONS.Add(newAi.gameObject);

                // Remove ai (new AI variable) from dead minion array
                _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

                // -1 from ai lives cap
                _CrystalMinorLives -= 1;
            }
        }
    }

    public void OnRespawnMajor() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMajorLives > 0) {

            // Get the character from the end of the dead array
            GameObject newAi = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;

            if (newAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _CrystalMajorSpawnPositions.Count - 1);

                // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                // Set ai transform's to the random spawn's position
                newAi.transform.position = _CrystalMajorSpawnPositions[randIndex].position;

                // Show the ai's mesh renderer
                newAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                // Add ai (newAI variable) to active minion array
                _POOL_ALIVE_MINIONS.Add(newAi.gameObject);

                // Remove ai (new AI variable) from dead minion array
                _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

                // -1 from ai lives cap
                _CrystalMajorLives -= 1;
            }
        }
    }

    public void OnRespawnCursed() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalCursedLives > 0) {

            // Get the character from the end of the dead array
            GameObject newAi = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;

            if (newAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _CrystalCursedSpawnPositions.Count - 1);

                // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                // Set ai transform's to the random spawn's position
                newAi.transform.position = _CrystalCursedSpawnPositions[randIndex].position;

                // Show the ai's mesh renderer
                newAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                // Add ai (newAI variable) to active minion array
                _POOL_ALIVE_MINIONS.Add(newAi.gameObject);

                // Remove ai (new AI variable) from dead minion array
                _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

                // -1 from ai lives cap
                _CrystalCursedLives -= 1;
            }
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