using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public enum PlayerState{None, Backpack, Help, Map, InPuzzle, OutPuzzle, NameAsk}
public class PlayerUI : NetworkBehaviour {
	public PlayerState playerState = PlayerState.NameAsk;
	private PlayerController currentPlayer;
	public GUIStyle customButton;
	public GUIStyle styleButton;
	public GUIStyle textStyle;

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
	public Texture2D InputFieldBackground;
	public Texture2D ButtonBackground;
	public Texture2D Background;
	public Texture2D infoBox;
	public Texture2D StartPuzzle;
	public Font currentFont;


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
	public Texture2D LocationTexture;
	public float minLatitude;
	public float maxLatitude;

	public float minLongitude;
	public float maxLongitude;
	public float latitudeLocation;
	public float longitudeLocation;
	//============================

	private bool ScorePuzzle1 = true;
	private bool ScorePuzzle2 = true;
	private bool ScorePuzzle3 = true;
	private int puzzlesDone = 0;

	private int currentTextNumber = 0;
	private bool kairo;
	private bool milk;
	private bool night;
	private string newName;
	void Start(){
		if (!isLocalPlayer)
        {
			if(isServer){
				this.enabled = false;
				return;
			}
        } 
		currentPlayer = GetComponent<PlayerController>();
		playerState = PlayerState.NameAsk;
	}

	void Update(){
		if(currentPlayer.infoPuzzle){
			playerState = PlayerState.InPuzzle;
		}
	}

	void OnGUI(){
		styleButton = new GUIStyle("button");
		styleButton.fontSize = Screen.width;

		textStyle = new GUIStyle();
		textStyle.fontSize = Screen.width/24;
		textStyle.font = currentFont;
		textStyle.alignment = TextAnchor.MiddleCenter;

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
				Rect kairoBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + heightMargin,Screen.width/2.8f,Screen.height/4.5f);
				if(currentPlayer.puzzle1){
					GUI.DrawTexture(kairoBlock,PuzzleColor[0]);
				} else {
					GUI.DrawTexture(kairoBlock,PuzzleGrey[0]);
				}
				ChangeGUI(customButton,infoButton);
				if(GUI.Button(new Rect(Screen.width/2, Screen.height/12 + Screen.height/12 + heightMargin*4, Screen.width/2.5f,Screen.height/10), "", customButton)){
					playerState = PlayerState.OutPuzzle;
					currentTextNumber = 0;
					kairo = true;
				}

