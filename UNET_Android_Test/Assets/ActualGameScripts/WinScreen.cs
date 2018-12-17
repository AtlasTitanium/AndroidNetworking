using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {
	public Texture2D Background;
	public Texture2D Orange;

	private GUIStyle style;
	private PlayerInfo pInfo;
	public Font Dyslectic_font;
	
	void Start(){
		pInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
	}
	void OnGUI(){
		style = new GUIStyle();
		style.normal.background = Orange;
		style.fontSize = Screen.height/24;
		style.alignment = TextAnchor.MiddleCenter;
		style.font = Dyslectic_font;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background, ScaleMode.StretchToFill, true, 10.0F);
		string tScore = "Bedankt!\nVoor het spelen\nvan onze game.\n\nJe score:\n" + pInfo.teamScore.ToString() + " Punten";
		GUI.Box(new Rect(Screen.width/8,Screen.height/10,Screen.width/1.25f,Screen.height/2), tScore, style);
	}
}
