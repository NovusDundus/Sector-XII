using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_currentHeatToText : MonoBehaviour {
    
    public Text TextComp;
    public Weapon WeaponTested;

    private float fCurrentHeat;

	void Start () {
		
	}
	
	
	void Update () {
		
        if (TextComp != null && WeaponTested != null) {

            fCurrentHeat = WeaponTested.GetCurrentHeat();
            TextComp.text = fCurrentHeat.ToString("0.000");
        }
	}
}
