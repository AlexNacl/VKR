using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int TurnOrder = 0;
    private int ReadyClicks = 0;
    public string GameState = "Initialized";
    public UIManager UIManager;

    private void Start() 
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        UIManager.UpdatePlayerText("");
        UIManager.ButtonUpdateText(GameState);
    }

    public void ChangeGameState(string stateRequest)
    {
        if (stateRequest == "Initialized")
        {
            ReadyClicks = 0;
            GameState = "Initialized";
        }
        else if (stateRequest == "Compile")
        {
            if (ReadyClicks == 1)
            {
                GameState = "Compile";
                Debug.Log("TurnOrder" + TurnOrder);
            } 
            else
            {
                ReadyClicks++;
            }
            
        }
        else if (stateRequest == "Restart")
        {
            GameState = "Restart";
            TurnOrder = 0;
        }
        UIManager.ButtonUpdateText(GameState);
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

    public void TurnFinished()
    {
        TurnOrder++;
    }
}
