using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnnouncer : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 24.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (Exposed)
    public List<AudioWrapper> _Vox_GetReady;
    public List<AudioWrapper> _Vox_PhaseOneStart;
    public List<AudioWrapper> _Vox_PhaseTwoStart;
    public List<AudioWrapper> _Vox_PlayerEliminated;
    public List<AudioWrapper> _Vox_SuddenDeath;
    public List<AudioWrapper> _Vox_GameOver;

    /// Public (Internal)
    [HideInInspector]
    public static GameAnnouncer _pInstance;

    /// Private
    private bool _PlayingSound = false;

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
    
    //--------------------------------------------------------------
    // *** FRAME ***
    
    private void Update () {

        // Check for any sounds being played
        if (_PlayingSound == true) {
            
            /// VOX Get Ready
            bool GetReadyPlaying = false;
            foreach (var vox in _Vox_GetReady) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    GetReadyPlaying = true;
                    break;
                }
            }

            /// VOX Phase One start
            bool PhaseOnePlaying = false;
            foreach (var vox in _Vox_PhaseOneStart) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    PhaseOnePlaying = true;
                    break;
                }
            }

            /// VOX Phase Two start
            bool PhaseTwoPlaying = false;
            foreach (var vox in _Vox_PhaseTwoStart) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    PhaseTwoPlaying = true;
                    break;
                }
            }

            /// VOX Player Eliminated
            bool PlayerEliminatedPlaying = false;
            foreach (var vox in _Vox_PlayerEliminated) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    PlayerEliminatedPlaying = true;
                    break;
                }
            }

            /// VOX Sudden Death
            bool SuddenDeathPlaying = false;
            foreach (var vox in _Vox_SuddenDeath) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    SuddenDeathPlaying = true;
                    break;
                }
            }

            /// VOX Game Over
            bool GameOverPlaying = false;
            foreach (var vox in _Vox_GameOver) {

                AudioSource source = vox._SoundSource;

                if (source.isPlaying == true) {

                    GameOverPlaying = true;
                    break;
                }
            }

            // Set '_PlayingSound' to TRUE if ANY game announcer AudioSources are currently playing
            _PlayingSound = GetReadyPlaying || PhaseOnePlaying || PhaseTwoPlaying || PlayerEliminatedPlaying || SuddenDeathPlaying || GameOverPlaying;
        }
    }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundVoxInt(List<AudioWrapper> VoxList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, VoxList.Count);
        return i;
    }

    public void PlayGetReady() {

        // Precautions
        if (_Vox_GetReady.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_GetReady[RandomSoundVoxInt(_Vox_GetReady)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

    public void PlayPhaseOneStart() {

        // Precautions
        if (_Vox_PhaseOneStart.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_PhaseOneStart[RandomSoundVoxInt(_Vox_PhaseOneStart)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

    public void PlayPhaseTwoStart() {

        // Precautions
        if (_Vox_PhaseTwoStart.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_PhaseTwoStart[RandomSoundVoxInt(_Vox_PhaseTwoStart)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

    public void PlayPlayerEliminated() {

        // Precautions
        if (_Vox_PlayerEliminated.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_PlayerEliminated[RandomSoundVoxInt(_Vox_PlayerEliminated)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

    public void PlaySuddenDeath() {

        // Precautions
        if (_Vox_SuddenDeath.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_SuddenDeath[RandomSoundVoxInt(_Vox_SuddenDeath)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

    public void PlayGameOver() {

        // Precautions
        if (_Vox_GameOver.Count > 0 && _PlayingSound == false) {

            // Get random sound from the list
            AudioWrapper source = _Vox_GameOver[RandomSoundVoxInt(_Vox_GameOver)];

            // Queue the sound to the voxel waiting list
            SoundManager._pInstance.GetVoxelWaitingList().Add(source);

            // If the sound is the only one in the list
            if (SoundManager._pInstance.GetVoxelWaitingList().Count == 1) {

                // And the sound has no owner associated with it (only the announcer sounds have no owner)
                if (SoundManager._pInstance.GetVoxelWaitingList()[0]._Owner == null) {

                    // Play the sound
                    SoundManager._pInstance.GetVoxelWaitingList()[0]._SoundSource.Play();
                    SoundManager._pInstance.StartingPlayingVoxels();
                    _PlayingSound = false;
                }
            }
        }
    }

}