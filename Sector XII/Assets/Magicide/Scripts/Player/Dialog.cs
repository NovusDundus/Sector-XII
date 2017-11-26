using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (designers)    
    [Header("---------------------------------------------------------------------------")]
    [Header("*** SOUND SFX")]
    [Header("")]
    public List<AudioSource> _OnHitSounds;
    public List<AudioSource> _OnDeathSounds;
    [Header("*** SOUND VOX")]
    [Header("")]
    public List<AudioWrapper> _TauntSounds;

    /// Private
    private Player _PlayerAssociated;
    private bool _OnHitPlaying = false;
    private bool _OnDeathPlaying = false;
    private bool _IsTauntPlaying = false;

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {
        
        // Check for OnHit sounds
        if (_OnHitPlaying == true) {

            bool soundPlaying = false;

            // Check if all sounds in the list have stopped playing
            foreach (var sound in _OnHitSounds) {

                if (sound.isPlaying == true) {

                    soundPlaying = true;
                    break;
                }
            }
            _OnHitPlaying = soundPlaying;
        }

        // Check for OnDeath sounds
        if (_OnDeathPlaying == true) {

            bool soundPlaying = false;

            // Check if all sounds in the list have stopped playing
            foreach (var sound in _OnDeathSounds) {

                if (sound.isPlaying == true) {

                    soundPlaying = true;
                    break;
                }
            }
            _OnDeathPlaying = soundPlaying;
        }

        // Check for Taunt sounds
        if (_IsTauntPlaying == true) {

            bool soundPlaying = false;

            // Check if all sounds in the list have stopped playing
            foreach (var sound in _TauntSounds) {

                AudioSource source = sound._SoundSource;

                if (source.isPlaying == true) {

                    soundPlaying = true;
                    break;
                }
            }
            _IsTauntPlaying = soundPlaying;
        }
    }

    //--------------------------------------------------------------
    // *** PLAYER ***

    public void SetPlayer(Player value) { _PlayerAssociated = value; }

    public Player GetPlayerAssociated() { return _PlayerAssociated; }

    public void SetIsTaunting(bool value) { _IsTauntPlaying = value; }

    public bool IsTaunting() { return _IsTauntPlaying; }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundInt(List<AudioSource> SoundList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, SoundList.Count);
        return i;
    }

    public int RandomSoundVoxInt(List<AudioWrapper> VoxList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, VoxList.Count);
        return i;
    }

    /// SFX
    public void PlayOnHit() {

        // Precautions
        if (_OnHitSounds.Count > 0) {

            // If a sound isnt current being played
            if (_OnHitPlaying == false) {

                // Get random sound from list
                int i = RandomSoundInt(_OnDeathSounds);
                AudioSource sound = _OnHitSounds[i];

                // Play the sound
                sound.Play();
                _OnHitPlaying = true;
            }
        }
    }

    /// SFX
    public void PlayOnDeath() {

        // Precautions
        if (_OnDeathSounds.Count > 0) {

            // If a sound isnt current being played
            if (_OnDeathPlaying == false) {

                // Get random sound from list
                int i = RandomSoundInt(_OnDeathSounds);
                AudioSource sound = _OnDeathSounds[i];

                // Play the sound
                sound.Play();
                _OnDeathPlaying = true;
            }
        }
    }

    /// VOX
    public void PlayTaunt() {

        // Precautions
        if (_TauntSounds.Count > 0) {

            // Get random sound from list
            AudioWrapper sound = _TauntSounds[RandomSoundVoxInt(_TauntSounds)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(sound);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound belongs to us
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner._Player == this.GetComponent<Dialog>().GetPlayerAssociated()) {

                    // Play the sound 
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _IsTauntPlaying = true;
                }
            }
        }
    }

}