				//Milkmaid
				Rect milkmaidBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + heightMargin*2,Screen.width/2.8f,Screen.height/4.5f);
				if(currentPlayer.puzzle2){
					GUI.DrawTexture(milkmaidBlock,PuzzleColor[1]);
				} else {
					GUI.DrawTexture(milkmaidBlock,PuzzleGrey[1]);
				}
				if(GUI.Button(new Rect(Screen.width/2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + heightMargin*5, Screen.width/2.5f,Screen.height/10), "", customButton)){
					playerState = PlayerState.OutPuzzle;
					currentTextNumber = 0;
					milk = true;
				}

				//Nightwatch
				Rect nightwatchBlock = new Rect(widthMargin*2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + Screen.height/4.5f + heightMargin*3,Screen.width/2.8f,Screen.height/4.5f);
				if(currentPlayer.puzzle3){
					GUI.DrawTexture(nightwatchBlock,PuzzleColor[2]);
				} else {
					GUI.DrawTexture(nightwatchBlock,PuzzleGrey[2]);
				}
				if(GUI.Button(new Rect(Screen.width/2, Screen.height/12 + Screen.height/12 + Screen.height/4.5f + Screen.height/4.5f + heightMargin*6, Screen.width/2.5f,Screen.height/10), "", customButton)){
					playerState = PlayerState.OutPuzzle;
					currentTextNumber = 0;
					night = true;
				}

				

				customButton.normal.background = Return;
				customButton.hover.background = Return;
				if (GUI.Button(new Rect(widthMargin, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), "", customButton)){
					playerState = PlayerState.None;
				}
			break;

			//HELP-------------------------------------------------------------------------------
			case PlayerState.Help:
				ChangeGUI(customButton, ButtonBackground);
				customButton.fontSize = Screen.width/16;
				customButton.font = currentFont;
				customButton.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(Screen.width/8, Screen.height/8, (Screen.width/8)*6, (Screen.height/8)*6), "Help is coming", customButton)){
					playerState = PlayerState.None;
				}
			break;

			//MAP--------------------------------------------------------------------------------
			case PlayerState.Map:
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),MapBackground);
				latitudeLocation = currentPlayer.GetComponent<LocationTracking>().latitudeLocation;
				longitudeLocation = currentPlayer.GetComponent<LocationTracking>().longitudeLocation;

				float createLatitude = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
				float createLongitude = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
				GUI.DrawTexture(new Rect(createLongitude-10,createLatitude-10,20,20), LocationTexture);

				ChangeGUI(customButton, ButtonBackground);
				customButton.fontSize = Screen.width/16;
				customButton.font = currentFont;
				customButton.alignment = TextAnchor.MiddleCenter;
				if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height/12), "Leave", customButton)){
					playerState = PlayerState.None;
				}
			break;

			//ROAMING----------------------------------------------------------------------------
			case PlayerState.None:
				customButton = new GUIStyle("button");
				float border = Screen.width/48;
				float box = (((Screen.width/48) * 46)/3);
				float height = Screen.height/8;
				
				//Backpack
				customButton.normal.background = Backpack;
				customButton.hover.background = Backpack;
				if (GUI.Button(new Rect(border, Screen.height-height-height/2, box*0.9f, height*1.5f), "", customButton)){
					playerState = PlayerState.Backpack;
				}

				//Help
				customButton.normal.background = Photo;
				customButton.hover.background = Photo;
				if (GUI.Button(new Rect(border*2 + box, Screen.height-(height/4)*3, box, height/2), "", customButton)){
					currentPlayer.help = true;
					playerState = PlayerState.Help;
				}

				//Map
				customButton.normal.background = Map;
				customButton.hover.background = Map;
				if (GUI.Button(new Rect(border*3 + box + box, Screen.height-height-height/4, box*0.8f, height), "", customButton)){
					playerState = PlayerState.Map;
				}
			break;

			//PUZZLE--------------------------------------------------------------------------------
			case PlayerState.InPuzzle:
				if(!currentPlayer.infoBusy){
					float border2 = Screen.width/24;
					float box2 = (((Screen.width/24) * 22)/3);
					float height2 = Screen.height/13;

					//Terug
					ChangeGUI(customButton,Return);
					if (GUI.Button(new Rect(border2, Screen.height-height2, box2, height2), "", customButton)){
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						currentPlayer.infoPuzzle = false;
						playerState = PlayerState.None;
						currentPlayer.cPuzle = Puzl.none;
					}

					//Info
					ChangeGUI(customButton,infoButton);
					if (GUI.Button(new Rect(border2 + box2, Screen.height-height2, box2, height2), "", customButton)){
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						currentPlayer.infoBusy = true;
					}

					//Help
					ChangeGUI(customButton,Photo);
					if (GUI.Button(new Rect(border2 + box2 + box2, Screen.height-height2, box2, height2), "", customButton)){
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						currentPlayer.infoPuzzle = false;
						playerState = PlayerState.Help;
						currentPlayer.cPuzle = Puzl.none;

						currentPlayer.helpNeeded = true;
					}
				} else {
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
					GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/40,Screen.width/2,Screen.height/3.5f),GarryTop);

					if(currentTextNumber != currentPlayer.textInfo.Count){
						GUI.DrawTexture(new Rect(Screen.width/40, (Screen.height/6)*2, Screen.width - Screen.width/20, Screen.height/3),infoBox);

						string currentText = currentPlayer.textInfo[currentTextNumber];
						currentText = currentText.Replace("<br>", "\n");
						GUI.Box(new Rect(Screen.width/40, (Screen.height/6)*2, Screen.width - Screen.width/20, Screen.height/3), currentText, textStyle);

						ChangeGUI(styleButton,next);
						if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11,Screen.width/3,Screen.height/12),"",styleButton)){
							currentTextNumber++;
						}
						if(currentTextNumber >= 1){
							ChangeGUI(styleButton,back);
							if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11 + Screen.height/12 + Screen.height/48,Screen.width/3,Screen.height/12),"",styleButton)){
								currentTextNumber--;
								if(currentTextNumber <= -1){
									currentTextNumber = 0;
								}
							}
						}
					} else {
						ChangeGUI(styleButton,StartPuzzle);
						if(GUI.Button(new Rect(Screen.width/8, (Screen.height/32)*11, (Screen.width/4)*3, Screen.height/3.6f),"",styleButton)){
							if(currentPlayer.cPuzle == Puzl.kairo){
								currentPlayer.kairoPuzzle.StartPuzzle();
							}
							if(currentPlayer.cPuzle == Puzl.contrast2){
								currentPlayer.contrastPuzzle2.StartPuzzle();
							}
							if(currentPlayer.cPuzle == Puzl.contrast1){
								currentPlayer.contrastPuzzle1.StartPuzzle();
							}
							currentPlayer.infoBusy = false;
							currentTextNumber = 0;
						}
						ChangeGUI(styleButton,Replay);
						if(GUI.Button(new Rect(Screen.width/10, (Screen.height/16)*12,Screen.width/4,Screen.height/8),"", styleButton)){
							currentTextNumber = 0;
						}
					}

					switch(currentPlayer.currentTeam){
						case Team.Team1:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team1Framed);
						break;
						case Team.Team2:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team2Framed);
						break;
						case Team.Team3:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team3Framed);
						break;
						case Team.Team4:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team4Framed);
						break;
					}
					
					ChangeGUI(customButton,Return);
					if (GUI.Button(new Rect(widthMargin, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), "", customButton)){
						currentPlayer.kairoPuzzle.StopPuzzle();
						currentPlayer.contrastPuzzle1.StopPuzzle();
						currentPlayer.contrastPuzzle2.StopPuzzle();
						currentPlayer.infoPuzzle = false;
						playerState = PlayerState.None;
						currentPlayer.cPuzle = Puzl.none;
						currentPlayer.infoBusy = false;
					}
				}
			break;

			//OUT PUZZLE--------------------------------------------------------------------------------
			case PlayerState.OutPuzzle:
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Background);
					GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/40,Screen.width/2,Screen.height/3.5f),GarryTop);

					if(kairo){
						if(currentTextNumber != currentPlayer.kairoPuzzle.information.Count){
							GUI.DrawTexture(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3),infoBox);

							string currentText = currentPlayer.kairoPuzzle.information[currentTextNumber];
							currentText = currentText.Replace("<br>", "\n");
							GUI.Box(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3), currentText, textStyle);

							ChangeGUI(styleButton,next);
							if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11,Screen.width/3,Screen.height/12),"",styleButton)){
								currentTextNumber++;
							}
							if(currentTextNumber >= 1){
								ChangeGUI(styleButton,back);
								if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11 + Screen.height/12 + Screen.height/48,Screen.width/3,Screen.height/12),"",styleButton)){
									currentTextNumber--;
									if(currentTextNumber <= -1){
										currentTextNumber = 0;
									}
								}
							}
						} else {
							playerState = PlayerState.Backpack;
							currentTextNumber = 0;
							kairo = false;
						}
					}
					if(milk){
						if(currentTextNumber != currentPlayer.contrastPuzzle2.information.Count){
							GUI.DrawTexture(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3),infoBox);

							string currentText = currentPlayer.contrastPuzzle2.information[currentTextNumber];
							currentText = currentText.Replace("<br>", "\n");
							GUI.Box(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3), currentText, textStyle);

							ChangeGUI(styleButton,next);
							if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11,Screen.width/3,Screen.height/12),"",styleButton)){
								currentTextNumber++;
							}
							if(currentTextNumber >= 1){
								ChangeGUI(styleButton,back);
								if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11 + Screen.height/12 + Screen.height/48,Screen.width/3,Screen.height/12),"",styleButton)){
									currentTextNumber--;
									if(currentTextNumber <= -1){
										currentTextNumber = 0;
									}
								}
							}
						} else {
							playerState = PlayerState.Backpack;
							currentTextNumber = 0;
							milk = false;
						}
					}
					if(night){
						if(currentTextNumber != currentPlayer.contrastPuzzle1.information.Count){
							GUI.DrawTexture(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3),infoBox);

							string currentText = currentPlayer.contrastPuzzle1.information[currentTextNumber];
							currentText = currentText.Replace("<br>", "\n");
							GUI.Box(new Rect(Screen.width/20, (Screen.height/6)*2, Screen.width/1.2f, Screen.height/3), currentText, textStyle);

							ChangeGUI(styleButton,next);
							if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11,Screen.width/3,Screen.height/12),"",styleButton)){
								currentTextNumber++;
							}
							if(currentTextNumber >= 1){
								ChangeGUI(styleButton,back);
								if(GUI.Button(new Rect(Screen.width/16, (Screen.height/16)*11 + Screen.height/12 + Screen.height/48,Screen.width/3,Screen.height/12),"",styleButton)){
									currentTextNumber--;
									if(currentTextNumber <= -1){
										currentTextNumber = 0;
									}
								}
							}
						} else {
							playerState = PlayerState.Backpack;
							currentTextNumber = 0;
							night = false;
						}
					}

					switch(currentPlayer.currentTeam){
						case Team.Team1:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team1Framed);
						break;
						case Team.Team2:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team2Framed);
						break;
						case Team.Team3:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team3Framed);
						break;
						case Team.Team4:
						GUI.DrawTexture(new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2.2f, Screen.height/3.2f),team4Framed);
						break;
					}
					
					ChangeGUI(customButton,Return);
					if (GUI.Button(new Rect(widthMargin, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), "", customButton)){
						playerState = PlayerState.Backpack;
					}
			break;

			//Ask for name
			case PlayerState.NameAsk:
			GUIStyle customInputField = new GUIStyle("");
			customInputField.name = "room name";
			customInputField.normal.background = InputFieldBackground;
			customInputField.hover.background = InputFieldBackground;
			customInputField.fontSize = Screen.height/24;
			customInputField.alignment = TextAnchor.MiddleCenter;
			customInputField.font = currentFont;
			customInputField.normal.textColor = Color.white;
			newName = GUI.TextField(new Rect(0, Screen.height-((Screen.height/6)*1.5f), Screen.width, Screen.height/12), newName, customInputField);

			GUIStyle custoButton = new GUIStyle("button");
			customButton.normal.background = ButtonBackground;
			customButton.hover.background = ButtonBackground;
			customButton.alignment = TextAnchor.MiddleCenter;
			customButton.font = currentFont;
			customButton.fontSize = Screen.height/12;
			customButton.normal.textColor = Color.white;
			if (GUI.Button(new Rect(0, Screen.height-(Screen.height/6), Screen.width, Screen.height/6), "Zet naam!!", customButton)){
				Debug.Log("Change name" + newName);
				currentPlayer.name = newName;
				currentPlayer.CmdChangeName(newName);
				playerState = PlayerState.None;
			}
			break;
		}
	}

	private void ChangeGUI(GUIStyle currentStyle, Texture2D buttonTexture){
		currentStyle.normal.background = buttonTexture;
		currentStyle.hover.background = buttonTexture;
		currentStyle.focused.background = buttonTexture;
		currentStyle.active.background = buttonTexture;
	}
}

//switch(this.GetComponent<PlayerController>().currentTeam){
				// 	case Team.Team1:
				// 	GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team1Texture);
				// 	break;

				// 	case Team.Team2:
				// 	GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team2Texture);
				// 	break;

				// 	case Team.Team3:
				// 	GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team3Texture);
				// 	break;

				// 	case Team.Team4:
				// 	GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),team4Texture);
				// 	break;
				// }
