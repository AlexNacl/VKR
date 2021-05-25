using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class PlayerManager : NetworkBehaviour
{
    public UIManager UIManager;
    public GameObject Mercenary;
    public GameObject Guard;
    public GameObject Spy;
    public GameObject UnitLayer;
    public GameObject PointInfo;
    public GameObject InfoText;
    public GameObject PointsTextBox;
    public NetworkManagerHUD NetworkGUI;
    public MapCombiner mapCombiner;
    public Text PointsText;
    public Text TerritoriesText;
    private List<GameObject> MercsAr = new List<GameObject>();
    private List<GameObject> GuardsAr = new List<GameObject>();
    private List<GameObject> SpyAr = new List<GameObject>();
    private List<GameObject> UnitsAr = new List<GameObject>();
    private List<GameObject> GuardUnitsAr = new List<GameObject>();
    public GameManager GameManager;
    public bool isMyTurn = false;
    public bool FirstPlayer = false;
    Vector2 NullVector = new Vector2(0,0);

    public GameObject EndCurtain;
    public Text winnerText;

    public List<GameObject> LocationSockets = new List<GameObject>();
    public List<GameObject> PlayerTerritories = new List<GameObject>();

    bool Econom = false;
    bool Millit = false;
    bool Lawyer = false;

    int turnPoints;
    int turnPointsLeft;

    int MercHave = 0;
    int MercWas = 0;

    int GuardHave = 0;
    int GuardWas = 0;

    int SpyHave = 0;

    int curRegCount = 0;

    public RegionScript regionScript;

    //Locations
    public GameObject LocationSlot1;
    public GameObject LocationSlot2;
    public GameObject LocationSlot3;
    public GameObject LocationSlot4;
    public GameObject LocationSlot5;
    public GameObject LocationSlot6;
    public GameObject LocationSlot7;
    public GameObject LocationSlot8;
    public GameObject LocationSlot9;
    public GameObject LocationSlot10;
    public GameObject LocationSlot11;
    public GameObject LocationSlot12;
    public GameObject LocationSlot13;
    public GameObject LocationSlot14;
    public GameObject LocationSlot15;
    public GameObject LocationSlot16;
    public GameObject LocationSlot17;
    public GameObject LocationSlot18;
    public GameObject LocationSlot19;
    public GameObject LocationSlot20;
    public GameObject LocationSlot21;
    public GameObject LocationSlot22;
    public GameObject LocationSlot23;
    public GameObject LocationSlot24;
    public GameObject LocationSlot25;
    public GameObject LocationSlot26;
    public GameObject LocationSlot27;
    public GameObject LocationSlot28;
    public GameObject LocationSlot29;
    public GameObject LocationSlot30;
    public GameObject LocationSlot31;
    public GameObject LocationSlot32;
    public GameObject LocationSlot33;
    public GameObject LocationSlot34;
    public GameObject LocationSlot35;
    public GameObject LocationSlot36;
    public GameObject LocationSlot37;
    public GameObject LocationSlot38;
    public GameObject LocationSlot39;
    public GameObject LocationSlot40;

    public  override void OnStartClient()
    {
        base.OnStartClient();

        NetworkGUI = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mapCombiner = GameObject.Find("MapCombiner").GetComponent<MapCombiner>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        UnitLayer = GameObject.Find("UnitLayer");
        PointInfo = GameObject.Find("PointInfo");
        InfoText = GameObject.Find("InfoBox");
        PointsTextBox = GameObject.Find("PointsTextBox");
        PointsText = GameObject.Find("Points").GetComponent<Text>();
        TerritoriesText = GameObject.Find("Territories").GetComponent<Text>();
        EndCurtain = GameObject.Find("EndCurtain");
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        
        LocationSlot1 = GameObject.Find("AeroportLoc1");
        LocationSlot2 = GameObject.Find("AeroportLoc2");
        LocationSlot3 = GameObject.Find("FinanceLoc1");
        LocationSlot4 = GameObject.Find("FinanceLoc2");
        LocationSlot5 = GameObject.Find("FinanceLoc3");
        LocationSlot6 = GameObject.Find("FinanceLoc4");
        LocationSlot7 = GameObject.Find("SlumsLoc1");
        LocationSlot8 = GameObject.Find("SlumsLoc2");
        LocationSlot9 = GameObject.Find("ClearLoc32");
        LocationSlot10 = GameObject.Find("ClearLoc1");
        LocationSlot11 = GameObject.Find("ClearLoc2");
        LocationSlot12 = GameObject.Find("ClearLoc3");
        LocationSlot13 = GameObject.Find("ClearLoc4");
        LocationSlot14 = GameObject.Find("ClearLoc5");
        LocationSlot15 = GameObject.Find("ClearLoc6");
        LocationSlot16 = GameObject.Find("ClearLoc7");
        LocationSlot17 = GameObject.Find("ClearLoc8");
        LocationSlot18 = GameObject.Find("ClearLoc9");
        LocationSlot19 = GameObject.Find("ClearLoc10");
        LocationSlot20 = GameObject.Find("ClearLoc11");
        LocationSlot21 = GameObject.Find("ClearLoc12");
        LocationSlot22 = GameObject.Find("ClearLoc13");
        LocationSlot23 = GameObject.Find("ClearLoc14");
        LocationSlot24 = GameObject.Find("ClearLoc15");
        LocationSlot25 = GameObject.Find("ClearLoc16");
        LocationSlot26 = GameObject.Find("ClearLoc17");
        LocationSlot27 = GameObject.Find("ClearLoc18");
        LocationSlot28 = GameObject.Find("ClearLoc19");
        LocationSlot29 = GameObject.Find("ClearLoc20");
        LocationSlot30 = GameObject.Find("ClearLoc21");
        LocationSlot31 = GameObject.Find("ClearLoc22");
        LocationSlot32 = GameObject.Find("ClearLoc23");
        LocationSlot33 = GameObject.Find("ClearLoc24");
        LocationSlot34 = GameObject.Find("ClearLoc25");
        LocationSlot35 = GameObject.Find("ClearLoc26");
        LocationSlot36 = GameObject.Find("ClearLoc27");
        LocationSlot37 = GameObject.Find("ClearLoc28");
        LocationSlot38 = GameObject.Find("ClearLoc29");
        LocationSlot39 = GameObject.Find("ClearLoc30");
        LocationSlot40 = GameObject.Find("ClearLoc31");

        LocationSockets.Add(LocationSlot1);
        LocationSockets.Add(LocationSlot2);
        LocationSockets.Add(LocationSlot3);
        LocationSockets.Add(LocationSlot4);
        LocationSockets.Add(LocationSlot5);
        LocationSockets.Add(LocationSlot6);
        LocationSockets.Add(LocationSlot7);
        LocationSockets.Add(LocationSlot8);
        LocationSockets.Add(LocationSlot9);
        LocationSockets.Add(LocationSlot10);
        LocationSockets.Add(LocationSlot11);
        LocationSockets.Add(LocationSlot12);
        LocationSockets.Add(LocationSlot13);
        LocationSockets.Add(LocationSlot14);
        LocationSockets.Add(LocationSlot15);
        LocationSockets.Add(LocationSlot16);
        LocationSockets.Add(LocationSlot17);
        LocationSockets.Add(LocationSlot18);
        LocationSockets.Add(LocationSlot19);
        LocationSockets.Add(LocationSlot20);
        LocationSockets.Add(LocationSlot21);
        LocationSockets.Add(LocationSlot22);
        LocationSockets.Add(LocationSlot23);
        LocationSockets.Add(LocationSlot24);
        LocationSockets.Add(LocationSlot25);
        LocationSockets.Add(LocationSlot26);
        LocationSockets.Add(LocationSlot27);
        LocationSockets.Add(LocationSlot28);
        LocationSockets.Add(LocationSlot29);
        LocationSockets.Add(LocationSlot30);
        LocationSockets.Add(LocationSlot31);
        LocationSockets.Add(LocationSlot32);
        LocationSockets.Add(LocationSlot33);
        LocationSockets.Add(LocationSlot34);
        LocationSockets.Add(LocationSlot35);
        LocationSockets.Add(LocationSlot36);
        LocationSockets.Add(LocationSlot37);
        LocationSockets.Add(LocationSlot38);
        LocationSockets.Add(LocationSlot39);
        LocationSockets.Add(LocationSlot40);
        //LocationSockets = mapCombiner.Combine();

        turnPoints = 2;
        turnPointsLeft = turnPoints;

        DisplayPoints();
        //TerCount();
        //WhoTurn();

        if (isClientOnly)
        {
            isMyTurn = true;
            FirstPlayer = true;
        }

        NetworkGUI.offsetX += 1920;
        NetworkGUI.offsetY += 1080;
    }

    [Server]
    public override void OnStartServer()
    {
        MercsAr.Add(Mercenary);
        GuardsAr.Add(Guard);
        SpyAr.Add(Spy);
    }

    public void setEco()
    {
        Econom = true;
        Millit = false;
        Lawyer = false;
    }

    public void setMil()
    {
        Econom = false;
        Millit = true;
        Lawyer = false;
    }

    public void setLaw()
    {
        Econom = false;
        Millit = false;
        Lawyer = true;
    }

    public bool GetMil(){
        return Millit;
    }

    public bool GetLaw(){
        return Lawyer;
    }

    public void PlayUnit(GameObject unit, GameObject location)
    {
        CmdPlayUnit(unit, location);
    }

    [Command]
    void CmdPlayUnit(GameObject unit, GameObject location) 
    {
        RpcShowUnit(unit, location);
    }

    public void PlayUnitSpy(GameObject unit, GameObject location)
    {
        CmdPlayUnitSpy(unit, location);
    }

    [Command]
    void CmdPlayUnitSpy(GameObject unit, GameObject location) 
    {
        RpcShowUnitSpy(unit, location);
    }

    [Command]
    public void UnitSpawn(int unitID, GameObject parent)
    {
        if (unitID == 0)
        {
            GameObject Mercenary = Instantiate(MercsAr[Random.Range(0, MercsAr.Count)], NullVector, Quaternion.identity);
            NetworkServer.Spawn(Mercenary, connectionToClient);
            RpcShowUnit(Mercenary, parent);  
            regionScript = parent.GetComponent<RegionScript>();
            regionScript.SetCurrentUnit(Mercenary);
        } 
        else if (unitID == 1)
        {
            GameObject Guard = Instantiate(GuardsAr[Random.Range(0, GuardsAr.Count)], NullVector, Quaternion.identity);
            NetworkServer.Spawn(Guard, connectionToClient);
            RpcShowUnit(Guard, parent); 
            regionScript = parent.GetComponent<RegionScript>();
            regionScript.SetCurrentUnit(Guard);
            GuardUnitsAr.Add(Guard);
        } 
        else if (unitID == 2)
        {
            GameObject Spy = Instantiate(SpyAr[Random.Range(0, SpyAr.Count)], NullVector, Quaternion.identity);
            NetworkServer.Spawn(Spy, connectionToClient);
            RpcShowUnitSpy(Spy, parent); 
            regionScript = parent.GetComponent<RegionScript>();
            if(FirstPlayer)
            {
                regionScript.SetCurrentSpyFirst(Spy); 
            }
            else
            {
                regionScript.SetCurrentSpySecond(Spy); 
            }
        } 
        else if (unitID == 10)
        {
            regionScript = parent.GetComponent<RegionScript>();
            RpcBuildBarracks(regionScript);
            regionScript.CheckBuildings();
        } 
        else if (unitID == 11)
        {
            regionScript = parent.GetComponent<RegionScript>();
            RpcBuildPolice(regionScript); 
            regionScript.CheckBuildings();
        } 
        else if (unitID == 12)
        {
            regionScript = parent.GetComponent<RegionScript>();
            RpcBuildOffice(regionScript);
            regionScript.CheckBuildings();
        } 
        else if (unitID == 13)
        {
            regionScript = parent.GetComponent<RegionScript>();
            RpcBuildMine(regionScript);
            regionScript.CheckBuildings();
        } 
    }

    [ClientRpc]
    void RpcBuildOffice(RegionScript region)
    {
        region.SetOfficeTrue();
        region.CheckBuildings();
    }
    [ClientRpc]
    void RpcBuildPolice(RegionScript region)
    {
        region.SetPoliceTrue();
        region.CheckBuildings();
    }
    [ClientRpc]
    void RpcBuildBarracks(RegionScript region)
    {
        region.SetBarracksTrue();
        region.CheckBuildings();
    }
    [ClientRpc]
    void RpcBuildMine(RegionScript region)
    {
        region.SetMineTrue();
        region.CheckBuildings();
    }

    [ClientRpc]
    void RpcShowUnit(GameObject unit, GameObject parent)
    {
         if (hasAuthority)
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y);
                UnitsAr.Add(unit);
            }
            else
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y);
                unit.GetComponent<UnitFlipper>().FlipBack();
            }
        regionScript = parent.GetComponent<RegionScript>();
        regionScript.SetCurrentUnit(unit);
        UnitInfoScript info = unit.GetComponent<UnitInfoScript>();
        UnitSetParent(info, parent);
    }

    [ClientRpc]
    void RpcShowUnitSpy(GameObject unit, GameObject parent)
    {
        if (hasAuthority)
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x+50, parent.transform.position.y+50);
                UnitsAr.Add(unit);
            }
            else
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x-50, parent.transform.position.y-50);
                unit.GetComponent<UnitFlipper>().FlipBack();
                unit.GetComponent<Image>().enabled = false;
            }
        regionScript = parent.GetComponent<RegionScript>();
    }

    public void DestroyUnit(GameObject Unit, RegionScript reg)
    {
        CmdDestroyUnit(Unit, reg);
    }

    [Command]
    void CmdDestroyUnit(GameObject Unit, RegionScript reg)
    {
        RpcDestroyUnit(Unit, reg);
    }

    [ClientRpc]
    void RpcDestroyUnit(GameObject Unit, RegionScript reg)
    {
        reg.SetBusyFalse();
        reg.CurrentUnit = null;
        UnitsAr.Remove(Unit);
        NetworkServer.Destroy(Unit);
        Debug.Log("Unit destroyed");
    }


    public void DestroyUnitSpy(GameObject Unit, RegionScript reg)
    {
        CmdDestroyUnitSpy(Unit, reg);
    }

    [Command]
    void CmdDestroyUnitSpy(GameObject Unit, RegionScript reg)
    {
        RpcDestroyUnitSpy(Unit, reg);
    }

    [ClientRpc]
    void RpcDestroyUnitSpy(GameObject Unit, RegionScript reg)
    {
        if (FirstPlayer)
        {
            reg.SetRegionHaveSecondSpyFalse();
            reg.CurrentSpySecond = null;
        } else
        {
            reg.SetRegionHaveFirstSpyFalse();
            reg.CurrentSpyFirst = null;
        }
        UnitsAr.Remove(Unit);
        NetworkServer.Destroy(Unit);
        Debug.Log("Spy destroyed");
    }


    public void UnitsDraggRefresher()
    {
        foreach (GameObject unit in UnitsAr)
        {
            if (unit != null)
            {
                if (unit.GetComponent<SoldierDragDropScript>() != null)
                {
                    unit.GetComponent<SoldierDragDropScript>().IsDrugSetTrue();
                } else {
                    unit.GetComponent<SpyDragNDrop>().IsDrugSetTrue();
                }
               
            }
        }
        foreach (GameObject location in LocationSockets)
        {
            location.GetComponent<RegionScript>().SetTurnInfluence();
        }
    }

    [ClientRpc]
    void RpcGMChangeState(string command)
    {
        GameManager.ChangeGameState(command);
        if (command == "Compile")
        {
            GameManager.ChangeReadyClicks();
        }
    }

    [ClientRpc]
    void RpcFinishTurn()
    {
        PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        pm.isMyTurn = !pm.isMyTurn;
        UnitsDraggRefresher();
        for (int i = 0; i <= LocationSockets.Count-1; i++)
            {
                regionScript = LocationSockets[i].GetComponent<RegionScript>();
                if (regionScript.IsOwnerFirst == true && regionScript.IsOwnerNone == false)
                {
                    if (regionScript.HaveOffice)
                    {
                        regionScript.AddInfluece(3);
                    }
                }
                regionScript = LocationSockets[i].GetComponent<RegionScript>();
                if (regionScript.IsOwnerFirst == false && regionScript.IsOwnerNone == false)
                {
                    if (regionScript.HaveOffice)
                    {
                        regionScript.AddInfluece(-3);
                    }
                }
            }       
    }

    public void FinishTurn()
    {
        CmdFinishTurn();
    }

    [Command]
    void CmdFinishTurn() 
    {
        RpcFinishTurn();
        RpcTerCount();
        RpcWhosTurn();
        RpcTerCount();
        RpcShowGuard();
    }

    public void ShowGuard()
    {
        CmdShowGuard();
    }

    [Command]
    void CmdShowGuard()
    {
        RpcShowGuard();
    }

    [ClientRpc]
    void RpcShowGuard()
    {
        foreach (GameObject unit in GuardsAr)
        {
            if (!hasAuthority)
                {
                    UnitInfoScript info = unit.GetComponent<UnitInfoScript>();
                    GameObject reg = info.GetParent();
                    if (reg == null) {Debug.Log("null"); return;}
                    RegionScript rg = reg.GetComponent<RegionScript>();
                    if (rg == null) {Debug.Log("null"); return;}
                    if (FirstPlayer)
                    {
                        if (rg.HaveFirstSpy)
                        {
                            unit.GetComponent<UnitFlipper>().FlipPolice();
                        } else {
                            unit.GetComponent<UnitFlipper>().FlipBack();
                        }
                    } 
                    else 
                    {
                        if (rg.HaveSecondSpy)
                        {
                            unit.GetComponent<UnitFlipper>().FlipPolice();
                        } else {
                            unit.GetComponent<UnitFlipper>().FlipBack();
                        }
                    }
                }
        }
    }

    public void WhoTurn()
    {
        CmdWhoTurn();
    }

    [Command]
    void CmdWhoTurn() 
    {
        RpcWhosTurn();
    }

    [ClientRpc]
    public void RpcWhosTurn()
    {
        PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        if (pm.isMyTurn){
            //UIManager.UpdatePlayerText("Your Turn");
            PointsTextBox.SetActive(true);
            InfoText.SetActive(false);
        } 
        if (!pm.isMyTurn)
        {
            UIManager.UpdatePlayerText("Enemy Turn");
            PointsTextBox.SetActive(false);
            InfoText.SetActive(true);
        }
    }

    public void TerCount() 
    {
        CmdTerCount();
    }

    [Command]
    void CmdTerCount() 
    {
        RpcTerCount();
    }

    [ClientRpc]
    void RpcTerCount()
    {
        int regCount = 0;
        int MineIncome = 0;
        PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        if (pm.FirstPlayer)
        {
            for (int i = 0; i <= LocationSockets.Count-1; i++)
            {
                Debug.Log("its here");
                regionScript = LocationSockets[i].GetComponent<RegionScript>();
                if (regionScript.IsOwnerFirst == true && regionScript.IsOwnerNone == false)
                {
                    regCount++;
                    if (regionScript.HaveMine && regionScript.isFinanceCener) 
                    {
                        MineIncome += 3;
                    } else if(regionScript.HaveMine || regionScript.isFinanceCener) MineIncome+=1;
                    if(Econom && regionScript.isFinanceCener)
                    {
                        MineIncome+=1;
                    }
                }
            }       
            TerritoriesText.text = regCount + " out of " + LocationSockets.Count;
            turnPoints = regCount + MineIncome + 1;
            curRegCount = regCount;
            turnPointsLeft = turnPoints;
            DisplayPoints();
            return;
        }
        if (!pm.FirstPlayer)
        {
            for (int i = 0; i <= LocationSockets.Count-1; i++)
            {
                Debug.Log("its in second here");
                regionScript = LocationSockets[i].GetComponent<RegionScript>();
                if (regionScript.IsOwnerFirst == false && regionScript.IsOwnerNone == false)
                {
                    regCount++;
                    if (regionScript.HaveMine && regionScript.isFinanceCener) 
                    {
                        MineIncome += 3;
                    } else if(regionScript.HaveMine || regionScript.isFinanceCener) MineIncome+=1;
                }
            } 
            TerritoriesText.text = regCount + " out of " + LocationSockets.Count;
            turnPoints = regCount + MineIncome + 1;
            curRegCount = regCount;
            turnPointsLeft = turnPoints;
            DisplayPoints();
            return;
        }
    }

    [Command]
    public void CmdGMChangeState(string request)
    {
        RpcGMChangeState(request);
    }

    public void SetBusy(RegionScript region, bool setter)
    {
        CmdSetBusy(region, setter);
    }

    [Command]
    void CmdSetBusy(RegionScript region, bool setter) 
    {
        RpcSetBusy(region, setter);
    }

    [ClientRpc]
    void RpcSetBusy(RegionScript region, bool setter)
    {
        if (setter)
        {
            region.SetBusyTrue();
        } else {
            region.SetBusyFalse();
        }
    }

    public void SetRegionHaveFirstSpy(RegionScript region, bool setter)
    {
        CmdSetRegionHaveFirstSpy(region, setter);
    }

    [Command]
    void CmdSetRegionHaveFirstSpy(RegionScript region, bool setter) 
    {
        RpcSetRegionHaveFirstSpy(region, setter);
    }

    [ClientRpc]
    void RpcSetRegionHaveFirstSpy(RegionScript region, bool setter)
    {
        if (setter)
        {
            region.SetRegionHaveFirstSpyTrue();
        } else {
            region.SetRegionHaveFirstSpyFalse();
        }
    }

    public void SetRegionHaveSecondSpy(RegionScript region, bool setter)
    {
        CmdSetRegionHaveSecondSpy(region, setter);
    }

    [Command]
    void CmdSetRegionHaveSecondSpy(RegionScript region, bool setter) 
    {
        RpcSetRegionHaveSecondSpy(region, setter);
    }

    [ClientRpc]
    void RpcSetRegionHaveSecondSpy(RegionScript region, bool setter)
    {
        if (setter)
        {
            region.SetRegionHaveSecondSpyTrue();
        } else {
            region.SetRegionHaveSecondSpyFalse();
        }
    }
    

    public void SetRegionOwner(RegionScript region, bool player)
    {
        CmdSetRegionOwner(region, player);
    }

    [Command]
    void CmdSetRegionOwner(RegionScript region, bool player)
    {
        RpcSetRegionOwner(region, player);
    }

    [ClientRpc]
    void RpcSetRegionOwner(RegionScript region, bool player)
    {
        region.SetOwnerCmd(player);
        region.OwnerSomebody();
        region.OutlineChange();
    }

    bool GetPlayer()
    {
        return FirstPlayer;
    }

    public void MoneyActivity(RegionScript region,  bool isFirst)
    {
        CmdMoneyActivity(region, isFirst);
    }

    [Command]
    void CmdMoneyActivity(RegionScript region,  bool isFirst)
    {
        RpcMoneyActivity(region, isFirst);
    }

    [ClientRpc]
    void RpcMoneyActivity(RegionScript region, bool isFirst)
    {
        if (isFirst)
        {
            Debug.Log("first");
            region.AddInfluece(10);
        } else {
            Debug.Log("second");
            region.AddInfluece(-10);
        }
        DisplayPoints();
    }

    public void CounterSpyActivity(RegionScript region)
    {
        CmdCounterSpyActivity(region);
    }

    [Command]
    void CmdCounterSpyActivity(RegionScript region)
    {
        RpcCounterSpyActivity(region);
    }

    [ClientRpc]
    void RpcCounterSpyActivity(RegionScript region)
    {
        if (region.IsOwnerFirst && region.HaveSecondSpy)
        {
            DestroyUnitSpy(region.CurrentSpySecond, region);
            UIManager.UpdatePlayerText("Spy eliminated");
        } else if (region.IsOwnerFirst && !region.HaveSecondSpy)
        {
            UIManager.UpdatePlayerText("No enemy spy found");
        } else
        if (!region.IsOwnerFirst && region.HaveFirstSpy)
        {
            DestroyUnitSpy(region.CurrentSpyFirst, region);
            UIManager.UpdatePlayerText("Spy eliminated");
        } else 
        if (!region.IsOwnerFirst && !region.HaveFirstSpy)
        {
            UIManager.UpdatePlayerText("No enemy spy found");
        } else
        PlayPointsConsumed(1);
    }

    public int GetPlayPoints()
    {
        return turnPointsLeft;
    }

    public void PlayPointsConsumed(int price) {
        turnPointsLeft = turnPointsLeft - price;
        DisplayPoints();
    }

    public void DisplayPoints()
    {
        PointsText.text = turnPointsLeft.ToString();
    }

    public int MercCount()
    {
        return MercHave;
    }

    public int GuardCount()
    {
        return GuardHave;
    }

    public int MercHadCount()
    {
        return MercWas;
    }

    public int GuardHadCount()
    {
        return GuardWas;
    }

    public int SpyCount()
    {
        return SpyHave;
    }

    public void MercPlus()
    {
        MercHave+=1;
        MercWas+=1;
    }

    public void GuardPlus()
    {
        GuardHave+=1;
        GuardWas+=1;
    }

    public void SpyPlus()
    {
        SpyHave+=1;
    }

    public void DestroyBuildingsInArea(RegionScript rg, PlayerManager pm)
    {
        CmdDestroyBuildingsInArea(rg, pm);
    }

    [Command]
    void CmdDestroyBuildingsInArea(RegionScript rg, PlayerManager pm)
    {
        RpcDestroyBuildingsInArea(rg, pm);
    }

    [ClientRpc]
    void RpcDestroyBuildingsInArea(RegionScript rg, PlayerManager pm)
    {
        if (rg.IsOwnerFirst != pm.FirstPlayer) rg.DestroyBuildings();
    }
    
    public void SetRegionBusy(RegionScript region, bool set)
    {
        CmdSetRegionBusy(region, set);
    }

    [Command]
    void CmdSetRegionBusy(RegionScript region, bool set)
    {
        RpcSetRegionBusy(region, set);
    }

    [ClientRpc]
    void RpcSetRegionBusy(RegionScript region, bool set)
    {
        if (set) {region.SetBusyTrue();} else {region.SetBusyFalse();}
    }

    public void SlamsEffect()
    {
        CmdSlamsEffect();
    }

    [Command]
    void CmdSlamsEffect()
    {
        RpcSlamsEffect();
    }

    [ClientRpc]
    void RpcSlamsEffect()
    {
        foreach (GameObject unit in UnitsAr)
        {
            if (unit != null)
            {
                UnitInfoScript unitScript = unit.GetComponent<UnitInfoScript>();
                if (unitScript != null)
                {
                    RegionScript parentRegion = unitScript.ParentRegion.GetComponent<RegionScript>();
                    if (parentRegion.isSlums)
                    {
                        unitScript.MinusHP(1);
                    }
                }
                
            }
            
        }
    }

    public List<GameObject> GetLocationSockets()
    {
        return LocationSockets;
    }

    public void ConnectRegions()
    {
        CmdConnectRegions();
    }

    [Command]
    void CmdConnectRegions()
    {
        RpcConnectRegions();
    }

    [ClientRpc]
    void RpcConnectRegions()
    {
        foreach (GameObject region in LocationSockets)
        {
            RegionScript reg = region.GetComponent<RegionScript>();
            reg.Connection();
        }
    }

    public void SetCurrentSpy(bool pm, RegionScript exLoc, RegionScript newLoc, GameObject unit)
    {
        CmdSetCurrentSpy(pm, exLoc, newLoc, unit);
    }

    [Command]
    public void CmdSetCurrentSpy(bool pm, RegionScript exLoc, RegionScript newLoc, GameObject unit)
    {
        RpcSetCurrentSpy(pm, exLoc, newLoc, unit);
    }

    [ClientRpc]
    public void RpcSetCurrentSpy(bool pm, RegionScript exLoc, RegionScript newLoc, GameObject unit)
    {
        if (pm)
        {
            exLoc.SetCurrentSpyFirst(null);
            exLoc.SetRegionHaveFirstSpyFalse();
            newLoc.SetCurrentSpyFirst(unit);
            newLoc.SetRegionHaveFirstSpyTrue();
        }
        else
        {
            exLoc.SetCurrentSpySecond(null);
            exLoc.SetRegionHaveSecondSpyFalse();
            newLoc.SetCurrentSpySecond(unit);
            newLoc.SetRegionHaveSecondSpyTrue();
        }
    }

    public void Sabotage(int id, RegionScript reg)
    {
        CmdSabotage(id, reg);
    }

    [Command]
    void CmdSabotage(int id, RegionScript reg)
    {
        RpcSabotage(id, reg);
    }

    [ClientRpc]
    void RpcSabotage(int id, RegionScript reg)
    {
        switch(id){
            case 1: reg.HaveBarracks = false; break;
            case 2: reg.HaveMine = false; break;
            case 3: reg.HaveOffice = false; break;
            case 4: reg.HavePoliceStation = false; break;
            default: break;
        }
        
    }

    public void SabotageUnit(RegionScript region, GameObject unit)
    {
        CmdSabotageUnit(region, unit);
    }

    [Command]
    void CmdSabotageUnit(RegionScript region, GameObject unit)
    {
        RpcSabotageUnit(region, unit);
    }

    [ClientRpc]
    void RpcSabotageUnit(RegionScript region, GameObject unit)
    {
        UnitInfoScript info = unit.GetComponent<UnitInfoScript>();
            int power = info.GetUnitOPower();
            if (power == 1)
            {
                UIManager.UpdatePlayerText("This already looks like a mess...");
            } else {
                power = power/2;
                info.MinusHP(power);
            }
    }

    public int GetCurRegCount()
    {
        Debug.Log(curRegCount);
        return curRegCount;
    }

    public int GetTerCountTotal()
    {
        Debug.Log(GetLocationSockets().Count);
        return GetLocationSockets().Count;
    }

    public void UnitSetParent(UnitInfoScript uInfo, GameObject reg)
    {
        CmdUnitSetParent(uInfo, reg);
    }

    [Command]
    public void CmdUnitSetParent(UnitInfoScript uInfo, GameObject reg)
    {
        RpcUnitSetParent(uInfo, reg);
    }

    [ClientRpc]
    public void RpcUnitSetParent(UnitInfoScript uInfo, GameObject reg)
    {
        uInfo.SetParent(reg);
    }


    public void EndGame(int res)
    {
        CmdEndGame(res);
    }

    [Command]
    void CmdEndGame(int res)
    {
        RpcEndGame(res);
    }

    [ClientRpc]
    void RpcEndGame(int res)
    {
        switch(res)
        {
            case 0:
            EndCurtain.SetActive(true);
            winnerText.text = "Game ended prematurely";
            break;
            case 1:
            EndCurtain.SetActive(true);
            winnerText.text = "Winner:\nFirst player!";
            break;
            case 2:
            EndCurtain.SetActive(true);
            winnerText.text = "Winner:\nSecond player!";        
            break;
        }
    
    }
        
}

