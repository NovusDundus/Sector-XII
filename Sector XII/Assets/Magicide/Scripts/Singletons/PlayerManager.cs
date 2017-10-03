using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public static PlayerManager _pInstance;                 // This is a singleton script, Initialized in Startup().

    [Header("* HUMANOID character")]
    [Tooltip("The movement speed of the player when possessing a humanoid character.")]
    public float _pHumanoidSpeed = 50f;                     // The movement speed of the player when possessing a humanoid character.
    [Tooltip("The rotation/aiming speed of the player when possessing a humanoid character.")]
    public float _pHumanoidRotationRate = 20f;              // The rotation/aiming speed of the player when possessing a humanoid character.
    [Tooltip("The initial health of all humanoid characters when a match starts.")]
    public int _pHumanoidStartingHealth = 100;              // The initial health of all humanoid characters when a match starts.
    [Header("* Orb weapon")]
    [Tooltip("The delay (seconds) between each firing of orb projectiles.")]
    public float _pOrbFiringRate = 0.5f;                    // The delay (seconds) between each firing of orb projectiles.
    [Header("* Fireball projectile")]
    [Tooltip("The traversal speed of the fireball projectile.")]
    public float _pFireballSpeed = 10f;                     // The traversal speed of the fireball projectile.
    [Tooltip("The impact damage of a fireball projectile when it collides with a target.")]
    public int _pFireballDamage = 100;                      // The impact damage of a fireball projectile when it collides with a target.

    [Header("* ETHEREAL character")]
    [Tooltip("The movement speed of the player when possessing a humanoid character.")]
    public float _pEtherealSpeed = 20f;                     // The movement speed of the player when possessing a humanoid character.
    [Tooltip("The rotation/aiming speed of the player when possessing a humanoid character.")]
    public float _pEtherealRotationRate = 20f;              // The rotation/aiming speed of the player when possessing a humanoid character.
    [Tooltip("The initial health of all ethereal characters when a match starts.")]
    public int _pEtherealStartingHealth = 100;              // The initial health of all ethereal characters when a match starts.
    [Header("* Aura Pool weapon")]
    [Tooltip("The starting amount of minions an ethereal aura wall.")]
    public int _pAuraMinionCount = 6;                       // The starting amount of minions an ethereal aura wall.
    [Tooltip("The spacing between each minion when instantiated around the pool.")]
    public float _pAuraMinionSpacing = 2f;                  // The spacing between each minion when instantiated around the pool.
    [Tooltip("The speed in which the minions rotate around the character that owns this weapon.")]
    public float _pAuraOrbitSpeed = 1f;                     // The speed in which the minions rotate around the character that owns this weapon.
    [Header("* Aura Minion Projectile")]
    [Tooltip("The delay (seconds) between each firing of a stun minion from the aura pool.")]
    public float _pAuraMinionFiringRate = 3f;               // The delay (seconds) between each firing of a stun minion from the aura pool.
    [Tooltip("The traversal speed of the aura minion projectile.")]
    public float _pAuraMinionSpeed = 10f;                   // The traversal speed of the aura minion projectile.
    [Tooltip("The stun time (seconds) of a successful hit on a humanoid character.")]
    public float _pAuraMinionStunTime = 2f;                 // The stun time (seconds) of a successful hit on a humanoid character.
    [Tooltip("The stun aoe around the minion when it collides with a valid object.")]
    public float _pAuraMinionImpactRadius = 5f;             // The stun aoe around the minion when it collides with a valid object.
    [Tooltip("The speed in which an ethereal minion will rotate on its own axis.")]
    public float _pAuraMinionRotationSpeed = 5f;            // The speed in which an ethereal minion will rotate on its own axis.

    private List<GameObject> _POOL_ACTIVE_ETHEREALS;
    private List<GameObject> _POOL_ACTIVE_AURAMINIONS;
    private List<GameObject> _POOL_INACTIVE_ETHEREALS;
    private List<GameObject> _POOL_INACTIVE_AURAMINIONS;

    //--------------------------------------
    // FUNCTIONS

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        _pInstance = this;
        
        // Create object pools
        _POOL_ACTIVE_ETHEREALS = new List<GameObject>();
        _POOL_ACTIVE_AURAMINIONS = new List<GameObject>();
    }

    void Update() {

    }

    private void FixedUpdate() {

    }

    public List<GameObject> GetActiveEthereals() {

        return _POOL_ACTIVE_ETHEREALS;
    }

    public List<GameObject> GetActiveAuraMinions() {

        return _POOL_ACTIVE_AURAMINIONS;
    }
}