using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/TargetState")]

public class LevelTargetStates : MonoBehaviour {
	private Vector3 hiddenPos;
	private BallState ballState;
	public LevelValues levelValues;
	public AudioClip[] explosionSoundList;
	private GameObject text;

	void Start () {
		hiddenPos=transform.position;	
		ballState=levelValues.ball;
	}
	
	void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Ball"&&!levelValues.gameActive)
			DestroyTarget();
	}
	
	public void ResetTarget(){
		transform.position=hiddenPos;
	}
	
	void DestroyTarget(){
		text=(GameObject)Instantiate(Resources.Load("ScoreText",typeof(GameObject)));
		text.transform.position=gameObject.transform.position;
		text.GetComponent<TextMesh>().text="100";
		iTween.MoveBy(text,iTween.Hash("y",2,"easeInOutExpo",1));
		Invoke("DestroyText",2);
		
		audio.PlayOneShot(explosionSoundList[Random.Range(0,explosionSoundList.Length)]);
		ResetTarget();
		ballState.Explosion();
		ballState.DestroyBall();
	}
	
	
	void DestroyText(){
		DestroyObject(text);
	}
}
