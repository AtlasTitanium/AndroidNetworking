using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class TeamPicker : NetworkBehaviour {
	private PlayerController playerController;
	public HostInfo theHost;

	//buttons
	private GUIStyle buttonStyle;

	private GUIStyle Team1Button;
	private GUIStyle Team2Button;
	private GUIStyle Team3Button;
	private GUIStyle Team4Button;
	//endButtons

	//textures
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
	public Texture2D Background;
	//end textures

	void Start(){
		playerController = GetComponent<PlayerController>();
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 
		if(isServer){
			this.enabled = false;
			return;
		}

		if(theHost == null){
			CmdFindHost();
		}
	}

	void Update () {
		if (!isLocalPlayer)
        {
			this.enabled = false;
            return;
        } 
		if(isServer){
			this.enabled = false;
			return;
		}

		if(theHost == null){
			CmdFindHost();
		}
	}

	void OnGUI(){
		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = Screen.height/14;
		buttonStyle.normal.background = NextButton;
		buttonStyle.hover.background = NextButton;

		//change team icon when clicked
		Team1Button = new GUIStyle("button");
		if(playerController.currentTeam == Team.Team1){
			Team1Button.normal.background = Team1_Check;
			Team1Button.hover.background = Team1_Texture;
		} else {
			Team1Button.normal.background = Team1_Texture;
			Team1Button.hover.background = Team1_Check;
		}

		Team2Button = new GUIStyle("button");
		if(playerController.currentTeam == Team.Team2){
			Team2Button.normal.background = Team2_Check;
			Team2Button.hover.background = Team2_Texture;
		} else {
			Team2Button.normal.background = Team2_Texture;
			Team2Button.hover.background = Team2_Check;
		}

		Team3Button = new GUIStyle("button");
		if(playerController.currentTeam == Team.Team3){
			Team3Button.normal.background = Team3_Check;
			Team3Button.hover.background = Team3_Texture;
		} else {
			Team3Button.normal.background = Team3_Texture;
			Team3Button.hover.background = Team3_Check;
		}

		Team4Button = new GUIStyle("button");
		if(playerController.currentTeam == Team.Team4){
			Team4Button.normal.background = Team4_Check;
			Team4Button.hover.background = Team4_Texture;
		} else {
			Team4Button.normal.background = Team4_Texture;
			Team4Button.hover.background = Team4_Check;
		}
		//------------------------------

		//TeamButtons
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background, ScaleMode.StretchToFill, true, 10.0F);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/14), KiesJeTeam, ScaleMode.StretchToFill, true, 10.0F);
		int heightMargin = Screen.height/24;
		int widthMargin = Screen.width/24;

		//topLeft
		if(theHost != null){
			if(theHost.amountTeam1.Count <= 4){
				GUI.DrawTexture(new Rect(widthMargin,Screen.height/14 + heightMargin/2,Screen.width/2 - widthMargin,Screen.height/3), Team1_Texture, ScaleMode.StretchToFill, true, 10.0F);
				if(GUI.Button(new Rect(widthMargin,Screen.height/14 + heightMargin/2,Screen.width/2 - widthMargin,Screen.height/3), "", Team1Button)){
					playerController.currentTeam = Team.Team1;
				}
			}

			//topRight
			if(theHost.amountTeam2.Count <= 4){
				GUI.DrawTexture(new Rect(Screen.width/2 + widthMargin,Screen.height/16 + (heightMargin * 2.5f),Screen.width/2 - widthMargin*2,Screen.height/3f), Team2_Texture, ScaleMode.StretchToFill, true, 10.0F);
				if(GUI.Button(new Rect(Screen.width/2 + widthMargin,Screen.height/16 + (heightMargin * 2.5f),Screen.width/2 - widthMargin*2,Screen.height/3f), "", Team2Button)){
					playerController.currentTeam = Team.Team2;
				}
			}

			//bottomLeft
			if(theHost.amountTeam3.Count <= 4){
				GUI.DrawTexture(new Rect(widthMargin,Screen.height/2 + heightMargin,Screen.width/2 - widthMargin,Screen.height/3), Team3_Texture, ScaleMode.StretchToFill, true, 10.0F);
				if(GUI.Button(new Rect(widthMargin,Screen.height/2 + heightMargin,Screen.width/2 - widthMargin,Screen.height/3), "", Team3Button)){
					playerController.currentTeam = Team.Team3;
				}
			}

			//bottomRight
			if(theHost.amountTeam4.Count <= 4){
				GUI.DrawTexture(new Rect(Screen.width/2 + widthMargin,Screen.height/2 + (heightMargin * 2.5f),Screen.width/2 - widthMargin*2,Screen.height/3), Team4_Texture, ScaleMode.StretchToFill, true, 10.0F);
				if(GUI.Button(new Rect(Screen.width/2 + widthMargin,Screen.height/2 + (heightMargin * 2.5f),Screen.width/2 - widthMargin*2,Screen.height/3), "", Team4Button)){
					playerController.currentTeam = Team.Team4;
				}
			}
		}

		//confirmTeam
		if(GUI.Button(new Rect(widthMargin,Screen.height - Screen.height/10,Screen.width/2.5f,Screen.height/12), "", buttonStyle)){
			if(playerController.currentTeam != Team.NoTeam){
				switch(playerController.currentTeam){
					case Team.Team1:
					Debug.Log("Color Blue");
					CmdAddPlayer(1, this.gameObject);
					break;

					case Team.Team2:
					Debug.Log("Color Red");
					CmdAddPlayer(2, this.gameObject);
					break;

					case Team.Team3:
					Debug.Log("Color Green");
					CmdAddPlayer(3, this.gameObject);
					break;

					case Team.Team4:
					Debug.Log("Color Yellow");
					CmdAddPlayer(4, this.gameObject);
					break;
				}
				this.GetComponent<PlayerUI>().enabled = true;
				this.enabled = false;
			}
		}
	}

	[Command]
	public void CmdAddPlayer(int teamNumber, GameObject player){
		switch(teamNumber){
			case 1:
				Debug.Log("addToTeam1");
				theHost.amountTeam1.Add(player);
				playerController.currentTeam = Team.Team1;
				break;
			case 2:
				Debug.Log("addToTeam2");
				theHost.amountTeam2.Add(player);
				playerController.currentTeam = Team.Team2;
				break;
			case 3:
				Debug.Log("addToTeam3");
				theHost.amountTeam3.Add(player);
				playerController.currentTeam = Team.Team3;
				break;
			case 4:
				Debug.Log("addToTeam4");
				theHost.amountTeam4.Add(player);
				playerController.currentTeam = Team.Team4;
				break;
			default:
				Debug.Log("Error Boi");
				break;
		}
	}

	[Command]
	public void CmdFindHost(){
		RpcFindHost();
	}

	[ClientRpc]
	public void RpcFindHost(){
		theHost = GameObject.FindGameObjectWithTag("Host").GetComponent<HostInfo>();
	}
}

