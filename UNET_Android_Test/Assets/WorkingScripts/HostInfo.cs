using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public enum LocationTrack {NoTrack, Team1, Team2, Team3, Team4}
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
	GUIStyle button = new GUIStyle("button");

	private LocationTrack localTrack = LocationTrack.NoTrack;
	
	private int currentPlayer = 0;
	public bool loopActive = false;

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

		button.fontSize = Screen.height/10;
		Debug.Log("loopactive is " + loopActive);

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
		//Location tracking---------------------------------------------------
		switch(localTrack){
			//TEAM 1
			case LocationTrack.Team1:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);
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

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				//RpcUpdatePlayer(1, false);
				localTrack = LocationTrack.NoTrack;
			}

			if(loopActive){
				//RpcUpdatePlayer(1, true);
				loopActive = false;
			}

			//show numbers
			GUIStyle style2 = new GUIStyle();
			style2.fontSize = Screen.height/16;
			GUI.Label(new Rect(Screen.width/16, Screen.height/100, Screen.width, Screen.height/8), latitudeLocation.ToString(),style2);
			GUI.Label(new Rect(Screen.width/16, Screen.height/16, Screen.width, Screen.height/8), longitudeLocation.ToString(),style2);
			//end numbers
			break;

			//TEAM 2
			case LocationTrack.Team2:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);
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

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				//RpcUpdatePlayer(2, false);
				localTrack = LocationTrack.NoTrack;
			}

			if(loopActive){
				//RpcUpdatePlayer(2, true);
				loopActive = false;
			}
			break;

			//TEAM 3
			case LocationTrack.Team3:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);
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

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				//RpcUpdatePlayer(3, false);
				localTrack = LocationTrack.NoTrack;
			}

			if(loopActive){
				//RpcUpdatePlayer(3, true);
				loopActive = false;
			}
			break;

			//TEAM 4
			case LocationTrack.Team4:
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mapTexture, ScaleMode.StretchToFill, true, 10.0F);
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

			if(GUI.Button(new Rect(0,0, Screen.width, Screen.height/10), "Leave", button)){
				//RpcUpdatePlayer(4, false);
				localTrack = LocationTrack.NoTrack;
			}

			if(loopActive){
				//RpcUpdatePlayer(4, true);
				loopActive = false;
			}
			break;


			//NORMAL------------------------------------------------------------------------
			case LocationTrack.NoTrack:
			loopActive = true;
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
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, Screen.height/10, Screen.width/10, _BlockTitleHeight), LocationTexture)){
						localTrack = LocationTrack.Team1;
					}
				}

				//Team 2
				string team2Info = "  T_Size: " + amountTeam2.Count + "   Score: " + team2Score;
				GUI.DrawTexture(new Rect(0, ((Screen.height/10) + BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, ((Screen.height/10) + BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team2Info, style);

				if(amountTeam2.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + BlockHeight), Screen.width/10, _BlockTitleHeight), LocationTexture)){
						localTrack = LocationTrack.Team2;
					}
				}

				//Team 3
				string team3Info = "  T_Size: " + amountTeam3.Count + "   Score: " + team3Score;
				GUI.DrawTexture(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)), Screen.width-Screen.width/10, _BlockTitleHeight), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, ((Screen.height/10) + (BlockHeight * 2)) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team3Info, style);

				if(amountTeam3.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, ((Screen.height/10) + (BlockHeight * 2)), Screen.width/10, _BlockTitleHeight), LocationTexture)){
						localTrack = LocationTrack.Team3;
					}
				}

				//Team 4
				string team4Info = "  T_Size: " + amountTeam4.Count + "   Score: " + team4Score;
				GUI.DrawTexture(new Rect(0, (Screen.height - BlockHeight), Screen.width-Screen.width/10, _BlockTitleHeight), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
				GUI.Box(new Rect(0, (Screen.height - BlockHeight) + _BlockTitleHeight, Screen.width, _BlockTitleHeight), team4Info, style);

				if(amountTeam4.Count != 0){
					if(GUI.Button(new Rect(Screen.width-Screen.width/10, (Screen.height - BlockHeight), Screen.width/10, _BlockTitleHeight), LocationTexture)){
						localTrack = LocationTrack.Team4;
					}
				}
			break;
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