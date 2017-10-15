using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_ShowDevUI : MonoBehaviour {

    public GameObject _Panel;
    public Character _PlayerOne;

    private bool PanelShowing = true;
    private bool pauseInitiated = false;
    private float _TimerActive = 0f;
    private float _TimerInactive = 0f;

    void Start () {
        
	}
	
	void Update () {
		
	}

    void FixedUpdate() {

        if (PanelShowing == true) {

            _TimerActive += Time.deltaTime;
            _TimerInactive = 0f;
        }
        else {

            _TimerInactive += Time.deltaTime;
            _TimerActive = 0f;
        }

        if (_PlayerOne != null) {

            if (_PlayerOne._Player.GetSpecialLeftButton == true) {

                if (PanelShowing == true && _TimerActive > 0.5f) {

                    _Panel.SetActive(false);
                    PanelShowing = false;
                }

                else if (_TimerInactive > 0.5f) {

                    _Panel.SetActive(true);
                    PanelShowing = true;
                }
            }
        }
    }
}