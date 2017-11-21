using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Lives : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 6.11.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public Player _Player;
    public GameObject _Life1;
    public GameObject _Life2;
    public GameObject _Life3;

    /// Private
    private int _RespawnsRemaining;
    
    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update () {

        // Set the amount of lives remaining for the player
        if (_Player != null) 
            _RespawnsRemaining = _Player.GetRespawnsLeft();

        // Final life
        if (_RespawnsRemaining >= 0) {

            if (_Life1 != null) 
                _Life1.SetActive(true);

            // 1 respawn remaining
            if (_RespawnsRemaining >= 1) {

                if (_Life1 != null)
                    _Life1.SetActive(true);

                if (_Life2 != null) 
                    _Life2.SetActive(true);

                // 2 respawns remaining
                if (_RespawnsRemaining >= 2) {

                    if (_Life1 != null)
                        _Life1.SetActive(true);

                    if (_Life2 != null)
                        _Life2.SetActive(true);

                    if (_Life3 != null) 
                        _Life3.SetActive(true);
                }

                // Less than 2 respawns remaining
                else { /// _RespawnsRemaining < 2

                    if (_Life3 != null) 
                        _Life3.SetActive(false);
                }
            }

            // Final life
            else { /// _RespawnsRemaining < 1

                if (_Life2 != null) 
                    _Life2.SetActive(false);

                if (_Life3 != null) 
                    _Life3.SetActive(false);
            }
        }
        
        // No lives left
        else { /// _RespawnsRemaining < 0

            if (_Life1 != null) 
                _Life1.SetActive(false);

            if (_Life2 != null) 
                _Life2.SetActive(false);

            if (_Life3 != null) 
                _Life3.SetActive(false);
        }
	}

}