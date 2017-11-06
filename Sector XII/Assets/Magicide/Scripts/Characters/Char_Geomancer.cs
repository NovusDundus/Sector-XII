using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Geomancer : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public Transform _RespawnPoint;

    /// Private
    private bool _DashEnabled;
    private XboxCtrlrInput.XboxButton _DashInputButton = XboxCtrlrInput.XboxButton.B;
    private float _DashDistance;
    private float _DashCooldown;
    private float _CurrentDashCooldown = 0f;
    private bool _JustDashed = false;
    private float _TimeSinceLastDash = 0f;
    private bool _KnockbackEnabled;
    private XboxCtrlrInput.XboxButton _KnockbackInputButton = XboxCtrlrInput.XboxButton.Y;
    private float _KnockbackForceNormal;
    private float _KnockbackForceDash;
    private float _KnockbackCooldown;
    private float _CurrentKnockbackCooldown = 0f;
    private float _SpeedBoostModifier = 1f;
    private bool _SpeedBoostActive = false;
    private float _SpeedBoostTimer = 0f;
    private float _MovementSpeedModifier = 1f;
    private bool _TabbingWeapon = false;
    private XboxCtrlrInput.XboxButton _TabInputButton = XboxCtrlrInput.XboxButton.RightBumper;
    private LinearGoToTarget _LinearGoTo;

    /// Delegates / Events
    private delegate void CharacterAbility();
    CharacterAbility _Ability;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Start() {

        // Stores reference to the player associated with the character
        _Player = GetComponent<Player>();
        _LinearGoTo = GetComponent<LinearGoToTarget>();

        // Set character's health & get collision reference
        _StartingHealth = PlayerManager._pInstance._GeomancerStartingHealth;
        base.Awake();

        // Set character's speed
        _MovementSpeed = PlayerManager._pInstance._GeomancerMovementSpeed;

        // Set dash properties
        _DashEnabled = PlayerManager._pInstance._DashEnabled;
        _DashInputButton = PlayerManager._pInstance._DashButton;
        _DashDistance = PlayerManager._pInstance._DashDistance;
        _DashCooldown = PlayerManager._pInstance.DashCooldown;

        // Set knockback properties
        _KnockbackEnabled = PlayerManager._pInstance._KnockbackEnabled;
        _KnockbackInputButton = PlayerManager._pInstance._KnockbackButton;
        _KnockbackForceNormal = PlayerManager._pInstance._KnockbackForceNormal;
        _KnockbackForceDash = PlayerManager._pInstance.KnockbackForceDash;
        _KnockbackCooldown = PlayerManager._pInstance._KnockbackCooldown;

        // Set tabbing weapon input
        _TabInputButton = PlayerManager._pInstance._WeaponSwapButton;

        // Set's either the orb or the flamethrower as the primary depending on whats specified in the player manager
        switch (PlayerManager._pInstance._PrimaryWeapon) {

            case PlayerManager.WeaponList.Orb: {

                    // Create players's primary weapon (orb)
                    _WeaponPrimary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_OrbWeapon").GetComponent<Weapon>();
                    if (_WeaponPrimary != null) {

                        // Initialize PRIMARY weapon
                        _WeaponPrimary.SetOwner(this);
                        _WeaponPrimary.Init(); /// Create fireball object pool (inactive projectiles)
                    }
                    // Create players's secondary weapon (flamethrower)
                    _WeaponSecondary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_FlamethrowerWeapon").GetComponent<Weapon>();
                    if (_WeaponSecondary != null) {

                        // Initialize SECONDARY weapon
                        _WeaponSecondary.SetOwner(this);
                        _WeaponSecondary.Init(); /// Create flamethrower object pool (inactive projectiles)
                    }
                    break;
                }

            case PlayerManager.WeaponList.Flamethrower: {

                    // Create players's primary weapon (flamethrower)
                    _WeaponPrimary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_FlamethrowerWeapon").GetComponent<Weapon>();
                    if (_WeaponPrimary != null) {

                        // Initialize PRIMARY weapon
                        _WeaponPrimary.SetOwner(this);
                        _WeaponPrimary.Init(); /// Create flamethrower object pool (inactive projectiles)
                    }
                    // Create players's secondary weapon (orb)
                    _WeaponSecondary = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_OrbWeapon").GetComponent<Weapon>();
                    if (_WeaponSecondary != null) {

                        // Initialize SECONDARY weapon
                        _WeaponSecondary.SetOwner(this);
                        _WeaponSecondary.Init(); /// Create fireball object pool (inactive projectiles)
                    }
                    break;
                }

            default: {
                    break;
                }
        }

        // Create player's special weapon (shield)
        _WeaponSpecial = GameObject.FindGameObjectWithTag("P" + _Player._pPlayerID + "_ShieldWeapon").GetComponent<Weapon>();
        if (_WeaponSpecial != null) {

            // Initialize SPECIAL weapon
            _WeaponSpecial.SetOwner(this);
            _WeaponSpecial.Init(); /// Create minion object pool (empty)
        }
        // Can be controlled by player / ai controller
        SetActive(true);

        // Add the gameObject associated to this script to the playermanager object pools
        PlayerManager._pInstance.GetAliveNecromancers().Add(this);
        PlayerManager._pInstance.GetAllPlayers().Add(this);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public override void Update() {

        base.Update();

        // If in gameplay
        if (MatchManager._pInstance.GetGameplay() == true) {

            // Only proceed if the character is actively being possessed by a controller
            if (_Active == true) {

                _Collision.enabled = true;

                // ************************
                //   MOVEMENT CONTROLLER 
                // ************************

                // Character is receiving right stick input
                if (_Player.GetRotationInput != new Vector3(0, 90, 0)) {

                    // Get directional input (movement & rotation)
                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * (_MovementSpeed * _SpeedBoostModifier * _MovementSpeedModifier) * Time.fixedDeltaTime, Quaternion.Euler(_Player.GetRotationInput));
                }

                // Character is NOT receiving right stick input
                else { /// GetRotationInput == new Vector3(0, 90, 0)

                    // Get directional input (movement ONLY)
                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * (_MovementSpeed * _SpeedBoostModifier * _MovementSpeedModifier) * Time.fixedDeltaTime, transform.rotation);
                }

                // ************************
                //    FIRING CONTROLLER   
                // ************************

                // Detect firing input
                if (_Player.GetFireInput) {

                    if (_PrimaryWeaponActive == true) {

                        // Fire primary weapon (orb?)
                        _WeaponPrimary.Fire();

                    }

                    else { /// _PrimaryWeaponActive == false

                        // Fire secondary weapon (flamethrower?)
                        _WeaponSecondary.Fire();
                    }
                }

                // ************************
                //    ABILITY CONTROLLER   
                // ************************

                // KNOCKBACK ABILITY 
                if (_KnockbackEnabled == true) {

                    // Knockback ability cooldown in progress
                    if (_CurrentKnockbackCooldown > 0f) {

                        _CurrentKnockbackCooldown -= Time.deltaTime;

                        // Clamp to 0f
                        if (_CurrentKnockbackCooldown < 0f) {

                            _CurrentKnockbackCooldown = 0f;
                        }
                    }

                    // Knockback cooldown complete
                    else {

                        // Check for controller input
                        PerformActionFromInput(Knockback, _KnockbackInputButton);
                    }
                }

                // DASH ABILITY 
                if (_DashEnabled == true) {

                    // Dash ability cooldown in progress
                    if (_CurrentDashCooldown > 0f) {

                        _CurrentDashCooldown -= Time.deltaTime;

                        // Clamp to 0f
                        if (_CurrentDashCooldown < 0f) {

                            _CurrentDashCooldown = 0f;
                        }
                    }

                    // Dash cooldown complete
                    else {

                        // Check for controller input
                        PerformActionFromInput(Dash, _DashInputButton);
                    }

                    // Successful dash was just performed
                    if (_JustDashed == true) {

                        if (_TimeSinceLastDash < 1f) {

                            _TimeSinceLastDash += Time.deltaTime;
                        }
                        else {

                            _JustDashed = false;
                            _TimeSinceLastDash = 0f;
                        }
                    }
                }

                // TAB WEAPON ABILITY
                if (PlayerManager._pInstance._SecondaryWeaponEnabled == true) {

                    // Check for controller input
                    PerformActionFromInput(TabWeapons, _TabInputButton);

                    if (XboxCtrlrInput.XCI.GetButtonUp(_TabInputButton, _Player._Controller)) {

                        // Reset weapon tab
                        _TabbingWeapon = false;
                    }
                }

                // SPEED BOOST
                if (_SpeedBoostActive == true) {

                    // Deduct from speed boost timer (1 per second)
                    _SpeedBoostTimer -= Time.deltaTime;

                    // Boost complete
                    if (_SpeedBoostTimer <= 0f) {

                        // Disable speed boost
                        _SpeedBoostModifier = 1f;
                        _SpeedBoostActive = false;
                    }
                }
            }

            else { ///_Active == false

                // Disable collisions
                _Collision.enabled = false;
            }
        }
    }     
    
    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void Damage(float amount) {
        
        // Only damage character if match is in phase2
        if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {
            
            if (_Active == true)
                base.Damage(amount);
        }
    }

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // Kill all the minions associated with the character
   ///     Wep_Shield shield = _WeaponSecondary.GetComponent<Wep_Shield>();
   ///     foreach (var minion in shield.GetMeatMinionPool())
   ///     {
   ///         minion.GetComponent<Proj_ShieldMinion>().ForceDeath();
   ///     }
        // Reset score to 0
        ///_Player.SetScore(0);
        
        if (_Player.GetRespawnsLeft() > 0) {
            
            // Disable controller input
            _Active = false;

            // Deduct life from respawn cap
            _Player.DeductRespawn();

            // Move character to its respawn point
            gameObject.transform.position = _RespawnPoint.position;

            // Reset health
            _Health = _StartingHealth;

            // Move the character into the gameplay area
            _LinearGoTo.enabled = true;
        }

        // Player is out of lives and is eliminated from the match
        else {

            // Deduct life from respawn cap
            _Player.DeductRespawn();

            // hide character & move out of playable space
            GetComponentInChildren<Renderer>().enabled = false;
            transform.position = new Vector3(1000, 0, 1000);

            // Find self in active pool
            foreach (var necromancer in PlayerManager._pInstance.GetAliveNecromancers()) {

                // Once we have found ourself in the pool
                if (necromancer == this) {

                    // Move to inactive pool
                    PlayerManager._pInstance.GetDeadNecromancers().Add(necromancer);
                    PlayerManager._pInstance.GetAliveNecromancers().Remove(necromancer);
                    break;
                }
            }

            // Disable controller input
            _Active = false;
        }
    }

    public bool GetActive() {

        return _Active;
    }

    //--------------------------------------------------------------
    // *** ABILITIES ***

    private void PerformActionFromInput(CharacterAbility ability, XboxCtrlrInput.XboxButton button) {

        _Ability = ability;
        switch (button) {

            // Face button bottom (A)
            case XboxCtrlrInput.XboxButton.A: {

                    if (_Player.GetFaceBottomInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            // Face button right (B)
            case XboxCtrlrInput.XboxButton.B: {

                    if (_Player.GetFaceRightInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            // Face button left (X)
            case XboxCtrlrInput.XboxButton.X: {

                    if (_Player.GetFaceLeftInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            // Face button top (Y)
            case XboxCtrlrInput.XboxButton.Y: {

                    if (_Player.GetFaceTopInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            // Right bumper
            case XboxCtrlrInput.XboxButton.RightBumper: {

                    if (_Player.GetRightBumperInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            // Left bumper
            case XboxCtrlrInput.XboxButton.LeftBumper: {

                    if (_Player.GetLeftBumperInput) {

                        // Perform action
                        _Ability();
                    }
                    break;
                }

            default: {

                    break;
                }
        }
    }
    
    private void Dash() {

        // If dash cooldown is complete
        if (_CurrentDashCooldown <= 0f) {

            // If the character has minions they can dispense for the dash to proceed
            if (_WeaponSpecial.GetComponent<Wep_Shield>().GetMinionCount() > 0) {

                // Determine how far the character can teleport
                Vector3 DashPos = transform.position;
                Vector3 DashDirection = _Player.GetMovementInput;
                DashPos += DashDirection * _DashDistance;

                // Perform dash
                gameObject.transform.position = DashPos;
                _JustDashed = true;

                // Reset cooldown
                _CurrentDashCooldown = _DashCooldown;

                // Destroy minion from the shield
                _WeaponSpecial.GetComponent<Wep_Shield>().DestroyMinion();
            }
        }
    }

    public float GetDashCooldown() {

        return _CurrentDashCooldown;
    }

    private void Knockback() {

        // Create sphere collider to detect for the knockback
        SphereCollider knockbackCol = GetComponent<SphereCollider>();

        // Detect for any collisions with other player controlled characters
        foreach (Character playerCharacter in PlayerManager._pInstance.GetAliveNecromancers()) {

            // Dont test against ourself
            if (playerCharacter != this) {

                // If collider intersects with the character's collider
                if (knockbackCol.bounds.Intersects(playerCharacter.GetCollider().bounds)) {

                    if (_JustDashed == true) {

                        // Apply knockback effect (Dash version)
                        playerCharacter.GetComponent<Rigidbody>().AddForce(_Player.GetMovementInput * _KnockbackForceDash);
                    }

                    else { /// _JustDashed == false

                        // Apply knockback effect (Normal version)
                        playerCharacter.GetComponent<Rigidbody>().AddForce(_Player.GetMovementInput * _KnockbackForceNormal);
                    }
                    break;
                }
            }
        }
    }

    private void TabWeapons() {

        // Not currently tabbing weapons
        if (_TabbingWeapon == false) {

            if (_PrimaryWeaponActive == true) {

                // Swap to primary weapon
                _PrimaryWeaponActive = false;
            }

            else { /// _PrimaryWeaponActive == false

                // Swap to secondary weapon
                _PrimaryWeaponActive = true;
            }
            _TabbingWeapon = true;
        }
    }

    //--------------------------------------------------------------
    // *** KILLTAGS ***

    public void ActivateSpeedBoost(float SpeedModifier, float BoostTime) {

        // Set speed boost
        _SpeedBoostModifier = SpeedModifier;
        _SpeedBoostTimer = BoostTime;
        _SpeedBoostActive = true;
    }

    public bool IsSpeedBoost() {

        return _SpeedBoostActive;
    }

    public void AddHealth(int amount) {

        // Add health
        _Health += amount;

        // Clamp to max health
        if (_Health > _StartingHealth) {

            _Health = _StartingHealth;
        }
    }

}