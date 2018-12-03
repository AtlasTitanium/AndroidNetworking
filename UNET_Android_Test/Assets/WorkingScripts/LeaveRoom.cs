using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class LeaveRoom : MonoBehaviour {
	private NetworkManager networkManager;
	public GUIStyle customButton;

	void Start(){
		networkManager = NetworkManager.singleton;
	}
	void OnGUI(){
		customButton = new GUIStyle("button");

		//buttonToLeave
		MatchInfo matchInfo = networkManager.matchInfo;
		customButton.fontSize = Screen.height/12;
        if (GUI.Button(new Rect(0, Screen.height-(Screen.height/6), Screen.width, Screen.height/6), "LeaveMatch", customButton)){
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
			networkManager.StopHost();
            Debug.Log("LeftTheMatch");
        }
	}
}
