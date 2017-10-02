using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    protected float _FiringRate = 1;                        // The amount of time between shots.
    protected PlayerCharacter _Owner;                       // Current instigator assosiated with the weapon.

    protected float _FiringDelay = 0;                       // Current amount of time left till it can fire a projectile.
    protected bool _CanFire = true;                         // Returns true if the weapon is allowed to successfully fire a projectile.

    //--------------------------------------
    // FUNCTIONS

    public virtual void Start() {
		
	}

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

        // Deduct from the firing delay
        _FiringDelay -= Time.deltaTime;

        // enable OR disable firing sequence
        _CanFire = _FiringDelay <= 0f;
    }

    public virtual void Fire() {

        // Reset firing delay (only executes after a successful firing sequence)
        _FiringDelay = _FiringRate;
    }

    public void SetOwner(PlayerCharacter a_Owner) {

        // Set the new owner for this weapon
        _Owner = a_Owner;
    }

    public virtual void Init() {

    }
}