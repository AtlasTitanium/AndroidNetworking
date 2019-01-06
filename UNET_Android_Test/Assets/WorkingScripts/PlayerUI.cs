﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PlayerUI : NetworkBehaviour {
	public GUIStyle customButton;

	public Texture2D Backpack;
	public Texture2D Photo;
	public Texture2D Map;

	public Texture2D team1Texture;
    public Texture2D team2Texture;
    public Texture2D team3Texture;
    public Texture2D team4Texture;

	public Texture2D Background;

	void Start(){
		if(isServer){
			this.enabled = false;
			return;
		}
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 
	}

	void OnGUI(){
		switch(this.GetComponent<PlayerController>().currentTeam){
            case Team.Team1:
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team1Texture);
            break;

            case Team.Team2:
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team2Texture);
            break;

            case Team.Team3:
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team3Texture);
            break;

            case Team.Team4:
            GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team4Texture);
            break;
        }


		customButton = new GUIStyle("button");
		float border = Screen.width/16;
		float box = ((Screen.width * 0.875f)/3);
		float height = Screen.height/13;
		
		//Backpack
		customButton.normal.background = Backpack;
		customButton.hover.background = Backpack;
		if (GUI.Button(new Rect(border, Screen.height-height, box, height), "", customButton)){
			OpenBackpack();
		}

		//Photo = Should be help button
		customButton.normal.background = Photo;
		customButton.hover.background = Photo;
		if (GUI.Button(new Rect(border + box, Screen.height-height, box, height), "", customButton)){
			OpenPhoto(); //OpenHelp();
		}

		//Map
		customButton.normal.background = Map;
		customButton.hover.background = Map;
		if (GUI.Button(new Rect(border + box + box, Screen.height-height, box, height), "", customButton)){
			OpenMap();
		}
	}

	void OpenBackpack(){

	}

	void OpenPhoto(){
		
	}

	void OpenMap(){
		
	}
}