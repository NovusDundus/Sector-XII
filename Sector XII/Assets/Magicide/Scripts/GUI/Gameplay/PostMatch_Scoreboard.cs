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
    public Text _FirstPlayerNameText;
    public Text _FirstPlayerKillsText;
    public Text _FirstPlayerMinText;
    public Text _FirstPlayerSecText;

    public Text _SecondPlayerNameText;
    public Text _SecondPlayerKillsText;
    public Text _SecondPlayerMinText;
    public Text _SecondPlayerSecText;

    public Text _ThirdPlayerNameText;
    public Text _ThirdPlayerKillsText;
    public Text _ThirdPlayerMinText;
    public Text _ThirdPlayerSecText;

    public Text _FourthPlayerNameText;
    public Text _FourthPlayerKillsText;
    public Text _FourthPlayerMinText;
    public Text _FourthPlayerSecText;
        
    //--------------------------------------------------------------
    // *** SCOREBOARD *** 

    public void ResetScoreboard() {

        foreach (var player in PlayerManager._pInstance.GetAllPlayers()) {

            // If the player is coming first
            if (player._Player.GetPlacement() == 1) {

                GetText(_FirstPlayerNameText, _FirstPlayerKillsText, _FirstPlayerMinText, _FirstPlayerSecText, player.GetComponent<Player>());
                break;
            }
        }

        foreach (var player in PlayerManager._pInstance.GetAllPlayers()) {

            // If the player is coming second
            if (player._Player.GetPlacement() == 2) {

                GetText(_SecondPlayerNameText, _SecondPlayerKillsText, _SecondPlayerMinText, _SecondPlayerSecText, player.GetComponent<Player>());
                break;
            }
        }

        foreach (var player in PlayerManager._pInstance.GetAllPlayers()) {

            // If the player is coming third
            if (player._Player.GetPlacement() == 3) {

                GetText(_ThirdPlayerNameText, _ThirdPlayerKillsText, _ThirdPlayerMinText, _ThirdPlayerSecText, player.GetComponent<Player>());
                break;
            }
        }

        foreach (var player in PlayerManager._pInstance.GetAllPlayers()) {

            // If the player is coming fourth
            if (player._Player.GetPlacement() == 4) {

                GetText(_FourthPlayerNameText, _FourthPlayerKillsText, _FourthPlayerMinText, _FourthPlayerSecText, player.GetComponent<Player>());
                break;
            }
        }
    }

    public void GetText(Text nameText, Text killText, Text minText, Text secText, Player player) {
        
        // Update player information
        switch (player._pPlayerID) {

            // Player ONE
            case 1: {

                    nameText.text = "Player One";
                    nameText.color = HUD._pInstance._PlayerOneColour;
                    killText.text = player.GetKillCount().ToString();
                    minText.text = player.GetMinutesAlive().ToString();
                    secText.text = player.GetSecondsAlive().ToString();
                    break;
                }

            // Player TWO
            case 2: {
                    
                    nameText.text = "Player Two";
                    nameText.color = HUD._pInstance._PlayerTwoColour;
                    killText.text = player.GetKillCount().ToString();
                    minText.text = player.GetMinutesAlive().ToString();
                    secText.text = player.GetSecondsAlive().ToString();
                    break;
                }
            
            // Player THREE
            case 3: {

                    nameText.text = "Player Three";
                    nameText.color = HUD._pInstance._PlayerThreeColour;
                    killText.text = player.GetKillCount().ToString();
                    minText.text = player.GetMinutesAlive().ToString();
                    secText.text = player.GetSecondsAlive().ToString();
                    break;
                }

            // Player FOUR
            case 4: {

                    nameText.text = "Player Four";
                    nameText.color = HUD._pInstance._PlayerFourColour;
                    killText.text = player.GetKillCount().ToString();
                    minText.text = player.GetMinutesAlive().ToString();
                    secText.text = player.GetSecondsAlive().ToString();
                    break;
                }

            default: {

                    nameText.text = "MISSING STRING";
                    nameText.color = Color.black;
                    killText.text = "0";
                    minText.text = "00";
                    secText.text = "00";
                    break;
                }
        }
    }

}