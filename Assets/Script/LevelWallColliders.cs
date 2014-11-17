using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/WallColliders")]

public class LevelWallColliders : MonoBehaviour {
	public float velocityAlteration;
	void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Ball"){
			current.gameObject.GetComponent<BallState>().ChangeVelocity(velocityAlteration);
		}
	}
}
