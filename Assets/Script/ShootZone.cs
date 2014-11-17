using UnityEngine;
using System.Collections;

public class ShootZone : MonoBehaviour {
	Ray ray;
	public GameObject targetLight;
	private BallState ball;
	public LevelValues level;
	
	void Awake(){
		ball=level.ball;
	}
	
	void Update () {
		if(level.gameActive){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			Physics.Raycast(ray, out hit);
			targetLight.transform.position=hit.point;		
		}else
			targetLight.renderer.enabled=false;
	}
	
	void OnMouseDown(){
		if(level.gameActive && targetLight.renderer){
			ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			ball.LaunchBall(ray.GetPoint(10));
		}
	}
	
	void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Crosshair")
			targetLight.renderer.enabled=true;
	}
	
	void OnCollisionExit(Collision current){
		if(current.gameObject.tag=="Crosshair")
			targetLight.renderer.enabled=false;
	}
}
