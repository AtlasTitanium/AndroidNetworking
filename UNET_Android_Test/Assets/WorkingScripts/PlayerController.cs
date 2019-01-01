using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Team {NoTeam, Team1, Team2, Team3, Team4}
public class PlayerController : NetworkBehaviour {
    [SyncVar]
    public Team currentTeam = Team.NoTeam;
    private Color playerColor;
    public HostInfo theHost;
    [SyncVar]
    public int personalScore;
    [SyncVar]
    public bool ServerActive = false;

    [SyncVar]	
	public bool puzzle1 = false;
	[SyncVar]	
	public bool puzzle2 = false;
	[SyncVar]	
	public bool puzzle3 = false;

	void Update () {
        if(!isLocalPlayer){
            return;
        }
        ServerActive = isServer;

        if(!isServer){
            this.gameObject.name = "Client";
            CmdFindHost();
            return;
        } else {
            RpcCreateHost();
            this.gameObject.GetComponent<HostInfo>().enabled = true;
            this.gameObject.name = "Host";
            return;
        }
	}

    [Command]
    public void CmdGainScore(int score, int puzzleNumber){
        personalScore += score;
        switch(currentTeam){
            case Team.Team1:
            theHost.team1Score += score;
            break;

            case Team.Team2:
            theHost.team2Score += score;
            break;

            case Team.Team3:
            theHost.team3Score += score;
            break;

            case Team.Team4:
            theHost.team4Score += score;
            break;
        }
        switch(puzzleNumber){
            case 1:
            puzzle1 = true;
            break;
            case 2:
            puzzle2 = true;
            break;
            case 3:
            puzzle3 = true;
            break;
        }
    }

    [ClientRpc]
    public void RpcCreateHost(){
        this.gameObject.tag = "Host";
    }

    [Command]
	public void CmdFindHost(){
		RpcFindHost();
	}

	[ClientRpc]
	public void RpcFindHost(){
		theHost = GameObject.FindGameObjectWithTag("Host").GetComponent<HostInfo>();
	}
}
