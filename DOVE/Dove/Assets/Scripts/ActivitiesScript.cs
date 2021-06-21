using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ActivitiesScript : MonoBehaviour
{
    bool MoneyActivity = false;
    bool CounterSpyActivity = false;
    bool RumorsActivity = false;
    bool BuyAuthorities = false;
    bool SabotageBarracks = false;
    bool SabotageMine = false;
    bool SabotageOffice = false;
    bool SabotagePolice = false;
    bool SabotageUnit = false;
    public PlayerManager PlayerManager;
    public GameObject currentLocation;
    public RegionScript locationSpawn;
    public UIManager UIManager;
    public GameManager GameManager;
    public GameObject sabotageList;

    public void OnClick()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (!PlayerManager.isMyTurn) {
            UIManager.UpdatePlayerText("Imposible. Not Your turn.");
            return;
        }
        int Playpoints = PlayerManager.GetPlayPoints();
        if (Playpoints < 1)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        if (MoneyActivity)
        {
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            MoneyActivity = false;
            if(locationSpawn.isSlums)
            {
                PlayerManager.PlayPointsConsumed(2);
            } else 
            {
                PlayerManager.PlayPointsConsumed(1);
            }
        }
        if (CounterSpyActivity)
        {
            PlayerManager.CounterSpyActivity(locationSpawn);
            CounterSpyActivity = false;
            PlayerManager.PlayPointsConsumed(1);
        }
        if (RumorsActivity)
        {
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            RumorsActivity = false;
            PlayerManager.PlayPointsConsumed(1);
        }
        if (BuyAuthorities)
        {
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            PlayerManager.MoneyActivity(locationSpawn, PlayerManager.FirstPlayer);
            BuyAuthorities = false;
            PlayerManager.PlayPointsConsumed(3);
        }   
        if (SabotageBarracks)
        {
            PlayerManager.Sabotage(1, locationSpawn);
            PlayerManager.PlayPointsConsumed(4);
            SabotageBarracks = false;
        }
        if (SabotageMine)
        {
            PlayerManager.Sabotage(2, locationSpawn);
            PlayerManager.PlayPointsConsumed(3);
            SabotageMine = false;
        }
        if (SabotageOffice)
        {
            PlayerManager.Sabotage(3, locationSpawn);
            PlayerManager.PlayPointsConsumed(5);
            SabotageOffice = false;
        }
        if (SabotagePolice)
        {
            PlayerManager.Sabotage(4, locationSpawn);
            PlayerManager.PlayPointsConsumed(7);
            SabotagePolice = false;
        }
        if (SabotageUnit)
        {
            if (!locationSpawn.GetBusy()) return;
            PlayerManager.PlayPointsConsumed(3);
            GameObject unit = locationSpawn.CurrentUnit;
            PlayerManager.SabotageUnit(locationSpawn, unit);
            SabotageUnit = false;
        }
        UIManager.UpdatePlayerText(" ");
    }

    public void SetLocation(GameObject thisLocation)
    {
        currentLocation = thisLocation;
        locationSpawn = currentLocation.GetComponent<RegionScript>();
    }
    
    public void MoneyActivitySpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (Playpoints < 1)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        MoneyActivity = true;
        OnClick();
    }

    public void CounterSpyActivitySpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (Playpoints < 1)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        if (locationSpawn.HavePoliceStation)
        {
            CounterSpyActivity = true;
            OnClick();
        } else {
            UIManager.UpdatePlayerText("No police is around");
        }
        
    }

    public void RumorsActivitySpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (Playpoints < 1)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        RumorsActivity = true;
        OnClick();
    }

    public void BuyAuthoritiesSpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (Playpoints < 3)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        if (!locationSpawn.HavePoliceStation)
        {
            BuyAuthorities = true;
            OnClick();
        } else {
            UIManager.UpdatePlayerText("You can't do this while Police is whatching");
        }
    }

    public void SabotageSpawn()
    {
        sabotageList.SetActive(true);
    }

    public void SabotageBarracksSpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (!locationSpawn.HaveBarracks)
        {
            UIManager.UpdatePlayerText("There is no such building");
            return;
        }
        if (Playpoints < 4)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        SabotageBarracks = true;
        OnClick();
    }

    public void SabotageMineSpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (!locationSpawn.HaveMine)
        {
            UIManager.UpdatePlayerText("There is no such building");
            return;
        }
        if (Playpoints < 3)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        SabotageMine = true;
        OnClick();
    }

    public void SabotageOfficeSpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (!locationSpawn.HaveOffice)
        {
            UIManager.UpdatePlayerText("There is no such building");
            return;
        }
        if (Playpoints < 5)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        SabotageOffice = true;
        OnClick();
    }

    public void SabotagePoliceSpawn()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        int Playpoints = PlayerManager.GetPlayPoints();
        if (!locationSpawn.HavePoliceStation)
        {
            UIManager.UpdatePlayerText("There is no such building");
            return;
        }
        if (Playpoints < 7)
        {
            UIManager.UpdatePlayerText("Not enough points");
            return;
        }
        SabotagePolice = true;
        OnClick();
    }

    public void SabotageUnitSpawn()
    {
        if (!locationSpawn.GetBusy()) return;
        GameObject unit = locationSpawn.CurrentUnit;
        if (unit != null)
        {
            SabotageUnit = true;
            OnClick();
        }
    }
}
