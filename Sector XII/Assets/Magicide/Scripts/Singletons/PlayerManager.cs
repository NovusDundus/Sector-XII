using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public static PlayerManager _pInstance;                 // This is a singleton script, Initialized in Startup().

    [Header("* HUMANOID character")]
    public float _pHumanoidSpeed = 50f;                     // The movement speed of the player when possessing a humanoid character.
    public float _pHumanoidRotationRate = 20f;              // The rotation/aiming speed of the player when possessing a humanoid character.
    public int _pHumanoidStartingHealth = 100;              // The initial health of all humanoid characters when a match starts.
    [Header("* Orb weapon")]
    public float _pOrbFiringRate = 0.5f;                    // The delay (seconds) between each firing of orb projectiles.
    [Header("* Fireball projectile")]
    public float _pFireballSpeed = 10f;                     // The traversal speed of the fireball projectile.
    public int _pFireballDamage = 100;                      // The impact damage of a fireball projectile when it collides with a target.

    [Header("* ETHEREAL character")]
    public float _pEtherealSpeed = 20f;                     // The movement speed of the player when possessing a humanoid character.
    public float _pEtherealRotationRate = 20f;              // The rotation/aiming speed of the player when possessing a humanoid character.
    public int _pEtherealStartingHealth = 100;              // The initial health of all humanoid characters when a match starts.
    [Header("* Aura Pool weapon")]
    public int _pAuraMinionCount = 6;                       // The starting amount of minions an ethereal aura wall.
    public float _pAuraMinionSpacing = 5f;                  // The spacing between each minion when instantiated around the pool.
    public float _pAuraOrbitSpeed = 1f;                     // The speed in which the minions rotate around the character that owns this weapon.
    [Header("* Aura Minion Projectile")]
    [Tooltip("The delay (seconds) between each firing of a stun minion from the aura pool.")]
    public float _pAuraMinionFiringRate = 3f;               // The delay (seconds) between each firing of a stun minion from the aura pool.
    public float _pAuraMinionSpeed = 10f;                   // The traversal speed of the aura minion projectile.
    public float _pAuraMinionStunTime = 2f;                 // The stun time (seconds) of a successful hit on a humanoid character.
    public float _pAuraMinionImpactRadius = 5f;             // The stun aoe around the minion when it collides with a valid object.

    //--------------------------------------
    // FUNCTIONS

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
    }

    void Update() {

    }

    private void FixedUpdate() {

    }
}