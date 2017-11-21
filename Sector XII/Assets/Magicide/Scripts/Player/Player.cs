using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

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
    public XboxController _Controller;                              // The xbox gamepad associated with the player.

    /// Private
    private int _Score = 0;                                         // The player's individual score for the match.
    private int _KillCount = 0;                                     // Amount of kills a player has done throughout the match.
    private float _TimeAlive = 0;                                   // Total amount of time the player is alive for.
    private int _Placement = 1;                                     // What match placement the player is currently at.
    private int _RespawnsLeft;                                      // Amount of respawns left for the player.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Set the respawn cap to the player.
        if (PlayerManager._pInstance != null)
            _RespawnsLeft = PlayerManager._pInstance._Respawns;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        if (MatchManager._pInstance != null) {

            // If in gameplay
            if (MatchManager._pInstance.GetGameplay() == true) {

                // Detect pause input against indiviual player
                if (GetSpecialRightButton == true) {

                    if (_Controller != XboxController.All) {

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
        }
    }

    //--------------------------------------------------------------
    // *** SCORE ***

    public void SetScore(int amount) { _Score = amount; }

    public void AddScore(int amount) { _Score += amount; }

    public int GetScore() { return _Score; }

    public void AddKillCount() { _KillCount += 1; }

    public int GetKillCount() { return _KillCount; }

    public void AddTimeAlive(float amount) { _TimeAlive += amount; }

    public int GetMinutesAlive() { return (int)_TimeAlive / 60; }

    public int GetSecondsAlive() { return (int)_TimeAlive % 60; }

    public int GetPlacement() { return _Placement; }

    public void SetPlacement(int place) { _Placement = place; }

    public int GetRespawnsLeft() { return _RespawnsLeft; }

    public void SetRespawnsLeft(int value) { _RespawnsLeft = value; }

    public void DeductRespawn() { _RespawnsLeft -= 1; }

    //--------------------------------------------------------------
    // *** INPUT ***

    // Thumbsticks

    public Vector3 GetMovementInput {

        // Combines the horizontal & vertical input into 1 vector to use for directional movement
        get
        {
            ///return new Vector3(Input.GetAxis(string.Concat("LeftStick_X_P", _pPlayerID)), 0, Input.GetAxis(string.Concat("LeftStick_Y_P", _pPlayerID)));
            return new Vector3(XCI.GetAxisRaw(XboxAxis.LeftStickX, _Controller),0, XCI.GetAxisRaw(XboxAxis.LeftStickY, _Controller));
        }
    }

    public Vector3 GetRotationInput {

        // Gets directional rotation input
        get
        {
            ///return new Vector3(0, 90f + (Mathf.Atan2(Input.GetAxis(string.Concat("RightStick_Y_P", _pPlayerID)), Input.GetAxis(string.Concat("RightStick_X_P", _pPlayerID))) * 180 / Mathf.PI), 0);
            return new Vector3(0, 90f - (Mathf.Atan2(XCI.GetAxisRaw(XboxAxis.RightStickY, _Controller), XCI.GetAxisRaw(XboxAxis.RightStickX, _Controller)) * 180 / Mathf.PI), 0);
        }
    }

    public bool GetFireInput {

        // Uses thumbstick axis as button input
        get
        {
            ///return Input.GetAxis(string.Concat("FireX_P" + _pPlayerID)) != 0f || Input.GetAxis(string.Concat("FireY_P" + _pPlayerID)) != 0f;
            return XCI.GetAxisRaw(XboxAxis.RightStickX, _Controller) != 0f || XCI.GetAxisRaw(XboxAxis.RightStickY, _Controller) != 0f;
        }
    }

    // Triggers

    public Vector3 GetLeftTriggerInput {

        // Get the input axis amount as a Vector rotating left
        get
        {
            ///return new Vector3(0, -Input.GetAxis(string.Concat("LeftTrigger_P", _pPlayerID)), 0);
            return new Vector3(0, -XCI.GetAxisRaw(XboxAxis.LeftTrigger, _Controller), 0);
        }
    }

    public Vector3 GetRightTriggerInput {

        // Get the input axis amount as a Vector rotating right
        get
        {
            ///return new Vector3(0, Input.GetAxis(string.Concat("RightTrigger_P", _pPlayerID)), 0);
            return new Vector3(0, XCI.GetAxisRaw(XboxAxis.RightTrigger, _Controller), 0);
        }
    }

    // Specials

    public bool GetSpecialRightButton {

        // Returns if the special right button has been pressed
        get
        {
            ///return Input.GetButton("SpecialRight");
            return XCI.GetButtonDown(XboxButton.Start, _Controller);
        }
    }

    public bool GetSpecialLeftButton {

        // Returns if the special right button has been pressed
        get
        {
            ///return Input.GetButton("SpecialLeft");
            return XCI.GetButton(XboxButton.Back, _Controller);
        }
    }
    
    // Face buttons

    public bool GetFaceBottomInput {

        // Returns if the xbox A button has been pressed
        get
        {
            ///return Input.GetButton("FaceBottom");
            return XCI.GetButton(XboxButton.A, _Controller);
        }
    }

    public bool GetFaceTopInput {

        // Returns if the xbox Y button has been pressed
        get
        {
            ///return Input.GetButton("FaceTop");
            return XCI.GetButton(XboxButton.Y, _Controller);
        }
    }

    public bool GetFaceLeftInput {

        // Returns if the xbox X button has been pressed
        get
        {
            ///return Input.GetButton("FaceLeft");
            return XCI.GetButton(XboxButton.X, _Controller);
        }
    }

    public bool GetFaceRightInput {

        // Returns if the xbox B button has been pressed
        get
        {
            ///return Input.GetButton("FaceRight");
            return XCI.GetButton(XboxButton.B, _Controller);
        }
    }

    public bool GetLeftBumperInput {

        // Returns if the xbox LEFT bumper button has been pressed
        get
        {
            ///return Input.GetButton("FaceLeft");
            return XCI.GetButton(XboxButton.LeftBumper, _Controller);
        }
    }

    public bool GetRightBumperInput {

        // Returns if the xbox RIGHT bumper button has been pressed
        get
        {
            ///return Input.GetButton("FaceRight");
            return XCI.GetButton(XboxButton.RightBumper, _Controller);
        }
    }

    public bool GetLeftAxisUpInput {

        // Uses left thumbstick axis UP as a button input
        get
        {
            ///return Input.GetAxis(string.Concat("FireX_P" + _pPlayerID)) != 0f || Input.GetAxis(string.Concat("FireY_P" + _pPlayerID)) != 0f;
            return XCI.GetAxisRaw(XboxAxis.LeftStickY, _Controller) > 0f;
        }
    }

    public bool GetLeftAxisDownInput {

        // Uses left thumbstick axis DOWN as a button input
        get
        {
            return XCI.GetAxisRaw(XboxAxis.LeftStickY, _Controller) < 0f;
        }
    }
}