using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ScriptPanelChange : MonoBehaviour
{
    public GameObject ChangeBtn;
    public Text ChangeBtnText;
    public Text BuildingBtnText;
    public Text SpawnBtnText;
    public GameObject InfoList;
    public GameObject UnitList;
    public GameObject BuildingList;
    public GameObject SpawnBtn;
    public GameObject BuildingBtn;
    public ScriptPanelChange AnotherBtn;
    public SpawnBtnScript SpawnBtnScript;
    public int State = 0;

     private void Start() 
    {
        UnitList = GameObject.Find("UnitList");
        InfoList = GameObject.Find("InfoList");
        SpawnBtnScript = GameObject.Find("SpawnBtn").GetComponent<SpawnBtnScript>();
        SpawnBtnText= GameObject.Find("SpawnBtnText").GetComponent<Text>();
    }

    public void OnClickChange()
    {
        ChangeBtn = GameObject.Find("ChangeBtn");
        ChangeBtnText = GameObject.Find("ChangeBtnText").GetComponent<Text>();
        BuildingBtnText = GameObject.Find("BuildingBtnText").GetComponent<Text>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        if (SpawnBtnScript.GetSpawn())
        {
            if (State == 0 || State == 2)
            {
                toUnits();
                BuildingBtnText.text = "To buildings";
            } 
            else 
            {
                toPanel();
            }
        }
        
    }

    public void OnClickBuild()
    {
        BuildingBtn = GameObject.Find("BuildingBtn");
        BuildingBtnText = GameObject.Find("BuildingBtnText").GetComponent<Text>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        if (SpawnBtnScript.GetSpawn())
        {
            if (State == 0 || State == 1)
            {
                toBuildings();
                ChangeBtnText.text = "To units";
            } 
            else 
            {
                toPanel();
            }
        }
        
    }

    void toUnits()
    {
        SpawnBtn.gameObject.SetActive(true);
        UnitList.gameObject.SetActive(true);
        InfoList.gameObject.SetActive(false);
        BuildingList.gameObject.SetActive(false);
        ChangeBtnText.text = "To info";
        SpawnBtnText.text = "Deploy";
        SpawnBtnScript.InSpawn();
        State = 1;
        AnotherBtn.State = 1;
    }

    void toBuildings()
    {
        SpawnBtn.gameObject.SetActive(true);
        BuildingList.gameObject.SetActive(true);
        InfoList.gameObject.SetActive(false);
        UnitList.gameObject.SetActive(false);
        BuildingBtnText.text = "To info";
        SpawnBtnText.text = "Build";
        SpawnBtnScript.InBuild();
        State = 2;
        AnotherBtn.State = 2;
    }

    void toPanel()
    {
        SpawnBtn.gameObject.SetActive(false);
        UnitList.gameObject.SetActive(false);
        InfoList.gameObject.SetActive(true);
        BuildingList.gameObject.SetActive(false);
        ChangeBtnText.text = "To units";
        BuildingBtnText.text = "To buildings";
        State = 0;
        AnotherBtn.State = 0;
    }

    public void toNormal()
    {
        toPanel();
    }
}
