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
    private float _OrbitSpeed = 200;                                // The speed in which the minions rotate around the character that owns this weapon.
    private float _MinionSpacing = 2f;         /* TEMPORARY */      // Unit of space between each minion.
    private Quaternion _rotation;                                   // Current rotation of the weapon's transform.
    private List<GameObject> _POOL_Minions;                         // Object pool of all minions attached to this weapon.
    private bool _AutomatedRotation;
    private bool _RotateRight;
    private bool _CanRotate = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Precausions
        base.Start();

        // Set orbit speed
        _OrbitSpeed = WeaponManager._pInstance._ShieldOrbitSpeed;

        // Set minion cap & spacing
        _MaxMinions = WeaponManager._pInstance._MaxSize * 2;
        _MinionSpacing = WeaponManager._pInstance._MinionSpacing;

        // Set rotation properties
        _AutomatedRotation = WeaponManager._pInstance._AutoRotateShield;
        _RotateRight = WeaponManager._pInstance._ShieldOrbitClockwise;
        _rotation = transform.rotation;

        // Create arrays
        _POOL_Minions = new List<GameObject>();
    }

    public override void Init() {

        if (_Owner != null) {

            // Create aura minions based on the defined size
            for (int i = _PreviousMinionCount; i < _MinionCount; i++) {

                // If the designers do their job
                if (_ShieldMinionPrefab != null) {

                    // Determine the position of the minion in the pool
                    ///Quaternion rot = new Quaternion(transform.rotation.x, transform.rotation.y + _rotation.y, transform.rotation.z, transform.rotation.w);
                    
                    float angle = i * (Mathf.PI * WeaponManager._pInstance._ShieldDivider / (float)GetMaxMinions()) + transform.rotation.y;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _MinionSpacing;
                    pos += new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                    
                    // Create the minion prefab
                    var minion = Instantiate(_ShieldMinionPrefab, pos, Quaternion.identity, gameObject.transform).GetComponent<Proj_ShieldMinion>();
                    minion.GetComponentInChildren<Projectile>().Init();
                    minion.AddToPool(this);
                    minion.SetOwner(this);

                    // Set player score
                    _Owner._Player.SetScore(_MinionCount);
                }
            }

            // Set initial rotation
            _rotation = transform.rotation;
        }
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

        if (_Owner != null) {

            // Update weapons position based of the owning character's position
            transform.position = _Owner.transform.position;

            // Only proceed if a valid player has been assigned to this weapon
            if (_Owner._Player != null) {

                // Meatshield rotates automatically
                if (_AutomatedRotation == true) {

                    // Rotate shield clockwise
                    if (_RotateRight == true) {

                        transform.Rotate(0f, transform.rotation.y + _OrbitSpeed * Time.deltaTime, 0f);
                    }

                    // Rotate shield counter clockwise
                    else { /// _RotateRight == false

                        transform.Rotate(0f, transform.rotation.y - _OrbitSpeed * Time.deltaTime, 0f);
                    }
                }

                // Meatshield  will have to be rotated manually
                else { /// _AutomatedRotation == false

                    // Right trigger input ONLY
                    if (_Owner._Player.GetRightTriggerInput.y != 0f && _Owner._Player.GetLeftTriggerInput.y == 0f) {

                        if (_CanRotate == true) {

                            // Rotate shield right
                            transform.Rotate(0f, transform.rotation.y + _OrbitSpeed * Time.deltaTime, 0f);
                        }
                    }

                    // Left trigger input ONLY
                    if (_Owner._Player.GetLeftTriggerInput.y != 0f && _Owner._Player.GetRightTriggerInput.y == 0f) {

                        if (_CanRotate == true) {

                            // Rotate shield left
                            transform.Rotate(0f, transform.rotation.y - _OrbitSpeed * Time.deltaTime, 0f);
                        }
                    }

                    // Apply new rotation
                    _rotation.y = transform.rotation.y;

                    // Update minion count
                    _MinionCount = _POOL_Minions.Count;
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** FIRING ***

    public override void Fire() {

        // DO NOTHING
    }

    IEnumerator RotateObj(GameObject spinner, float timeToRotate, Vector3 direction) {

        float t = 0;

        while (t < timeToRotate) {

            spinner.transform.Rotate(direction * (Time.deltaTime / timeToRotate));
            t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    public void SetCanRotate(bool enabled) {

        _CanRotate = enabled;
    }

    //--------------------------------------------------------------
    // *** OBJECT POOL ***

    public void AddMinion(Char_Crystal wyrm) {

        // If minion count hasnt reached max capacity yet
        if (_MinionCount < _MaxMinions) {

            // Add minion to the shield
            _PreviousMinionCount = _MinionCount;
            _MinionCount += 1;
            Init();

            // Deduct movement speed from the player associated to the shield
            _Owner.SetMovementSpeed(_Owner.GetMovementSpeed() - WeaponManager._pInstance._MovementSpeedSap);
        }
    }
    
    public int GetMaxMinions() {

        return _MaxMinions;
    }

    public int GetMinionCount() {

        return _MinionCount;
    }

    public List<GameObject> GetMeatMinionPool() {

        return _POOL_Minions;
    }

    public void SetMinionCount(int amount) {

        _MinionCount = amount;
    }

    public void DestroyMinion() {

        if (_MinionCount > 0) {

            // Get the last minion in the pool
            GameObject minionObj = _POOL_Minions[_POOL_Minions.Count - 1];
            Proj_ShieldMinion minion = minionObj.GetComponent<Proj_ShieldMinion>();

            // Kill it
            minion.ForceDeath();

            // Set new score
            _Owner._Player.SetScore(_MinionCount);
        }
    }

}