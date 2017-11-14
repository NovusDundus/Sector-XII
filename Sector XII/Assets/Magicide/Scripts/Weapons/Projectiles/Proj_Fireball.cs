using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Fireball : Projectile {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Private
    private int _ImpactDamage;                                      // Amount of damage inflicted to any object that collides with this projectile.
    private float _TravelSpeed;                                     // Movement speed of the projectile.
    private bool _Active = false;                                   // Returns TRUE if the projectile is active in the world.
    private float distanceTraveled = 0f;                            // Used to test if max range has been reached.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get referenece to projectile collision
        base.Start();

        // Set fireball impact damage
        _ImpactDamage = WeaponManager._pInstance._FireballImpactDamage;

        // Set travel speed for the projectile
        _TravelSpeed = WeaponManager._pInstance._FireballSpeed;
    }

    public override void Init() {

        // Reinitialize
        Start();

        // Projectile is now an active gameObject in the scene
        _Active = true;
    }

    public void FreeProjectile() {
        
        // Reset properties
        _Active = false;
        distanceTraveled = 0f;
        
        // Find reference to self in active projectile pool
        int i = 0;
        foreach (var item in _Owner.GetComponent<Wep_Orb>().GetActivePool()) {
        
            // Once we have found ourself
            if (item.gameObject == this.gameObject) {
        
                // Remove from active pool
                _Owner.GetComponent<Wep_Orb>().GetActivePool().RemoveAt(i);
        
                // Needs to move to the inactive projectile pool
                _Owner.GetComponent<Wep_Orb>().GetInactivePool().Add(item);
                break;
            }
            ++i;
        }
    }  

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

        // If this fireball is active in the world
        if (_Active == true) {

            // Check for any collision against all potential targets
            CollisionChecks();

            // If max range hasnt been reached yet
            if (distanceTraveled < WeaponManager._pInstance._FireballRange) {

                // Move forwards                
                transform.position = transform.position + transform.forward * _TravelSpeed * Time.deltaTime;
                ///(SmoothMove(transform.forward, _TravelSpeed * Time.deltaTime));

                // Add 1 unit of distance per second
                distanceTraveled += Time.deltaTime * 60;
            }

            // Max range has been reached
            else { /// distanceTraveled => WeaponManager._pInstance._FireballRange

                // Destroy
                FreeProjectile();
            }
        }
    }
    
    public void CollisionChecks() {

        // Check against all alive minions
        foreach (var minion in AiManager._pInstance.GetActiveMinions()) {
   
            Char_Crystal crystal = minion.GetComponent<Char_Crystal>();

            // Precaution
            if (crystal != null) {

                // If minion has valid collision reference set
                if (crystal.GetCollider() != null) {

                    // Has the fireball collided with the minion's collision?
                    if (_Collision.bounds.Intersects(crystal.GetCollider().bounds)) {

                        // Damage minion
                        crystal.Damage(_Owner.GetOwner(), _ImpactDamage);

                        // Play impact sound
                        SoundManager._pInstance.PlayFireballImpact();

                        // Check if minion has been killed
                        if (crystal.GetHealth() <= 0) {

                            // Add to instigator's kill count
                            _Owner.GetOwner()._Player.AddKillCount();
                        }

                        // Destroy fireball
                        FreeProjectile();
                        break;
                    }
                }
            }
        }

        if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

            // Check against all alive players
            foreach (var necromancer in PlayerManager._pInstance.GetActiveNecromancers()) {

                // If necromancer has valid collision reference set
                if (necromancer.GetCollider() != null) {

                    // If it isnt the instigator who is being tested against
                    if (necromancer != _Owner.GetOwner()) {

                        // Check against all meat shield minions associated to the player
                        foreach (GameObject meatMinion in necromancer.GetSpecialWeapon().GetComponent<Wep_Shield>().GetMeatMinionPool()) {

                            Proj_ShieldMinion minion = meatMinion.GetComponent<Proj_ShieldMinion>();

                            // Has the fireball collided with the minions's collision?
                            if (_Collision.bounds.Intersects(minion.GetCollision().bounds)) {

                                // Damage minion
                                minion.Damage(_Owner.GetOwner(), _ImpactDamage);

                                // Play impact sound
                                SoundManager._pInstance.PlayFireballImpact();

                                // Check if minion has been killed
                                if (minion.GetHealth() <= 0) {

                                    // Add to instigator's kill count
                                    _Owner.GetOwner()._Player.AddKillCount();

                                    // Remove minion from the shield count (-1)
                                    ///meatMinion.GetOwner().GetOwner().GetComponentInChildren<Wep_Shield>().SetMinionCount(meatMinion.getowner)
                                    ///meatMinion.GetOwner().GetComponent<Wep_Shield>().SetMinionCount(meatMinion.GetOwner().GetComponent<Wep_Shield>().GetMinionCount() - 1);
                                }

                                // Destroy fireball
                                FreeProjectile();
                                _Active = false;
                                break;
                            }
                        }

                        // Has the fireball collided with the necromancer's collision?
                        if (_Collision.bounds.Intersects(necromancer.GetCollider().bounds) && _Active) {

                            // Damage necromancer
                            necromancer.Damage(_Owner.GetOwner(), _ImpactDamage /*+ (_ImpactDamage * _DamageMultiplier)*/);
                            
                            // Play impact sound
                            SoundManager._pInstance.PlayFireballImpact();

                            // Check if necromancer has been killed
                            if (necromancer.GetHealth() <= 0) {

                                // Add to instigator's kill count
                                _Owner.GetOwner()._Player.AddKillCount();
                            }

                            // Destroy fireball
                            FreeProjectile();
                            break;
                        }
                    }
                }
            }
        }

        // Check against all faceTrees
        foreach (FaceTree tree in DeviceManager._pInstance._FaceTrees) {
            
            // Precaution
            if (tree != null) {

                // Tree has valid collision
                if (tree.GetCollider() != null) {

                    // Has the fireball collided with the tree's collision?
                    if (_Collision.bounds.Intersects(tree.GetCollider().bounds)) {

                        tree.OnHit();
                        print("HIT TREE");

                        // Play impact sound
                        SoundManager._pInstance.PlayFireballImpact();

                        // Destroy fireball
                        FreeProjectile();
                        break;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other) {

        Tags tagComp = other.gameObject.GetComponent<Tags>();
        if (tagComp != null) {

            if (tagComp.ContainsTag("Character")) {

                ///print(other.tag);
            }

            if (tagComp.ContainsTag("Collision")) {

                ///print(other.tag);
            }
        }
    }

    public void OnCollisionEnter(Collision collision) {

        Tags tagComp = collision.gameObject.GetComponent<Tags>();
        if (tagComp != null) {

            if (tagComp.ContainsTag("Character")) {

                ///print(collision.gameObject.tag);
            }

            if (tagComp.ContainsTag("Collision")) {

                ///print(collision.gameObject.tag);
            }
        }
    }
    
    IEnumerator SmoothMove(Vector3 direction, float speed) {

        // Coroutine to move this gameObjects.transform in a direction * speed
        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction;

        while (startPos != endPos && ((Time.time - startTime) * speed) < 1f) {

            float move = Mathf.Lerp(0, 1, (Time.time - startTime) * speed);

            transform.position += direction * move;

            yield return null;
        }
    }

    //--------------------------------------------------------------
    // *** DAMAGE ***

    public int GetImpactDamage() {

        return _ImpactDamage;
    }

}