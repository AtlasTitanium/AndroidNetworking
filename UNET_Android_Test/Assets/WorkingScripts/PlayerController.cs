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
            this.gameObject.name = "Client";
            return;
        } else {
            RpcCreateHost();
            this.gameObject.GetComponent<HostInfo>().enabled = true;
            this.gameObject.name = "Host";
            return;
        }
	}

	[Command]
    public void CmdChooseTeam(Color color){
        RpcChangeColor(color);
    }

	[ClientRpc]
    public void RpcChangeColor(Color color){
        playerColor = color;
        GetComponent<MeshRenderer>().material.color = color;
    }

    [ClientRpc]
    public void RpcCreateHost(){
        this.gameObject.tag = "Host";
    }

    
}
