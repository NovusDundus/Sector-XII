using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_TriggerRight : MonoBehaviour {

    public Text TextComp;
    public Char_Geomancer NecroTested;

    void Start() {

    }


    void Update() {

        if (TextComp != null && NecroTested != null) {

            TextComp.text = NecroTested._Player.GetRightTriggerInput.ToString();
        }
    }
}
