using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (internal)
    [HideInInspector]
    public Player _Player;                                          // Reference to the player controller that controls this character.
    public Material _DamagedMaterial;                               // The material that is shown on the character when receiving damage.

    /// Protected
    protected bool _Active;                                         // Represents if the character is current being controller by its player controller.
    protected int _StartingHealth;                                  // The health of the character upon Startup().
    protected int _Health;                                          // Current health of the character.
    protected Weapon _WeaponPrimary;                                // Current primary weapon being owned by the character.
    protected Weapon _WeaponSecondary;                              // Current secondary weapon being owned by the character.
    protected float _MovementSpeed;                                 // The walking speed of the character.
    protected float _RotationSpeed;                                 // The rotating speed of the character.
    protected Vector3 _DeathPosition;                               // World location point of where the character was killed.
    protected Vector3 _CurrentRotationInput;                        // Current Vector in the world that is stored by the gamepad axis.
    protected Collider _Collision;                                  // The collision associated with the character.
    protected MeshRenderer _MeshRenderer;                           // Reference to the character's mesh renderer.
    protected Material _OriginalMaterial;                           // Reference to the mesh renderer's original material.
    protected float _ImpactFlashTimer = 0f;
    protected bool _ReceivingDamage = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public virtual void Start() {

        // Set the starting health for the character
        _Health = _StartingHealth;

        // Get reference to collision
        _Collision = GetComponent<Collider>();

        // Store the original material so it can be reverted back on the mesh renderer later
        _MeshRenderer = GetComponent<MeshRenderer>();
        _OriginalMaterial = _MeshRenderer.material;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public virtual void Update() {
        
    }

    public virtual void FixedUpdate() {

        if (_ReceivingDamage == true) {

            if (_ImpactFlashTimer > 0f) {

                _ImpactFlashTimer -= Time.fixedDeltaTime * 20;
            }

            else {

                _ReceivingDamage = false;
            }
        }
        else {

            _MeshRenderer.material = _OriginalMaterial;
        }
    }

    // -------------------------------------------------------------
    // *** INPUT ***

    public void SetActive(bool active) {

        // Set the character's active state based on the parameter
        _Active = active;
    }

    public void SetController(Player controller) {

        // Set the character's controller based on the parameter
        _Player = controller;
    }

    public float GetMovementSpeed() {

        return _MovementSpeed;
    }

    // -------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public int GetStartingHealth() {

        // Returns the starting health associated with the character
        return _StartingHealth;
    }

    public int GetHealth() {

        // Returns the current amount of health associated with the character
        return _Health;
    }

    public virtual void Damage(float amount) {

        // Material change for feedback on impact
        if (_DamagedMaterial != null) {

            _MeshRenderer.material = _DamagedMaterial;
            _ReceivingDamage = true;
            _ImpactFlashTimer = 1f;
        }

        // Damage character based on amount passed through
        _Health -= (int)amount;

        // Returns TRUE if character has no health
        if (_Health <= 0) {

            // Character has died
            OnDeath();
        }
    }

    public virtual void OnDeath() {

        // Get reference to character's death location in the world
        _DeathPosition = transform.position;
    }

    public Collider GetCollider() {

        return _Collision;
    }

    // -------------------------------------------------------------
    // *** COMBAT ***

    public Weapon GetPrimaryWeapon() {

        return _WeaponPrimary;
    }

    public Weapon GetSecondaryWeapon() {

        return _WeaponSecondary;
    }

}