//--------------------|
//OLD TEAM PICK SYSTEM|==========================================
//--------------------|

// customButton.fontSize = Screen.height/12;
// if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height/4), "Team 1", customButton)){
// 	Debug.Log("Color Blue");
// 	playerController.CmdChooseTeam(Color.blue);
// 	playerController.currentTeam = Team.Team1;

// 	CmdAddPlayer(1, this.gameObject);
// 	this.GetComponent<PlayerUI>().enabled = true;
// 	this.enabled = false;
// }
// if (GUI.Button(new Rect(0, Screen.height/4, Screen.width, Screen.height/4), "Team 2", customButton)){
// 	Debug.Log("Color Red");
// 	playerController.CmdChooseTeam(Color.red);
// 	playerController.currentTeam = Team.Team2;

// 	CmdAddPlayer(2, this.gameObject);
// 	this.GetComponent<PlayerUI>().enabled = true;
// 	this.enabled = false;
// }
// if (GUI.Button(new Rect(0, Screen.height/2, Screen.width, Screen.height/4), "Team 3", customButton)){
// 	Debug.Log("Color Green");
// 	playerController.CmdChooseTeam(Color.green);
// 	playerController.currentTeam = Team.Team3;

// 	CmdAddPlayer(3, this.gameObject);
// 	this.GetComponent<PlayerUI>().enabled = true;
// 	this.enabled = false;
// }
// if (GUI.Button(new Rect(0, Screen.height-(Screen.height/4), Screen.width, Screen.height/4), "Team 4", customButton)){
// 	Debug.Log("Color Yellow");
// 	playerController.CmdChooseTeam(Color.yellow);
// 	playerController.currentTeam = Team.Team4;

// 	CmdAddPlayer(4, this.gameObject);
// 	this.GetComponent<PlayerUI>().enabled = true;
// 	this.enabled = false;
// }

//=========================================================