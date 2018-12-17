using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {
	public Team currentTeam = Team.NoTeam;
	public int teamScore;
	public string ARScene;
	public string winScene;

	//check if player did the puzzles
	[HideInInspector]	
	public bool puzzle1 = false;
	[HideInInspector]	
	public bool puzzle2 = false;
	[HideInInspector]	
	public bool puzzle3 = false;

	private bool win = false;

	
	void Start () {
		DontDestroyOnLoad(this);
	}

	void Update(){
		if(!win){
			if(puzzle1 && puzzle2 && puzzle3){
				StartCoroutine(Win());
				win = true;
			}
		}
	}

	public void GoToScene(string scene){
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	public void GainScore(int score){
		teamScore += score;
	}

	IEnumerator Win(){
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(winScene, LoadSceneMode.Single);
	}
}
