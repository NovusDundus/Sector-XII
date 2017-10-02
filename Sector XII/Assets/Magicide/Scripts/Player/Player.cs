using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //--------------------------------------
    // VARIABLES
   
    public int _pPlayerID = 0;                      // ID Reference of the individual player.
    public Color _pPlayerColour;                    // The colour that represents the individual player.

    private MatchManager.Team _Team;                // The current team the player is factioned to.
    private int _MatchScore = 0;                    // The player's individual score for the match.

    //--------------------------------------
    // FUNCTIONS

    void Start() {

        // Player is a humanoid at match startup
        _Team = MatchManager.Team.Humanoid;
    }
	
	void Update() {

    }

    void FixedUpdate() {

    }

    public void AddScore(int amount) {

        // Add x points to the current score of the player
        _MatchScore += amount;
    }

    public int GetScore() {

        // Get the current score of the player
        return _MatchScore;
    }

    public void SetTeam(MatchManager.Team newTeam) {

        // Set the new team for the player to faction with
        _Team = newTeam;
    }

    public MatchManager.Team GetTeam() {

        // Returns the current faction that the player is currently associated with
        return _Team;
    }

}