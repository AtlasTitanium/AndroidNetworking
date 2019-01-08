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

    void Start()
    {
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

	void Update () {
        if(!isLocalPlayer){
            return;
        }
        ServerActive = isServer;
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

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        base.OnDeserialize(reader, initialState);
    }

    void OnApplicationPauze(){
        Debug.Log("oofo");
        CmdSendMessage("pauze");
        if(!isServer){
            CmdExitPlayer();
        }
    }

    void OnApplicationQuit(){
        Debug.Log("oof");
        CmdSendMessage("player quit");
        CmdHostInform();
    }

    [Command]
    void CmdExitPlayer(){
        switch(currentTeam){
            case Team.Team1:
            theHost.amountTeam1.Remove(this.gameObject);
            break;

            case Team.Team2:
            theHost.amountTeam2.Remove(this.gameObject);
            break;

            case Team.Team3:
            theHost.amountTeam3.Remove(this.gameObject);
            break;

            case Team.Team4:
            theHost.amountTeam4.Remove(this.gameObject);
            break;
        }
    }

    [Command]
    void CmdSendMessage(string str){
        Debug.Log(str);
    }

    [Command]
    void CmdHostInform(){
        theHost.RpcInform();
    }
}
