using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class HostGame : MonoBehaviour {
	private uint roomSize = 16;
	private string roomName;
	private NetworkManager networkManager;
	public GUIStyle customButton;

	void Start(){
		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker();
		}
	}

	public void SetRoomName(string _name){
		roomName = _name;
	}

	public void CreateRoom(){
		if(roomName != "" && roomName != null){
			networkManager.matchMaker.ListMatches(0, 10, roomName, true, 0, 0, OnMatchList);
		}
	}

	void OnGUI(){
		customButton = new GUIStyle("button");

		//Host game Button
		customButton.fontSize = Screen.height/12;
		if (GUI.Button(new Rect(0, Screen.height-(Screen.height/6), Screen.width, Screen.height/6), "Host Game", customButton)){
			CreateRoom();
		}

		//InputField
		customButton.fontSize = Screen.height/24;
		roomName = GUI.TextField(new Rect(0, Screen.height-((Screen.height/6)*1.5f), Screen.width, Screen.height/12), roomName, customButton);
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
		if(matches.Count >= 1){
			Debug.Log("Already a Match");
		} else{
			Debug.Log("No matches found!");
			networkManager.matchMaker.CreateMatch(roomName,roomSize,true,"","","",0,0,networkManager.OnMatchCreate);
		}
    }
}
