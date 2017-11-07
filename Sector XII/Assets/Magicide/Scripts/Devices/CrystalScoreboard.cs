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
    public GameObject _Crystal;
    public Light _LightSource;
    public Material _PlayerAlphaMaterial;
    public Material _PlayerBravoMaterial;
    public Material _PlayerCharlieMaterial;
    public Material _PlayerDeltaMaterial;
    public Material _DefaultMaterial;

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

                meshRenderer.material = _DefaultMaterial;
                _LightSource.color = _DefaultMaterial.color;
                break;

            case 1:

                meshRenderer.material = _PlayerAlphaMaterial;
                _LightSource.color = _PlayerAlphaMaterial.color;
                break;

            case 2:

                meshRenderer.material = _PlayerBravoMaterial;
                _LightSource.color = _PlayerBravoMaterial.color;
                break;

            case 3:

                meshRenderer.material = _PlayerCharlieMaterial;
                _LightSource.color = _PlayerCharlieMaterial.color;
                break;

            case 4:
                
                meshRenderer.material = _PlayerDeltaMaterial;
                _LightSource.color = _PlayerDeltaMaterial.color;
                break;

            default:

                meshRenderer.material = _DefaultMaterial;
                _LightSource.color = _DefaultMaterial.color;
                break;

        }
    }

}