using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile {

    //--------------------------------------
    // VARIABLES

    private int _ImpactDamage;
    private float _TravelSpeed;
    private bool _Active = true;
    private SphereCollider _collider;

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Set fireball impact damage
        _ImpactDamage = PlayerManager._pInstance._pFireballDamage;

        // Set travel speed for the projectile
        _TravelSpeed = PlayerManager._pInstance._pFireballSpeed;

        // Get referenece to projectile collision
        _collider = GetComponent<SphereCollider>();
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously move forward at a set speed
        StartCoroutine(SmoothMove(transform.forward, _TravelSpeed));

        // Test collision against minions
        foreach (var minion in WavesManager._pInstance.GetActiveAI()) {

            // if projectile collides with minion collision bounds
            if (_collider.bounds.Intersects(minion.GetComponent<CapsuleCollider>().bounds)) {

                // destroy
                _Active = false;

                // Hide minion
                minion.GetComponent<Renderer>().enabled = false;
                minion.transform.position = new Vector3(1000,0,1000);
                break;
            }    
        }

        // Test collision against ethereal
        foreach (var ethereal in PlayerManager._pInstance.GetActiveEthereals()) {

            // if projectile collides with ethereal collision bounds
            if (_collider.bounds.Intersects(ethereal.GetComponentInChildren<CapsuleCollider>().bounds)) {

                // destroy fireball
                _Active = false;

                // damage ethereal
                ///ethereal.GetComponent<PlayerCharacter>().Damage(_ImpactDamage);
                break;
            }
        }

        // Test collision against aura minions
        foreach (var auraMinion in PlayerManager._pInstance.GetActiveAuraMinions()) {

            // if projectile collides with auraminions collision bounds
            if (_collider.bounds.Intersects(auraMinion.GetComponentInChildren<CapsuleCollider>().bounds)) {

                // destroy fireball
                _Active = false;

                // damage minion
                auraMinion.GetComponent<AuraMinion>().Damage(_ImpactDamage);
                break;
            }
        }

        // Draw if active
        GetComponent<Renderer>().enabled = _Active;
    }

    // Coroutine to move the gameObjects.transform in a direction X speed
    IEnumerator SmoothMove(Vector3 direction, float speed) {

        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + direction;

        while (startPos != endPos && ((Time.time - startTime)*speed) < 1f) {

            float move = Mathf.Lerp(0, 1, (Time.time - startTime) * speed);

            transform.position += direction * move;

            yield return null;
        }
    }
}