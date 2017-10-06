using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_fireRate : MonoBehaviour {

    public Text TextComp;
    public Weapon WeaponTested;

    private float fCurrentDelay;

    void Start() {

    }


    void Update() {

        if (TextComp != null && WeaponTested != null) {

            fCurrentDelay = WeaponTested.GetFireDelay();
            TextComp.text = fCurrentDelay.ToString("0.00");

            // Can shoot
            if (fCurrentDelay <= 0f) {

                TextComp.color = Color.blue;
            }

            // Cannot shoot
            else {

                TextComp.color = Color.red;
            }
        }
    }
}
