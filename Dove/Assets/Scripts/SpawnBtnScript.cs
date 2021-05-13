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
    public int UnitId = 100;
    public GameObject currentLocation;
    public RegionScript locationSpawn;
    bool canSpawn = false;
    public UIManager UIManager;
    bool inBuildingMenu = false;
    bool inSpawnMenu = false;

    private void Start() 
    {
        sectorName = GameObject.Find("SectorName").GetComponent<Text>();
    }

    public void OnClick()
    {
        locationSpawn = currentLocation.GetComponent<RegionScript>();
        locationSpawn.CheckBuildings();
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

    public void SetVector(GameObject thisLocation)
    {
        currentLocation = thisLocation;
        InfoPanel.gameObject.SetActive(true);
        sectorName.text = thisLocation.gameObject.name;
    }

    void InitializeClick()
    {
        locationSpawn = currentLocation.GetComponent<RegionScript>();
        locationSpawn.CheckBuildings();
        if (locationSpawn.GetBusy() == false && UnitId < 10)
        {
            Debug.Log("Spawning");
            int Playpoints = PlayerManager.GetPlayPoints();
            if (Playpoints < 1)
            {
                UIManager.UpdatePlayerText("Not enough points");
                return;
            }

            if (UnitId == 0)
            {
                if (locationSpawn.HaveBarracks == false)
                {
                    UIManager.UpdatePlayerText("Need barracks to hire a mercinary squad");
                    return;
                }
                if (Playpoints < 2)
                {
                    UIManager.UpdatePlayerText("Not enough points");
                    return;
                }
                if (PlayerManager.MercCount() >= 4 || PlayerManager.MercHadCount() >= 10)
                {
                    UIManager.UpdatePlayerText("Mercinary limit exceeded");
                    return;
                }
                PlayerManager.MercPlus();
                PlayerManager.PlayPointsConsumed(2);
                PlayerManager.DisplayPoints();
            }

            if (UnitId == 1)
            {
                if (locationSpawn.HavePoliceStation == false)
                {
                    UIManager.UpdatePlayerText("Need a police station to hire a guards");
                    return;
                }
                if (PlayerManager.GuardCount() >= 4 || PlayerManager.GuardHadCount() >= 10)
                {
                    UIManager.UpdatePlayerText("Guard limit exceeded");
                    return;
                }
                PlayerManager.GuardPlus();
                PlayerManager.PlayPointsConsumed(1);
                PlayerManager.DisplayPoints();
            }

            if (UnitId == 2)
            {
                if (locationSpawn.HaveOffice == false)
                {
                    UIManager.UpdatePlayerText("Need an office to hire a spy");
                    return;
                }
                if (PlayerManager.SpyCount() >= 3)
                {
                    UIManager.UpdatePlayerText("Spy limit exceeded");
                    return;
                }
                if (Playpoints < 4)
                {
                    UIManager.UpdatePlayerText("Not enough money");
                    return;
                }
                PlayerManager.PlayPointsConsumed(4);
                PlayerManager.DisplayPoints();
                PlayerManager.SpyPlus();
            }

            if (inSpawnMenu && UnitId > 5) return;
            if (UnitId == 100) return;
            if (locationSpawn.IsOwnerFirst != PlayerManager.FirstPlayer) return;

            PlayerManager.UnitSpawn(UnitId, currentLocation);
            if (inSpawnMenu && UnitId != 2) PlayerManager.SetRegionBusy(locationSpawn, true);
            locationSpawn.CheckBuildings();
            UnitId = 100;
        }
        else if (UnitId >= 10 && inBuildingMenu)
        {
            Debug.Log("Building");
            int Playpoints = PlayerManager.GetPlayPoints();
            if (UnitId == 100) return;
            if (Playpoints < 5)
            {
                UIManager.UpdatePlayerText("Not enough points");
                return;
            }
            PlayerManager.PlayPointsConsumed(5);
            PlayerManager.UnitSpawn(UnitId, currentLocation);
            locationSpawn.CheckBuildings();
            PlayerManager.DisplayPoints();
            UnitId = 100;
        }
        else 
        {
            UIManager.UpdatePlayerText("Imposible. Location is already busy.");
            UnitId = 100;
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

    public void SetIDToSpy()
    {
        UnitId = 2;
    }

    public void SetIDToBarrack()
    {
        UnitId = 10;
    }

    public void SetIDToPoliceStation()
    {
        UnitId = 11;
    }

    public void SetIDToOffice()
    {
        UnitId = 12;
    }

    public void SetIDToMine()
    {
        UnitId = 13;
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

    public void InBuild()
    {
        inBuildingMenu = true;
        inSpawnMenu = false;
    }

    public void InSpawn()
    {
        inBuildingMenu = false;
        inSpawnMenu = true;
    }

}
