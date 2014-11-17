using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/LevelSphereCollider")]

public class LevelSphereColliders : MonoBehaviour {
	public float velocityAlteration;
	private LevelValues levelValues;
	private Color currentColor;
	GameObject text;
	
	void Start () {
		levelValues=GameObject.Find("LevelValues").GetComponent<LevelValues>();
		ResetPeg();
	}
	
	void Update () {
		if(levelValues.resetPegs)
			ResetPeg();	
	}
	
	public void ResetPeg(){
		renderer.material.color=Color.grey;
	}
	
	void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Ball"){
			levelValues.PlayPegSound();
			levelValues.resetPegs=false;
			current.gameObject.GetComponent<BallState>().ChangeVelocity(velocityAlteration);
			
			if(!particleSystem.isPlaying){
				currentColor=levelValues.GetRandomColor();
				renderer.material.color=currentColor;
				particleSystem.startColor=new Color(currentColor.r,currentColor.g,currentColor.b,1);
				particleSystem.Play();
			}
			text=(GameObject)Instantiate(Resources.Load("ScoreText",typeof(GameObject)));
			text.transform.position=gameObject.transform.position;
			text.GetComponent<TextMesh>().text="1";
			iTween.MoveBy(text,iTween.Hash("y",2,"easeInOutExpo",1));
			Invoke("DestroyText",2);
		}
	}
	
	void DestroyText(){
		DestroyObject(text);
	}
}
