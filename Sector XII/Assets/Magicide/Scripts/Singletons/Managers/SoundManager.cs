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
    public AudioSource _SFX_ButtonHover;
    public AudioSource _SFX_ButtonGoBack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DASH SFX")]
    [Header("")]
    public AudioSource _SFX_Dash1;
    public AudioSource _SFX_Dash2;
    public AudioSource _SFX_Dash3;
    public AudioSource _SFX_Dash4;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** ORB FIREBALL SFX")]
    [Header("")]
    public AudioSource _SFX_FireballAttack1;
    public AudioSource _SFX_FireballAttack2;
    public AudioSource _SFX_FireballAttack3;
    public AudioSource _SFX_FireballAttack4;
    public AudioSource _SFX_FireballImpact1;
    public AudioSource _SFX_FireballImpact2;
    public AudioSource _SFX_FireballImpact3;
    public AudioSource _SFX_FireballImpact4;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FLAMETHROWER SFX")]
    [Header("")]
    public AudioSource _SFX_FlamethrowerAttack1;
    public AudioSource _SFX_FlamethrowerAttack2;
    public AudioSource _SFX_FlamethrowerImpact1;
    public AudioSource _SFX_FlamethrowerImpact2;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** KILLTAG SFX")]
    [Header("")]
    public AudioSource _SFX_OnTagPickupMinion;
    public AudioSource _SFX_OnTagPickupSpeed;
    public AudioSource _SFX_OnTagPickupHealth;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** GEOMANCER DIALOGUE PACKS")]
    [Header("")]
    public GeomancerDialogue _SFX_GeomancerDialogue1;
    public GeomancerDialogue _SFX_GeomancerDialogue2;
    public GeomancerDialogue _SFX_GeomancerDialogue3;
    public GeomancerDialogue _SFX_GeomancerDialogue4;
    public GeomancerDialogue _SFX_GeomancerDialogue5;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CRYSTAL SFX")]
    [Header("")]
    public AudioSource _SFX_CrystalHit;
    public AudioSource _SFX_CrystalDeath1;
    public AudioSource _SFX_CrystalDeath2;
    public AudioSource _SFX_CrystalDeath3;
    public AudioSource _SFX_CrystalDeath4;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** MUSIC & AMBIENCE")]
    [Header("")]
    public AudioSource _MUSIC_MainMenu;
    public AudioSource _MUSIC_Gameplay;
    public AudioSource _AMBIENCE_MainMenu;
    public AudioSource _AMBIENCE_Gameplay;

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
    
}