using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostMatch_Scoreboard : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 16.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES *** 

    /// Public (designers)
    public Char_Geomancer _PlayerOne;
    public Text _FirstPlayerPosText;
    public Text _FirstPlayerNameText;
    public Text _FirstPlayerKillsText;
    public Text _FirstPlayerMinText;
    public Text _FirstPlayerSecText;

    public Char_Geomancer _PlayerTwo;
    public Text _SecondPlayerPosText;
    public Text _SecondPlayerNameText;
    public Text _SecondPlayerKillsText;
    public Text _SecondPlayerMinText;
    public Text _SecondPlayerSecText;

    public Char_Geomancer _PlayerThree;
    public Text _ThirdPlayerPosText;
    public Text _ThirdPlayerNameText;
    public Text _ThirdPlayerKillsText;
    public Text _ThirdPlayerMinText;
    public Text _ThirdPlayerSecText;

    public Char_Geomancer _PlayerFour;
    public Text _FourthPlayerPosText;
    public Text _FourthPlayerNameText;
    public Text _FourthPlayerKillsText;
    public Text _FourthPlayerMinText;
    public Text _FourthPlayerSecText;

    //--------------------------------------------------------------
    // *** SCOREBOARD *** 

    private void Update() {
        
        // Player ONE
        if (_PlayerOne != null) {

            // Get placement to text
            if (_PlayerOne._Player.GetPlacement() == 1) {

                _FirstPlayerPosText.text = string.Concat(_PlayerOne._Player.GetPlacement().ToString() + "st");
            }
            if (_PlayerOne._Player.GetPlacement() == 2) {

                _FirstPlayerPosText.text = string.Concat(_PlayerOne._Player.GetPlacement().ToString() + "nd");
            }
            if (_PlayerOne._Player.GetPlacement() == 3) {

                _FirstPlayerPosText.text = string.Concat(_PlayerOne._Player.GetPlacement().ToString() + "rd");
            }
            if (_PlayerOne._Player.GetPlacement() == 4) {

                _FirstPlayerPosText.text = string.Concat(_PlayerOne._Player.GetPlacement().ToString() + "th");
            }

            // Get kills to text
            _FirstPlayerKillsText.text = _PlayerOne._Player.GetKillCount().ToString();

            // Get time alive to text
            _FirstPlayerMinText.text = _PlayerOne._Player.GetMinutesAlive().ToString("00");
            _FirstPlayerSecText.text = _PlayerOne._Player.GetSecondsAlive().ToString("00");
        }

        // Player TWO
        if (_PlayerTwo != null) {

            // Get placement to text
            if (_PlayerTwo._Player.GetPlacement() == 1) {

                _SecondPlayerPosText.text = string.Concat(_PlayerTwo._Player.GetPlacement().ToString() + "st");
            }
            if (_PlayerTwo._Player.GetPlacement() == 2) {

                _SecondPlayerPosText.text = string.Concat(_PlayerTwo._Player.GetPlacement().ToString() + "nd");
            }
            if (_PlayerTwo._Player.GetPlacement() == 3) {

                _SecondPlayerPosText.text = string.Concat(_PlayerTwo._Player.GetPlacement().ToString() + "rd");
            }
            if (_PlayerTwo._Player.GetPlacement() == 4) {

                _SecondPlayerPosText.text = string.Concat(_PlayerTwo._Player.GetPlacement().ToString() + "th");
            }

            // Get kills to text
            _SecondPlayerKillsText.text = _PlayerTwo._Player.GetKillCount().ToString();

            // Get time alive to text
            _SecondPlayerMinText.text = _PlayerTwo._Player.GetMinutesAlive().ToString("00");
            _SecondPlayerSecText.text = _PlayerTwo._Player.GetSecondsAlive().ToString("00");
        }

        // Player THREE
        if (_PlayerThree != null) {

            // Get placement to text
            if (_PlayerThree._Player.GetPlacement() == 1) {

                _ThirdPlayerPosText.text = string.Concat(_PlayerThree._Player.GetPlacement().ToString() + "st");
            }
            if (_PlayerThree._Player.GetPlacement() == 2) {

                _ThirdPlayerPosText.text = string.Concat(_PlayerThree._Player.GetPlacement().ToString() + "nd");
            }
            if (_PlayerThree._Player.GetPlacement() == 3) {

                _ThirdPlayerPosText.text = string.Concat(_PlayerThree._Player.GetPlacement().ToString() + "rd");
            }
            if (_PlayerThree._Player.GetPlacement() == 4) {

                _ThirdPlayerPosText.text = string.Concat(_PlayerThree._Player.GetPlacement().ToString() + "th");
            }

            // Get kills to text
            _ThirdPlayerKillsText.text = _PlayerThree._Player.GetKillCount().ToString();

            // Get time alive to text
            _ThirdPlayerMinText.text = _PlayerThree._Player.GetMinutesAlive().ToString("00");
            _ThirdPlayerSecText.text = _PlayerThree._Player.GetSecondsAlive().ToString("00");
        }

        // Player FOUR
        if (_PlayerFour != null) {

            // Get placement to text
            if (_PlayerFour._Player.GetPlacement() == 1) {

                _FourthPlayerPosText.text = string.Concat(_PlayerFour._Player.GetPlacement().ToString() + "st");
            }
            if (_PlayerFour._Player.GetPlacement() == 2) {

                _FourthPlayerPosText.text = string.Concat(_PlayerFour._Player.GetPlacement().ToString() + "nd");
            }
            if (_PlayerFour._Player.GetPlacement() == 3) {

                _FourthPlayerPosText.text = string.Concat(_PlayerFour._Player.GetPlacement().ToString() + "rd");
            }
            if (_PlayerFour._Player.GetPlacement() == 4) {

                _FourthPlayerPosText.text = string.Concat(_PlayerFour._Player.GetPlacement().ToString() + "th");
            }

            // Get kills to text
            _FourthPlayerKillsText.text = _PlayerFour._Player.GetKillCount().ToString();

            // Get time alive to text
            _FourthPlayerMinText.text = _PlayerFour._Player.GetMinutesAlive().ToString("00");
            _FourthPlayerSecText.text = _PlayerFour._Player.GetSecondsAlive().ToString("00");
        }

    }

}