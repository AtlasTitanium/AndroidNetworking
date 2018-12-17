﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class JoinGame : MonoBehaviour {
	public GUIStyle customButton;
	public GUIStyle customInputField;
	private NetworkManager networkManager;
	private string roomName;
	public Texture2D Background;
	public Texture2D Button;
	public Texture2D inpField;
	public Font currentFont;
	void Start(){
		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker();
		}
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background, ScaleMode.StretchToFill, true, 10.0F);
		customButton = new GUIStyle("button");
		customButton.normal.background = Button;
		customButton.hover.background = Button;
		customButton.fontSize = Screen.height/14;
		customButton.font = currentFont;

		//Host game Button
		customButton.fontSize = Screen.height/12;
		// if (GUI.Button(new Rect(0, Screen.height/12, Screen.width, Screen.height/6), "Join Game", customButton)){
		// 	JoinRoom();
		// }
		if (GUI.Button(new Rect(0, Screen.height-(Screen.height/6), Screen.width, Screen.height/6), "Join Game", customButton)){
			JoinRoom();
		}

		//InputField
		customInputField = new GUIStyle("input field");
		customInputField.normal.background = inpField;
		customInputField.hover.background = inpField;
		customInputField.fontSize = Screen.height/24;
		customInputField.font = currentFont;
		//roomName = GUI.TextField(new Rect(0, 0, Screen.width, Screen.height/12), roomName, customButton);
		roomName = GUI.TextField(new Rect(0, Screen.height-((Screen.height/6)*1.5f), Screen.width, Screen.height/12), roomName, customInputField);
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
