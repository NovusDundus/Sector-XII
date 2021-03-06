﻿using UnityEngine;

public class CrystalScoreboard : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Christos Nicolas & Daniel Marton
    /// Created on: 09/10/2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers)
    public GameObject _Crystal;
    public Light _LightSource;
    public Material _PlayerOneCrystalMaterial;
    public Color _PlayerOneLightColour = Color.red;
    public Color _PlayerOneAmbientColour = Color.red;
    public Material _PlayerTwoCrystalMaterial;
    public Color _PlayerTwoLightColour = Color.blue;
    public Color _PlayerTwoAmbientColour = Color.blue;
    public Material _PlayerThreeCrystalMaterial;
    public Color _PlayerThreeLightColour = Color.green;
    public Color _PlayerThreeAmbientColour = Color.green;
    public Material _PlayerFourCrystalMaterial;
    public Color _PlayerFourLightColour = Color.yellow;
    public Color _PlayerFourAmbientColour = Color.yellow;
    public Material _DefaultMaterial;
    public Color _DefaultLightColour = Color.grey;
    public Color _DefaultAmbientColour = Color.grey;
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
    private Color _LightColourTarget;
    private Color _SceneColourTarget;
    private float _timerColour = 0f;
    private bool _RangeUp = true;
    private bool _IntensityUp = true;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        meshRenderer = GetComponent<MeshRenderer>();
        _LightColourTarget = _LightSource.color;
        _SceneColourTarget = RenderSettings.ambientLight;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    private void Update() {

        // Checks to see if the winning player is currently eliminated
        foreach (Character plyr in PlayerManager._pInstance.GetEliminatedNecromancers()) {

            // Get reference to the player
            Player p = plyr.GetComponent<Player>();

            if (p == _HighestPlayer)
            {
                // Reset whose the leading player
                _HighestPlayer = null;
                _CurrentHighest = 0;
                _TopPlayerID = 0;
                break;
            }
        }

        _CurrentHighest = 0;

        // Check to see who is currently winning out of the alive player list
        foreach (Character plyr in PlayerManager._pInstance.GetActiveNecromancers()) {

            // Get reference to the player
            Player p = plyr.GetComponent<Player>();

            // Found new winning player
            if (p.GetScore() > _CurrentHighest)
            {
                _CurrentHighest = p.GetScore();
                _TopPlayerID = p._pPlayerID;
                _HighestPlayer = p;

                // Play sound
                SoundManager._pInstance.PlayCrystalUpdate();
            }
        }

        // Swap the colours based off who is currently winning
        switch (_TopPlayerID) {

            // Player one is currently winning
            case 1:

                meshRenderer.material = _PlayerOneCrystalMaterial;
                _LightColourTarget = _PlayerOneLightColour;
                _SceneColourTarget = _PlayerOneAmbientColour;
                _timerColour = 0;
                break;

            // Player two is currently winning
            case 2:

                meshRenderer.material = _PlayerTwoCrystalMaterial;
                _LightColourTarget = _PlayerTwoLightColour;
                _SceneColourTarget = _PlayerTwoAmbientColour;
                _timerColour = 0;
                break;

            // Player three is currently winning
            case 3:

                meshRenderer.material = _PlayerThreeCrystalMaterial;
                _LightColourTarget = _PlayerThreeLightColour;
                _SceneColourTarget = _PlayerThreeAmbientColour;
                _timerColour = 0;
                break;
            
            // Player four is currently winning
            case 4:
                
                meshRenderer.material = _PlayerFourCrystalMaterial;
                _LightColourTarget = _PlayerFourLightColour;
                _SceneColourTarget = _PlayerFourAmbientColour;
                _timerColour = 0;
                break;

            // No one is currently winning
            default:

                meshRenderer.material = _DefaultMaterial;
                _LightColourTarget = _DefaultLightColour;
                RenderSettings.ambientLight = _DefaultAmbientColour;
                _timerColour = 0;
                break;

        }

        // Smoothly lerp through the current colours to the new target players
        if (_LightSource.color != _LightColourTarget && _timerColour < 1f) {

            _timerColour += Time.deltaTime * 2;
            _LightSource.color = Color.Lerp(_LightSource.color, _LightColourTarget, _timerColour);
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, _SceneColourTarget, _timerColour);
        }     
        
        // Light is breathing upwards
        if (_RangeUp == true) {

            if (_LightSource.range < _LightMaxRange) {

                // Increase light range
                _LightSource.range += _LightRangeSpeed * Time.deltaTime;
            }

            // Has reached maximum threshold
            else { ///_LightSource.range >= _LightMaxRange

                _RangeUp = false;
            }
        }

        // Light is breathing inwards
        else { /// _RangeUp == false

            if (_LightSource.range > _LightMinRange) {

                // Decrease light range
                _LightSource.range -= _LightRangeSpeed * Time.deltaTime;
            }

            // Has reached minimum threshold
            else { ///_LightSource.range < _LightMinRange

                _RangeUp = true;
            }
        }

        // Light is breathing the intensity upwards
        if (_IntensityUp == true) {

            if (_LightSource.intensity < _LightMaxIntensity) {

                // Increase intensity
                _LightSource.intensity += _LightIntensitySpeed * Time.deltaTime;
            }

            // Has reached maximum threshold
            else { ///_LightSource.intensity >= _LightMaxRange

                _IntensityUp = false;
            }
        }

        // Light is breathing the intensity inwards
        else { /// _IntensityUp == false

            if (_LightSource.intensity > _LightMinIntensity) {

                // Decrease intensity
                _LightSource.intensity -= _LightIntensitySpeed * Time.deltaTime;
            }

            // Has reached minimum threshold
            else { ///_LightSource.intensity < _LightMinIntensity

                _IntensityUp = true;
            }
        }
    }

}