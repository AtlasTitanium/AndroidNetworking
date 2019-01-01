using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PlayerUI : NetworkBehaviour {
	public GUIStyle customButton;

	public Texture2D Backpack;
	public Texture2D Photo;
	public Texture2D Map;

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
		GUI.DrawTexture(new Rect(0, (Screen.height-Screen.height/6), Screen.width, Screen.height/6), Background, ScaleMode.StretchToFill, true, 10.0F);
		customButton = new GUIStyle("button");

		//Backpack
		customButton.normal.background = Backpack;
		customButton.hover.background = Backpack;
		if (GUI.Button(new Rect(0+Screen.width/24, Screen.height-(Screen.height/6), Screen.width/4, Screen.height/7), "", customButton)){
			OpenBackpack();
		}

		//Photo
		customButton.normal.background = Photo;
		customButton.hover.background = Photo;
		if (GUI.Button(new Rect(Screen.width/3+Screen.width/24, Screen.height-(Screen.height/6), Screen.width/4, Screen.height/7), "", customButton)){
			OpenPhoto();
		}

		//Map
		customButton.normal.background = Map;
		customButton.hover.background = Map;
		if (GUI.Button(new Rect((Screen.width - Screen.width/3)+Screen.width/24, Screen.height-(Screen.height/6), Screen.width/4, Screen.height/7), "", customButton)){
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
