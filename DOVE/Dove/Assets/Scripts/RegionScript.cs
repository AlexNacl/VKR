using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class RegionScript : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public Outline regionOutline;
    public Image regionImage;
    public RegionScript region;
    public GameManager GameManager;
    public UIManager UIManager;
    public SpawnBtnScript SpawnBtnScript;
    public ScriptPanelChange PanelChange;
    public ActivitiesScript activitiesScript;
    public ActivitiesScript SpyactivitiesScript;
    public GameObject ChangeBtn;
    public GameObject ActivitiesBtn;
    public GameObject SpyActivitiesBtn;
    public GameObject BuildingBtn;
    public GameObject SpyFirst;
    public GameObject SpySecond; 
    public GameObject CurrentUnit;
    public GameObject CurrentSpyFirst;
    public GameObject CurrentSpySecond;
    public GameObject InfoPanel;
    public GameObject InfoList;
    public GameObject buildingList;
    public GameObject UnitList;
    public GameObject SpawnBtn;
    public GameObject ActivitiSpy;
    public GameObject Activiti;
    public GameObject ActivitiSpyClose;
    public GameObject ActivitiClose;

    public bool isHomeforFirst;
    public bool isHomeforSecond;
    public bool isFinanceCener;
    public bool isSlums;
    public bool isAeroport;

    public bool IsOwnerNone = true;
    public bool IsOwnerFirst = false;
    public bool HaveFirstSpy = false;
    public bool HaveSecondSpy = false;
    public bool isBusy = false;

    public bool HaveOffice = false;
    public bool HavePoliceStation = false;
    public bool HaveBarracks = false;
    public bool HaveMine = false;

    //Обводка
    Color firstPlayerColor = new Vector4 (1,0,1,0.5f);
    Color secondPlayerColor = new Vector4 (1,0,0,0.5f);
    Color nonePlayerColor = new Vector4 (0.5f,0.5f,0.5f,0.5f);
    Color ActiveColor = new Vector4 (1f,1f,1f,0.5f);
    Color ChooseActiveColor = new Vector4 (0f,1f,0f,0.5f);
    Color firstPlayerColorImg = new Vector4 (1,0,1,1);
    Color secondPlayerColorImg = new Vector4 (1,0,0,1);
    Color nonePlayerColorImg = new Vector4 (0.5f,0.5f,0.5f,1);
    Color ActiveColorImg = new Vector4 (1f,1f,1f,1);


    //Объекты зданий и их постройки
    public GameObject OfficeBtn;
    public GameObject PoliceBtn;
    public GameObject BarracksBtn;
    public GameObject MineBtn;
    public GameObject OfficeBtnActive;
    public GameObject PoliceBtnActive;
    public GameObject BarracksBtnActive;
    public GameObject MineBtnActive;


    //Логика влияния
    public int Influence = 50;
    public Slider slider;
    public GameObject sliderObject;
    public Text alliagnceText;

    //Соседние локации
    public GameObject Neighbour1;
    public GameObject Neighbour2;
    public GameObject Neighbour3;
    public GameObject Neighbour4;

    //Отображение зданий
    public GameObject OfficeLabel;
    public GameObject PoliceLabel;
    public GameObject BarrackLabel;
    public GameObject StockLabel;

    public RegionScript NeighbourScript1;
    public RegionScript NeighbourScript2;
    public RegionScript NeighbourScript3;
    public RegionScript NeighbourScript4;

    private List<GameObject> NeighboursArr = new List<GameObject>();
    private List<GameObject> AeroportsArr = new List<GameObject>();
    private List<GameObject> LocationsArr = new List<GameObject>();
    private List<RegionScript> NeighbourScriptArr = new List<RegionScript>();
    public int NeighbourCount;

    public  override void OnStartClient()
    {
        base.OnStartClient();
        OfficeLabel = transform.Find("OfficeLabel").gameObject;
        PoliceLabel = transform.Find("PoliceLabel").gameObject;
        BarrackLabel = transform.Find("BarracksLabel").gameObject;
        StockLabel = transform.Find("StockLabel").gameObject;
        PanelChange = GameObject.Find("ChangeBtn").GetComponent<ScriptPanelChange>();
        SpawnBtn = GameObject.Find("SpawnBtn");
        sliderObject = GameObject.Find("Slider");
        alliagnceText = GameObject.Find("AlliagnceText").GetComponent<Text>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        regionOutline = this.gameObject.GetComponent<Outline>();
        regionImage = this.gameObject.GetComponent<Image>();
        region = this.gameObject.GetComponent<RegionScript>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        activitiesScript = GameObject.Find("Activities").GetComponent<ActivitiesScript>();
        SpyactivitiesScript = GameObject.Find("SpyActivities").GetComponent<ActivitiesScript>();
        SpyActivitiesBtn = GameObject.Find("SpyActivitiesMenuOpen");
        SpawnBtnScript = GameObject.Find("SpawnBtn").GetComponent<SpawnBtnScript>();
        InfoPanel = GameObject.Find("InfoPanel");
        ActivitiSpy = GameObject.Find("SpyActivities");
        Activiti = GameObject.Find("Activities");
        ActivitiesBtn = GameObject.Find("ActivitiesMenuOpen");
        ActivitiClose = GameObject.Find("ActivitiesMenuClose");
        ActivitiSpyClose = GameObject.Find("SpyActivitiesMenuClose");
        ChangeBtn = GameObject.Find("ChangeBtn");
        BuildingBtn = GameObject.Find("BuildingBtn");
        OfficeBtn = GameObject.Find("Office");
        PoliceBtn = GameObject.Find("Police");
        BarracksBtn = GameObject.Find("Barracks");
        MineBtn = GameObject.Find("Mine");
        OfficeBtnActive = GameObject.Find("OfficeActive");
        PoliceBtnActive = GameObject.Find("PoliceActive");
        BarracksBtnActive = GameObject.Find("BarracksActive");
        MineBtnActive = GameObject.Find("MineActive");
        InfoList = GameObject.Find("InfoList");
        buildingList = GameObject.Find("BuildingList");
        UnitList = GameObject.Find("UnitList");
        OutlineChange();

        if (Neighbour1 != null)
        {
            Debug.Log("Found!");
            NeighbourScript1 = Neighbour1.GetComponent<RegionScript>();
            NeighboursArr.Add(Neighbour1);
            NeighbourScriptArr.Add(NeighbourScript1);
        }
        if (Neighbour2 != null)
        {
            NeighbourScript2 = Neighbour2.GetComponent<RegionScript>();
            NeighboursArr.Add(Neighbour2);
            NeighbourScriptArr.Add(NeighbourScript2);
        }
        if (Neighbour3 != null)
        {
            NeighbourScript3 = Neighbour3.GetComponent<RegionScript>();
            NeighboursArr.Add(Neighbour3);
            NeighbourScriptArr.Add(NeighbourScript3);
        }
        if (Neighbour4 != null)
        {
            NeighbourScript4 = Neighbour4.GetComponent<RegionScript>();
            NeighboursArr.Add(Neighbour4);
            NeighbourScriptArr.Add(NeighbourScript4);
        }

    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        PlayerManager.OutlineBack();
        regionOutline.effectColor = ChooseActiveColor;
        
        UIManager.UpdatePlayerText(" ");
        SliderSet();
        AlliagnceSet();
        activitiesScript.SetLocation(this.gameObject);
        SpyactivitiesScript.SetLocation(this.gameObject);
        buildingList.SetActive(false);
        UnitList.SetActive(false);
        InfoList.SetActive(true);
        InfoPanel.SetActive(true);
        OfficeBtn.SetActive(true);
        PoliceBtn.SetActive(true);
        BarracksBtn.SetActive(true);
        MineBtn.SetActive(true);
        OfficeBtnActive.SetActive(false);
        PoliceBtnActive.SetActive(false);
        BarracksBtnActive.SetActive(false);
        MineBtnActive.SetActive(false);
        
        CheckBuildings();

        SpyActivitiesBtn.SetActive(false);
        ActivitiesBtn.SetActive(false);
        ChangeBtn.SetActive(false);
        BuildingBtn.SetActive(false);
        SpawnBtn.SetActive(false);
        Activiti.SetActive(false);
        ActivitiSpy.SetActive(false);
        ActivitiSpyClose.SetActive(false);
        ActivitiClose.SetActive(false);
        PanelChange.toNormal();
        Debug.Log("Click");
            SpawnBtnScript.SetName(this.gameObject);
        if (!IsOwnerNone && PlayerManager.FirstPlayer == IsOwnerFirst)
        {
            if (isMineCheck())
            {
                SpawnBtnScript.SetVector(this.gameObject);
                activitiesScript.SetLocation(this.gameObject);
                ChangeBtn.SetActive(true);
                BuildingBtn.SetActive(true);
                ActivitiesBtn.SetActive(true);
            }    
        }
        if (IsOwnerNone)
        {
            if (PlayerManager.FirstPlayer)
            {
                if (HaveFirstSpy)
                {
                    SpyActivitiesBtn.SetActive(true);
                }
            } 
            if(!PlayerManager.FirstPlayer)
            {
                if (HaveSecondSpy)
                {
                    SpyActivitiesBtn.SetActive(true);
                }
            } 
        }
        if (PlayerManager.FirstPlayer != IsOwnerFirst)
        {
            ChangeBtn.SetActive(false);
            BuildingBtn.SetActive(false);
            ActivitiesBtn.SetActive(false);
            if (PlayerManager.FirstPlayer)
            {
                if (HaveFirstSpy)
                {
                    SpyActivitiesBtn.SetActive(true);
                }
            } 
            if(!PlayerManager.FirstPlayer)
            {
                if (HaveSecondSpy)
                {
                    SpyActivitiesBtn.SetActive(true);
                }
            } 
        }
    }

    public bool isMineCheck()
    {
        if (!IsOwnerNone && PlayerManager.FirstPlayer == IsOwnerFirst) {
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

    public bool GetFirstSpy()
    {
        return HaveFirstSpy;
    }

    public bool GetSecondSpy()
    {
        return HaveSecondSpy;
    }

    public void SetRegionHaveFirstSpyTrue()
    {
        HaveFirstSpy = true;
    }

    public void SetRegionHaveFirstSpyFalse()
    {
        HaveFirstSpy = false;
    }

    public void SetRegionHaveSecondSpyTrue()
    {
        HaveSecondSpy = true;
    }

    public void SetRegionHaveSecondSpyFalse()
    {
        HaveSecondSpy = false;
    }

    public void SetOfficeTrue()
    {
        HaveOffice = true;
    }

    public void SetOfficeFalse()
    {
        HaveOffice = false;
    }

    public void SetPoliceTrue()
    {
        HavePoliceStation = true;
    }

    public void SetPoliceFalse()
    {
        HavePoliceStation = false;
    }

    public void SetBarracksTrue()
    {
        HaveBarracks = true;
    }

    public void SetBarracksFalse()
    {
        HaveBarracks = false;
    }

    public void SetMineTrue()
    {
        HaveMine = true;
    }

    public void SetMineFalse()
    {
        HaveMine = false;
    }

    public void CheckBuildings()
    {
        Debug.Log("checking buildings");
        if (HaveOffice) 
        {
            OfficeBtn.SetActive(false);
            OfficeBtnActive.SetActive(true);
        }
        if (HavePoliceStation)
        {
            PoliceBtn.SetActive(false);
            PoliceBtnActive.SetActive(true);
        } 
        if (HaveBarracks) 
        {
            BarracksBtn.SetActive(false);
            BarracksBtnActive.SetActive(true);
        }
        if (HaveMine)
        {
            MineBtn.SetActive(false);
            MineBtnActive.SetActive(true);
        } 
    }

    public void OwnerSet(bool player)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.SetRegionOwner(region, player);
        OutlineChange();
    }

    public void OwnerSomebody()
    {
        IsOwnerNone = false;
    }

    public void SetOwnerCmd(bool isFirst)
    {
        IsOwnerFirst = isFirst;
    }

    public void LabelsChange()
    {
        if(IsOwnerFirst == PlayerManager.FirstPlayer)
        {
            if (HaveMine) {StockLabel.SetActive(true);} else if (StockLabel!=null) {StockLabel.SetActive(false);}
            if (HaveOffice) {OfficeLabel.SetActive(true);} else if (OfficeLabel!=null) {OfficeLabel.SetActive(false);}
            if (HaveBarracks) {BarrackLabel.SetActive(true);} else if (BarrackLabel!=null) {BarrackLabel.SetActive(false);}
            if (HavePoliceStation) {PoliceLabel.SetActive(true);} else if (PoliceLabel!=null) {PoliceLabel.SetActive(false);}
        } else 
        {
            if (StockLabel!=null) {StockLabel.SetActive(false);}
            if (OfficeLabel!=null) {OfficeLabel.SetActive(false);}
            if (BarrackLabel!=null) {BarrackLabel.SetActive(false);}
            if (PoliceLabel!=null) {PoliceLabel.SetActive(false);}
        }
    }

    public void OutlineChange()
    {
        StockLabel.SetActive(false);
        OfficeLabel.SetActive(false);
        BarrackLabel.SetActive(false);
        PoliceLabel.SetActive(false);

        if (IsOwnerNone) 
        {
            regionOutline.effectColor = nonePlayerColor;
            regionImage.color = nonePlayerColorImg;
        }
        if (!IsOwnerNone && IsOwnerFirst)
        {
            regionOutline.effectColor = firstPlayerColor;
            regionImage.color = firstPlayerColorImg;
        } 
        if (!IsOwnerNone && !IsOwnerFirst) 
        {
            regionOutline.effectColor = secondPlayerColor;
            regionImage.color = secondPlayerColorImg;
        }
        if (PlayerManager != null)
        {
            LabelsChange();
        }
    }

    public void SliderSet()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (PlayerManager.FirstPlayer)
        {
            slider.value = Influence;
        } 
        else if (!PlayerManager.FirstPlayer)
        {
            slider.value = 100 - Influence;
        } 
    }

    public void AlliagnceSet()
    {
        if (Influence < 25 && PlayerManager.FirstPlayer)
        {
            alliagnceText.text = "Enemy";
        } 
        else if (Influence < 25 && !PlayerManager.FirstPlayer)
        {
            alliagnceText.text = "You";
        } 
        else if (Influence >= 25 && Influence <= 75)
        {
            alliagnceText.text = "None";
        }
        else if (Influence > 75 && PlayerManager.FirstPlayer)
        {
            alliagnceText.text = "You";
        }
        else if (Influence > 75 && !PlayerManager.FirstPlayer)
        {
            alliagnceText.text = "Enemy";
        }
    }

    public void AddInfluece(int add)
    {
        if (Influence <= 100 && Influence >= 0)
        {
            Influence += add;
        } 
        if(Influence > 100) Influence = 100;
        if(Influence < 0) Influence = 0;
        SliderSet();
    }

    public void SetTurnInfluence()
    {
        if (isBusy)
        {
            if (Influence >= 15 && Influence < 50)
            {
                Influence+=3;
            } 
            if (Influence > 50 && Influence <= 85)
            {
                Influence-=3;
            } 
            return;
        }
        if (Influence < 15)
        {
            IsOwnerFirst = false;
            IsOwnerNone = false;
        } 
        if (Influence >= 15 && Influence < 50)
        {
            IsOwnerFirst = false;
            IsOwnerNone = true;
            Influence+=3;
        } 
        if (Influence == 50)
        {
            IsOwnerFirst = false;
            IsOwnerNone = true;
        } 
        if (Influence > 50 && Influence <= 85)
        {
            IsOwnerFirst = false;
            IsOwnerNone = true;
            Influence-=3;
        } 
        if (Influence > 85)
        {
            IsOwnerFirst = true;
            IsOwnerNone = false;
        } 
        OutlineChange();
    }

    public GameObject IfIsNeighbours(int StepRegion)
    {
        Debug.Log("Checking, i = " + StepRegion + " and NeighboursArr = " + NeighboursArr.Count);
        GameObject istrue = NeighboursArr[StepRegion];
        return istrue;
    }

    public int NeighboursCountGet()
    {
        return NeighbourCount;
    }

    public void SetNeighbourRegionsFlicker(bool set)
    {
        switch(NeighboursCountGet())
        {
            case 1:
            if (set)
            {
                Outline outline = Neighbour1.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
            }
            else
            {
                RegionScript regionScript = Neighbour1.GetComponent<RegionScript>();
                regionScript.OutlineChange();
            }
            break;

            case 2:
            if (set)
            {
                Outline outline = Neighbour1.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour2.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
            }
            else
            {
                RegionScript regionScript = Neighbour1.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour2.GetComponent<RegionScript>();
                regionScript.OutlineChange();
            }
            break;

            case 3:
           if (set)
            {
                Outline outline = Neighbour1.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour2.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour3.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
            }
            else
            {
                RegionScript regionScript = Neighbour1.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour2.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour3.GetComponent<RegionScript>();
                regionScript.OutlineChange();
            }
            break;

            case 4:
            if (set)
            {
                Outline outline = Neighbour1.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour2.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour3.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                outline = Neighbour4.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
            }
            else
            {
                RegionScript regionScript = Neighbour1.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour2.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour3.GetComponent<RegionScript>();
                regionScript.OutlineChange();
                regionScript = Neighbour4.GetComponent<RegionScript>();
                regionScript.OutlineChange();
            }
            break;

            default:
            break;
        }

        foreach (GameObject location in AeroportsArr)
        {
            RegionScript reg = location.GetComponent<RegionScript>();
            if (set)
            {
                if (reg.IsOwnerFirst == PlayerManager.FirstPlayer && reg.IsOwnerNone == false)
                {
                Outline outline = location.GetComponent<Outline>();
                outline.effectColor = ActiveColor;
                }  
            } else 
            {
                RegionScript regionScript = location.GetComponent<RegionScript>();
                regionScript.OutlineChange();
            } 
        }
    }

    public void DestroyBuildings()
    {
        HaveBarracks = false;
        HaveOffice = false;
        HaveMine = false;
        HavePoliceStation = false;
    }

    public void SetCurrentUnit(GameObject unit)
    {
        CurrentUnit = unit;
    }

    public void SetCurrentSpyFirst(GameObject unit)
    {
        CurrentSpyFirst = unit;
    }

    public void SetCurrentSpySecond(GameObject unit)
    {
        CurrentSpySecond = unit;
    }

    public GameObject GetCurrentUnit()
    {
        return CurrentUnit;
    }

    public List<GameObject> GetAeroports()
    {
        return AeroportsArr;
    }

    public void Connection()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        LocationsArr = PlayerManager.GetLocationSockets();
        foreach (GameObject location in LocationsArr)
        {
            RegionScript region = location.GetComponent<RegionScript>();
            if (region.isAeroport)
            {
                AeroportsArr.Add(location);
            }
        }
    }


}
