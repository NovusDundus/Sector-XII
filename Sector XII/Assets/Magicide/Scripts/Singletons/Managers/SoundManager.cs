using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers) 
    [Header("---------------------------------------------------------------------------")]
    [Header("*** BUTTON SFX")]
    [Header("")]
    public AudioSource _SFX_ButtonClick;
    public float _DB_ButtonClick;
    [Header("")]
    public AudioSource _SFX_ButtonHover;
    public float _DB_ButtonHover;
    [Header("")]
    public AudioSource _SFX_ButtonGoBack;
    public float _DB_ButtonGoBack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DASH SFX")]
    [Header("")]
    public List<AudioSource> _SFX_Dash;
    public List<float> _DB_Dash;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** ORB FIREBALL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FireballAttack;
    public List<float> _DB_FireballAttack;
    [Header("")]
    public List<AudioSource> _SFX_FireballImpact;
    public List<float> _DB_FireballImpact;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FLAMETHROWER SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FlamethrowerAttack;
    public List<float> _DB_FlamethrowerAttack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DEVICE SFX")]
    [Header("")]
    public List<AudioSource> _SFX_OnTeleport;
    public List<float> _DB_OnTeleport;
    [Header("")]
    public List<AudioSource> _SFX_OnTagPickupMinion;
    public List<float> _DB_OnTagPickupMinion;
    [Header("")]
    public List<AudioSource> _SFX_OnTagPickupSpeed;
    public List<float> _DB_OnPickupSpeed;
    [Header("")]
    public List<AudioSource> _SFX_OnTagPickupHealth;
    public List<float> _DB_OnPickupHealth;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CRYSTAL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_CrystalHit;
    public List<float> _DB_CrystalHit;
    [Header("")]
    public List<AudioSource> _SFX_CrystalDeath;
    public List<float> _DB_CrystalDeath;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** MUSIC & AMBIENCE")]
    [Header("")]
    public AudioSource _MUSIC_MainMenu;
    public float _DB_MUSIC_MainMenu;
    [Header("")]
    public AudioSource _MUSIC_Gameplay;
    public float _DB_MUSIC_Gameplay;
    [Header("")]
    public AudioSource _AMBIENCE_MainMenu;
    public float _DB_AMBIENCE_MainMenu;
    [Header("")]
    public AudioSource _AMBIENCE_Gameplay;
    public float _DB_AMBIENCE_Gameplay;

    /// Public (internal)
    [HideInInspector]
    public static SoundManager _pInstance;                          // This is a singleton script, Initialized in Startup().

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundInt(List<AudioSource> SoundList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, SoundList.Count);
        return i;
    }

    /// -------------------------------------------
    /// 
    ///     BUTTON SFX 
    /// 
    /// -------------------------------------------

    public void PlayButtonClick() {

        _SFX_ButtonClick.volume = _DB_ButtonClick;
        _SFX_ButtonClick.Play();
    }

    public void PlayButtonHover() {

        _SFX_ButtonHover.volume = _DB_ButtonHover;
        _SFX_ButtonHover.Play();
    }

    public void PlayButtonGoBack() {

        _SFX_ButtonGoBack.volume = _DB_ButtonGoBack;
        _SFX_ButtonGoBack.Play();
    }

    /// -------------------------------------------
    /// 
    ///     DASH SFX 
    /// 
    /// -------------------------------------------

    public void PlayDash() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_Dash);
        AudioSource sound = _SFX_Dash[i];

        // Play the sound
        sound.volume = _DB_Dash[i];
        sound.Play();
    }

    /// -------------------------------------------
    /// 
    ///     ORB FIREBALL SFX 
    /// 
    /// -------------------------------------------

    public void PlayFireballAttack() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_FireballAttack);
        AudioSource sound = _SFX_FireballAttack[i];

        // Play the sound
        sound.volume = _DB_FireballAttack[i];
        sound.Play();
    }

    public void PlayFireballImpact() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_FireballImpact);
        AudioSource sound = _SFX_FireballImpact[i];

        // Play the sound
        sound.volume = _DB_FireballImpact[i];
        sound.Play();
    }

    /// -------------------------------------------
    /// 
    ///     FLAMETHROWER SFX 
    /// 
    /// -------------------------------------------

    public void PlayFlamethrowerAttack() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_FlamethrowerAttack);
        AudioSource sound = _SFX_FlamethrowerAttack[i];

        // Play the sound
        sound.volume = _DB_FlamethrowerAttack[i];
        sound.Play();
    }

    /// -------------------------------------------
    /// 
    ///     DEVICE SFX 
    /// 
    /// -------------------------------------------

    public void PlayTeleport() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_OnTeleport);
        AudioSource sound = _SFX_OnTeleport[i];

        // Play the sound
        sound.volume = _DB_OnTeleport[i];
        sound.Play();
    }

    public void PlayPickupMinion() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_OnTagPickupSpeed);
        AudioSource sound = _SFX_OnTagPickupSpeed[i];

        // Play the sound
        sound.volume = _DB_OnPickupSpeed[i];
        sound.Play();
    }

    public void PlayPickupSpeedBoost() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_OnTagPickupMinion);
        AudioSource sound = _SFX_OnTagPickupMinion[i];

        // Play the sound
        sound.volume = _DB_OnTagPickupMinion[i];
        sound.Play();
    }

    public void PlayPickupHealthpack() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_OnTagPickupHealth);
        AudioSource sound = _SFX_OnTagPickupHealth[i];

        // Play the sound
        sound.volume = _DB_OnPickupHealth[i];
        sound.Play();
    }

    /// -------------------------------------------
    ///     
    ///     CRYSTAL SFX 
    /// 
    /// -------------------------------------------

    public void PlayCrystalHit() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_CrystalHit);
        AudioSource sound = _SFX_CrystalHit[i];

        // Play the sound
        sound.volume = _DB_CrystalHit[i];
        sound.Play();
    }

    public void PlayCrystalDeath() {

        // Get random sound from list
        int i = RandomSoundInt(_SFX_CrystalDeath);
        AudioSource sound = _SFX_CrystalDeath[i];

        // Play the sound
        sound.volume = _DB_CrystalDeath[i];
        sound.Play();
    }

    /// -------------------------------------------
    ///     
    ///     MUSIC & AMBIENCE
    /// 
    /// -------------------------------------------

    public void PlayMusicMainMenu() {

        _MUSIC_MainMenu.volume = _DB_MUSIC_MainMenu;
        _MUSIC_MainMenu.Play();
    }

    public void PlayMusicGameplay() {

        _MUSIC_Gameplay.volume = _DB_MUSIC_Gameplay;
        _MUSIC_Gameplay.Play();
    }

    public void PlayAmbienceMainMenu() {

        _AMBIENCE_MainMenu.volume = _DB_AMBIENCE_MainMenu;
        _AMBIENCE_MainMenu.Play();
    }

    public void PlayAmbienceGameplay() {

        _AMBIENCE_Gameplay.volume = _DB_AMBIENCE_Gameplay;
        _AMBIENCE_Gameplay.Play();
    }

}