using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile {

    //--------------------------------------
    // VARIABLES

    private int _ImpactDamage;
    private float _TravelSpeed;

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Set fireball impact damage
        _ImpactDamage = PlayerManager._pInstance._pFireballDamage;

        // Set travel speed for the projectile
        _TravelSpeed = PlayerManager._pInstance._pFireballSpeed;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously move forward at a set speed
        ///transform.position = transform.forward * Time.deltaTime * _TravelSpeed;
        StartCoroutine(SmoothMove(transform.forward, _TravelSpeed));
    }

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