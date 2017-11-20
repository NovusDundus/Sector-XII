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
    public LayerMask _DashLayer;

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
    private bool _WaitingToRespawn = false;
    private float _RespawnTimer = 0f;
    private float _TauntCooldown = 5f;
    private float _TauntTimer = 0f;
    private Dialog _CharacterDialog;
    private Rigidbody _RigidBody;
    private bool _Burning = false;
    private float _BurnTimer = 0f;
    private bool _Invincible = false;
    private float _InvincibleTimer = 0f;
    private float _InvincibleTime;
    private Material _InvincibleMaterial;
    private Animator _Animator;

    /// Delegates / Events
    private delegate void CharacterAbility();
    CharacterAbility _Ability;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public override void Start() {

        // Stores reference to the player associated with the character
        _Player = GetComponent<Player>();
        _LinearGoTo = GetComponent<LinearGoToTarget>();

        // Store rigidbody reference
        _RigidBody = GetComponent<Rigidbody>();

        // Store animator controller reference
        _Animator = GetComponent<Animator>();

        // Set character's health & get collision reference
        _StartingHealth = PlayerManager._pInstance._GeomancerStartingHealth;
        base.Start();
        
        // Set OnDeath effect
        _EffectOnDeath = PlayerManager._pInstance._OnDeathEffect;

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

        // Set invincibility properties
        _InvincibleTime = DeviceManager._pInstance._InvincibilityTime;
        _InvincibleMaterial = PlayerManager._pInstance._InvincibleMaterial;

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
        PlayerManager._pInstance.GetActiveNecromancers().Add(this);
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
                    transform.SetPositionAndRotation(transform.position + vec * (_MovementSpeed * _SpeedBoostModifier * _MovementSpeedModifier) * Time.deltaTime, Quaternion.Euler(_Player.GetRotationInput));
                }

                // Character is NOT receiving right stick input
                else { /// GetRotationInput == new Vector3(0, 90, 0)

                    // Get directional input (movement ONLY)
                    Vector3 vec = _Player.GetMovementInput.normalized;
                    transform.SetPositionAndRotation(transform.position + vec * (_MovementSpeed * _SpeedBoostModifier * _MovementSpeedModifier) * Time.deltaTime, transform.rotation);
                }

                // Used for animation blending
                _Animator.SetFloat("Forward", _Player.GetMovementInput.x);
                _Animator.SetFloat("Right", _Player.GetMovementInput.z);

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

                // TAUNT
                if (_Player.GetFaceLeftInput) {

                    // Taunt cooldown IS complete
                    if (_TauntTimer >= _TauntCooldown) {

                        // Play taunt sequence
                        Taunt();
                    }

                    // Taunt cooldown is NOT complete
                    else { /// _TauntTimer < _TauntCooldown

                        // Add to timer
                        _TauntTimer += Time.deltaTime;
                    }

                }                
            }

            // Movement controller is disabled for the character
            else { ///_Active == false

                // Disable collisions
                _Collision.enabled = false;
            }

            // The character is dead and waiting to respawn
            if (_WaitingToRespawn == true) {

                // Respawn timer is still counting
                if (_RespawnTimer < PlayerManager._pInstance._RespawnTime) {

                    // Add to timer
                    _RespawnTimer += Time.deltaTime;
                }

                // Respawn timer has finished
                else { /// _RespawnTimer >= PlayerManager._pInstance._RespawnTime
                                        
                    // Move the character into the gameplay area
                    _LinearGoTo.enabled = true;

                    // Reset health & respawn timer
                    _Health = _StartingHealth;
                    _RespawnTimer = 0f;
                    _WaitingToRespawn = false;
                }
            }

            // If character is in burning state
            if (_Burning == true) {

                // Apply damage
                Damage(null, WeaponManager._pInstance._BurnDamage);

                // Add to timer
                if (_BurnTimer < WeaponManager._pInstance._BurnTime) {

                    _BurnTimer += Time.deltaTime;
                }

                // Burning timer is complete
                else { /// _BurnTimer >= WeaponManager._pInstance._BurnTime

                    _Burning = false;
                    _BurnTimer = 0f;
                }
            }

            // If character is in an invincibility state
            if (_Invincible == true) {

                // Set material
                if (_InvincibleMaterial != null)
                    _MeshRenderer.material = _InvincibleMaterial;

                // Add to timer
                if (_InvincibleTimer < _InvincibleTime) {

                    _InvincibleTimer += Time.deltaTime;
                }

                // Timer is complete
                else { /// _InvincibleTimer >= _InvincibleTime

                    // Return to normal
                    _Invincible = false;
                    _InvincibleTimer = 0f;
                    _MeshRenderer.material = _OriginalMaterial;
                }
            }
        }
    }

    private void FixedUpdate() {

        // Attempting to fix the weird physics glitch
        _RigidBody.maxAngularVelocity = 0f;
    }

    //--------------------------------------------------------------
    // *** HEALTH & DAMAGE ***

    public override void Damage(Character instigator, float amount) {
        
        // Only damage character if match is in phase2
        if (MatchManager._pInstance.GetGameState() == MatchManager.GameState.Phase2) {

            // Can only damage an actively controller player character & if its NOT currently invincible
            if (_Active == true && !_Invincible) {

                base.Damage(instigator, amount);

                // Play grunt sound
                if (_CharacterDialog != null)
                    _CharacterDialog.PlayOnHit();
            }
        }
    }

    public override void OnDeath(Character instigator) {

        // Get last known alive position and store it
        base.OnDeath(instigator);

        // Destroy all minions in the character's shield
        Wep_Shield shield = _WeaponSpecial.GetComponent<Wep_Shield>();
        foreach (var item in shield.GetMeatMinionPool()) {

            Proj_ShieldMinion minion = item.GetComponent<Proj_ShieldMinion>();
            minion.ForceDeath();
        }

        // Play OnDeath effect
        if (_EffectOnDeath != null)
            Instantiate(_EffectOnDeath, transform.position, Quaternion.identity);
        
        if (_Player.GetRespawnsLeft() > 0) {
            
            // Disable controller input
            _Active = false;

            // Deduct life from respawn cap
            _Player.DeductRespawn();

            // Move character to its respawn point
            gameObject.transform.position = _RespawnPoint.position;

            // Start respawn timer
            _WaitingToRespawn = true;
        }

        // Player is out of lives and is eliminated from the match
        else {

            // Deduct life from respawn cap
            _Player.DeductRespawn();

            // hide character & move out of playable space
            GetComponentInChildren<Renderer>().enabled = false;
            transform.position = new Vector3(1000, 0, 1000);

            // Find self in active pool
            foreach (var necromancer in PlayerManager._pInstance.GetActiveNecromancers()) {

                // Once we have found ourself in the pool
                if (necromancer == this) {

                    // Move to inactive pool
                    PlayerManager._pInstance.GetEliminatedNecromancers().Add(necromancer);
                    PlayerManager._pInstance.GetActiveNecromancers().Remove(necromancer);
                    break;
                }
            }
            
            // Set our match placement depending on how many other players are still alive in the game
            int playersRemaining = PlayerManager._pInstance.GetActiveNecromancers().Count;
            _Player.SetPlacement(playersRemaining + 1);

            // Disable controller input
            _Active = false;
        }

        // Instigator plays a taunt
        if (instigator.GetComponent<Char_Geomancer>().GetDialog() != null)
            instigator.GetComponent<Char_Geomancer>().GetDialog().PlayTaunt();

        // Play death sound
        if (_CharacterDialog != null)
            _CharacterDialog.PlayOnDeath();
    }

    public bool GetActive() {

        return _Active;
    }
    
    public void SetBurnState(bool value) {

        _Burning = value;
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

                // Raycast to determine how far the character can teleport
                Vector3 DashPos = transform.position;
                Vector3 DashDirection = _Player.GetMovementInput;
                DashDirection.Normalize();

                RaycastHit hit;
                // Raycast hit an object
                if (Physics.Raycast(DashPos, DashDirection.normalized, out hit, _DashDistance, _DashLayer)) {

                    DashPos = hit.point;
                    Debug.DrawLine(DashPos, hit.point, Color.red);
                }

                // Raycast did not hit 
                else {

                    DashPos += DashDirection * _DashDistance;
                }
                
                // Perform dash
                gameObject.transform.position = DashPos;
                _JustDashed = true;

                // Reset cooldown
                _CurrentDashCooldown = _DashCooldown;

                // Destroy minion from the shield
                _WeaponSpecial.GetComponent<Wep_Shield>().DestroyMinion();

                // Play sound effect
                SoundManager._pInstance.PlayDash();
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
        foreach (Character playerCharacter in PlayerManager._pInstance.GetActiveNecromancers()) {

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

    private void Taunt() {

        // Play a taunt sound
        if (_CharacterDialog != null) {

            _CharacterDialog.PlayTaunt();
            _TauntTimer = 0f;
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

    public bool IsSpeedBoost() { return _SpeedBoostActive; }

    public void AddHealth(int amount) {

        // Add health
        _Health += amount;

        // Clamp to max health
        if (_Health > _StartingHealth) {

            _Health = _StartingHealth;
        }
    }

    public void ActivateInvincibility() {

        // Reset the invincibility
        _InvincibleTimer = 0f;
        _Invincible = true;
    }

    public bool IsInvincible() { return _Invincible; }

    //--------------------------------------------------------------
    // *** SOUND ***

    public Dialog GetDialog() {

        return _CharacterDialog;
    }

}