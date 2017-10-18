using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScoreboard : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Christos Nicolas
    /// Created on: 09/10/2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public GameObject CrystalObject;
    public Material PlayerAlphaMaterial;
    public Material PlayerBravoMaterial;
    public Material PlayerCharlieMaterial;
    public Material PlayerDeltaMaterial;
    public Material DefaultMaterial;

    /// Private
    private int _CurrentHighest = 0;
    private int _TopPlayerID = 0;
    private MeshRenderer meshRenderer;
    private Player _HighestPlayer;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        meshRenderer = GetComponent<MeshRenderer>();
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void FixedUpdate()
    {
        foreach (Character plyr in PlayerManager._pInstance.GetDeadNecromancers())
        {
            Player p = plyr.GetComponent<Player>();

            if (p == _HighestPlayer)
            {
                _HighestPlayer = null;
                _CurrentHighest = 0;
                _TopPlayerID = 0;
                break;
            }
        }

        foreach (Character plyr in PlayerManager._pInstance.GetAliveNecromancers())
        {
            Player p = plyr.GetComponent<Player>();

            if (p.GetScore() > _CurrentHighest)
            {
                _CurrentHighest = p.GetScore();
                _TopPlayerID = p._pPlayerID;
                _HighestPlayer = p;
                break;
            }
        }

        switch (_TopPlayerID) {

            case 0:

                meshRenderer.material = DefaultMaterial;
                break;

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