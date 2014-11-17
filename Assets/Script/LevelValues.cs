using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/LevelValues")]

public class LevelValues : MonoBehaviour {
	public Color[] colors;
	public Transform[] targetLocations;
	public Transform[] ballSpawnPoints;
	public BallState ball;
	public GameObject target;
	public GameObject shootZone;
	public GameObject[] targetsEarned;
	private GameObject text;
	
	int i=0;
	public AudioSource audioSource;
	public AudioClip[] pegSoundList;

	public Rect[] ballSpawnPointLoc;
	
	public int numberOfTurns;
	public bool gameActive;
	public bool resetPegs;
	public bool levelOver;
	
	void Update () {
		if(numberOfTurns<=0){
			gameActive=false;
			numberOfTurns=0;
		}
	}
	
	public void FlashShootZone(){
		//shootZone.renderer.enabled=true;
		shootZone.GetComponentInChildren<TextMesh>().text="FIRE!";
		iTween.FadeTo(shootZone,1,3);
		Invoke("FadeShootZone",3);
	}
	
	private void FadeShootZone(){
		iTween.FadeTo(shootZone,0,3);
		//shootZone.renderer.enabled=false;
		shootZone.GetComponentInChildren<TextMesh>().text="";
	}
	
	public Color GetRandomColor(){
		return colors[Random.Range(0,colors.Length)];
	}
	
	public void GetRandomTargetPosition(){
		target.transform.position=targetLocations[Random.Range(0,targetLocations.Length)].position;
	}
	
	public void RemoveTarget(){
		target.GetComponent<LevelTargetStates>().ResetTarget();
	}
	
	public void AddTurns(int val){
		numberOfTurns+=val;
	}
	
	public void ResetTurns(){
		numberOfTurns=0;
	}
	
	public void PlayPegSound(){
		audioSource.PlayOneShot(pegSoundList[Random.Range(0,pegSoundList.Length)]);
	}
	
	public void LoadTargetEarned(){
		for(i=0;i<targetsEarned.Length;i++){
			if(!targetsEarned[i].renderer.enabled){
				targetsEarned[i].renderer.enabled=true;
				targetsEarned[i].GetComponentInChildren<ParticleSystem>().Play();
				text=(GameObject)Instantiate(Resources.Load("ScoreText",typeof(GameObject)));
				text.transform.position=targetsEarned[i].transform.position;
				text.GetComponent<TextMesh>().text="FREE TURN!";
				iTween.MoveBy(text,iTween.Hash("y",2,"easeInOutExpo",1));
				Invoke("DestroyText",2);
				return;
			}
		}
	}
	
	void DestroyText(){
		DestroyObject(text);
	}
	
	public void ResetTargetsEarned(){
		for(i=0;i<targetsEarned.Length;i++){
			targetsEarned[i].renderer.enabled=false;
			targetsEarned[i].GetComponentInChildren<ParticleSystem>().Stop();
		}
	}
}
