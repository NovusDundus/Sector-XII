using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTree : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 14.11.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public bool _NorthTree;

    /// Private
    private List<AudioSource> _OnHitSounds;
    private CapsuleCollider _HitCollision;
    private bool _PlayingSound = false;
    private AudioSource _SoundBeingPlayed;
    private int _LastSoundPlayed;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        _HitCollision = GetComponent<CapsuleCollider>();

        if (_NorthTree == true) {

            _OnHitSounds = SoundManager._pInstance._VOX_FaceTreeNorthDialoglist;
        }

        else { /// _NorthTree == false

            _OnHitSounds = SoundManager._pInstance._VOX_FaceTreeSouthDialoglist;
        }
    }

    //--------------------------------------------------------------
    // *** HIT ***

    public void Update() {

        if (_PlayingSound == true) {

            // Sound has finished playing
            if (_SoundBeingPlayed.isPlaying == false && SoundManager._pInstance.GetFaceTreeSoundIsPlaying() == false) {

                // Reset
                _PlayingSound = false;
                SoundManager._pInstance.SetFaceTreeSoundPlaying(false);
            }
        }
    }

    public void OnHit() {

        // Precautions
        if (_OnHitSounds.Count > 0) {

            // Not currently playing a sound
            if (!_PlayingSound) {

                bool _FoundSound = false;
                while (!_FoundSound) {

                    // Get a random sound from the audio list
                    int i = Random.Range(0, _OnHitSounds.Count);

                    if (_LastSoundPlayed != i) {

                        _SoundBeingPlayed = _OnHitSounds[i];
                        _SoundBeingPlayed.Play();
                        _LastSoundPlayed = i;
                        _FoundSound = true;
                        _PlayingSound = true;
                        SoundManager._pInstance.SetFaceTreeSoundPlaying(true);
                    }
                }
            }
        }
    }

    public CapsuleCollider GetCollider() {

        return _HitCollision;
    }

}