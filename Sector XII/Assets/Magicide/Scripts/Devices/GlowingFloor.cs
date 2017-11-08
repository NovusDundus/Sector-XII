using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingFloor : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Christos Nicolas
    /// Created on: 08/11/2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public Material _floorMat;
    public Color _ColourMin;
    public Color _ColourMax;
    public float _GlowTime = 1f;

    /// Private
    private bool _GlowingUp = true;
    private float _tGlow = 0f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        // Set glow colour to min
        _floorMat.SetColor("_GlowColour", _ColourMin);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
        if (_GlowingUp == true) {

            // Colour hasnt completed its lerp
            if (_floorMat.GetColor("_GlowColour") != _ColourMax && _tGlow < 1f) {
     
                // Change the colour to max
				_tGlow += Time.deltaTime * 0.5f;
				_floorMat.SetColor("_GlowColour", Color.Lerp(_floorMat.GetColor("_GlowColour"), _ColourMax, _tGlow * _GlowTime * Time.deltaTime));
            }
     
            // Lerp completed
            else {
     
                _GlowingUp = false;
            }
        }
     
        else { /// _GlowingUp == false
     
            // Colour hasnt completed its lerp
			if (_floorMat.GetColor("_GlowColour") != _ColourMin && _tGlow > 0f) {
     
                // Change the colour to min
                _tGlow -= Time.deltaTime * 0.5f;
				_floorMat.SetColor("_GlowColour", Color.Lerp(_floorMat.GetColor("_GlowColour"), _ColourMin, _tGlow * _GlowTime * Time.deltaTime));
            }
     
            // Lerp completed
            else {
     
                _GlowingUp = true;
            }
        }
    }

}