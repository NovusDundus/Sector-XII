using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    //--------------------------------------
    // VARIABLES

    [HideInInspector]
    public Player _Player;                                  // Reference to the player controller that controls this character.

    protected bool _pActive = true;                                // Represents if the character is current being controller by its player controller.
    protected int _Health;                                  // Current health of the character.
    protected Weapon _Weapon;                               // Current weapon being instigated by the character.
    protected float _MovementSpeed;                         // The walking speed of the character.
    protected float _RotationSpeed;                         // The rotating speed of the character.
    protected Vector3 _DeathPosition;                       // World location point of where the character was killed.
    protected Vector3 _CurrentRotationInput;                // Current Vector in the world that is stored by the gamepad axis.

    //--------------------------------------
    // FUNCTIONS

    public virtual void Start() {

    }

    public virtual void Update() {

    }

    public virtual void FixedUpdate() {

        // Detect for firing input
        if (Input.GetAxis(string.Concat("Fire_P", _Player._pPlayerID)) > 0) {

            // Execute firing definition
            FireInput();
        }
    }

    public virtual void OnDeath() {

        // Get reference to character's death location in the world
        _DeathPosition = transform.position;
    }

    public virtual void FireInput() {

    }

    public Vector3 GetMovementInput {

        // Combines the horizontal & vertical input into 1 vector to use for directional movement
        get
        {
            return new Vector3(Input.GetAxis(string.Concat("LeftStick_X_P", _Player._pPlayerID)), 0, Input.GetAxis(string.Concat("LeftStick_Y_P", _Player._pPlayerID)));
        }
    }

    public Vector3 GetRotationInput {

        // Gets facing rotation input
        get
        {
            return new Vector3(0, Mathf.Atan2(Input.GetAxis(string.Concat("RightStick_Y_P", _Player._pPlayerID)), Input.GetAxis(string.Concat("RightStick_X_P", _Player._pPlayerID))) * 180 / Mathf.PI, 0);
        }
    }

    public void SetController(Player controller) {

        // Set the character's controller based on the parameter
        _Player = controller;
    }

    public void SetActive(bool active) {

        // Set the character's active state based on the parameter
        _pActive = active;
    }

    public void Damage(int amount) {

        // Damage character based on amount passed through
        _Health -= amount;

        // Check if character is no health
        if (_Health <= 0) {

            // Character has died
            OnDeath();
        }
    }
}