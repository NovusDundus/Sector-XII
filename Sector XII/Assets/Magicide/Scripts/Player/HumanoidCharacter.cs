using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCharacter : PlayerCharacter {

    //--------------------------------------
    // VARIABLES



    //--------------------------------------
    // FUNCTIONS

    public override void Start() {

        // Stores reference to the player controller
        _Player = GetComponent<Player>();

        // Set character's speed
        _MovementSpeed = PlayerManager._pInstance._pHumanoidSpeed;
        _RotationSpeed = PlayerManager._pInstance._pHumanoidRotationRate;

        // Set character's health
        _Health = PlayerManager._pInstance._pHumanoidStartingHealth;

        // Create character's weapon
        _Weapon = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_Orb").GetComponent<Weapon>();
        _Weapon.SetOwner(this);
        _Weapon.Init(); /// Create fireball object pool

        // Active by default
        SetActive(false);
    }

    public override void Update() {

        base.Update();
    }

    public override void FixedUpdate() {

        // Detect firing input
        base.FixedUpdate();

        // Only proceed if the character is actively being controller in the world
        if (_pActive == true) {

            // Move character based of controller input * character movement speed
            ///transform.Translate(GetMovementInput * Time.deltaTime * _MovementSpeed);
            ///transform.position.Set(transform.position.x + GetMovementInput.x, transform.position.y + GetMovementInput.y, transform.position.z + GetMovementInput.z);

            // Rotate character based of last known controller input
            if (GetRotationInput != new Vector3(0, 0, 0)) {

                ///transform.SetPositionAndRotation(transform.position + GetMovementInput, Quaternion.Euler(GetRotationInput));
                ///transform.rotation = Quaternion.Euler(GetRotationInput);
            }

            ///transform.Rotate(GetRotationInput * Time.deltaTime * _RotationSpeed);
            ///transform.eulerAngles = GetRotationInput;

            // Placeholder movement controller (DOESNT RELY ON SPEED, JUST PURE CONTROLLER INPUT
            if (GetRotationInput != new Vector3(0, 0, 0)) {

                transform.SetPositionAndRotation(transform.position + GetMovementInput / 4, Quaternion.Euler(GetRotationInput));
            }
                        
            else { // GetRotationInput == new Vector3(0, 0, 0)

                transform.SetPositionAndRotation(transform.position + GetMovementInput / 4, transform.rotation);
            }
        }
    }

    public override void OnDeath() {

        // Do default death related things
        base.OnDeath();

        // Change to ethereal faction
        _Player.SetTeam(MatchManager.Team.Ethereal);

        // Transition into ethereal character

        // Get ethereal character matched to player ID
        GameObject obj = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID +"_Ethereal");

        /// *** PLAY TRANSITION EFFECTS AND PLAY HUMANOID DEATH ANIMATION ***    

        // Move ethereal character to humanoid's death location
        obj.transform.SetPositionAndRotation(_DeathPosition, transform.rotation);

        /// *** PLAY ETHEREAL RISE ANIMATION ***

        // Move humanoid character to out of bounds
        transform.position = new Vector3(0, 0, 0);
    }

    public override void FireInput() {
        
        // Only initiate if there is a valid weapon attached to the character
        if (_Weapon != null) {

            // Shoot weapon
            _Weapon.Fire();
        }
    }
}