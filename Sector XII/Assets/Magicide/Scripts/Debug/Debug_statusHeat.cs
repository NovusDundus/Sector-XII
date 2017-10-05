using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_statusHeat : MonoBehaviour {

    public Text TextComp;
    public Weapon WeaponTested;

    void Start() {

    }


    void Update() {

        if (TextComp != null && WeaponTested != null) {

            TextComp.text = WeaponTested.GetOverheatedStatus().ToString();
        }
    }
}
