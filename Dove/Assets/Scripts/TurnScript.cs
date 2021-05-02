using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnScript : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public UIManager UIManager;

    private void Start() 
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isMyTurn)
        {
            PlayerManager.CmdFinishTurn();
        } 
        else 
        {
            UIManager.UpdatePlayerText("Imposible. Not Your turn.");
        }
        
    }
}
