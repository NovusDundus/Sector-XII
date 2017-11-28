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

    /// Public (Exposed)
    public bool _NorthTree;
    public Material _DamagedMaterial;                               // The material that is shown on the character when receiving damage.

    /// Private
    private List<AudioSource> _OnHitSounds;
    private CapsuleCollider _HitCollision;
    private bool _PlayingSound = false;
    private AudioSource _SoundBeingPlayed;
    private int _LastSoundPlayed;
    private float _ImpactFlashTimer = 0f;
    private bool _ReceivingDamage = false;
    private MeshRenderer _MeshRenderer;                             // Reference to the objects's mesh renderer.
    private Material _OriginalMaterial;                             // Reference to the mesh renderer's original material.


    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start() {

        // Get reference to the object's collision
        _HitCollision = GetComponent<CapsuleCollider>();

        // Store the original material so it can be reverted back on the mesh renderer later
        _MeshRenderer = GetComponentInChildren<MeshRenderer>();
        _OriginalMaterial = _MeshRenderer.material;

        // The tree positioned in the northern area of the map
        if (_NorthTree == true) {

            // Assign dialog sound list
            _OnHitSounds = SoundManager._pInstance._VOX_FaceTreeNorthDialoglist;
        }
        // The tree positioned in the southern area of the map
        else { /// _NorthTree == false

            // Assign dialog sound list
            _OnHitSounds = SoundManager._pInstance._VOX_FaceTreeSouthDialoglist;
        }
    }

    //--------------------------------------------------------------
    // *** HIT ***

    public void Update() {
         
        if (_PlayingSound == true) {

            // Sound has finished playing
            if (_SoundBeingPlayed.isPlaying == false/* && SoundManager._pInstance.GetFaceTreeSoundIsPlaying() == false*/) {

                // Reset
                _PlayingSound = false;
                SoundManager._pInstance.SetFaceTreeSoundPlaying(false);
            }
        }
        
        DamageFlashChecks();
    }

    public void DamageFlashChecks() {

        // Flash momentarilty when receiving damage
        if (_ReceivingDamage == true) {

            if (_ImpactFlashTimer > 0f) {

                _ImpactFlashTimer -= Time.deltaTime * 100;
            }

            else {

                _ReceivingDamage = false;
            }
        }

        // Has been at least 1 second since the last registered damage
        else { /// _ReceivingDamage == false

            // Revert back to original material
            _MeshRenderer.material = _OriginalMaterial;
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
        
        // Material change for feedback on impact
        if (_DamagedMaterial != null) {

            _MeshRenderer.material = _DamagedMaterial;
            _ReceivingDamage = true;
            _ImpactFlashTimer = 1f;
        }
    }

    public CapsuleCollider GetCollider() {

        return _HitCollision;
    }

}