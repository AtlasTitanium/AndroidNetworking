using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public enum PlayerState{None, Backpack, Help, Map, InPuzzle}
public class PlayerUI : NetworkBehaviour {
	public PlayerState playerState = PlayerState.None;
	private PlayerController currentPlayer;
	public GUIStyle customButton;
	public GUIStyle styleButton;

	[Header("Icons")]
	public Texture2D Backpack;
	public Texture2D Photo;
	public Texture2D Map;

	[Header("Teams")]
	public Texture2D team1Texture;
	public Texture2D team1Framed;

    public Texture2D team2Texture;
	public Texture2D team2Framed;

    public Texture2D team3Texture;
	public Texture2D team3Framed;

    public Texture2D team4Texture;
	public Texture2D team4Framed;

	[Header("General")]
	public Texture2D Background;
	public Texture2D infoBox;
	public Texture2D StartPuzzle;


	[Header("Backpack")]
	public Texture2D BackpackTop;
	public List<Texture2D> ScoreBar = new List<Texture2D>(4);
	public List<Texture2D> PuzzleGrey = new List<Texture2D>(3);
	public List<Texture2D> PuzzleColor = new List<Texture2D>(3);
	public Texture2D infoButton;
	public Texture2D infoBar;

	[Header("Help")]
	public Texture2D GarryTop;
	public Texture2D Replay;
	public Texture2D Return;

	public Texture2D next;
	public Texture2D back;
	

	[Header("Map")]
	public Texture2D MapBackground;

	private bool ScorePuzzle1 = true;
	private bool ScorePuzzle2 = true;
	private bool ScorePuzzle3 = true;
	private int puzzlesDone = 0;

	void Start(){
		if (!isLocalPlayer)
        {
			if(isServer){
				this.enabled = false;
				return;
			}
        } 
		currentPlayer = GetComponent<PlayerController>();
	}

	void Update(){
		if(currentPlayer.infoPuzzle1){
			playerState = PlayerState.InPuzzle;
		}
		if(currentPlayer.infoPuzzle2){
			playerState = PlayerState.InPuzzle;
		}
		if(currentPlayer.infoPuzzle3){
			playerState = PlayerState.InPuzzle;
		}
	}

	void OnGUI(){
		styleButton = new GUIStyle("button");
		styleButton.fontSize = Screen.width;

		int widthMargin = Screen.width/20;
		int heightMargin = Screen.height/48;
		switch(playerState){
			//BACKPACK---------------------------------------------------------------------
			case PlayerState.Backpack:
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height/12),BackpackTop);

				//ScoreBar
				if(currentPlayer.puzzle1 && ScorePuzzle1){
					puzzlesDone++;
					ScorePuzzle1 = false;
				}
				if(currentPlayer.puzzle2 && ScorePuzzle2){
					Debug.Log("MilkmaidDone");
					puzzlesDone++;
					ScorePuzzle2 = false;
				}
				if(currentPlayer.puzzle3 && ScorePuzzle3){
					puzzlesDone++;
					ScorePuzzle3 = false;
				}
				GUI.DrawTexture(new Rect(widthMargin, Screen.height/12 + heightMargin, Screen.width - (widthMargin*2), Screen.height/16),ScoreBar[puzzlesDone]);

