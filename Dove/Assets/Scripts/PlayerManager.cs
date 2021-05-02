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
    public GameObject UnitLayer;
    private List<GameObject> MercsAr = new List<GameObject>();
    private List<GameObject> GuardsAr = new List<GameObject>();
    public GameManager GameManager;
    public bool isMyTurn = false;
    public bool FirstPlayer = false;

    public GameObject LocationSlot1;
    public GameObject LocationSlot2;
    public GameObject LocationSlot3;
    public GameObject LocationSlot4;

    public List<GameObject> LocationSockets = new List<GameObject>();

    public  override void OnStartClient()
    {
        base.OnStartClient();

        LocationSlot1 = GameObject.Find("Location1");
        LocationSlot2 = GameObject.Find("Location2");
        LocationSlot3 = GameObject.Find("Location3");
        LocationSlot4 = GameObject.Find("Location4");

        LocationSockets.Add(LocationSlot1);
        LocationSockets.Add(LocationSlot2);
        LocationSockets.Add(LocationSlot3);
        LocationSockets.Add(LocationSlot4);

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        UnitLayer = GameObject.Find("UnitLayer");

        if (isClientOnly)
        {
            isMyTurn = true;
            FirstPlayer = true;
        }
    }

    [Server]
    public override void OnStartServer()
    {
        MercsAr.Add(Mercenary);
        GuardsAr.Add(Guard);
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

    [Command]
    public void UnitSpawn(Vector2 vector, int unitID, GameObject parent)
    {
        if (unitID == 0)
        {
            GameObject Mercenary = Instantiate(MercsAr[Random.Range(0, MercsAr.Count)], vector, Quaternion.identity);
            NetworkServer.Spawn(Mercenary, connectionToClient);
            RpcShowUnit(Mercenary, parent);   
        } else if (unitID == 1){
            GameObject Guard = Instantiate(GuardsAr[Random.Range(0, GuardsAr.Count)], vector, Quaternion.identity);
            NetworkServer.Spawn(Guard, connectionToClient);
            RpcShowUnit(Guard, parent); 
        }
        
    }

    [ClientRpc]
    void RpcShowUnit(GameObject unit, GameObject parent)
    {
         if (hasAuthority)
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y);
            }
            else
            {
                unit.transform.SetParent(parent.transform, true);
                unit.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y);
                unit.GetComponent<UnitFlipper>().FlipBack();
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
    }

    [Command]
    public void CmdFinishTurn() 
    {
        RpcFinishTurn();
        if (isMyTurn){
            UIManager.UpdatePlayerText("Enemy Turn");
        } 
        else 
        {
            UIManager.UpdatePlayerText("Your Turn");
        }
    }

    [Command]
    public void CmdGMChangeState(string request)
    {
        RpcGMChangeState(request);
    }
}
