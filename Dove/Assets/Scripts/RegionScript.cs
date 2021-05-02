using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class RegionScript : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public UIManager UIManager;
    public SpawnBtnScript SpawnBtnScript;
    public GameObject ChangeBtn;
    public bool IsOwnerFirst = false;
    public Vector2 startPoint;
    public bool isBusy = false;

    public  override void OnStartClient()
    {
        base.OnStartClient();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        SpawnBtnScript = GameObject.Find("SpawnBtn").GetComponent<SpawnBtnScript>();
        ChangeBtn = GameObject.Find("ChangeBtn");
        startPoint = new Vector2 (this.transform.position.x, this.transform.position.y);
    }

    public void OnClick()
    {
        ChangeBtn.SetActive(false);
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        Debug.Log("Click");
            SpawnBtnScript.SetName(this.gameObject);
        if (PlayerManager.FirstPlayer == IsOwnerFirst)
        {
            if (isMineCheck())
            {
                SpawnBtnScript.SetVector(startPoint, this.gameObject);
                ChangeBtn.SetActive(true);
            }     
        }
    }

    public bool isMineCheck()
    {
        if (PlayerManager.FirstPlayer == IsOwnerFirst) {
            SpawnBtnScript.SetSpawnTrue();
            return true;
        } else {
            SpawnBtnScript.SetSpawnFalse();
            return false;
        }
    }

    public bool GetBusy()
    {
        return isBusy;
    }

    public void SetBusyTrue()
    {
        isBusy = true;
    }

    public void SetBusyFalse()
    {
        isBusy = false;
    }

}
