using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class KairoPuzzle : NetworkBehaviour {

	private PlayerController pController;
	public GameObject brokenPuzzle;
	public Button startPuzzleButton;
	public GameObject[] puzzlePieces;
	private int countToWin;
	private int finalScore;
	private GUIStyle end;
	public Font Dyslectic_font;
	public Texture2D winImage;
	private bool won = false;
	private bool active = false;
	public List<string> information = new List<string>();
	
	void Start(){
		startPuzzleButton.onClick.AddListener(TaskOnClick);	
		foreach(GameObject piece in puzzlePieces){
			piece.GetComponent<Draggable>().enabled = false;
		}
	}

	void Update(){
		if(isServer){
            this.enabled = false;
            return;
        }
		if(pController != null){
			//Debug.Log("found player");
			pController.kairoPuzzle = this;
		} else{
			pController = GameObject.Find("Client").GetComponent<PlayerController>();
			pController.kairoPuzzle = this;
			//Debug.Log("player not found");
			return;
		}

		brokenPuzzle.SetActive(!pController.puzzle3);

		if(!startPuzzleButton.gameObject.activeSelf){
			Debug.Log("puzzle running");
			foreach(GameObject piece in puzzlePieces){
				if(piece.GetComponent<Draggable>().isRight){
					countToWin++;
				} else {
					countToWin = 0;
					break;
				}

				Debug.Log(countToWin);
				if(countToWin >= puzzlePieces.Length){
					StopAllCoroutines();
					if(active){
						for(int i = 0; i < information.Count; i++){
							Debug.Log(i);
							pController.textInfo.RemoveAt(0);
						}
						active = false;
					}
					pController.infoPuzzle = false;
					startPuzzleButton.gameObject.SetActive(false);
					pController.puzzle3 = true;
					won = true;
					countToWin = 0;
				}
			}
		}
	}

	void OnGUI(){
		end = new GUIStyle("button");
		end.normal.background = winImage;
		end.alignment = TextAnchor.MiddleCenter;
		end.font = Dyslectic_font;

		if(won){
			end.fontSize = Screen.height/17;
			string score = "Goed gedaan\nJe wint:\n" + finalScore + " Punten";
			if(GUI.Button(new Rect(Screen.width/8,Screen.height/4,Screen.width/1.25f,Screen.height/2), score, end)){
				pController.GetComponent<PlayerUI>().playerState = PlayerState.None;
				pController.GainScore(finalScore,1);
				this.enabled = false;
			}
		} 
	}
	public void TaskOnClick(){
		pController.cPuzle = Puzl.kairo;
		pController.infoPuzzle = true;
		pController.infoBusy = true;
		if(pController.textInfo.Count < information.Count){
			for(int i = 0; i < information.Count; i++){
				pController.textInfo.Add(information[i]);
			}
		}
	}

	public void StartPuzzle(){
		foreach(GameObject piece in puzzlePieces){
			piece.GetComponent<Draggable>().enabled = true;
		}
		while(startPuzzleButton.gameObject.activeSelf){
			//Debug.Log("still active");
			startPuzzleButton.gameObject.SetActive(false);
		}
		//Debug.Log("start puzzle");

		active = true;
		StartCoroutine(CountScore());
	}

	private IEnumerator CountScore(){
		finalScore = 200;
		yield return new WaitForSeconds(60);
		finalScore = 100;
		yield return new WaitForSeconds(60);
		finalScore = 50;
	}

	public void StopPuzzle(){
		StopAllCoroutines();
		finalScore = 200;
		foreach(GameObject piece in puzzlePieces){
			piece.GetComponent<Draggable>().enabled = false;
		}
		while(!startPuzzleButton.gameObject.activeSelf){
			startPuzzleButton.gameObject.SetActive(true);
		}
		if(active){
			for(int i = 0; i < information.Count; i++){
				Debug.Log(i);
				pController.textInfo.RemoveAt(0);
			}
			active = false;
		}
	}
}
