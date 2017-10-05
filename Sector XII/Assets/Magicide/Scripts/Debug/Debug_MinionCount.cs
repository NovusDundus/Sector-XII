using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_MinionCount : MonoBehaviour {

    public Text TextComp;
    public Wep_Shield ShieldTested;

    void Start() {

    }


    void Update() {

        if (TextComp != null && ShieldTested != null) {

            TextComp.text = ShieldTested.GetMinionCount().ToString();
        }
    }
}
