using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class JoinGame : MonoBehaviour {
	public GUIStyle customButton;
	private NetworkManager networkManager;
	private string roomName;

	void Start(){
		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker();
		}
	}

	void OnGUI(){
		customButton = new GUIStyle("button");

		//Host game Button
		customButton.fontSize = Screen.height/12;
		if (GUI.Button(new Rect(0, Screen.height/12, Screen.width, Screen.height/6), "Join Game", customButton)){
			JoinRoom();
		}

		//InputField
		customButton.fontSize = Screen.height/24;
		roomName = GUI.TextField(new Rect(0, 0, Screen.width, Screen.height/12), roomName, customButton);
	}

	public void JoinRoom (){
		if(roomName != "" && roomName != null){
			networkManager.matchMaker.ListMatches(0, 10, roomName, true, 0, 0, OnMatchList);
		}
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
		if(matches.Count >= 1){
			Debug.Log("MatchFound!");
			networkManager.matchMaker.JoinMatch(matches[0].networkId,"","","",0,0,networkManager.OnMatchJoined);
		} else{
			Debug.Log("no matches found");
		}
    }
}
