using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public
    public int _pPlayerID = 0;                                      // ID Reference of the individual player.
    public Color _pPlayerColour;                                    // The colour that represents the individual player.

    /// Private
    private int _Score = 0;                                         // The player's individual score for the match.

    //--------------------------------------------------------------
    // CONSTRUCTORS

    void Start() {

    }

    void Update() {

    }

    void FixedUpdate() {

    }

    public void SetScore(int amount) {

        // set the player's score to match the parameter
        _Score = amount;
    }

    public void AddScore(int amount) {

        // Add 'x' points to the player's score
        _Score += amount;
    }

    public int GetScore() {

        // Returns the current score of the player
        return _Score;
    }
}
