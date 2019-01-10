using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Team {NoTeam, Team1, Team2, Team3, Team4}
public class PlayerController : NetworkBehaviour {
    
    public Team currentTeam = Team.NoTeam;

    public HostInfo theHost;
   
    public int personalScore;
    
    public bool ServerActive = false;

    public bool infoPuzzle1 = false;	
	public bool infoPuzzle2 = false;
	public bool infoPuzzle3 = false;
 	
	public bool puzzle1 = false;	
	public bool puzzle2 = false;
	public bool puzzle3 = false;
    public bool infoBusy = false;

    public KairoPuzzle kairoPuzzle;
    public ContrastPuzzle1 contrastPuzzle1;
    public ContrastPuzzle2 contrastPuzzle2;

    public void Start()
    {
        //Debug.Log("OneStart");
        if(isLocalPlayer){
            if(!isServer){
                this.gameObject.name = "Client";
                CmdFindHost(); 
            } else {
                RpcCreateHost();
                this.gameObject.GetComponent<HostInfo>().enabled = true;
            }
        }
    }

	public void Update(){
        //Debug.Log("Updating");
        ServerActive = isServer;

        if(isLocalPlayer){
            //Debug.Log("isLocalPlayer");
            if(!isServer){
                //Debug.Log("isClient");
                this.gameObject.name = "Client";
                CmdFindHost(); 
            } else {
                //Debug.Log("isServer");
                RpcCreateHost();
                this.gameObject.GetComponent<HostInfo>().enabled = true;
            }
        }
	}

    public void GainScore(int score, int puzzleNumber){
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
        CmdGainScore(score,puzzleNumber);
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
        this.gameObject.name = "Host";
        this.gameObject.tag = "Host";
        //Debug.Log("updatePlayer");
    }

    [Command]
	public void CmdFindHost(){
        //Debug.Log("findingHost");
		RpcFindHost();
	}

	[ClientRpc]
	public void RpcFindHost(){
		theHost = GameObject.FindGameObjectWithTag("Host").GetComponent<HostInfo>();
        //Debug.Log("findingHostFromHost");
	}

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        base.OnDeserialize(reader, initialState);
    }

    void OnApplicationQuit(){
        if(!isServer){
            NetworkManager.singleton.StopClient();
        } else{
            NetworkManager.singleton.StopHost();
        }
    }
}
