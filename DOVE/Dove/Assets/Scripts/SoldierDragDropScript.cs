using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoldierDragDropScript : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public PlayerManager PlayerManager;
    public RegionScript ParentRegionScript;
    public RegionScript NewRegionScript;
    public BattleScript BattleScript;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    public bool isDraggable = true;
    public GameObject DropZone;
    private GameObject startParent;
    private Vector2 startPosition;
    private Vector2 NullVector = new Vector2(0, 0);

    private void Start() {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Canvas = GameObject.Find("MainCanvas"); 
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        BattleScript = GameObject.Find("BattleManager").GetComponent<BattleScript>();
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("InSockets");
        if (collision.gameObject.GetComponent<RegionScript>() == null && collision.gameObject.GetComponent<UnitInfoScript>() == null) 
        {
            isOverDropZone = false;
            DropZone = null;
            return;
        }
        isOverDropZone = true;
        DropZone = collision.gameObject;
        if (DropZone.gameObject.GetComponent<UnitInfoScript>() != null)
        {
            DropZone = DropZone.gameObject.transform.parent.gameObject;
        }
        Debug.Log("OverDropZone");
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isOverDropZone = false;
        DropZone = null;
    }

    public void StartDrag()
    {
        if (PlayerManager.GetPlayPoints() < 1) return;
        if (!hasAuthority) return;
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        ParentRegionScript = startParent.GetComponent<RegionScript>();
        isDragging = true;
        ParentRegionScript.SetNeighbourRegionsFlicker(true);
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        if (PlayerManager.GetPlayPoints() < 1) return;
        if (!hasAuthority) return;
        isDragging = false;
        if (DropZone == null || DropZone == startParent)
        {
            transform.SetParent(startParent.transform, false);
            transform.position = startPosition;
            ParentRegionScript.SetNeighbourRegionsFlicker(false);
        }
        NewRegionScript = DropZone.transform.gameObject.GetComponent<RegionScript>();
        ParentRegionScript = startParent.GetComponent<RegionScript>();
        int neighb = ParentRegionScript.NeighboursCountGet();
        bool isNeighbour = NeighbourChecker(neighb, DropZone, ParentRegionScript);
        GameObject CurrentUnit;
        UnitInfoScript CurrentUnitInfo;
        UnitInfoScript ThisUnitInfo;
        ThisUnitInfo = this.gameObject.GetComponent<UnitInfoScript>();
        if (NewRegionScript.GetBusy())
        {
            CurrentUnit = NewRegionScript.GetCurrentUnit();
            CurrentUnitInfo = CurrentUnit.GetComponent<UnitInfoScript>();
            if (NewRegionScript.IsOwnerFirst != PlayerManager.FirstPlayer)
            {
                int result;
                result = BattleScript.Battle(ThisUnitInfo, CurrentUnitInfo);
                Debug.Log("Buttle result " + result);
                if (result == 1) return;
                if (result == 3)
                {
                    transform.SetParent(startParent.transform, false);
                    transform.position = startPosition;
                    ParentRegionScript.SetNeighbourRegionsFlicker(false);
                    PlayerManager.PlayPointsConsumed(1);
                }
                if (result == 0 || result == 2)
                {
                    isDraggable = false;
                    PlayerManager.UnitSetParent(ThisUnitInfo, DropZone);
                    PlayerManager.SetRegionBusy(ParentRegionScript, false);
                    PlayerManager.SetRegionBusy(NewRegionScript, true);
                    PlayerManager.PlayUnit(this.gameObject, DropZone);
                    PlayerManager.PlayPointsConsumed(1);
                    PlayerManager.DestroyBuildingsInArea(NewRegionScript, PlayerManager);
                    NewRegionScript.OwnerSet(PlayerManager.FirstPlayer);
                    NewRegionScript.SetCurrentUnit(this.gameObject);
                    ParentRegionScript.SetNeighbourRegionsFlicker(false);
                    PlayerManager.TerCount(); 
                    ParentRegionScript = DropZone.GetComponent<RegionScript>();
                }
            }
            else 
            {
                    PlayerManager.SetRegionBusy(NewRegionScript, false);
                    transform.SetParent(startParent.transform, false);
                    transform.position = startPosition;
                    ParentRegionScript.SetNeighbourRegionsFlicker(false);
            }
        } else if (isOverDropZone && PlayerManager.isMyTurn && !NewRegionScript.GetBusy() && isNeighbour)
        {
            isDraggable = false;
            PlayerManager.UnitSetParent(ThisUnitInfo, DropZone);
            PlayerManager.SetRegionBusy(ParentRegionScript, false);
            PlayerManager.SetRegionBusy(NewRegionScript, true);
            PlayerManager.PlayUnit(this.gameObject, DropZone);
            PlayerManager.PlayPointsConsumed(1);
            PlayerManager.DestroyBuildingsInArea(NewRegionScript, PlayerManager);
            NewRegionScript.OwnerSet(PlayerManager.FirstPlayer);
            NewRegionScript.SetCurrentUnit(this.gameObject);
            ParentRegionScript.SetNeighbourRegionsFlicker(false);
            ParentRegionScript = DropZone.GetComponent<RegionScript>();
        } 
        else 
        {
                transform.SetParent(startParent.transform, false);
                transform.position = startPosition;
                ParentRegionScript.SetNeighbourRegionsFlicker(false);
        }
    }

    public void IsDrugSetTrue()
    {
        isDraggable = true;
    }

    public bool NeighbourChecker(int count, GameObject DropZone, RegionScript regionScript)
    {
        bool temp = false;
        for (int i = 0; i <= count-1; i++)
        {
            if (regionScript.IfIsNeighbours(i) == DropZone) temp = true;
        }
        if (ParentRegionScript.isAeroport)
        {
            List<GameObject> aeros = regionScript.GetAeroports();
            foreach (GameObject location in aeros)
            {
                if (location == DropZone)
                {
                    RegionScript reg = location.GetComponent<RegionScript>();
                    if (reg.IsOwnerFirst == PlayerManager.FirstPlayer && reg.IsOwnerNone == false)
                    {
                        temp = true;
                    }
                }
                
            }
        }
        Debug.Log(temp);
        return temp;
    }
}
