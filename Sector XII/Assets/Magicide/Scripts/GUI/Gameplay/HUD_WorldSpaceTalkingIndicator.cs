﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_WorldSpaceTalkingIndicator : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 10.11.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public
    public Camera _Camera;
    public Char_Geomancer _Character;
    [Range(-0.1f, 0.1f)]
    public float _VerticalOffset = 0.04f;
    [Range(-0.1f, 0.1f)]
    public float _HorizontalOffset = 0f;

    /// Private
    private RawImage _TalkingIndicatorImage;
    private Vector3 _CharacterPosition;
    private RectTransform _RectTransform;
    private Vector3 _PanelPosition;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    void Start () {

        // Get references
        _TalkingIndicatorImage = GetComponent<RawImage>();
        _RectTransform = GetComponent<RectTransform>();
	}

    //--------------------------------------------------------------
    // *** FRAME ***

    void Update () {
		
        // Precautions
        if (_Character != null && _Camera != null) {

            // Character is currently taking talking
            if (_Character.GetDialog().IsTaunting() == true) { 

                // Convert character world position to screen space
                _CharacterPosition = _Camera.WorldToViewportPoint(_Character.transform.position);

                // Show damage indicator image
                _TalkingIndicatorImage.enabled = true;

                // Set talking indicator above the character
                _PanelPosition = new Vector3(_CharacterPosition.x + _HorizontalOffset, _CharacterPosition.y + _VerticalOffset, _CharacterPosition.z);
                _RectTransform.anchorMin = _PanelPosition;
                _RectTransform.anchorMax = _PanelPosition;
            }

            else { ///_Character.GetTakingDamage() == false

                // Hide talking indicator image
                _TalkingIndicatorImage.enabled = false;
            }
        }           
	}

}