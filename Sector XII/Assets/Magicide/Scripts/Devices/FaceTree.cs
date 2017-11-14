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

    /// Private
    private List<AudioSource> _OnHitSounds;
    private CapsuleCollider _HitCollision;
    private bool _PlayingSound = false;
    private AudioSource _SoundBeingPlayed;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        _HitCollision = GetComponent<CapsuleCollider>();
        _OnHitSounds = SoundManager._pInstance._VOX_FaceTreeDialoglist;
    }

    //--------------------------------------------------------------
    // *** HIT ***

    public void Update() {

        if (_PlayingSound == true) {

            // Sound has finished playing
            if (_SoundBeingPlayed.isPlaying == false) {

                // Reset
                _PlayingSound = false;
            }
        }
    }

    public void OnHit() {

        // Precautions
        if (_OnHitSounds.Count > 0) {

            // Not currently playing a sound
            if (!_PlayingSound) {

                // Get a random sound from the audio list
                int i = Random.Range(0, _OnHitSounds.Count);
                _SoundBeingPlayed = _OnHitSounds[i];
                _SoundBeingPlayed.Play();
                _PlayingSound = true;
            }
        }
    }

    public CapsuleCollider GetCollider() {

        return _HitCollision;
    }

}