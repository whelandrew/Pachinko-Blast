using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/LevelGUI")]
[ExecuteInEditMode]
public class LevelGUI : MonoBehaviour {
	private BallState ball;
	public LevelValues level;
	public GameValues gameValues;
	public GUISkin scoreBoard;
	public AudioSource[] audioSources;
	
	int i=0;
	
	private Rect newGameButton=new Rect(5,545,60,50);	
	private Rect scoreBoardBox=new Rect(5,250,180,85);
	private Rect remainingShotsRect=new Rect(7,275,175,25);
	private Rect totalScoreRect=new Rect(7,304,175,25);
	private Rect nextLevelButton=new Rect(70,545,100,50);
	private Rect soundOffButton=new Rect(175,545,50,50);
	
	void Awake(){
		ball=level.ball;
	}
	
	void OnGUI(){
		if(GUI.Button(newGameButton,"START")){
			ball.DestroyBall();
			level.gameActive=true;
			level.resetPegs=true;
			level.ResetTurns();
			level.AddTurns(10);
			level.GetRandomTargetPosition();
			level.ResetTargetsEarned();
			level.FlashShootZone();
			gameValues.ResetScore();
		}
		
		if(GUI.Button(nextLevelButton,"NEXT LEVEL")){
			gameValues.LoadNextLevel();
		}
		
		if(GUI.Button(soundOffButton,"MUTE")){
			for(i=0;i<audioSources.Length;i++){
				if(audioSources[i].mute)
					audioSources[i].mute=false;
				else
					audioSources[i].mute=true;
			}				
		}
		
		GUI.Box(scoreBoardBox,"SCORE",scoreBoard.box);
		GUI.TextField(remainingShotsRect,"Remaining Tries : "+level.numberOfTurns);
		GUI.TextField(totalScoreRect,"Score : "+gameValues.GetTotalScore());
	}
}
