using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Shield : Weapon {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    public GameObject _ShieldMinionPrefab;

    /// Private
    private int _MaxMinions = 14;                                   // Cap of how many minions are allowed to make the shield.
    private int _MinionCount = 0;                                   // Amount of minions that composes the weapon.
    private int _PreviousMinionCount = 0;
    private float _OrbitSpeed;                                      // The speed in which the minions rotate around the character that owns this weapon.
    private float _MinionSpacing = 1f;         /* TEMPORARY */      // Unit of space between each minion.
    private Quaternion rotation;                                    // Current rotation of the weapon's transform.
    private List<Proj_ShieldMinion> _POOL_Minions;                         // Object pool of all minions attached to this weapon.        
    private List<Vector3> _MeatPositions;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Precausions
        base.Start();

        // Set orbit speed
        _OrbitSpeed = WeaponManager._pInstance._OrbitSpeed;

        // Set minion cap
        _MaxMinions = WeaponManager._pInstance._MaxSize;

        // Set initial rotation
        rotation = transform.rotation;

        // Create arrays
        _MeatPositions = new List<Vector3>();
        _POOL_Minions = new List<Proj_ShieldMinion>();
    }

    public override void Init() {

        if (_Owner != null) {

            // Create aura minions based on the defined size
            for (int i = _PreviousMinionCount; i < _MinionCount; i++) {

                // If the designers do their job
                if (_ShieldMinionPrefab != null) {

                    // Determine the position of the minion in the pool
                    float angle = i * Mathf.PI * 2 / _MinionCount;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _MinionSpacing;
                    pos += new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    // Create the minion prefab
                    var minion = Instantiate(_ShieldMinionPrefab, pos, Quaternion.identity, gameObject.transform).GetComponent<Proj_ShieldMinion>();
                    minion.GetComponentInChildren<Projectile>().Init();
                    minion.AddToPool(this);

                    // Set player score
                    _Owner._Player.SetScore(_MinionCount);
                }
            }

            // Hide the templated minion prefab
            ///_ShieldMinionPrefab.GetComponent<Renderer>().enabled = false;

            // Set initial rotation
            rotation = transform.rotation;
        }
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {

        if (_Owner != null) {

            // Only proceed if a valid player has been assigned to this weapon
            if (_Owner._Player != null) {

                /// IF RIGHT TRIGGER IS BEING USED
                if (_Owner._Player.GetRightTriggerInput.y != 0f) {

                    // Rotate shield right
                    rotation = new Quaternion(rotation.x, transform.rotation.y + _OrbitSpeed * Time.deltaTime, rotation.z, rotation.w);
                    ///transform.Rotate(0f, transform.rotation.y + _OrbitSpeed * Time.deltaTime, 0f);
                }

                /// IF LEFT TRIGGER IS BEING USED
                if (_Owner._Player.GetLeftTriggerInput.y != 0f) {

                    // Rotate shield left
                    rotation = new Quaternion(rotation.x, transform.rotation.y - _OrbitSpeed * Time.deltaTime, rotation.z, rotation.w);
                    ///transform.Rotate(0f, transform.rotation.y - _OrbitSpeed * Time.deltaTime, 0f);
                }
            }
        }

        // Update weapons position based of the owning character's position (if VALID)
        if (_Owner != null)
        {
            transform.position = _Owner.transform.position;
        }

        // Maintain rotation
        ///rotation.y = transform.rotation.y;
        transform.rotation = rotation;
    }

    //--------------------------------------------------------------
    // *** FIRING ***

    public override void Fire() {

        // DO NOTHING
    }

    IEnumerator RotateObj(GameObject spinner, float timeToRotate, Vector3 direction) {

        float t = 0;

        while (t < timeToRotate) {

            spinner.transform.Rotate(direction * (Time.fixedDeltaTime / timeToRotate));
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    //--------------------------------------------------------------
    // *** OBJECT POOL ***

    public void AddMinion(Char_Wyrm wyrm) {

        // If minion count hasnt reached max capacity yet
        if (_MinionCount < _MaxMinions) {

            // Determine position of where the wyrm should be placed in the shield

            // TEMPORARY CODE IDK IT SOMEHOW WORKS LOL 
            // *******************************************
            _PreviousMinionCount = _MinionCount;
            _MinionCount += 1;
            Init();
            // *******************************************
        }
    }
    
    public int GetMaxMinions() {

        return _MaxMinions;
    }

    public int GetMinionCount() {

        return _MinionCount;
    }

    public List<Proj_ShieldMinion> GetMeatMinionPool() {

        return _POOL_Minions;
    }

}