				//Paintings
				//Kairo
				Rect kairoBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + heightMargin,Screen.width/2.5f,Screen.height/4.5f);
				if(currentPlayer.puzzle1){
					GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
				} else {
					GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
				}

				//Milkmaid
				Rect milkmaidBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + heightMargin*2,Screen.width/2.5f,Screen.height/4.5f);
				if(currentPlayer.puzzle2){
					GUI.DrawTexture(milkmaidBlock,PuzzleColor[1]);
				} else {
					GUI.DrawTexture(milkmaidBlock,PuzzleGrey[1]);
				}

				//Nightwatch
				Rect nightwatchBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + Screen.height/4.5f + heightMargin*3,Screen.width/2.5f,Screen.height/4.5f);
				if(currentPlayer.puzzle3){
					GUI.DrawTexture(nightwatchBlock,PuzzleColor[2]);
				} else {
					GUI.DrawTexture(nightwatchBlock,PuzzleGrey[2]);
				}
				

				customButton.normal.background = Return;
				customButton.hover.background = Return;
				if (GUI.Button(new Rect(widthMargin, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), "", customButton)){
					playerState = PlayerState.None;
				}
			break;

			//HELP-------------------------------------------------------------------------------
			case PlayerState.Help:
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
			
				if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Leave", styleButton)){
					playerState = PlayerState.None;
				}
			break;

			//MAP--------------------------------------------------------------------------------
			case PlayerState.Map:
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
			
				if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Leave", styleButton)){
					playerState = PlayerState.None;
				}
			break;

			//ROAMING----------------------------------------------------------------------------
			case PlayerState.None:
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
				float border = Screen.width/24;
				float box = (((Screen.width/24) * 22)/3);
				float height = Screen.height/13;
				
				//Backpack
				customButton.normal.background = Backpack;
				customButton.hover.background = Backpack;
				if (GUI.Button(new Rect(border, Screen.height-height, box, height), "", customButton)){
					playerState = PlayerState.Backpack;
				}

				//Photo = Should be help button
				customButton.normal.background = Photo;
				customButton.hover.background = Photo;
				if (GUI.Button(new Rect(border + box, Screen.height-height, box, height), "", customButton)){
					playerState = PlayerState.Help;
				}

				//Map
				customButton.normal.background = Map;
				customButton.hover.background = Map;
				if (GUI.Button(new Rect(border + box + box, Screen.height-height, box, height), "", customButton)){
					playerState = PlayerState.Map;
				}
			break;

			//MAP--------------------------------------------------------------------------------
			case PlayerState.InPuzzle:
				if(!currentPlayer.infoBusy){
					float border2 = Screen.width/24;
					float box2 = (((Screen.width/24) * 22)/3);
					float height2 = Screen.height/13;

					//Terug
					customButton.normal.background = Return;
					customButton.hover.background = Return;
					if (GUI.Button(new Rect(border2, Screen.height-height2, box2, height2), "", customButton)){
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						playerState = PlayerState.None;
					}

					//Info
					customButton.normal.background = infoButton;
					customButton.hover.background = infoButton;
					if (GUI.Button(new Rect(border2 + box2, Screen.height-height2, box2, height2), "", customButton)){
						//--
					}

					//Help
					customButton.normal.background = Photo;
					customButton.hover.background = Photo;
					if (GUI.Button(new Rect(border2 + box2 + box2, Screen.height-height2, box2, height2), "", customButton)){
						//--
					}
				} else {
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
					GUI.DrawTexture(new Rect(Screen.width/3,Screen.height/40,Screen.width/3,Screen.height/5),GarryTop);

					GUI.DrawTexture(new Rect(Screen.width/20, (Screen.height/5) * 2, Screen.width/1.2f, Screen.height/3),infoBox);
					GUI.Box(new Rect(Screen.width/20, (Screen.height/5) * 2, Screen.width/1.2f, Screen.height/3), "Lorem ishizlle");

					GUI.DrawTexture(new Rect(Screen.width/16, (Screen.height/3)*2,Screen.width/4,Screen.height/16),next);
					GUI.DrawTexture(new Rect(Screen.width/16, (Screen.height/3)*2 + Screen.height/16 + Screen.height/24,Screen.width/4,Screen.height/16),back);

					customButton.normal.background = Return;
					customButton.hover.background = Return;
					if (GUI.Button(new Rect(widthMargin, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), "", customButton)){
						currentPlayer.infoBusy = false;
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						playerState = PlayerState.None;
					}
				}
			break;
		}
	}
}
