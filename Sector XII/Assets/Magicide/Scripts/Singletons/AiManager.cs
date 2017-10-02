﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public static AiManager _pInstance;                     // This is a singleton script, Initialized in Startup().

    [Header("* Grunt Minion")]
    [Tooltip("The movement speed of the grunt minion characters.")]
    public float _pGruntMinionSpeed = 45f;                  // The movement speed of the grunt minion characters.
    [Tooltip("The starting health of the grunt")]
    public int _pGruntHealth = 20;                          // The starting health of the grunt
    [Tooltip("The hitpoint damage of a grunt melee attack.")]
    public int _pGruntAttackDamage = 20;                    // The hitpoint damage of a grunt melee attack.

    [Header("* Brute Minion")]
    [Tooltip("The movement speed of the brute minion characters.")]
    public float _pBruteMinionSpeed = 35f;                  // The movement speed of the brute minion characters.
    [Tooltip("The starting health of the brute.")]
    public int _pBruteHealth = 40;                          // The starting health of the brute.
    [Tooltip("The hitpoint damage of a brute melee attack.")]
    public int _pBruteAttackDamage = 20;                    // The hitpoint damage of a brute melee attack.

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

    void Update() {

    }

    private void FixedUpdate() {

    }
}