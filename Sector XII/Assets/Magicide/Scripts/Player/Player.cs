using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public int _pPlayerID = 0;                                      // ID Reference of the individual player.
    public LayerMask Layers;                                        // Layers associated with the player.

    /// Private
    private int _Score = 0;                                         // The player's individual score for the match.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        if (GetSpecialLeftButton) {

            Debug.Log("Special Left");
        }
        if (GetSpecialRightButton) {

            Debug.Log("Special Right");
        }
        if (GetFaceBottomInput) {

            Debug.Log("A button");
        }
        if (GetFaceTopInput) {

            Debug.Log("Y button");
        }
        if (GetFaceLeftInput) {

            Debug.Log("X button");
        }
        if (GetFaceRightInput) {

            Debug.Log("B button");
        }
    }

    public void FixedUpdate() {

        // If in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Detect pause input
            if (GetSpecialRightButton == true) {

                // Game is currently in a PAUSED state
                if (MatchManager._pInstance.GetPaused() == true) {

                    // Unpause game
                    MatchManager._pInstance.SetPause(false);
                }

                // Game is currently in an UNPAUSED state
                else { /// MatchManager._pInstance.GetPaused() == true

                    // Pause game
                    MatchManager._pInstance.SetPause(true);
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** SCORE ***

    public void SetScore(int amount) {

        // set the player's score to match the parameter
        _Score = amount;
    }

    public void AddScore(int amount) {

        // Add 'x' points to the player's score
        _Score += amount;
    }

    public int GetScore() {

        // Returns the current score of the player
        return _Score;
    }

    //--------------------------------------------------------------
    // *** INPUT ***

    // Thumbsticks

    public Vector3 GetMovementInput {

        // Combines the horizontal & vertical input into 1 vector to use for directional movement
        get
        {
            return new Vector3(Input.GetAxis(string.Concat("LeftStick_X_P", _pPlayerID)), 0, Input.GetAxis(string.Concat("LeftStick_Y_P", _pPlayerID)));
        }
    }

    public Vector3 GetRotationInput {

        // Gets directional rotation input
        get
        {
            return new Vector3(0, 90f + (Mathf.Atan2(Input.GetAxis(string.Concat("RightStick_Y_P", _pPlayerID)), Input.GetAxis(string.Concat("RightStick_X_P", _pPlayerID))) * 180 / Mathf.PI), 0);
        }
    }

    public bool GetFireInput {

        // Uses thumbstick axis as button input
        get
        {
            return Input.GetAxis(string.Concat("FireX_P" + _pPlayerID)) != 0f || Input.GetAxis(string.Concat("FireY_P" + _pPlayerID)) != 0f;
        }
    }

    // Triggers

    public Vector3 GetLeftTriggerInput {

        // Get the input axis amount as a Vector rotating left
        get
        {
            return new Vector3(0, -Input.GetAxis(string.Concat("LeftTrigger_P", _pPlayerID)), 0);
        }
    }

    public Vector3 GetRightTriggerInput {

        // Get the input axis amount as a Vector rotating right
        get
        {
            return new Vector3(0, Input.GetAxis(string.Concat("RightTrigger_P", _pPlayerID)), 0);
        }
    }

    // Specials

    public bool GetSpecialRightButton {

        // Returns if the special right button has been pressed
        get
        {
            return Input.GetButton("SpecialRight");
        }
    }

    public bool GetSpecialLeftButton {

        // Returns if the special right button has been pressed
        get
        {
            return Input.GetButton("SpecialLeft");
        }
    }
    
    // Face buttons

    public bool GetFaceBottomInput {

        // Returns if the xbox A button has been pressed
        get
        {
            return Input.GetButton("FaceBottom");
        }
    }

    public bool GetFaceTopInput {

        // Returns if the xbox Y button has been pressed
        get
        {
            return Input.GetButton("FaceTop");
        }
    }

    public bool GetFaceLeftInput {

        // Returns if the xbox X button has been pressed
        get
        {
            return Input.GetButton("FaceLeft");
        }
    }

    public bool GetFaceRightInput {

        // Returns if the xbox B button has been pressed
        get
        {
            return Input.GetButton("FaceRight");
        }
    }
    
}