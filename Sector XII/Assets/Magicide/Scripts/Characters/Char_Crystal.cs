using System.Collections;
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
    }

    //--------------------------------------------------------------
    // *** FRAME ***
    

    public override void Update() {

        base.Update();
    }

    public override void FixedUpdate() {

        base.FixedUpdate();
    }

    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void Damage(float amount) {
        
        base.Damage(amount);
    }

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // hide THIS character & move out of playable space
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        gameObject.transform.position = new Vector3(1000, 0, 1000);

        // Find self in active pool
        foreach (var minion in AiManager._pInstance.GetActiveMinions()) {

            // Once we have found ourself in the pool
            if (minion == this.gameObject) {

                // Move to inactive pool
                AiManager._pInstance.GetInactiveMinions().Add(minion.gameObject);
                AiManager._pInstance.GetActiveMinions().Remove(minion);
                break;
            }
        }

        // Create kill tag at death position associated with THIS wyrm
        GameObject killTag = Instantiate(GameObject.FindGameObjectWithTag("KillTag"), _DeathPosition, Quaternion.identity);
        killTag.GetComponent<KillTag>().Init(this);
    }
}