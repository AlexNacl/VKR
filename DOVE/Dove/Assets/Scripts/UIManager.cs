using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject button;
    public GameObject playerText;
    public GameObject playerTurnText;

    private void Start() 
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerText = GameObject.Find("ExternalInfo");
        playerTurnText = GameObject.Find("TurnInfo");
    }

    public void UpdatePlayerText(string text)
    {
        playerText.GetComponent<Text>().text = text;
    }

    public void UpdateTurnText(string text)
    {
        playerTurnText.GetComponent<Text>().text = text;
    }


    public void ButtonUpdateText(string GameState)
    {
        button = GameObject.Find("FinishTurnButton");
        button.GetComponentInChildren<Text>().text = GameState;
    }

}
