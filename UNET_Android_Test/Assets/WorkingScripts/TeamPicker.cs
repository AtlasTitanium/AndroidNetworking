using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class TeamPicker : NetworkBehaviour {
	private PlayerController playerController;
	private NetworkManager networkManager;
	public GUIStyle customButton;
	public HostInfo theHost;

	void Start(){
		networkManager = NetworkManager.singleton;
		playerController = this.transform.GetComponent<PlayerController>();
	}

	void Update () {
		if(isServer){
			this.enabled = false;
			return;
		}
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 

		if(theHost == null){
			theHost = GameObject.FindGameObjectWithTag("Host").GetComponent<HostInfo>();
		} else {
			Debug.Log("noHostFound");
		}
	}

	void OnGUI(){
		customButton = new GUIStyle("button");

		//TeamButtons
		MatchInfo matchInfo = networkManager.matchInfo;
		customButton.fontSize = Screen.height/12;
        if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height/4), "Team 1", customButton)){
            Debug.Log("Color Blue");
			playerController.CmdChooseTeam(Color.blue);
			playerController.currentTeam = Team.Team1;

			playerController.gameObject.GetComponent<LeaveRoom>().enabled = true;
			CmdAddPlayer(1, this.gameObject);
			this.enabled = false;
        }
		if (GUI.Button(new Rect(0, Screen.height/4, Screen.width, Screen.height/4), "Team 2", customButton)){
            Debug.Log("Color Red");
			playerController.CmdChooseTeam(Color.red);
			playerController.currentTeam = Team.Team2;

			playerController.gameObject.GetComponent<LeaveRoom>().enabled = true;
			CmdAddPlayer(2, this.gameObject);
			this.enabled = false;
        }
		if (GUI.Button(new Rect(0, Screen.height/2, Screen.width, Screen.height/4), "Team 3", customButton)){
            Debug.Log("Color Green");
			playerController.CmdChooseTeam(Color.green);
			playerController.currentTeam = Team.Team3;

			playerController.gameObject.GetComponent<LeaveRoom>().enabled = true;
			CmdAddPlayer(3, this.gameObject);
			this.enabled = false;
        }
		if (GUI.Button(new Rect(0, Screen.height-(Screen.height/4), Screen.width, Screen.height/4), "Team 4", customButton)){
            Debug.Log("Color Yellow");
			playerController.CmdChooseTeam(Color.yellow);
			playerController.currentTeam = Team.Team4;

			playerController.gameObject.GetComponent<LeaveRoom>().enabled = true;
			CmdAddPlayer(4, this.gameObject);
			this.enabled = false;
        }
	}

	[Command]
	public void CmdAddPlayer(int teamNumber, GameObject player){
		switch(teamNumber){
			case 1:
				Debug.Log("addToTeam1");
				theHost.amountTeam1.Add(player);
				break;
			case 2:
				Debug.Log("addToTeam2");
				theHost.amountTeam2.Add(player);
				break;
			case 3:
				Debug.Log("addToTeam3");
				theHost.amountTeam3.Add(player);
				break;
			case 4:
				Debug.Log("addToTeam4");
				theHost.amountTeam4.Add(player);
				break;
			default:
				Debug.Log("Error Boi");
				break;
		}
	}
}
