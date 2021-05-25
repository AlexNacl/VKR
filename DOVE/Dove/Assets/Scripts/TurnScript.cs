using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TurnScript : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public Text winnerText;
    public GameManager GameManager;
    public GameObject EndCurtain;
    public UIManager UIManager;

    float result;

    private void Start() 
    {
        EndCurtain = GameObject.Find("EndCurtain");
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.isMyTurn)
        {
            PlayerManager.FinishTurn();
            PlayerManager.DisplayPoints();
            PlayerManager.SlamsEffect();
            result = (float)PlayerManager.GetCurRegCount() / (float)PlayerManager.GetTerCountTotal();
            Debug.Log(result);
            if (result > 0.7)
            {
                FinishGame(PlayerManager);
            }
            PlayerManager.DisplayPoints();
        } 
        else 
        {
            PlayerManager.DisplayPoints();
            UIManager.UpdatePlayerText("Imposible. Not Your turn.");
        }
        
    }

    void FinishGame(PlayerManager pm)
    {
        if (pm.FirstPlayer)
        {
            PlayerManager.EndGame(1);
        } else {
            PlayerManager.EndGame(2);
        }
    }

    public void EndGame()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.FirstPlayer)
        {
            PlayerManager.EndGame(0);
        } else {
            PlayerManager.EndGame(0);
        }
    }

    public void Disconnect()
    {
        if (PlayerManager.FirstPlayer)
        {
            NetworkManager.singleton.StopClient();
        } else {
            NetworkManager.singleton.StopHost();
        }
    }
}
