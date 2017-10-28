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

    /// Public (designers)    
    public CrystalType _Type = CrystalType.Minor;
    
    /// Public (internal)
    [HideInInspector]
    public enum CrystalType {

        Minor,
        Major,
        Cursed
    }

    /// Private
    private KillTag.PickupType _PickupType = KillTag.PickupType.AddToShield;
    
    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {
        
        // Get references
        base.Start();

        switch (_Type) {

            // MINOR VARIANT
            case CrystalType.Minor: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMinorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMinorMovementSpeed;
                    _PickupType = AiManager._pInstance._MinorTagType;
                    _MeshRenderer.material = AiManager._pInstance._MinorTypeMaterial;
                    break;
                }

            // MAJOR VARIANT
            case CrystalType.Major: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMajorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMajorMovementSpeed;
                    _PickupType = AiManager._pInstance._MajorTagType;
                    _MeshRenderer.material = AiManager._pInstance._MajorTypeMaterial;
                    break;
                }

            // CURSED VARIANT
            case CrystalType.Cursed: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMajorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMajorMovementSpeed;
                    _PickupType = AiManager._pInstance._CursedTagType;
                    _MeshRenderer.material = AiManager._pInstance._CursedTypeMaterial;
                    break;
                }

            default: {

                    break;
                }
        }

        // Reset health now that starting health has been updated
        _Health = _StartingHealth;

        // Store the new material that has been applied to the character
        _OriginalMaterial = _MeshRenderer.material;
    }

    //--------------------------------------------------------------
    // *** FRAME ***
    
    public override void Update() {

        base.Update();
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

        // Create kill tag at death position associated with THIS minion
        GameObject killTag = Instantiate(GameObject.FindGameObjectWithTag("KillTag"), _DeathPosition, Quaternion.identity);
        killTag.GetComponent<KillTag>().Init(this, _PickupType);
    }
    
}