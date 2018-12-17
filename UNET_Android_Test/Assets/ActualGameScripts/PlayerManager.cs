using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour {
	private PlayerInfo pInfo;
	private TeamScore tScore;
	private GUIStyle buttonStyle;
	private GUIStyle Team1Button;
	private GUIStyle Team2Button;
	private GUIStyle Team3Button;
	private GUIStyle Team4Button;
	public Texture BackgroundTexture;
	public Texture TopTexture;              // Drag a Texture onto this item in the Inspector
	public Texture2D Team1_Texture;
	public Texture2D Team1_Check;
	public Texture2D Team2_Texture;
	public Texture2D Team2_Check;
	public Texture2D Team3_Texture;
	public Texture2D Team3_Check;
	public Texture2D Team4_Texture;
	public Texture2D Team4_Check;
	public Texture2D KiesJeTeam;
	public Texture2D NextButton;
	void Start () {
		tScore = GetComponent<TeamScore>();
		pInfo = GetComponent<PlayerInfo>();
	}

	void OnGUI(){
		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = Screen.height/14;
		buttonStyle.normal.background = NextButton;
		buttonStyle.hover.background = NextButton;

		Team1Button = new GUIStyle("button");
		if(pInfo.currentTeam == Team.Team1){
			Team1Button.normal.background = Team1_Check;
		} else {
			Team1Button.normal.background = Team1_Texture;
		}

		Team2Button = new GUIStyle("button");
		if(pInfo.currentTeam == Team.Team2){
			Team2Button.normal.background = Team2_Check;
		} else {
			Team2Button.normal.background = Team2_Texture;
		}

		Team3Button = new GUIStyle("button");
		if(pInfo.currentTeam == Team.Team3){
			Team3Button.normal.background = Team3_Check;
		} else {
			Team3Button.normal.background = Team3_Texture;
		}

		Team4Button = new GUIStyle("button");
		if(pInfo.currentTeam == Team.Team4){
			Team4Button.normal.background = Team4_Check;
		} else {
			Team4Button.normal.background = Team4_Texture;
		}

		//chooseTeam
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), KiesJeTeam, ScaleMode.StretchToFill, true, 10.0F);

		GUI.DrawTexture(new Rect(0,Screen.height/8,Screen.width/2,Screen.height/2.65f), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
		if(GUI.Button(new Rect(0,Screen.height/8,Screen.width/2,Screen.height/2.65f), "", Team1Button)){
			pInfo.currentTeam = Team.Team1;
		}

		GUI.DrawTexture(new Rect(Screen.width/2,Screen.height/8,Screen.width/2,Screen.height/2.65f), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
		if(GUI.Button(new Rect(Screen.width/2,Screen.height/8,Screen.width/2,Screen.height/2.65f), "", Team2Button)){
			pInfo.currentTeam = Team.Team2;
		}

		GUI.DrawTexture(new Rect(0,Screen.height/2,Screen.width/2,Screen.height/2.65f), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
		if(GUI.Button(new Rect(0,Screen.height/2,Screen.width/2,Screen.height/2.65f), "", Team3Button)){
			pInfo.currentTeam = Team.Team3;
		}

		GUI.DrawTexture(new Rect(Screen.width/2,Screen.height/2,Screen.width/2,Screen.height/2.65f), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
		if(GUI.Button(new Rect(Screen.width/2,Screen.height/2,Screen.width/2,Screen.height/2.65f), "", Team4Button)){
			pInfo.currentTeam = Team.Team4;
		}


		//confirmTeam
		if(GUI.Button(new Rect(Screen.width/16,(Screen.height - Screen.height/9),Screen.width/1.125f,Screen.height/10), "", buttonStyle)){
			if(pInfo.currentTeam != Team.NoTeam){
				pInfo.GoToScene(pInfo.ARScene);
				tScore.enabled = true;
				this.enabled = false;
			}
		}
	}
}
