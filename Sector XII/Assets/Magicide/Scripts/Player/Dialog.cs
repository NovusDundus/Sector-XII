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
    public List<AudioSource> _OnHitSounds;
    public List<AudioSource> _OnDeathSounds;
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

    public void PlayOnHit() {

        // If a sound isnt current being played
        if (_OnHitPlaying == false) {

            // Get random sound from list
            AudioSource sound = _OnHitSounds[RandomSoundInt(_OnHitSounds)];

            // Play the sound
            sound.Play();
            _OnHitPlaying = true;
        }
    }

    public void PlayOnDeath() {

        // If a sound isnt current being played
        if (_OnDeathPlaying == false) {

            // Get random sound from list
            AudioSource sound = _OnDeathSounds[RandomSoundInt(_OnDeathSounds)];

            // Play the sound
            sound.Play();
            _OnDeathPlaying = true;
        }
    }

    public void PlayTaunt() {

        // If a sound isnt current being played
        if (_TauntPlaying == false) {

            // Get random sound from list
            AudioSource sound = _TauntSounds[RandomSoundInt(_TauntSounds)];

            // Play the sound
            sound.Play();
            _TauntPlaying = true;
        }
    }

}