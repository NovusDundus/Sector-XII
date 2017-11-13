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
    public float _LightMinRange = 50f;
    public float _LightMaxRange = 52f;
    public float _LightRangeSpeed = 0.1f;
    public float _LightMinIntensity = 10f;
    public float _LightMaxIntensity = 12f;
    public float _LightIntensitySpeed = 0.1f;

    /// Private
    private int _CurrentHighest = 0;
    private int _TopPlayerID = 0;
    private MeshRenderer meshRenderer;
    private Player _HighestPlayer;
    private Color _ColourTarget;
    private float _timerColour = 0f;
    private bool _RangeUp = true;
    private bool _IntensityUp = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        meshRenderer = GetComponent<MeshRenderer>();
        _ColourTarget = _LightSource.color;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update()
    {
        foreach (Character plyr in PlayerManager._pInstance.GetEliminatedNecromancers())
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

        foreach (Character plyr in PlayerManager._pInstance.GetActiveNecromancers())
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

            case 1:

                meshRenderer.material = _PlayerAlphaMaterial;
                _ColourTarget = _PlayerAlphaMaterial.color;
                _timerColour = 0;
                break;

            case 2:

                meshRenderer.material = _PlayerBravoMaterial;
                _ColourTarget = _PlayerBravoMaterial.color;
                _timerColour = 0;
                break;

            case 3:

                meshRenderer.material = _PlayerCharlieMaterial;
                _ColourTarget = _PlayerCharlieMaterial.color;
                _timerColour = 0;
                break;

            case 4:
                
                meshRenderer.material = _PlayerDeltaMaterial;
                _ColourTarget = _PlayerDeltaMaterial.color;
                _timerColour = 0;
                break;

            default:

                meshRenderer.material = _DefaultMaterial;
                _ColourTarget = _DefaultMaterial.color;
                _timerColour = 0;
                break;

        }

        if (_LightSource.color != _ColourTarget && _timerColour < 1f) {

            _timerColour += Time.deltaTime * 2;
            _LightSource.color = Color.Lerp(_LightSource.color, _ColourTarget, _timerColour);
        }     
        
        if (_RangeUp == true) {

            if (_LightSource.range < _LightMaxRange) {

                _LightSource.range += _LightRangeSpeed * Time.deltaTime;
            }

            else { ///_LightSource.range >= _LightMaxRange

                _RangeUp = false;
            }
        }

        else { /// _RangeUp == false

            if (_LightSource.range > _LightMinRange) {

                _LightSource.range -= _LightRangeSpeed * Time.deltaTime;
            }

            else { ///_LightSource.range < _LightMinRange

                _RangeUp = true;
            }
        }

        if (_IntensityUp == true) {

            if (_LightSource.intensity < _LightMaxIntensity) {

                _LightSource.intensity += _LightIntensitySpeed * Time.deltaTime;
            }

            else { ///_LightSource.intensity >= _LightMaxRange

                _IntensityUp = false;
            }
        }

        else { /// _IntensityUp == false

            if (_LightSource.intensity > _LightMinIntensity) {

                _LightSource.intensity -= _LightIntensitySpeed * Time.deltaTime;
            }

            else { ///_LightSource.intensity < _LightMinIntensity

                _IntensityUp = true;
            }
        }
    }

    //--------------------------------------------------------------
    // *** COLOURING ***

    public void LerpColour(Color a_Colour) {

        _ColourTarget = a_Colour;
    }

}