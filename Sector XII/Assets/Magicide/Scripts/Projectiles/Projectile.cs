using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    protected PlayerCharacter _Owner;                       // Current instigator assosiated with the projectile.

    //--------------------------------------
    // FUNCTIONS

    public virtual void Start() {

    }

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

    }

    public virtual void OnImpact() {

    }

    public void SetOwner(PlayerCharacter a_Owner) {

        // Set the new owner for this projectile
        _Owner = a_Owner;
    }

    public void Init() {

        // reinitialize
        Start();
    }

}