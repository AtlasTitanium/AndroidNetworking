using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class KairoPuzzle : MonoBehaviour {

	private PlayerInfo pInfo;
	public GameObject brokenPuzzle;
	public Button startPuzzleButton;
	public GameObject[] puzzlePieces;
	private int countToWin;
	private int finalScore;
	private GUIStyle end;
	public Font Dyslectic_font;
	public Texture2D winImage;
	private bool won = false;

	void OnEnable(){
		pInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
		startPuzzleButton.gameObject.SetActive(!pInfo.puzzle3);
	}
	void Start(){	
		startPuzzleButton.onClick.AddListener(TaskOnClick);	
		foreach(GameObject piece in puzzlePieces){
			piece.GetComponent<Draggable>().enabled = false;
		}
	}

	void Update(){
		brokenPuzzle.SetActive(!pInfo.puzzle3);

		if(!startPuzzleButton.gameObject.active){
			foreach(GameObject piece in puzzlePieces){
				if(piece.GetComponent<Draggable>().isRight){
					countToWin++;
				} else {
					countToWin = 0;
					break;
				}

				if(countToWin >= puzzlePieces.Length){
					StopAllCoroutines();
					pInfo.puzzle3 = true;
					won = true;
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
				pInfo.GetComponent<TeamScore>().enabled = true;
				pInfo.GainScore(finalScore);
				this.enabled = false;
			}
		} 
	}
	private void TaskOnClick(){
		startPuzzleButton.gameObject.SetActive(false);
		foreach(GameObject piece in puzzlePieces){
			piece.GetComponent<Draggable>().enabled = true;
		}
		pInfo.GetComponent<TeamScore>().enabled = false;
		StartCoroutine(CountScore());
	}

	private IEnumerator CountScore(){
		finalScore = 200;
		yield return new WaitForSeconds(60);
		finalScore = 100;
		yield return new WaitForSeconds(60);
		finalScore = 50;
	}
}
