using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class HostInfo : NetworkBehaviour {
	public Texture BackgroundTexture;
	public Texture TopTexture;              // Drag a Texture onto this item in the Inspector
	public Texture Team1_Texture;
	public Texture Team2_Texture;
	public Texture Team3_Texture;
	public Texture Team4_Texture;
	public List<GameObject> amountTeam1 = new List<GameObject>();
	public List<GameObject> amountTeam2 = new List<GameObject>();
	public List<GameObject> amountTeam3 = new List<GameObject>();
	public List<GameObject> amountTeam4 = new List<GameObject>();
	[SyncVar]
	public int team1Score;
	[SyncVar]
	public int team2Score;
	[SyncVar]
	public int team3Score;
	[SyncVar]
	public int team4Score;
    GUIStyle titleStyle = new GUIStyle();
	GUIStyle style = new GUIStyle();

	void Start(){
		if(!isServer){
			this.enabled = false;
			return;
		}
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 
		if(!localPlayerAuthority){
			this.enabled = false;
			return;
		}
	}

	void Update () {
        titleStyle.alignment = TextAnchor.MiddleCenter;
		titleStyle.fontSize = Screen.height/12;

		style.alignment = TextAnchor.MiddleLeft;
		style.fontSize = Screen.height/24;
	}

	void OnGUI(){
		//Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F);

		//Title Top
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/10), TopTexture, ScaleMode.StretchToFill, true, 10.0F);
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height/10), "Your Lobby", titleStyle);
		
		//Teams
		int BlockHeight = ((Screen.height - Screen.height/10) / 4);
		int _BlockTitleHeight = ((Screen.height - Screen.height/10) / 8);
			//Team 1
			string team1Info = "  T_Size: " + amountTeam1.Count + "   Score: " + team1Score;
			GUI.DrawTexture(new Rect(0, Screen.height/10, Screen.width, _BlockTitleHeight), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, Screen.height/10 + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team1Info, style);

			//Team 2
			string team2Info = "  T_Size: " + amountTeam2.Count + "   Score: " + team2Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + BlockHeight), Screen.width, _BlockTitleHeight), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team2Info, style);

			//Team 3
			string team3Info = "  T_Size: " + amountTeam3.Count + "   Score: " + team3Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)), Screen.width, _BlockTitleHeight), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team3Info, style);

			//Team 4
			string team4Info = "  T_Size: " + amountTeam4.Count + "   Score: " + team4Score;
			GUI.DrawTexture(new Rect(0, (Screen.height - BlockHeight), Screen.width, _BlockTitleHeight), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, (Screen.height - BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team4Info, style);
	}
}
