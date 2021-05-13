using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpyDragNDrop : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public PlayerManager PlayerManager;
    public RegionScript ParentRegionScript;
    public RegionScript NewRegionScript;

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
        bool haveSpy;
        if (PlayerManager.FirstPlayer)
            {
                haveSpy = NewRegionScript.GetFirstSpy();
            } else 
            {
                haveSpy = NewRegionScript.GetSecondSpy();
            }
        if (isOverDropZone && PlayerManager.isMyTurn && !haveSpy && isNeighbour)
        {
            isDraggable = false;
            PlayerManager.SetCurrentSpy(PlayerManager.FirstPlayer, ParentRegionScript, NewRegionScript, this.gameObject);
            PlayerManager.PlayUnitSpy(this.gameObject, DropZone);
            PlayerManager.PlayPointsConsumed(1);
            PlayerManager.DisplayPoints();
            ParentRegionScript.SetNeighbourRegionsFlicker(false);
        } 
        else 
        {
                transform.SetParent(startParent.transform, false);
                transform.position = startPosition;
                ParentRegionScript.SetNeighbourRegionsFlicker(false);
        }
        PlayerManager.ShowGuard();
    }

    public void IsDrugSetTrue()
    {
        isDraggable = true;
    }

    public bool NeighbourChecker(int count, GameObject DropZone, RegionScript regionScript)
    {
        bool temp = false;
        switch(count)
        {
            case 1:
            if (regionScript.IfIsNeighbours(0) == DropZone) temp = true;
            break;
            case 2:
            if (regionScript.IfIsNeighbours(0) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(1) == DropZone) temp = true;
            break;
            case 3:
            if (regionScript.IfIsNeighbours(0) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(1) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(2) == DropZone) temp = true;
            break;
            case 4:
            if (regionScript.IfIsNeighbours(0) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(1) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(2) == DropZone) temp = true;
            if (regionScript.IfIsNeighbours(3) == DropZone) temp = true;
            break;
            default:
            temp = false;
            break;
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
        return temp;
    }
}

