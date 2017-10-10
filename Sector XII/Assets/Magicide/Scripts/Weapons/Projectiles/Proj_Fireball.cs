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
        _ImpactDamage = WeaponManager._pInstance._ImpactDamage;

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

        // Needs to move to the inactive projectile pool

        /// PLACEHOLDER ****************
        Destroy(gameObject);
        _Owner.GetComponent<Wep_Orb>()._ActiveProjectiles -= 1;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

    }

    public override void FixedUpdate() {

        // If this fireball is active in the world
        if (_Active == true) {

            // Check for any collision against all potential targets
            CollisionChecks();

            // If max range hasnt been reached yet
            if (distanceTraveled < WeaponManager._pInstance._FireballRange) {

                // Move forwards
                StartCoroutine(SmoothMove(transform.forward, _TravelSpeed));

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

            // If minion has valid collision reference set
            if (minion.GetCollider() != null) {

                // Has the fireball collided with the minion's collision?
                if (_Collision.bounds.Intersects(minion.GetCollider().bounds)) {

                    // Damage minion
                    minion.Damage(_ImpactDamage);

                    // Destroy fireball
                    FreeProjectile();
                    break;
                }
            }
        }

        // Check against all alive players
        foreach (var necromancer in PlayerManager._pInstance.GetAliveNecromancers()) {
            
            // If necromancer has valid collision reference set
            if (necromancer.GetCollider() != null) {

                // If it isnt the instigator who is being tested against
                if (necromancer != _Owner.GetOwner()) {

                    // Check against all meat shield minions associated to the player
                    foreach (Proj_ShieldMinion meatMinion in necromancer.GetSecondaryWeapon().GetComponent<Wep_Shield>().GetMeatMinionPool()) {

                        // Has the fireball collided with the minions's collision?
                        if (_Collision.bounds.Intersects(meatMinion.GetCollision().bounds)) {

                            // Damage minion
                            meatMinion.Damage(_ImpactDamage);

                            // Destroy fireball
                            FreeProjectile();
                            break;
                        }
                    }

                    // Has the fireball collided with the necromancer's collision?
                    if (_Collision.bounds.Intersects(necromancer.GetCollider().bounds) && _Active) {

                        // Damage necromancer
                        necromancer.Damage(_ImpactDamage);

                        // Destroy fireball
                        FreeProjectile();
                        break;
                    }
                }
            }
        }

        // Check against all Static objects
        foreach (GameObject Object in LevelManager._pInstance.GetStaticObjects()) {

            LevelObject staticObject = Object.GetComponent<LevelObject>();

            // If object has valid collision
            if (staticObject.GetCollision() != null) {

                // Has the fireball collided with the object's collision?
                if (_Collision.bounds.Intersects(staticObject.GetCollision().bounds)) {

                    // Destroy fireball
                    FreeProjectile();
                    break;
                }
            }
        }

        // Check against all dynamic objects
        foreach (GameObject Object in LevelManager._pInstance.GetDynamicObjects()) {

            LevelObject dynamicObject = Object.GetComponent<LevelObject>();

            // If object has valid collision
            if (dynamicObject.GetCollision() != null) {

                // Has the fireball collided with the object's collision?
                if (_Collision.bounds.Intersects(dynamicObject.GetCollision().bounds)) {

                    // Damage object

                    // Destroy fireball
                    FreeProjectile();
                    break;
                }
            }
        }

    }

    //--------------------------------------------------------------
    // *** MOVEMENT ***

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
}