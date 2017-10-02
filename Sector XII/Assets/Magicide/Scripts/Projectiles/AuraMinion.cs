using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraMinion : Projectile {

    //--------------------------------------
    // VARIABLES

    private float _StunTime;
    private float _ImpactRadius;
    private float _TravelSpeed;
    private float _SpinSpeed;

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Set impact stats
        _StunTime = PlayerManager._pInstance._pAuraMinionStunTime;
        _ImpactRadius = PlayerManager._pInstance._pAuraMinionImpactRadius;

        // Set movement speed of the projectile
        _TravelSpeed = PlayerManager._pInstance._pAuraMinionSpeed;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Continuously rotate the minion on the spot 
        transform.Rotate(0f, transform.rotation.y + _SpinSpeed, 0f);
    }
}
