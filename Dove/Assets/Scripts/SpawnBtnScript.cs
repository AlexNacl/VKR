using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SpawnBtnScript : MonoBehaviour
{
    public Text sectorName;
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public GameObject InfoPanel;
    public Vector2 spawnVector;
    public int UnitId = 0;
    public GameObject currentLocation;
    public RegionScript locationSpawn;
    bool canSpawn = false;
    public UIManager UIManager;

    private void Start() 
    {
        sectorName = GameObject.Find("SectorName").GetComponent<Text>();
    }

    public void OnClick()
    {
        InfoPanel = GameObject.Find("InfoPanel");
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (!PlayerManager.isMyTurn) {
            UIManager.UpdatePlayerText("Imposible. Not Your turn.");
            return;
        }
        if (canSpawn == false){
            UIManager.UpdatePlayerText("Imposible. Need to choose Your territory.");
            return;
        }
        InitializeClick();
    }

    public void SetVector(Vector2 vector, GameObject thisLocation)
    {
        currentLocation = thisLocation;
        spawnVector = vector;
        InfoPanel.gameObject.SetActive(true);
        sectorName.text = thisLocation.gameObject.name;
    }

    void InitializeClick()
    {
        locationSpawn = currentLocation.GetComponent<RegionScript>();
        if (locationSpawn.GetBusy() == false)
        {
            PlayerManager.UnitSpawn(spawnVector, UnitId, currentLocation);
            locationSpawn.SetBusyTrue();
        }
        else 
        {
            UIManager.UpdatePlayerText("Imposible. Location is already busy.");
        }
    }

    public void SetName(GameObject thisLocation)
    {
        sectorName.text = thisLocation.gameObject.name;
    }

    public void SetIDToMercenary()
    {
        UnitId = 0;
    }

    public void SetIDToGuard()
    {
        UnitId = 1;
    }

    public void SetSpawnTrue()
    {
        canSpawn = true;
    }

    public void SetSpawnFalse()
    {
        canSpawn = false;
    }

    public bool GetSpawn()
    {
        return canSpawn;
    }

}
