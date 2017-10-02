using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Weapon {

    //--------------------------------------
    // VARIABLES

    private int _POOL_SIZE = 50;                            // Instance amount required for the object pool to function.

    LinkedList<Projectile> _POOL_INACTIVE;                  // Linked list of all inactive projectiles.
    LinkedList<Projectile> _POOL_ACTIVE;                    // Linked list of all active projectiles in the world.

    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Set firing rate
        _FiringRate = PlayerManager._pInstance._pOrbFiringRate;
    }

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Check if fire delay allows the firing sequence to be initiated
        base.FixedUpdate();

        // Always hide inactive projectiles
        if (_POOL_INACTIVE != null) { 

            foreach (var projectile in _POOL_INACTIVE)
            {
                projectile.GetComponent<Renderer>().enabled = false;
            }
        }

        // Always render active projectiles
        if (_POOL_ACTIVE != null) {

            foreach (var projectile in _POOL_ACTIVE)
            {
                projectile.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public override void Fire() {
        
        // PLACEHOLDER **************************
        if (_CanFire == true) {

            // Create fireball
            Instantiate(GameObject.FindGameObjectWithTag("P" + _Owner._Player._pPlayerID + "_FireBall"), transform.position, transform.rotation);

            // Reset firing delay
            base.Fire();
        }        
    }

    public Projectile GetInactiveProjectile() {

        if (_POOL_INACTIVE.Count > 0) {

            // Get the projectile from the end of the list
            ///Projectile proj = _POOL_INACTIVE.RemoveLast();

            return null;
        }

        // Empty pool >> returns NULL
        else {

            return null;
        }
    }

    public Projectile GetActiveProjectile() {

        return null;
    }

    public void SetInactive(Projectile proj) {

    }

    public void SetActive(Projectile proj) {

    }

    public override void Init() {

        // create inactive projectile pool by the defined size
        for (int i = 0; i < _POOL_SIZE; ++i)
        {
            // instantiate projectile
            var proj = Instantiate(GameObject.FindGameObjectWithTag("P" + _Owner._Player._pPlayerID + "_FireBall").GetComponent<Fireball>(), new Vector3(), Quaternion.identity);

            // Set projectile's tag (player 1 = 'P1_fireball1', 'P1_fireball2', 'P1_fireball3', etc...)
            ///proj.tag = string.Concat("P" + _Owner._Player._pPlayerID + "_FireBall" + (i + 1));

            // Add projectile to the end of the inactive object pool
            ///_POOL_INACTIVE.AddLast(proj);
        }
    }
    
}