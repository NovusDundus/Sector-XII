﻿using System.Collections;
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
    private ParticleSystem _ParticleEffect;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Get referenece to projectile collision & particle effect
        base.Start();
        _ParticleEffect = GetComponentInChildren<ParticleSystem>();

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

        // Restart the firing effect
        _ParticleEffect.Stop();
        _ParticleEffect.Play();
    }

    public void FreeProjectile() {
        
        // Reset properties
        _Active = false;
        distanceTraveled = 0f;

        // Move out of the scene
        transform.position = new Vector3(1000, 1, 000);
        
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
            GeomancerCollisionChecks();

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
    
    public void GeomancerCollisionChecks() {

        // Check against enemy player character
        if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

            if (PlayerManager._pInstance.GetActiveNecromancers().Count > 0) {

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
                                    }

                                    // Play impact effect
                                    ParticleSystem effect = Instantiate(WeaponManager._pInstance._FireballImpactEffect, gameObject.transform.position, Quaternion.identity);
                                    effect.gameObject.GetComponent<DestroyAfterTime>().enabled = true;

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

                                // Play impact effect
                                ParticleSystem effect = Instantiate(WeaponManager._pInstance._FireballImpactEffect, gameObject.transform.position, Quaternion.identity);
                                effect.gameObject.GetComponent<DestroyAfterTime>().enabled = true;

                                // Destroy fireball
                                FreeProjectile();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other) {

        if (_Active == true) {

            // Check against the static collisions
            if (other.gameObject.tag == "Collision") {

                // Play impact effect
                ParticleSystem effect = Instantiate(WeaponManager._pInstance._FireballImpactEffect, gameObject.transform.position, Quaternion.identity);
                effect.gameObject.GetComponent<DestroyAfterTime>().enabled = true;

                // Destroy projectile
                FreeProjectile();
            }
            
            // Has the fireball collided with the minion's collision?
            if (other.gameObject.tag == "Enemy") {

                Char_Crystal crystal = other.gameObject.GetComponent<Char_Crystal>();

                // Damage minion
                crystal.Damage(_Owner.GetOwner(), _ImpactDamage);

                // Check if minion has been killed
                if (crystal.GetHealth() <= 0) {

                    // Add to instigator's kill count
                    _Owner.GetOwner()._Player.AddKillCount();
                }

                // Play impact effect
                ParticleSystem effect = Instantiate(WeaponManager._pInstance._FireballImpactEffect, gameObject.transform.position, Quaternion.identity);
                effect.gameObject.GetComponent<DestroyAfterTime>().enabled = true;

                // Destroy fireball
                FreeProjectile();

                // Play impact sound
                SoundManager._pInstance.PlayFireballImpact();
            }

            // Check against face tree
            if (other.gameObject.tag == "FaceTree") {

                FaceTree tree = other.gameObject.GetComponent<FaceTree>();
                tree.OnHit();

                // Play impact effect
                ParticleSystem effect = Instantiate(WeaponManager._pInstance._FireballImpactEffect, gameObject.transform.position, Quaternion.identity);
                effect.gameObject.GetComponent<DestroyAfterTime>().enabled = true;

                // Destroy fireball
                FreeProjectile();

                // Play impact sound
                SoundManager._pInstance.PlayFireballImpact();
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