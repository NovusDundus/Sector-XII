using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile {

    //--------------------------------------
    // VARIABLES

    private int _ImpactDamage = 50;

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Set fireball impact damage
        _ImpactDamage = PlayerManager._pInstance._pFireballDamage;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

    }
}