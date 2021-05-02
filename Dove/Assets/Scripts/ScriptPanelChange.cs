using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ScriptPanelChange : MonoBehaviour
{
    public GameObject ChangeBtn;
    public Text ChangeBtnText;
    public GameObject InfoList;
    public GameObject UnitList;
    public GameObject SpawnBtn;
    public SpawnBtnScript SpawnBtnScript;
    public int State = 0;

     private void Start() 
    {
        UnitList = GameObject.Find("UnitList");
        InfoList = GameObject.Find("InfoList");
        SpawnBtnScript = GameObject.Find("SpawnBtn").GetComponent<SpawnBtnScript>();
    }

    public void OnClick()
    {
        ChangeBtn = GameObject.Find("ChangeBtn");
        ChangeBtnText = GameObject.Find("ChangeBtnText").GetComponent<Text>();
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        if (SpawnBtnScript.GetSpawn())
        {
            if (State == 0)
            {
                toUnits();
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
        ChangeBtnText.text = "To info";
        State = 1;
    }

    void toPanel()
    {
        SpawnBtn.gameObject.SetActive(false);
        UnitList.gameObject.SetActive(false);
        InfoList.gameObject.SetActive(true);
        ChangeBtnText.text = "To units";
        State = 0;
    }
}
