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

    private void Start() 
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerText = GameObject.Find("TurnInfo");
    }

    public void UpdatePlayerText(string text)
    {
        playerText.GetComponent<Text>().text = text;
    }


    public void ButtonUpdateText(string GameState)
    {
        button = GameObject.Find("FinishTurnButton");
        button.GetComponentInChildren<Text>().text = GameState;
    }

}
