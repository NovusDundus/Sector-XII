﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (Exposed)
    [Header("---------------------------------------------------------------------------")]
    [Header(" *** MINOR VARIANT ***")]
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalMinorBehaviourType = AiBehaviourType.Wander;
    [Header("- Movement")]
    [Tooltip("Movement speed of the minor crystal variant.")]
    public float _CrystalMinorMovementSpeed = 5f;                   
    [Header("- Spawning")]
    [Tooltip("")]
    public bool _UnlimitedLivesMinor = false;
    [Tooltip("The amount of respawns allowed for the minor crystal variant.")]
    public int _CrystalMinorLives = 10;                             
    [Tooltip("")]
    public AiSpawningTime _CrystalMinorSpawningTime = AiSpawningTime.MatchStart;
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalMinorSpawningBehaviour = AiSpawningBehaviour.RandomSpawning;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalMinorSpawnTime = 50;
    [Tooltip("Starting health of the minor crystal variant when spawning.")]
    public int _CrystalMinorStartingHealth = 100;                    
    public KillTag.PickupType _CrystalMinorTagType = KillTag.PickupType.AddToShield;
    [Header("- Appearance")]
    [Tooltip("The material to be placed on the mesh attached to the minor variant.")]
    public Material _CrystalMinorTypeMaterial;
    [Tooltip("The game object that is connected to the particle that is played on the minor variant's death.")]
    public GameObject _MinorOnDeathEffect;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** MAJOR VARIANT ***")]
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalMajorBehaviourType = AiBehaviourType.Flee;
    [Header("- Movement")]
    [Tooltip("Movement speed of the major crystal variant.")]
    public float _CrystalMajorMovementSpeed = 5f;                   
    [Header("- Spawning")]
    [Tooltip("")]
    public bool _UnlimitedLivesMajor = false;
    [Tooltip("The amount of respawns allowed for the major crystal variant.")]
    public int _CrystalMajorLives = 5;                              
    [Tooltip("")]
    public AiSpawningTime _CrystalMajorSpawningTime = AiSpawningTime.Phase2Start;
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalMajorSpawningBehaviour = AiSpawningBehaviour.TeleportingGates;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalMajorSpawnTime = 50;
    [Tooltip("Starting health of the major crystal variant when spawning.")]
    public int _CrystalMajorStartingHealth = 100;                   
    public KillTag.PickupType _CrystalMajorTagType = KillTag.PickupType.SpeedBoost;
    [Header("- Appearance")]
    [Tooltip("The material to be placed on the mesh attached to the major variant.")]
    public Material _CrystalMajorTypeMaterial;
    [Tooltip("The game object that is connected to the particle that is played on the major variant's death.")]
    public GameObject _MajorOnDeathEffect;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** CURSED VARIANT***")]
    [Header("- Behaviour")]
    public AiBehaviourType _CrystalCursedBehaviourType = AiBehaviourType.Seek;
    [Header("- Movement")]
    [Tooltip("Movement speed of the cursed crystal variant.")]
    public float _CrystalCursedMovementSpeed = 5f;                  
    [Header("- Spawning")]
    [Tooltip("")]
    public bool _UnlimitedLivesCursed = false;
    [Tooltip("The amount of respawns allowed for the cursed crystal variant.")]
    public int _CrystalCursedLives = 2;                             
    [Tooltip("")]
    public AiSpawningTime _CrystalCursedSpawningTime = AiSpawningTime.AtSpecificTime;
    [Tooltip("")]
    public AiSpawningBehaviour _CrystalCursedSpawningBehaviour = AiSpawningBehaviour.TeleportingGates;
    [Tooltip("The amount of time into the game on when the spawning for this variant should occur. (SPAWNING BEHAVIOUR MUST BE SET TO 'AtSpecificTiime'")]
    public int _CrystalCursedSpawnTime = 50;
    [Tooltip("Starting health of the cursed crystal variant when spawning.")]
    public int _CrystalCursedStartingHealth = 100;                  
    public KillTag.PickupType _CrystalCursedTagType = KillTag.PickupType.Healthpack;
    [Header("- Appearance")]
    [Tooltip("The material to be placed on the mesh attached to the cursed variant.")]
    public Material _CrystalCursedTypeMaterial;
    [Tooltip("The game object that is connected to the particle that is played on the cursed variant's death.")]
    public GameObject _CursedOnDeathEffect;

    [Header("---------------------------------------------------------------------------")]
    [Header(" *** RESPAWN POINTS ***")]
    [Header("")]
    [Tooltip("Array of spawn points inside the arena bounds.")]
    public List<Transform> _RandomSpawnPositions;                   
    [Tooltip("Array of spawn points positioned behind the gates.")]
    public List<Transform> _TeleportingGateSpawnPositions;          
    public List<Collider> _AgentTriggers;

    /// Public (internal)
    [HideInInspector]
    public static AiManager _pInstance;     
    
    public enum AiBehaviourType {

        Wander,
        Flee,
        Seek,
        Mixed
    }
    public enum AiSpawningTime {

        MatchStart,
        Phase2Start,
        AtSpecificTime
    }
    public enum AiSpawningBehaviour {

        RandomSpawning,
        TeleportingGates
    }

    /// Private
    private List<GameObject> _POOL_ALIVE_MINIONS;                   // Object pool of all ALIVE minions in the scene
    private List<GameObject> _POOL_DEAD_MINIONS;                    // Object pool of all DEAD minions in the scene
    private List<GameObject> _POOL_MINOR_MINIONS;
    private List<GameObject> _POOL_MAJOR_MINIONS;
    private List<GameObject> _POOL_CURSED_MINIONS;
    private float _GameTime = 0f;
    
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

        // Create vector arrays
        _POOL_ALIVE_MINIONS = new List<GameObject>();
        _POOL_DEAD_MINIONS = new List<GameObject>();
        _POOL_MINOR_MINIONS = new List<GameObject>();
        _POOL_MAJOR_MINIONS = new List<GameObject>();
        _POOL_CURSED_MINIONS = new List<GameObject>();
    }

    public void Start() {

        // Get all ai prefabs in the scene
        GameObject[] startupAi = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var ai in startupAi) {

            // If the AI is meant to be active at the start of the match
            if (ai.GetComponent<Char_Crystal>().GetSpawningTime() == AiSpawningTime.MatchStart) {

                // Add to alive pool
                _POOL_ALIVE_MINIONS.Add(ai.gameObject);

                // Enable agency
                ai.GetComponent<NavMeshAgent>().enabled = true;
            }

            // Ai is not meant to be active in the arena at match startup
            else {

                // Remove the minion from the arena
                ai.GetComponent<SkinnedMeshRenderer>().enabled = false;
                ai.transform.position = new Vector3(1000, 1, 1000);

                // Disable ALL behaviours
                Char_Crystal crystal = ai.GetComponent<Char_Crystal>();
                crystal.SetFleeEnable(false);
                crystal.SetWanderEnable(false);
                crystal.SetSeekEnable(false);
                crystal.SetLinearSeekEnable(false);
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
    // *** FRAME ***

    public void Update() {

        // Add to game time 1f per second
        _GameTime += Time.deltaTime;

        // The crystal variant is meant to spawn at a certain match time and that time has been reached
        if (_CrystalMinorSpawningTime == AiSpawningTime.AtSpecificTime && (int)_GameTime == _CrystalMinorSpawnTime) {

            // Start spawning the minor variant
            foreach (var minor in _POOL_MINOR_MINIONS) {

                if (_CrystalMinorSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnMinorRandom();
                }

                if (_CrystalMinorSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnMinorTeleporter();
                }
            }
        }

        // The crystal variant is meant to spawn at a certain match time and that time has been reached
        if (_CrystalMajorSpawningTime == AiSpawningTime.AtSpecificTime && (int)_GameTime == _CrystalMajorSpawnTime) {

            // Start spawning the minor variant
            foreach (var major in _POOL_MINOR_MINIONS) {

                if (_CrystalMajorSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnMajorRandom();
                }

                if (_CrystalMajorSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnMajorTeleporter();
                }
            }
        }

        // The crystal variant is meant to spawn at a certain match time and that time has been reached
        if (_CrystalCursedSpawningTime == AiSpawningTime.AtSpecificTime && (int)_GameTime == _CrystalCursedSpawnTime) {

            // Start spawning the minor variant
            foreach (var cursed in _POOL_MINOR_MINIONS) {

                if (_CrystalCursedSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnCursedRandom();
                }

                if (_CrystalCursedSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnCursedTeleporter();
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** INITIAL SPAWNING ***

    public void OnPhase2Start() {

        // MINOR VARIANT
        if (_CrystalMinorSpawningTime == AiSpawningTime.Phase2Start) {

            // Call the respawning event for the minor variant X amount of times (depending on how many exist in the pool)
            foreach (var minor in _POOL_MINOR_MINIONS) {

                if (_CrystalMinorSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnMinorRandom();
                }

                if (_CrystalMinorSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnMinorTeleporter();
                }
            }
        }

        // MAJOR VARIANT
        if (_CrystalMajorSpawningTime == AiSpawningTime.Phase2Start) {

            // Call the respawning event for the major variant X amount of times (depending on how many exist in the pool)
            foreach (var major in _POOL_MAJOR_MINIONS) {

                if (_CrystalMajorSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnMajorRandom();
                }

                if (_CrystalMajorSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnMajorTeleporter();
                }
            }
        }

        // CURSED VARIANT
        if (_CrystalCursedSpawningTime == AiSpawningTime.Phase2Start) {

            // Call the respawning event for the cursed variant X amount of times (depending on how many exist in the pool)
            foreach (var cursed in _POOL_CURSED_MINIONS) {

                if (_CrystalCursedSpawningBehaviour == AiSpawningBehaviour.RandomSpawning) {

                    // Spawn at random position within the arena bounds
                    SpawnCursedRandom();
                }

                if (_CrystalCursedSpawningBehaviour == AiSpawningBehaviour.TeleportingGates) {

                    // Spawn behind a teleporter gate and move into the gameplay area
                    SpawnCursedTeleporter();
                }
            }
        }
    }

    public void SpawnMinorRandom() {

        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _RandomSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Enable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }

    public void SpawnMajorRandom() {

        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _RandomSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Enable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }

    public void SpawnCursedRandom() {

        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _RandomSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Enable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }

    public void SpawnMinorTeleporter() {
        
        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _TeleportingGateSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Set LinearGoToTarget behaviour to be active
                    randAi.GetComponent<LinearGoToTarget>().enabled = true;

                    // Disable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = false;
                }
            }
        }
    }

    public void SpawnMajorTeleporter() {

        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _TeleportingGateSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Set LinearGoToTarget behaviour to be active
                    randAi.GetComponent<LinearGoToTarget>().enabled = true;

                    // Disable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = false;
                }
            }
        }
    }

    public void SpawnCursedTeleporter() {

        foreach (var randAi in GameObject.FindGameObjectsWithTag("Enemy")) {

            // MINOR variant
            if (randAi.GetComponent<Char_Crystal>().GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Currently inactive ai
                if (randAi.GetComponent<SkinnedMeshRenderer>().enabled == false) {

                    // Spawn at random position in the arena
                    // Get random int (min = 0, max = vector array.size -1)
                    int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                    // Get spawn position from spawnPoints [ randIndex ].position (newAI)
                    // Set ai transform's to the random spawn's position
                    randAi.transform.position = _TeleportingGateSpawnPositions[randIndex].position;

                    // Show the ai's mesh renderer
                    randAi.gameObject.GetComponentInChildren<Renderer>().enabled = true;

                    // Add ai (newAI variable) to active minion array
                    _POOL_ALIVE_MINIONS.Add(randAi.gameObject);

                    // Set LinearGoToTarget behaviour to be active
                    randAi.GetComponent<LinearGoToTarget>().enabled = true;

                    // Disable agency
                    randAi.GetComponent<NavMeshAgent>().enabled = false;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** RESPAWNING ***

    public void RespawnCrystal(Char_Crystal crystal, Vector3 position) {

        // Show the ai's mesh renderer
        crystal.gameObject.GetComponentInChildren<Renderer>().enabled = true;

        // Add ai (newAI variable) to active minion array
        _POOL_ALIVE_MINIONS.Add(crystal.gameObject);

        // Remove ai (new AI variable) from dead minion array
        _POOL_DEAD_MINIONS.RemoveAt(_POOL_DEAD_MINIONS.Count - 1);

        // Set position
        crystal.transform.position = position;

        // Set LinearGoToTarget behaviour to be active
        crystal.GetComponent<LinearGoToTarget>().enabled = true;

        // Disable agency
        crystal.GetComponent<NavMeshAgent>().enabled = false;

        // Disable all behaviours
        crystal.SetWanderEnable(false);
        crystal.SetFleeEnable(false);
        crystal.SetSeekEnable(false);
    }
    
    public void OnRespawnMinorRandom() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMinorLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _RandomSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesMinor == false) {

                    _CrystalMinorLives -= 1;
                }
            }
        }
    }

    public void OnRespawnMajorRandom() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMajorLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _RandomSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesMajor == false) {

                    _CrystalMajorLives -= 1;
                }
            }
        }
    }

    public void OnRespawnCursedRandom() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalCursedLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _RandomSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _RandomSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesCursed == false) {

                    _CrystalCursedLives -= 1;
                }
            }
        }
    }

    public void OnRespawnMinorTeleporter() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMinorLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Minor) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _TeleportingGateSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesMinor == false) {

                    _CrystalMinorLives -= 1;
                }
            }
        }
    }

    public void OnRespawnMajorTeleporter() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalMajorLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Major) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _TeleportingGateSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesMajor == false) {

                    _CrystalMajorLives -= 1;
                }
            }
        }
    }
    
    public void OnRespawnCursedTeleporter() {

        if (_POOL_DEAD_MINIONS.Count > 0 && _CrystalCursedLives > 0) {

            // Get the character from the end of the dead array
            GameObject aiObj = _POOL_DEAD_MINIONS[_POOL_DEAD_MINIONS.Count - 1].gameObject;
            Char_Crystal crystal = aiObj.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal.GetVariantType() == Char_Crystal.CrystalType.Cursed) {

                // Get random int (min = 0, max = vector array.size -1)
                int randIndex = Random.Range(0, _TeleportingGateSpawnPositions.Count - 1);

                // Set crystal's transform to a random spawn position
                RespawnCrystal(crystal, _TeleportingGateSpawnPositions[randIndex].position);

                // Deduct 1 from respawn lives counter
                if (_UnlimitedLivesCursed == false) {

                    _CrystalCursedLives -= 1;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** OBJECT POOLS ***

    public List<GameObject> GetActiveMinions() { return _POOL_ALIVE_MINIONS; }

    public List<GameObject> GetInactiveMinions() { return _POOL_DEAD_MINIONS; }

}