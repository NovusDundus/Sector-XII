using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed) 
    [Header("---------------------------------------------------------------------------")]
    [Header("*** MATCH ANNOUNCER")]
    [Header("")]
    public bool _EnableAnnouncer = false;
    public GameAnnouncer _Announcer;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** BUTTON SFX")]
    [Header("")]
    public AudioSource _SFX_ButtonClick;
    public AudioSource _SFX_ButtonHover;
    public AudioSource _SFX_ButtonGoBack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DASH SFX")]
    [Header("")]
    public List<AudioSource> _SFX_Dash;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** ORB FIREBALL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FireballAttack;
    public List<AudioSource> _SFX_FireballImpact;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FLAMETHROWER SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FlamethrowerAttack;
    [Header("")]
    public List<AudioSource> _SFX_WeaponTabbing;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DEVICE SFX")]
    [Header("")]
    public List<AudioSource> _SFX_OnTeleport;
    [Header("")]
    public List<AudioSource> _SFX_OnTagPickupMinion;
    public List<AudioSource> _SFX_OnTagPickupSpeed;
    public List<AudioSource> _SFX_OnTagPickupHealth;
    public List<AudioSource> _SFX_OnTagPickupInvincibility;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CRYSTAL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_CrystalHit;
    public List<AudioSource> _SFX_CrystalDeath;
    public List<AudioSource> _SFX_CrystalUpdate;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** MUSIC & AMBIENCE")]
    [Header("")]
    public AudioSource _MUSIC_MainMenu;
    public AudioSource _MUSIC_Gameplay;
    [Header("")]
    public AudioSource _AMBIENCE_MainMenu;
    public AudioSource _AMBIENCE_Gameplay;
    [Header("")]
    public AudioSource _SFX_PhaseTransition;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CHARACTER DIALOG")]
    [Header("")]
    public List<Dialog> _VOX_Dialoglist;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FACE TREE DIALOG")]
    [Header("")]
    public List<AudioSource> _VOX_FaceTreeNorthDialoglist;
    public List<AudioSource> _VOX_FaceTreeSouthDialoglist;

    /// Public (internal)
    [HideInInspector]
    public static SoundManager _pInstance;                          

    /// Private 
    private bool _IsPlayingVoxel = false;
    private List<AudioWrapper> _VoxelWaitingList;
    private float _TimeSinceLastVoxel = 0f;
    private List<bool> _DialogsUse;
    private bool _FaceTreeSoundIsPlaying = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    private void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    private void Start() {

        _VoxelWaitingList = new List<AudioWrapper>();
        _DialogsUse = new List<bool>();

        for (int i = 0; i < _VOX_Dialoglist.Count; i++) {

            // Dialog isnt used by default
            _DialogsUse.Add(false);
        }
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update() {

        // If there are voxel sounds waiting to be played
        if (_VoxelWaitingList.Count > 0) {

            if (_IsPlayingVoxel == true) {

                // Find the voxel sound that is current playing
                AudioWrapper vox = null;
                foreach (var sound in _VoxelWaitingList) {

                    AudioSource source = sound._SoundSource;

                    // If a sound from the voxel list is playing
                    if (source.isPlaying == true) {

                        // Then a voxel is playing
                        vox = sound;

                        if (vox._Owner != null)
                            vox._Owner.GetComponent<Char_Geomancer>().GetDialog().SetIsTaunting(true);
                    }
                    break;
                }

                _IsPlayingVoxel = vox != null;
            }

            // A vox has finished playing
            else { /// _IsPlayingVoxel == false

                // Character is no longer taunting
                if (_VoxelWaitingList[0]._Owner != null)
                    _VoxelWaitingList[0]._Owner.GetComponent<Char_Geomancer>().GetDialog().SetIsTaunting(false);
                
                // Get the last voxel that was playing (should be at the front of the list) & remove it from the queue
                _VoxelWaitingList.RemoveAt(0);

                // If there are still voxels in the queue
                if (_VoxelWaitingList.Count > 0) {

                    // Play the next vox sound in the queue
                    _VoxelWaitingList[0]._SoundSource.Play();

                    if (_VoxelWaitingList[0]._Owner != null)
                        _VoxelWaitingList[0]._Owner.GetComponent<Char_Geomancer>().GetDialog().SetIsTaunting(true);

                    _IsPlayingVoxel = true;
                    _TimeSinceLastVoxel = 0f;
                }
            }
        }

        // No more voxels are left in the playing queue
        else if (_VoxelWaitingList.Count == 0) {

            // Add to timer
            _TimeSinceLastVoxel += Time.deltaTime;
        }
        
        print(_VoxelWaitingList.Count);
    }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundInt(List<AudioSource> SoundList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, SoundList.Count);
        return i;
    }

    public List<AudioWrapper> GetVoxelWaitingList() { return _VoxelWaitingList; }

    public void StartingPlayingVoxels() { _IsPlayingVoxel = true; }

    public bool GetIsPlayingVoxel() { return _IsPlayingVoxel; }

    /// -------------------------------------------
    /// 
    ///     BUTTON SFX 
    /// 
    /// -------------------------------------------

    public void PlayButtonClick() {

        // Precautions
        if (_SFX_ButtonClick != null)
            _SFX_ButtonClick.Play();
    }

    public void PlayButtonHover() {

        // Precautions
        if (_SFX_ButtonHover != null)
            _SFX_ButtonHover.Play();
    }

    public void PlayButtonGoBack() {

        // Precautions
        if (_SFX_ButtonGoBack != null)
            _SFX_ButtonGoBack.Play();
    }

    /// -------------------------------------------
    /// 
    ///     DASH SFX 
    /// 
    /// -------------------------------------------

    public void PlayDash() {

        // Precautions
        if (_SFX_Dash.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_Dash);
            AudioSource sound = _SFX_Dash[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    /// 
    ///     ORB FIREBALL SFX 
    /// 
    /// -------------------------------------------

    public void PlayFireballAttack() {

        // Precautions
        if (_SFX_FireballAttack.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FireballAttack);
            AudioSource sound = _SFX_FireballAttack[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayFireballImpact() {

        // Precautions
        if (_SFX_FireballImpact.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FireballImpact);
            AudioSource sound = _SFX_FireballImpact[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    /// 
    ///     FLAMETHROWER SFX 
    /// 
    /// -------------------------------------------

    public void PlayFlamethrowerAttack() {

        // Precautions
        if (_SFX_FlamethrowerAttack.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FlamethrowerAttack);
            AudioSource sound = _SFX_FlamethrowerAttack[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayTabbing() {

        // Precautions
        if (_SFX_WeaponTabbing.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_WeaponTabbing);
            AudioSource sound = _SFX_WeaponTabbing[i];

            // Play the sound
            sound.Play();
        }
    }
    /// -------------------------------------------
    /// 
    ///     DEVICE SFX 
    /// 
    /// -------------------------------------------

    public void PlayTeleport() {

        // Precautions
        if (_SFX_OnTeleport.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTeleport);
            AudioSource sound = _SFX_OnTeleport[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupMinion() {

        // Precautions
        if (_SFX_OnTagPickupMinion.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupMinion);
            AudioSource sound = _SFX_OnTagPickupMinion[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupSpeedBoost() {

        // Precautions
        if (_SFX_OnTagPickupSpeed.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupSpeed);
            AudioSource sound = _SFX_OnTagPickupSpeed[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupHealthpack() {

        // Precautions
        if (_SFX_OnTagPickupHealth.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupHealth);
            AudioSource sound = _SFX_OnTagPickupHealth[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupInvincibility() {

        // Precautions
        if (_SFX_OnTagPickupInvincibility.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupInvincibility);
            AudioSource sound = _SFX_OnTagPickupInvincibility[i];

            // Play the sound
            sound.Play();
        }
    }
    /// -------------------------------------------
    ///     
    ///     CRYSTAL SFX 
    /// 
    /// -------------------------------------------

    public void PlayCrystalHit() {

        // Precautions
        if (_SFX_CrystalHit.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_CrystalHit);
            AudioSource sound = _SFX_CrystalHit[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayCrystalDeath() {

        // Precautions
        if (_SFX_CrystalDeath.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_CrystalDeath);
            AudioSource sound = _SFX_CrystalDeath[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayCrystalUpdate() {

        // Precautions
        if (_SFX_CrystalUpdate.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_CrystalUpdate);
            AudioSource sound = _SFX_CrystalUpdate[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    ///     
    ///     MUSIC & AMBIENCE SFX
    /// 
    /// -------------------------------------------

    public void PlayMusicMainMenu() {

        // Precautions
        if (_MUSIC_MainMenu != null)
            _MUSIC_MainMenu.Play();
    }

    public void PlayMusicGameplay() {

        // Precautions
        if (_MUSIC_Gameplay != null)
            _MUSIC_Gameplay.Play();
    }

    public void PlayAmbienceMainMenu() {

        // Precautions
        if (_AMBIENCE_MainMenu != null)
            _AMBIENCE_MainMenu.Play();
    }

    public void PlayAmbienceGameplay() {

        // Precautions
        if (_AMBIENCE_Gameplay != null)
            _AMBIENCE_Gameplay.Play();
    }

    public void PlayPhaseTransition() {

        // Precautions
        if (_SFX_PhaseTransition != null)
            _SFX_PhaseTransition.Play();
    }

    /// -------------------------------------------
    ///     
    ///     CHARACTER DIALOG
    /// 
    /// -------------------------------------------

    public Dialog GetRandomDialog() {

        // Precautions
        if (_VOX_Dialoglist.Count > 0 && _DialogsUse.Count == _VOX_Dialoglist.Count) {

            // Loop until we find a valid dialog
            Dialog dialog = null;
            int use = 0;
            for (int i = 0; i < _VOX_Dialoglist.Count; ++i) {

                dialog = _VOX_Dialoglist[i];

                // Has it already been used?
                if (_DialogsUse[i] == true) {

                    dialog = null;
                }

                // Dialog hasnt been used yet
                else {

                    use = i;
                    break;
                }
            }

            // A dialog reference has been successfully found
            _DialogsUse[use] = true;
            return dialog;
        }

        else { /// _VOX_Dialoglist.Count == 0

            return null;
        }
    }
    
    /// -------------------------------------------
    ///     
    ///     FACE TREE DIALOG
    /// 
    /// ------------------------------------------- ///

    public void SetFaceTreeSoundPlaying(bool value) { _FaceTreeSoundIsPlaying = value; }

    public bool GetFaceTreeSoundIsPlaying() { return _FaceTreeSoundIsPlaying; }

}