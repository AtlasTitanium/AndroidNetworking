using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ContrastPuzzle1 : NetworkBehaviour {
	public PlayerController pController;
	public Button startButton;
	public GameObject[] colors;
	private int finalScore;
	public Texture2D white;
	public Texture2D Color1;
	public Texture2D Color2;
	public Texture2D Color3;
	public Texture2D fout;
	private bool wrong = false;
	private bool _Color1, _Color2, _Color3;
	private GUIStyle foutStyle;
	public Font Dyslectic_font;
	private bool on = false;
	private bool won = false;
	private bool active = false;
	public List<string> information = new List<string>();
	public List<Texture2D> Images = new List<Texture2D>();
	void Start(){
		foreach(GameObject color in colors){
			color.SetActive(false);
		}
	}

	public void Color3Right(){
		_Color3 = true;
	}
	public void Color2Right(){
		_Color2 = true;
	}
	public void Color1Right(){
		_Color1 = true;
	}
	
	public void OnWrong(){
		wrong = true;
		finalScore -= 50;
	}

	void Update(){
		if(isServer){
            this.enabled = false;
            return;
        }
		if(pController != null){
			//Debug.Log("found player");
			pController.contrastPuzzle1 = this;
		} else{
			pController = GameObject.Find("Client").GetComponent<PlayerController>();
			pController.contrastPuzzle1 = this;
			//Debug.Log("player not found");
			return;
		}
		startButton.onClick.AddListener(StartChallenge);
		
		if(finalScore <= 20){
			finalScore = 20;
		}
		if(_Color3 && _Color1 && _Color2){
			StopAllCoroutines();
			won = true;
		}
	}

	void OnGUI(){
		if(on){
			foutStyle = new GUIStyle("button");
			foutStyle.normal.background = fout;
			foutStyle.alignment = TextAnchor.MiddleCenter;
			foutStyle.font = Dyslectic_font;

			if(won){
				foutStyle.fontSize = Screen.height/17;
				string score = "Goed gedaan\nJe wint:\n" + finalScore + " Punten";
				if(GUI.Button(new Rect(Screen.width/8,Screen.height/4,Screen.width/1.25f,Screen.height/2), score, foutStyle)){
					foreach(GameObject color in colors){
						color.SetActive(false);
					}
					if(active){
						for(int i = 0; i < information.Count; i++){
							Debug.Log(i);
							pController.textInfo.RemoveAt(0);
						}
						active = false;
					}
					pController.GetComponent<PlayerUI>().playerState = PlayerState.None;
					pController.GainScore(finalScore, 3);
					won = false;
					this.enabled = false;
				}
			} else {
				foutStyle.fontSize = Screen.height/8;
			}

			if(wrong){
				if(GUI.Button(new Rect(Screen.width/8,Screen.height/4,Screen.width/1.25f,Screen.height/2), "Fout", foutStyle)){
					wrong = false;
				}
			}

			int BlockWidth = Screen.width/3;
			int BlockHeight = Screen.height/12;

			//Color3
			if(_Color3){
				GUI.DrawTexture(new Rect(0, 0, BlockWidth, BlockHeight), Color3, ScaleMode.StretchToFill, true, 10.0F);
			} else {
				GUI.DrawTexture(new Rect(0, 0, BlockWidth, BlockHeight), white, ScaleMode.StretchToFill, true, 10.0F);
			}

			//Color2
			if(_Color2){
				GUI.DrawTexture(new Rect(BlockWidth, 0, BlockWidth, BlockHeight), Color2, ScaleMode.StretchToFill, true, 10.0F);
			} else {
				GUI.DrawTexture(new Rect(BlockWidth, 0, BlockWidth, BlockHeight), white, ScaleMode.StretchToFill, true, 10.0F);
			}

			//Color1
	
			if(_Color1){
				GUI.DrawTexture(new Rect(BlockWidth*2, 0, BlockWidth, BlockHeight), Color1, ScaleMode.StretchToFill, true, 10.0F);
			} else {
				GUI.DrawTexture(new Rect(BlockWidth*2, 0, BlockWidth, BlockHeight), white, ScaleMode.StretchToFill, true, 10.0F);
			}
		}
		
	}

	private void StartChallenge(){
		pController.cPuzle = Puzl.contrast1;
		pController.infoPuzzle = true;
		pController.infoBusy = true;
		if(pController.textInfo.Count < information.Count){
			for(int i = 0; i < information.Count; i++){
				pController.textInfo.Add(information[i]);
			}
		}
		if(pController.imageInfo.Count < Images.Count){
			for(int i = 0; i < Images.Count; i++){
				pController.imageInfo.Add(Images[i]);
			}
		}
		active = true;
	}

	public void StartPuzzle(){
		startButton.gameObject.SetActive(false);
		foreach(GameObject color in colors){
			color.SetActive(true);
		}
		on = true;
		StartCoroutine(ScoreTimer());
	}

	IEnumerator ScoreTimer(){
		finalScore = 200;
		yield return new WaitForSeconds(60);
		finalScore -= 100;
		yield return new WaitForSeconds(60);
		finalScore -= 50;
	}

	public void StopPuzzle(){
		StopAllCoroutines();
		//finalScore = 200;
		startButton.gameObject.SetActive(true);
		foreach(GameObject color in colors){
			color.SetActive(false);
		}
		on = false;

		if(active){
			for(int i = 0; i < information.Count; i++){
				Debug.Log(i);
				pController.textInfo.RemoveAt(0);
			}
			for(int i = 0; i < Images.Count; i++){
				Debug.Log(i);
				pController.imageInfo.RemoveAt(0);
			}
			active = false;
		}
	}
}
