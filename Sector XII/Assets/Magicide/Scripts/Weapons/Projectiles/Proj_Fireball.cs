using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Fireball : Projectile {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // VARIABLES

    /// Private
    private int _ImpactDamage;
    private float _TravelSpeed;
    private bool _Active = false;
    private SphereCollider _collider;
    private float distanceTraveled = 0f;

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public override void Start() {

        // Set fireball impact damage
        _ImpactDamage = WeaponManager._pInstance._ImpactDamage;

        // Set travel speed for the projectile
        _TravelSpeed = WeaponManager._pInstance._FireballSpeed;

        // Get referenece to projectile collision
        _collider = GetComponent<SphereCollider>();
    }

    public override void Init() {

        // Reinitialize
        Start();

        // Projectile is now an active gameObject in the scene
        _Active = true;
    }

    //--------------------------------------------------------------
    // FRAME

    public override void Update() {

    }

    public override void FixedUpdate() {

        // If this fireball is active in the world
        if (_Active == true) {

            // If max range hasnt been reached yet
            if (distanceTraveled < WeaponManager._pInstance._FireballRange) {

                // Move forwards
                StartCoroutine(SmoothMove(transform.right, _TravelSpeed));
                ///transform.Translate(transform.right * _TravelSpeed);

                // Add 1 unit of distance per second
                distanceTraveled += Time.deltaTime * 60;
            }

            // Max range has been reached
            else { /// distanceTraveled => WeaponManager._pInstance._FireballRange

                // Reset properties
                _Active = false;
                distanceTraveled = 0f;

                // Find reference to self in active projectile pool

                // Needs to move to the inactive projectile pool

                /// PLACEHOLDER ****************
                Destroy(gameObject);
                _Owner.GetComponent<Wep_Orb>()._ActiveProjectiles -= 1;
            }
        }
    }

    //--------------------------------------------------------------
    // MOVEMENT

    // Coroutine to move this gameObjects.transform in a direction X speed
    IEnumerator SmoothMove(Vector3 direction, float speed) {

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