using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Team {Team1, Team2, Team3, Team4}
public class PlayerController : NetworkBehaviour {
    public Team currentTeam;
    private Color playerColor;
    void Start(){
        if(isServer){
            this.gameObject.name = "Host";
        } else {
            CmdCreatePlayer();
			return;
        }
    }
	void Update () {
        if(isServer){
			this.enabled = false;
			return;
		}
		if (!isLocalPlayer)
        {
            return;
        } 
	}

    void FixedUpdate(){
        if(playerColor != null){
            CmdChooseTeam(playerColor);
            CmdUpdatePlayer();
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
        int playerNumber = NetworkServer.connections.Count - 1;
        this.gameObject.name = playerNumber.ToString();
    }

    [Command]
    public void CmdUpdatePlayer(){
        this.gameObject.name = this.gameObject.name;
    }
}
