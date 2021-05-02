using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ReadyScript : NetworkBehaviour
{
    public UIManager UIManager;
    public GameManager GameManager;
    public GameObject ReadyCanvas;
    public GameObject ServerScreen;

    [SyncVar]
    public int ReadyClicks = 0;

    public  override void OnStartClient()
    {
        base.OnStartClient();

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        ReadyCanvas = GameObject.Find("ReadyCanvas");
        ServerScreen = GameObject.Find("ServerScreen");

    }

    [Command(ignoreAuthority = true)]
    public void CmdReadyFunc()
    {
        if (ReadyClicks >= 1)
        {
            RpcRemoveCurtain();
        } 
        else 
        {
            ReadyClicks++;
            Debug.Log("Click");
        }
        Debug.Log(ReadyClicks);
    }

    [ClientRpc]
    void RpcRemoveCurtain()
    {
        ReadyCanvas.SetActive(false);
        ServerScreen.SetActive(false);
    }
}
