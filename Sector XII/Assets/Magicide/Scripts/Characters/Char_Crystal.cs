using UnityEngine;

public class Char_Crystal : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)    
    public CrystalType _Type;
    
    /// Public (internal)
    [HideInInspector]
    public enum CrystalType {

        Minor,
        Major,
        Cursed
    }

    /// Private
    private KillTag.PickupType _PickupType = KillTag.PickupType.AddToShield;
    private Behaviour_Wander _BehaviourWander;
    private Behaviour_Flee _BehaviourFlee;
    private Behaviour_Seek _BehaviourSeek;
    private LinearGoToTarget _LinearSeek;
    private AiManager.AiSpawningTime _SpawningTime;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get base character references
        base.Start();

        // Get behaviour references
        _BehaviourWander = GetComponent<Behaviour_Wander>();
        _BehaviourFlee = GetComponent<Behaviour_Flee>();
        _BehaviourSeek = GetComponent<Behaviour_Seek>();
        _LinearSeek = GetComponent<LinearGoToTarget>();

        switch (_Type) {

            // MINOR VARIANT
            case CrystalType.Minor: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMinorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMinorMovementSpeed;
                    _PickupType = AiManager._pInstance._CrystalMinorTagType;
                    _MeshRenderer.material = AiManager._pInstance._CrystalMinorTypeMaterial;
                    _SpawningTime = AiManager._pInstance._CrystalMinorSpawningTime;
                    _EffectOnDeath = AiManager._pInstance._MinorOnDeathEffect;

                    switch (AiManager._pInstance._CrystalMinorBehaviourType) {

                        case AiManager.AiBehaviourType.Wander: {

                                _BehaviourWander.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Flee: {

                                _BehaviourFlee.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Seek: {

                                _BehaviourSeek.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Mixed: {

                                break;
                            }
                        default: {

                                break;
                            }
                    }

                    break;
                }

            // MAJOR VARIANT
            case CrystalType.Major: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMajorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMajorMovementSpeed;
                    _PickupType = AiManager._pInstance._CrystalMajorTagType;
                    _MeshRenderer.material = AiManager._pInstance._CrystalMajorTypeMaterial;
                    _SpawningTime = AiManager._pInstance._CrystalMajorSpawningTime;
                    _EffectOnDeath = AiManager._pInstance._MajorOnDeathEffect;

                    switch (AiManager._pInstance._CrystalMajorBehaviourType) {

                        case AiManager.AiBehaviourType.Wander: {

                                _BehaviourWander.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Flee: {

                                _BehaviourFlee.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Seek: {

                                _BehaviourSeek.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Mixed: {

                                break;
                            }
                        default: {

                                break;
                            }
                    }
                    break;
                }

            // CURSED VARIANT
            case CrystalType.Cursed: {

                    // Initialize
                    _StartingHealth = AiManager._pInstance._CrystalMajorStartingHealth;
                    _MovementSpeed = AiManager._pInstance._CrystalMajorMovementSpeed;
                    _PickupType = AiManager._pInstance._CrystalCursedTagType;
                    _MeshRenderer.material = AiManager._pInstance._CrystalCursedTypeMaterial;
                    _SpawningTime = AiManager._pInstance._CrystalCursedSpawningTime;
                    _EffectOnDeath = AiManager._pInstance._CursedOnDeathEffect;

                    switch (AiManager._pInstance._CrystalCursedBehaviourType) {

                        case AiManager.AiBehaviourType.Wander: {

                                _BehaviourWander.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Flee: {

                                _BehaviourFlee.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Seek: {

                                _BehaviourSeek.enabled = true;
                                break;
                            }
                        case AiManager.AiBehaviourType.Mixed: {

                                break;
                            }
                        default: {

                                break;
                            }
                    }
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

    public override void Damage(Character instigator, float amount) {
        
        base.Damage(instigator, amount);
    }

    public override void OnDeath(Character instigator) {

        // Get last known alive position and store it
        base.OnDeath(instigator);

        // Play OnDeath effect
        Instantiate(_EffectOnDeath, transform.position, Quaternion.identity);

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

        // Play OnDeath sound
        SoundManager._pInstance.PlayCrystalDeath();       
        
        // Set LinearGoToTarget behaviour to be active
    }

    public AiManager.AiSpawningTime GetSpawningTime() {

        return _SpawningTime;
    }
    
    public CrystalType GetVariantType() {

        return _Type;
    }
    
    //--------------------------------------------------------------
    // *** BEHAVIOURS ***

    public void SetWanderEnable(bool enable) {

        _BehaviourWander.enabled = enable;
    }

    public void SetFleeEnable(bool enable) {

        _BehaviourFlee.enabled = enable;
    }

    public void SetSeekEnable(bool enable) {

        _BehaviourSeek.enabled = enable;
    }

    public void SetLinearSeekEnable(bool enable) {

        _LinearSeek.enabled = enable;
    }

}