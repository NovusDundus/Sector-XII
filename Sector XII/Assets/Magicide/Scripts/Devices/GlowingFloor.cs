using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingFloor : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 08/11/2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***
    
    /// Private
    private bool _GlowingUp = true;
    private float _tGlow = 0f;
    private float _GlowTime = 1f;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        // Set glow colour to min
        DeviceManager._pInstance._FloorMaterial.SetColor("_GlowColour", DeviceManager._pInstance._GlowFloorColourMin);
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
        if (_GlowingUp == true) {

            // Colour hasnt completed its lerp
            if (DeviceManager._pInstance._FloorMaterial.GetColor("_GlowColour") != DeviceManager._pInstance._GlowFloorColourMax && _tGlow < 1f) {
                
                // Change the colour to max
                _tGlow += Time.deltaTime * _GlowTime;
                DeviceManager._pInstance._FloorMaterial.SetColor("_GlowColour", Color.Lerp(DeviceManager._pInstance._FloorMaterial.GetColor("_GlowColour"), DeviceManager._pInstance._GlowFloorColourMax, _tGlow * _GlowTime * Time.deltaTime));
            }
     
            // Lerp completed
            else {
     
                _GlowingUp = false;
            }
        }
     
        else { /// _GlowingUp == false
     
            // Colour hasnt completed its lerp
			if (DeviceManager._pInstance._FloorMaterial.GetColor("_GlowColour") != DeviceManager._pInstance._GlowFloorColourMin && _tGlow > 0f) {
     
                // Change the colour to min
                _tGlow -= Time.deltaTime * _GlowTime;
                DeviceManager._pInstance._FloorMaterial.SetColor("_GlowColour", Color.Lerp(DeviceManager._pInstance._FloorMaterial.GetColor("_GlowColour"), DeviceManager._pInstance._GlowFloorColourMin, _tGlow * _GlowTime * Time.deltaTime));
            }
     
            // Lerp completed
            else {
     
                _GlowingUp = true;
            }
        }
    }

}