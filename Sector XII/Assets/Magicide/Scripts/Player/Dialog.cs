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
    public List<AudioSource> _TauntSounds;

    /// Private
    private bool _OnHitPlaying = false;
    private bool _OnDeathPlaying = false;
    private bool _TauntPlaying = false;

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
        if (_TauntPlaying == true) {

            bool soundPlaying = false;

            // Check if all sounds in the list have stopped playing
            foreach (var sound in _TauntSounds) {

                if (sound.isPlaying == true) {

                    soundPlaying = true;
                    break;
                }
            }
            _TauntPlaying = soundPlaying;
        }
    }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundInt(List<AudioSource> SoundList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, SoundList.Count);
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

            // If a sound isnt current being played
            if (_TauntPlaying == false) {

                // Get random sound from list
                AudioSource sound = _TauntSounds[RandomSoundInt(_TauntSounds)];

                // Queue the sound to the voxel waiting list
                SoundManager._pInstance.GetVoxelWaitingList().Add(sound);
                _TauntPlaying = true;
                SoundManager._pInstance.StartingPlayingVoxels();
            }
        }
    }

}