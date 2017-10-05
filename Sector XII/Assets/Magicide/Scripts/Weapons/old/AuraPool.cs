using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPool : Weapon {

    //--------------------------------------
    // VARIABLES

    public GameObject prefab;

    private int _MinionCount;                               // Amount of aura minions that composes the weapon.
    private float _MinionSpacing;                           // Unit of space between each minion.
    private float _OrbitSpeed;                              // The speed in which the minions rotate around the character that owns this weapon.

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {
        
        // Set firing rate
        ///_FiringRate = PlayerManager._pInstance._pOrbFiringRate;

        // Set orbit speed
        ///_OrbitSpeed = PlayerManager._pInstance._pAuraOrbitSpeed;

        // Set minion count
        ///_MinionCount = PlayerManager._pInstance._pAuraMinionCount;

        // Set minion spacing
        ///_MinionSpacing = PlayerManager._pInstance._pAuraMinionSpacing;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously rotate the aura minions around the owner of this weapon         
        transform.Rotate(0f, /*transform.parent.rotation.y + */_OrbitSpeed, 0f);

        // Update position based of the character's (that owns this weapon) position
        if (_Owner != null)
        {
            transform.position = _Owner.transform.position;
        }
        
        // Check if fire delay allows the firing sequence to be initiated
        base.FixedUpdate();
    }

    public override void Fire() {

    }

    public override void Init()
    {
        if (_Owner != null)
        {
            // Create aura minions based on the defined size
            for (int i = 0; i < _MinionCount; i++)
            {
                // Determine the position of the minion in the pool
                float angle = i * Mathf.PI * 2 / _MinionCount;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _MinionSpacing;
                pos += new Vector3(transform.position.x, transform.position.y, transform.position.z);

                // Create the minion prefab
                ///var minion = Instantiate(GameObject.FindGameObjectWithTag("P" + _Owner._Player._pPlayerID + "_AuraMinion"), pos, Quaternion.identity, gameObject.transform).GetComponent<AuraMinion>();

                // Set the tag of the newly created minion (player 1 = 'P1_AuraMinion1', 'P1_AuraMinion2', 'P1_AuraMinion3' etc...)
                ///minion.tag = string.Concat("P" + _Owner._Player._pPlayerID + "_AuraMinion" + (i + 1));
            }

            // Hide the templated minion prefab
            GameObject.FindGameObjectWithTag("P" + _Owner._Player._pPlayerID + "_AuraMinion").GetComponent<Renderer>().enabled = false;
        }
    }
}