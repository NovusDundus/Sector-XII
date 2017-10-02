using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public static WavesManager _pInstance;                  // This is a singleton script, Initialized in Startup().

    [Header("* Starting wave")]
    [Tooltip("The starting amount of grunt minions in the first wave of a match.")]
    public int _pInitialGruntAmount = 30;                   // The starting amount of grunt minions in the first wave of a match.
    [Tooltip("The starting amount of brute minions in the first wave of a match.")]
    public int _pInitialBruteAmount = 15;                   // The starting amount of brute minions in the first wave of a match.

    [Header("* All waves")]
    [Tooltip("Upon a new wave, add _ grunt minions to the lives count.")]
    public int _pGruntIncrement = 2;                        // Upon a new wave, add _ grunt minions to the lives count.
    [Tooltip("Upon a new wave, add _ brute minions to the lives count.")]
    public int _pBruteIncrement = 1;                        // Upon a new wave, add _ brute minions to the lives count.
    [Tooltip("the delay (seconds) between death and respawning of a grunt minion.")]
    public float _pGruntRespawnTime = 3f;                   // the delay (seconds) between death and respawning of a grunt minion.
    [Tooltip("the delay (seconds) between death and respawning of a brute minion.")]
    public float _pBruteRespawnTime = 3f;                   // the delay (seconds) between death and respawning of a brute minion.

    private LinkedList<GameObject> _POOL_INACTIVE_AI;
    private List<GameObject> _POOL_ACTIVE_AI;

    //--------------------------------------
    // FUNCTIONS

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;

        _POOL_ACTIVE_AI = new List<GameObject>();

        // add to _POOL_ACTIVE_AI
        foreach (var minion in GameObject.FindGameObjectsWithTag("Minion"))
        {
            _POOL_ACTIVE_AI.Add(minion);
        }
    }

    void Update () {
		
	}

    private void FixedUpdate() {

    }

    public void WaveStart(int GruntAmount, int BruteAmount) {

    }

    public void WaveCompleted() {

    }

    public List<GameObject> GetActiveAI() {

        return _POOL_ACTIVE_AI;
    }
}
