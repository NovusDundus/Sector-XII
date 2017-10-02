using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public static WavesManager _pInstance;                 // This is a singleton script, Initialized in Startup().

    [Header("* Starting wave")]
    public int      _pInitialGruntAmount       = 30;       // The starting amount of grunt minions in the first wave of a match.
    public int      _pInitialBruteAmount       = 15;       // The starting amount of brute minions in the first wave of a match.

    [Header("* All waves")]
    public int      _pGruntIncrement           = 2;        // Upon a new wave, add _ grunt minions to the lives count.
    public int      _pBruteIncrement           = 1;        // Upon a new wave, add _ brute minions to the lives count.
    public float    _pGruntRespawnTime         = 3f;       // the delay (seconds) between death and respawning of a grunt minion.
    public float    _pBruteRespawnTime         = 3f;       // the delay (seconds) between death and respawning of a brute minion.

    //--------------------------------------
    // FUNCTIONS

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
    }

    void Update () {
		
	}

    private void FixedUpdate() {

    }

    public void WaveStart(int GruntAmount, int BruteAmount) {

    }

    public void WaveCompleted() {

    }
}
