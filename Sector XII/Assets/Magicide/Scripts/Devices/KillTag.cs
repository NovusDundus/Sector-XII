using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTag : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: FIRSTNAME LASTNAME
    /// Created on: DAY.MONTH.YEAR
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Private
    private bool _Active = false;
    private Collider _Collision;
    private Char_Wyrm Wyrm;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Get reference to the collision
        _Collision = GetComponent<Collider>();
    }

    public void Init(Char_Wyrm Char) {

        // Set reference to the wyrm to be used in the meat shield
        Wyrm = Char;
        
        // Tag is now active in the world
        _Active = true;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

    }

    public void FixedUpdate() {

        CollisionChecks();
    }

    public void CollisionChecks() {

        // Precautions
        if (_Collision != null && _Active == true) {

            // Test against all alive necromancers in the game
            foreach (var necromancer in PlayerManager._pInstance.GetAliveNecromancers()) {

                // Once collision against the necro has happened
                if (_Collision.bounds.Intersects(necromancer._Collision.bounds)) {

                    // Pickup minion
                    OnPickup(necromancer.GetComponent<Char_Necromancer>());
                    break;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** PICKUP ***

    public void OnPickup(Char_Necromancer Necromancer) {

        // Add to meat shield
        Necromancer.GetSecondaryWeapon().GetComponent<Wep_Shield>().AddMinion(Wyrm);

        // Destroy tag
        Destroy(gameObject);
    }

}