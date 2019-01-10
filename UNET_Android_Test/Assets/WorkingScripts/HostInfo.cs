using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public enum LocationTrack {NoTrack, Team1, Team2, Team3, Team4, Team1Location, Team2Location, Team3Location, Team4Location}
public class HostInfo : NetworkBehaviour {
	//Location tracking
	public float latitudeLocation;
	public float longitudeLocation;

	public Texture2D LocationTexture;
	public Texture2D mapTexture;

	public float minLatitude;
	public float maxLatitude;

	public float minLongitude;
	public float maxLongitude;
	//end location tracking
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
	public int team1Score;
	public int team2Score;
	public int team3Score;
	public int team4Score;
    GUIStyle titleStyle = new GUIStyle();
	GUIStyle style = new GUIStyle();
	GUIStyle button = new GUIStyle();

	private LocationTrack localTrack = LocationTrack.NoTrack;
	
	private int currentPlayer = 0;

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

		// if(amountTeam1.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam2.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam3.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
		// if(amountTeam4.Count == 0){
		// 	localTrack = LocationTrack.NoTrack;
		// }
	}

	void OnGUI(){
		button = new GUIStyle("button");
		button.fontSize = Screen.height/16;
		// //show numbers
		// GUIStyle style2 = new GUIStyle();
		// style2.fontSize = Screen.height/16;
		// GUI.Label(new Rect(Screen.width/16, Screen.height/100, Screen.width, Screen.height/8), latitudeLocation.ToString(),style2);
		// GUI.Label(new Rect(Screen.width/16, Screen.height/16, Screen.width, Screen.height/8), longitudeLocation.ToString(),style2);
		// //end numbers
		
	//Location tracking---------------------------------------------------
		switch(localTrack){
			//TEAM 1
			case LocationTrack.Team1Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team1;
			}

			if(amountTeam1.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(1, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam1.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(1, true);
				}
			}
			latitudeLocation = amountTeam1[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
			longitudeLocation = amountTeam1[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;

			float createLatitude = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude-10,createLatitude-10,20,20), LocationTexture);
			break;

			//TEAM 2
			case LocationTrack.Team2Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team2;
			}

			if(amountTeam2.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(2, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam2.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(2, true);
				}
			}
			latitudeLocation = amountTeam2[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
			longitudeLocation = amountTeam2[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;

			float createLatitude2 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude2 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude2-10,createLatitude2-10,20,20), LocationTexture);
			break;

			//TEAM 3
			case LocationTrack.Team3Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team3;
			}

			if(amountTeam3.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(3, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam3.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(3, true);
				}
			}
			latitudeLocation = amountTeam3[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
			longitudeLocation = amountTeam3[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;

			float createLatitude3 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude3 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude3-10,createLatitude3-10,20,20), LocationTexture);
			break;

			//TEAM 4
			case LocationTrack.Team4Location:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				localTrack = LocationTrack.Team4;
			}

			if(amountTeam4.Count >= 2){
				if(GUI.Button(new Rect(0,Screen.height-Screen.height/10, Screen.width, Screen.height/10), "NextPlayer", button)){
					//RpcUpdatePlayer(4, false);
					currentPlayer++;
					if(currentPlayer >= amountTeam4.Count){
						currentPlayer = 0;
					}
					//RpcUpdatePlayer(4, true);
				}
			}

			latitudeLocation = amountTeam4[currentPlayer].GetComponent<LocationTracking>().latitudeLocation;
			longitudeLocation = amountTeam4[currentPlayer].GetComponent<LocationTracking>().longitudeLocation;

			float createLatitude4 = Screen.height - ((Screen.height / (maxLatitude - minLatitude)) * (latitudeLocation - minLatitude));
			float createLongitude4 = (Screen.width / (maxLongitude - minLongitude)) * (longitudeLocation - minLongitude);
			GUI.DrawTexture(new Rect(createLongitude4-10,createLatitude4-10,20,20), LocationTexture);
			break;
	//End=LocationInfo--------------------------------------------------------------------

	//TeamInformationScreens-------------------------------------------------------------
			//TEAM 1
			case LocationTrack.Team1:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/10), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F); //TopScreen

			int screenParts = (Screen.height - Screen.height/10)/amountTeam1.Count;
			bool isMissing = false;
			for(int i = 0; i < amountTeam1.Count; ++i){
				string playerName = "no name";
				if(amountTeam1[i] == null){
					playerName = "Player " + i + " - Missing";
					isMissing = true;
				} else {
					playerName = "Player " + i;
					isMissing = false;
				}
				GUI.Box(new Rect(0, (Screen.height/10 + screenParts*i), Screen.width - Screen.width/6, screenParts), playerName, style);

				if(!isMissing){
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts*i), Screen.width/6, screenParts), LocationTexture, button)){
						localTrack = LocationTrack.Team1Location;
					}
				} else {
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts*i), Screen.width/6, screenParts), "X", button)){
						amountTeam1.Remove(amountTeam1[i]);
						localTrack = LocationTrack.NoTrack;
					}
				}
			}

			if(GUI.Button(new Rect(Screen.width - Screen.width/6,0, Screen.width/6, Screen.height/10), "Back", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 2
			case LocationTrack.Team2:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/10), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F); //TopScreen

			int screenParts2 = (Screen.height - Screen.height/10)/amountTeam2.Count;
			bool isMissing2 = false;
			for(int i = 0; i < amountTeam2.Count; ++i){
				string playerName2 = "no name";
				if(amountTeam2[i] == null){
					playerName2 = "Player " + i + " - Missing";
					isMissing2 = true;
				} else {
					playerName2 = "Player " + i;
					isMissing2 = false;
				}
				GUI.Box(new Rect(0, (Screen.height/10 + screenParts2*i), Screen.width - Screen.width/6, screenParts2), playerName2, style);

				if(!isMissing2){
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts2*i), Screen.width/6, screenParts2), LocationTexture, button)){
						localTrack = LocationTrack.Team2Location;
					}
				} else {
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts2*i), Screen.width/6, screenParts2), "X", button)){
						amountTeam2.Remove(amountTeam2[i]);
						localTrack = LocationTrack.NoTrack;
					}
				}
			}

			if(GUI.Button(new Rect(Screen.width - Screen.width/6,0, Screen.width/6, Screen.height/10), "Back", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 3
			case LocationTrack.Team3:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/10), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F); //TopScreen

			int screenParts3 = (Screen.height - Screen.height/10)/amountTeam3.Count;
			bool isMissing3 = false;
			for(int i = 0; i < amountTeam3.Count; ++i){
				string playerName3 = "no name";
				if(amountTeam3[i] == null){
					playerName3 = "Player " + i + " - Missing";
					isMissing3 = true;
				} else {
					playerName3 = "Player " + i;
					isMissing3 = false;
				}
				GUI.Box(new Rect(0, (Screen.height/10 + screenParts3*i), Screen.width - Screen.width/6, screenParts3), playerName3, style);

				if(!isMissing3){
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts3*i), Screen.width/6, screenParts3), LocationTexture, button)){
						localTrack = LocationTrack.Team3Location;
					}
				} else {
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts3*i), Screen.width/6, screenParts3), "X", button)){
						amountTeam3.Remove(amountTeam3[i]);
						localTrack = LocationTrack.NoTrack;
					}
				}
			}

			if(GUI.Button(new Rect(Screen.width - Screen.width/6,0, Screen.width/6, Screen.height/10), "Back", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;

			//TEAM 4
			case LocationTrack.Team4:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture, ScaleMode.StretchToFill, true, 10.0F); //background
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/10), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F); //TopScreen

			int screenParts4 = (Screen.height - Screen.height/10)/amountTeam4.Count;
			bool isMissing4 = false;
			for(int i = 0; i < amountTeam4.Count; ++i){
				string playerName4 = "no name";
				if(amountTeam4[i] == null){
					playerName4 = "Player " + i + " - Missing";
					isMissing4 = true;
				} else {
					playerName4 = "Player " + i;
					isMissing4 = false;
				}
				GUI.Box(new Rect(0, (Screen.height/10 + screenParts4*i), Screen.width - Screen.width/6, screenParts4), playerName4, style);

				if(!isMissing4){
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts4*i), Screen.width/6, screenParts4), LocationTexture, button)){
						localTrack = LocationTrack.Team4Location;
					}
				} else {
					if(GUI.Button(new Rect(Screen.width - Screen.width/6,(Screen.height/10 + screenParts4*i), Screen.width/6, screenParts4), "X", button)){
						amountTeam4.Remove(amountTeam4[i]);
						localTrack = LocationTrack.NoTrack;
					}
				}
			}

			if(GUI.Button(new Rect(Screen.width - Screen.width/6,0, Screen.width/6, Screen.height/10), "Back", button)){
				localTrack = LocationTrack.NoTrack;
			}
			break;
	//End=TeamInfo------------------------------------------------------------------------

	//NORMAL------------------------------------------------------------------------
			case LocationTrack.NoTrack:
	
			currentPlayer = 0;

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
				GUI.DrawTexture(new Rect(0, Screen.height/10, Screen.width-Screen.width/10, _BlockTitleHeight), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, Screen.height/10 + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team1Info, style);

				if(amountTeam1.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, Screen.height/10, Screen.width/10, _BlockTitleHeight), "T", button)){
						localTrack = LocationTrack.Team1;
					}
				}

				//Team 2
				string team2Info = "  T_Size: " + amountTeam2.Count + "   Score: " + team2Score;
				GUI.DrawTexture(new Rect(0, ((Screen.height/10) + BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, ((Screen.height/10) + BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team2Info, style);

				if(amountTeam2.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + BlockHeight), Screen.width/10, _BlockTitleHeight), "T", button)){
						localTrack = LocationTrack.Team2;
					}
				}

				//Team 3
				string team3Info = "  T_Size: " + amountTeam3.Count + "   Score: " + team3Score;
				GUI.DrawTexture(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)), Screen.width-Screen.width/10, _BlockTitleHeight), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team3Info, style);

				if(amountTeam3.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + (BlockHeight * 2)), Screen.width/10, _BlockTitleHeight), "T", button)){
						localTrack = LocationTrack.Team3;
					}
				}

				//Team 4
				string team4Info = "  T_Size: " + amountTeam4.Count + "   Score: " + team4Score;
				GUI.DrawTexture(new Rect(0, (Screen.height - BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, (Screen.height - BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team4Info, style);

				if(amountTeam4.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, (Screen.height - BlockHeight), Screen.width/10, _BlockTitleHeight), "T", button)){
						localTrack = LocationTrack.Team4;
					}
				}
			break;
	//End=NormalScreen--------------------------------------------------------------------------------------------------
		}
	}

	[ClientRpc]
	public void RpcUpdatePlayer(int teamNumber, bool start){
		if(!start){
			switch(teamNumber){
				case 1:
				StopCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 2:
				StopCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 3:
				StopCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
				case 4:
				StopCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().Start());
				StartCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().loop());
				break;
			}
		} else {
			switch(teamNumber){
				case 1:
				StopCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam1[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 2:
				StopCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam2[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 3:
				StopCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam3[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
				case 4:
				StopCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().loop());
				StartCoroutine(amountTeam4[currentPlayer].GetComponent<LocationTracking>().Start());
				break;
			}
		}
	}
}

