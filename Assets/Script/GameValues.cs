using UnityEngine;
using System.Collections;
[AddComponentMenu("Game/GameValues")]
public class GameValues : MonoBehaviour {	
	static int currentLevel=1;
	public int maxLevels=3;
	private int totalScore;
	
	public void AddToTotalScore(int val){
		totalScore+=val;
	}
	
	public int GetTotalScore(){
		return totalScore;
	}
	
	public void ResetScore(){
		totalScore=0;
	}
	
	public void LoadNextLevel(){
		if(currentLevel==0 || currentLevel>=maxLevels)
			currentLevel=1;
		else
			currentLevel++;
		Application.LoadLevel("Level"+currentLevel);
	}
}
