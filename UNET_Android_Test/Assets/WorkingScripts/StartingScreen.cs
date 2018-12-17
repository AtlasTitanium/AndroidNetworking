using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartingScreen : MonoBehaviour {

	private GUIStyle buttonStyle;
	public string nextScene;
	public Texture2D Background;
	public Texture2D Button;
    public Font currentFont;
	void OnGUI(){
		buttonStyle = new GUIStyle("button");
		buttonStyle.normal.background = Button;
		buttonStyle.hover.background = Button;
		buttonStyle.fontSize = Screen.height/14;
        buttonStyle.font = currentFont;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background, ScaleMode.StretchToFill, true, 10.0F);

		if(GUI.Button(new Rect(Screen.width/16,(Screen.height - Screen.height/8),Screen.width/1.125f,Screen.height/10), "Start Game", buttonStyle)){
			SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
			this.enabled = false;
		}
	}
}