//Original team info
/*//Teams
		int BlockHeight = ((Screen.height - Screen.height/10) / 4);
		int _BlockTitleHeight = ((Screen.height - Screen.height/10) / 8);
			//Team 1
			string team1Info = "  T_Size: " + amountTeam1.Count + "   Score: " + team1Score;
			GUI.DrawTexture(new Rect(0, Screen.height/10, Screen.width-Screen.width/10, _BlockTitleHeight), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, Screen.height/10 + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team1Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, Screen.height/10, Screen.width/10, _BlockTitleHeight), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 2
			string team2Info = "  T_Size: " + amountTeam2.Count + "   Score: " + team2Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team2Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + BlockHeight), Screen.width/10, _BlockTitleHeight), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 3
			string team3Info = "  T_Size: " + amountTeam3.Count + "   Score: " + team3Score;
			GUI.DrawTexture(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)), Screen.width-Screen.width/10, _BlockTitleHeight), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team3Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + (BlockHeight * 2)), Screen.width/10, _BlockTitleHeight), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}

			//Team 4
			string team4Info = "  T_Size: " + amountTeam4.Count + "   Score: " + team4Score;
			GUI.DrawTexture(new Rect(0, (Screen.height - BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
			GUI.Box(new Rect(0, (Screen.height - BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team4Info, style);

			if(GUI.Button(new Rect(Screen.width-Screen.width/10, (Screen.height - BlockHeight), Screen.width/10, _BlockTitleHeight), LocationTexture)){
				localTrack = LocationTracking.Team1;
			}
			 */