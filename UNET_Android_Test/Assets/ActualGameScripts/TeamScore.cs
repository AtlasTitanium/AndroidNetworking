using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamScore : MonoBehaviour {
	public GUIStyle style;
	private PlayerInfo pInfo;
	public Texture2D infoBar;
	public Font Dyslectic_font;
	
	void Start(){
		pInfo = GetComponent<PlayerInfo>();
	}

	void Update(){
		if(pInfo.puzzle1 && pInfo.puzzle2 && pInfo.puzzle3){
			this.enabled = false;
		}
	}

	void OnGUI(){
		style = new GUIStyle();
		style.fontSize = Screen.height/18;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = Dyslectic_font;
		style.normal.background = infoBar;
		
		//TeamScore
		string tScore = "Team Score: " + pInfo.teamScore.ToString();
		GUI.Box(new Rect(0, (Screen.height - (Screen.height/8)), Screen.width, Screen.height/8), tScore, style);	
	}
}
