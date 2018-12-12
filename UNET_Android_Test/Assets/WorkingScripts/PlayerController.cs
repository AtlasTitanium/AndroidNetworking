using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Team {NoTeam, Team1, Team2, Team3, Team4}
public class PlayerController : NetworkBehaviour {
    public Team currentTeam;
    private Color playerColor;
    private int playerNumber;
    [SyncVar]
    public bool ServerActive = false;

	void Update () {
        if(!isLocalPlayer){
            return;
        }
        ServerActive = isServer;

        if(!isServer){
            CmdCreatePlayer();
            return;
        } else {
            RpcCreateHost();
            return;
        }
	}

	[Command]
    public void CmdChooseTeam(Color color){
		//Debug.Log("In the CMD: " + this.gameObject);
        RpcChangeColor(color);
    }

	[ClientRpc]
    public void RpcChangeColor(Color color){
		//Debug.Log("In the RPC: " + this.gameObject);
        playerColor = color;
        GetComponent<MeshRenderer>().material.color = color;
    }

    [Command]
    public void CmdCreatePlayer(){
        RpcCreatePlayer();
    }

    [ClientRpc]
    public void RpcCreatePlayer(){
        playerNumber = NetworkServer.connections.Count - 1;
        this.gameObject.name = playerNumber.ToString();
    }

    [ClientRpc]
    public void RpcCreateHost(){
        this.gameObject.GetComponent<HostInfo>().enabled = true;
        playerNumber = 0;
        this.gameObject.name = "Host";
        this.gameObject.tag = "Host";
        //this.enabled = false;
    }

    
}
