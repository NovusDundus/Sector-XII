using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_currentHeatToText : MonoBehaviour {
    
    public Text TextComp;
    public Weapon WeaponTested;

	void Start () {
		
	}
	
	
	void Update () {
		
        if (TextComp != null) {

            TextComp.text = "Current Heat: " + WeaponTested.GetCurrentHeat().ToString();
        }
	}
}
