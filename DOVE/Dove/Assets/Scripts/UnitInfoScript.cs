using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitInfoScript : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public PlayerManager PlayerManager;
    public GameObject ParentRegion;

    public bool isOwnerFirst;

    public int Power;

    private void Start() {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Canvas = GameObject.Find("MainCanvas"); 
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        isOwnerFirst = PlayerManager.FirstPlayer;
        ParentRegion = this.gameObject.transform.parent.gameObject;
    }


    public bool GetUnitOwner()
    {
        return isOwnerFirst;
    }

    public int GetUnitOPower()
    {
        return Power;
    }

    public void SetHP(int HP)
    {
        Power = HP;
    }

    public void MinusHP(int HP)
    {
        Power -= HP;
        if (Power <= 0)
        {
            DestroyUnit();
        }
    }

    public void PlusHP(int HP)
    {
        Power += HP;
    }

    public void DestroyUnit()
    {
        RegionScript reg = ParentRegion.GetComponent<RegionScript>();
        reg.SetNeighbourRegionsFlicker(false);
        Debug.Log("Unit is destroying...");
        PlayerManager.DestroyUnit(this.gameObject, reg);
    }

    public void SetParent(GameObject parent)
    {
        ParentRegion = parent;
    }

    public GameObject GetParent()
    {
        return ParentRegion;
    }
}
