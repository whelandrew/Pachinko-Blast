using UnityEngine;
using System.Collections;
[AddComponentMenu("Player/BallState")]

public class BallState : MonoBehaviour {
	private Vector3 startPosition=new Vector3(0,10,10);
	private Vector3 targetPosition;
	public LevelValues level;
	public GameValues game;
	public Detonator explosion;
	public AudioClip[] explosionSoundList;
	public float velocityAlteration;
	
	private int currentScore=0;
	
	private void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Peg"){
			transform.position=new Vector3(transform.position.x,transform.position.y,-1);
			rigidbody.constraints=RigidbodyConstraints.FreezePositionZ;
			currentScore++;
		}
		
		if(current.gameObject.tag=="Target"){
			currentScore+=100;
			level.LoadTargetEarned();
			level.AddTurns(1);
		}
		
		if(current.gameObject.tag=="Ground")
			Explosion();
		
		if(current.gameObject.tag=="Wall"){
			if(rigidbody.constraints!=RigidbodyConstraints.FreezePositionZ){
				transform.position=new Vector3(transform.position.x,transform.position.y,-1);
				rigidbody.constraints=RigidbodyConstraints.FreezePositionZ;
			}
			ChangeVelocity(velocityAlteration);
		}
	}
	
	public void ChangeVelocity(float velocity){
		rigidbody.AddForce(rigidbody.velocity.normalized*velocity,ForceMode.Impulse);
	}
	
	public void LaunchBall(Vector3 firePosition){
		level.gameActive=false;
		rigidbody.useGravity=true;
		level.AddTurns(-1);
		rigidbody.constraints=RigidbodyConstraints.None;
		transform.position=new Vector3(firePosition.x,firePosition.y,startPosition.z);
		rigidbody.AddForce(Vector3.forward*-1000);
	}
	
	public void TimeOutHandling(){
		if(rigidbody.useGravity){
			Explosion();
		}
	}
	
	public void Explosion(){
		explosion.transform.position=transform.position;
		explosion.Explode();
		audio.PlayOneShot(explosionSoundList[Random.Range(0,explosionSoundList.Length)]);
		DestroyBall();
	}
	
	public void DestroyBall(){
		transform.position=startPosition;
		rigidbody.constraints=RigidbodyConstraints.FreezeAll;
		rigidbody.useGravity=false;
		game.AddToTotalScore(currentScore);
		currentScore=0;
		level.gameActive=true;
		level.resetPegs=true;
		if(level.numberOfTurns>0)
			level.GetRandomTargetPosition();		
	}
}