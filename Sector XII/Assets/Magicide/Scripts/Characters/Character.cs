using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    [HideInInspector]
    public Player _Player;                                          // Reference to the player controller that controls this character.

    protected bool _Active;                                         // Represents if the character is current being controller by its player controller.
    protected int _StartingHealth;                                  // The health of the character upon Startup().
    protected int _Health;                                          // Current health of the character.
    protected Weapon _WeaponPrimary;                                // Current primary weapon being owned by the character.
    protected Weapon _WeaponSecondary;                              // Current secondary weapon being owned by the character.
    protected float _MovementSpeed;                                 // The walking speed of the character.
    protected float _RotationSpeed;                                 // The rotating speed of the character.
    protected Vector3 _DeathPosition;                               // World location point of where the character was killed.
    protected Vector3 _CurrentRotationInput;                        // Current Vector in the world that is stored by the gamepad axis.

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public virtual void Start() {

        // Set the starting health for the character
        _Health = _StartingHealth;
    }

    //--------------------------------------------------------------
    // FRAME

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

    }

    // -------------------------------------------------------------
    // INPUT

    public void SetActive(bool active) {

        // Set the character's active state based on the parameter
        _Active = active;
    }

    public void SetController(Player controller) {

        // Set the character's controller based on the parameter
        _Player = controller;
    }

    public Vector3 GetMovementInput {

        // Combines the horizontal & vertical input into 1 vector to use for directional movement
        get
        {
            return new Vector3(Input.GetAxis(string.Concat("LeftStick_X_P", _Player._pPlayerID)), 0, Input.GetAxis(string.Concat("LeftStick_Y_P", _Player._pPlayerID)));
        }
    }

    public Vector3 GetRotationInput {

        // Gets directional rotation input
        get
        {
            return new Vector3(0, Mathf.Atan2(Input.GetAxis(string.Concat("RightStick_Y_P", _Player._pPlayerID)), Input.GetAxis(string.Concat("RightStick_X_P", _Player._pPlayerID))) * 180 / Mathf.PI, 0);
        }
    }

    public Vector3 GetLeftTriggerInput {

        // Get
        get
        {
            return new Vector3(0, 0, 0);
        }
    }

    public Vector3 GetRightTriggerInput {

        // Get
        get
        {
            return new Vector3(0, 0, 0);
        }
    }

    // -------------------------------------------------------------
    // HEALTH & DAMAGE

    public int GetStartingHealth() {

        // Returns the starting health associated with the character
        return _StartingHealth;
    }

    public int GetHealth() {

        // Returns the current amount of health associated with the character
        return _Health;
    }

    public void Damage(int amount) {

        // Damage character based on amount passed through
        _Health -= amount;

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

    // -------------------------------------------------------------
    // COMBAT

}