using UnityEngine;
using System.Collections;
[AddComponentMenu("Misc/mainMenu")]

public class mainMenu : MonoBehaviour {
	Rect level1GameRect=new Rect(430,400,100,50);
	Rect level2GameRect=new Rect(430,455,100,50);
	Rect level3GameRect=new Rect(430,510,100,50);
	
	void OnGUI(){
		if(GUI.Button(level1GameRect,"Level 1"))
			Application.LoadLevel("Level1");
		if(GUI.Button(level2GameRect,"Level 2"))
			Application.LoadLevel("Level2");
		if(GUI.Button(level3GameRect,"Level 3"))
			Application.LoadLevel("Level3");
	}
}
