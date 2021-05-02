using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoldierDragDropScript : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public PlayerManager PlayerManager;
    public RegionScript regionScript;
    public RegionScript NewRegionScript;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private GameObject DropZone;
    private GameObject startParent;
    private Vector2 startPosition;
    private Vector2 NullVector = new Vector2(0, 0);

    private void Start() {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Canvas = GameObject.Find("MainCanvas"); 
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        if (!hasAuthority)
        {
            isDraggable = false;
        }
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
        if (collision.gameObject.GetComponent<RegionScript>() == null) return;
        isOverDropZone = true;
        DropZone = collision.gameObject;
        Debug.Log("OverDropZone");
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isOverDropZone = false;
        DropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;
        NewRegionScript = DropZone.transform.gameObject.GetComponent<RegionScript>();
        if (isOverDropZone && PlayerManager.isMyTurn && !NewRegionScript.GetBusy())
        {
            transform.SetParent(DropZone.transform, true);
            transform.position = new Vector2(DropZone.transform.position.x, DropZone.transform.position.y);
            isDraggable = false;
            regionScript = startParent.GetComponent<RegionScript>();
            regionScript.SetBusyFalse();
            NewRegionScript.SetBusyTrue();
            PlayerManager.PlayUnit(this.gameObject, DropZone);
            //PlayerManager.MakeStep();
        } 
        else 
        {
            transform.SetParent(startParent.transform, false);
            transform.position = startPosition;
        }
    }
}
