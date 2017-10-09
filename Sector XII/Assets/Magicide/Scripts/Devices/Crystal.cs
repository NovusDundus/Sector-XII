using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Christos Nicolas
    /// Created on: 10/09/2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    public GameObject CrystalObject;
    public Material PlayerAlphaMaterial;
    public Material PlayerBravoMaterial;
    public Material PlayerCharlieMaterial;
    public Material PlayerDeltaMaterial;
    public Material DefaultMaterial;

  
    private int _HighestScore = int.MinValue;
    private int _CurrentHighest = 0;
    private Player _TopPlayer = null;
    private int _TopPlayerID = 0;
    // private Color _CrystalColour;
    //public Material _CrystalMat;

    private MeshRenderer meshRenderer;


    //--------------------------------------------------------------
    // CONSTRUCTORS

    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();

    }
	
	void Update () {

       
	}

    private void FixedUpdate()
    {


        //create variable to store the current highest Player (set it null)
        //create variable to store the current highest Score (set that to int.MinValue)
        //create variable to store the current highest Player's index

        //loop over each player using for loop 
        //if this player's score is greater than current highest score
        //set current highest player to this player
        //set current highest score to this player's score


        _CurrentHighest = int.MinValue;

        foreach (Character plyr in PlayerManager._pInstance.GetAliveNecromancers())
        {
            Player p = plyr.GetComponent<Player>();

            if (p.GetScore() > _CurrentHighest)
            {
                _CurrentHighest = p.GetScore();
                _TopPlayer = p;
                _TopPlayerID = p._pPlayerID;
            }
        }
        Debug.Log("Winning Player: " + _TopPlayerID);
        // switch statement on colours
        // set colour of crystal to highest score

        //if currentHighestScore != 0


        switch (_TopPlayerID)
        {
            case 1:

                meshRenderer.material = PlayerAlphaMaterial;
                break;

            case 2:

                meshRenderer.material = PlayerBravoMaterial;
                break;

            case 3:

                meshRenderer.material = PlayerCharlieMaterial;
                break;

            case 4:
                
                meshRenderer.material = PlayerDeltaMaterial;
                break;

            default:
                meshRenderer.material = DefaultMaterial;
                break;

        }

                

                



    }
}
