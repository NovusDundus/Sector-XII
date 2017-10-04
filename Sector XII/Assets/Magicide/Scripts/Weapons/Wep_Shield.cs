using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Shield : Weapon {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // VARIABLES

    public GameObject _ShieldMinionPrefab;

    private int _MaxMinions = 14;                                   // Cap of how many minions are allowed to make the shield.
    private int _MinionCount = 8;                                   // Amount of minions that composes the weapon.
    private float _MinionSpacing = 1f;                              // Unit of space between each minion.
    private float _OrbitSpeed = 3f;                                 // The speed in which the minions rotate around the character that owns this weapon.

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public override void Start() {
        
        // Set firing rate
        ///_FiringRate = WeaponManager._pInstance._pOrbFiringRate;

        // Set orbit speed
        ///_OrbitSpeed = WeaponManager._pInstance._pAuraOrbitSpeed;

        // Set minion count
        ///_MinionCount = WeaponManager._pInstance._pAuraMinionCount;

        // Set minion spacing
        ///_MinionSpacing = WeaponManager._pInstance._pAuraMinionSpacing;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {
                
        if (_Owner != null) {

            /// IF RIGHT TRIGGER ISNT BEING USED

            /// IF LEFT TRIGGER IS BEING USED

                // Rotate shield left
                ///transform.Rotate(0f, transform.parent.rotation.y + _OrbitSpeed, 0f);

        }

        if (_Owner != null) {

            /// IF LEFT TRIGGER ISNT BEING USED
                    
            /// IF RIGHT TRIGGER IS BEING USED

                // Rotate shield right
                ///transform.Rotate(0f, transform.parent.rotation.y + _OrbitSpeed, 0f);
        }

        // Update weapons position based of the owning character's position (if VALID)
        if (_Owner != null)
        {
            transform.position = _Owner.transform.position;
        }
    }

    public override void Fire() {

    }

    public override void Init()
    {
        if (_Owner != null)
        {
            // Create aura minions based on the defined size
            for (int i = 0; i < _MinionCount; i++) {

                // If the designers do their job
                if (_ShieldMinionPrefab != null) {

                    // Determine the position of the minion in the pool
                    float angle = i * Mathf.PI * 2 / _MinionCount;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _MinionSpacing;
                    pos += new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    // Create the minion prefab
                    var minion = Instantiate(_ShieldMinionPrefab, pos, Quaternion.identity, gameObject.transform).GetComponent<Proj_ShieldMinion>();
                    minion.GetComponentInChildren<Projectile>().Init();

                    // Set owner for the aura minion
                    ///minion.GetComponentInChildren<Projectile>().SetOwner(_Owner);
                }
            }

            // Hide the templated minion prefab
            _ShieldMinionPrefab.GetComponent<Renderer>().enabled = false;
        }
    }
}