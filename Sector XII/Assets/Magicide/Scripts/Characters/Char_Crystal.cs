﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Crystal : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***



    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Set starting health & get collision reference
        _StartingHealth = AiManager._pInstance._CrystalStartingHealth;
        base.Start();

        // Set movement speed
        _MovementSpeed = AiManager._pInstance._CrystalMovementSpeed;

        // Add to object pool
        ///AiManager._pInstance.GetInactiveMinions().Add(this.gameObject);
        AiManager._pInstance.GetActiveMinions().Add(this.GetComponent<GameObject>());
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {
        
    }

    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // hide THIS character & move out of playable space
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        gameObject.transform.position = new Vector3(1000, 0, 1000);

        // Find self in active pool
        foreach (var wyrm in AiManager._pInstance.GetActiveMinions()) {

            // Once we have found ourself in the pool
            if (wyrm == this) {

                // Move to inactive pool
                AiManager._pInstance.GetInactiveMinions().Add(wyrm);
                AiManager._pInstance.GetActiveMinions().Remove(wyrm);
                break;
            }
        }

        // Create kill tag at death position associated with THIS wyrm
        GameObject killTag = Instantiate(GameObject.FindGameObjectWithTag("KillTag"), _DeathPosition, Quaternion.identity);
        killTag.GetComponent<KillTag>().Init(this);
    }
}