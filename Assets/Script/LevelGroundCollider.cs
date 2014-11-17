using UnityEngine;
using System.Collections;
[AddComponentMenu("Level/GroundCollider")]
public class LevelGroundCollider : MonoBehaviour {
	public BallState ballState;

	void OnCollisionEnter(Collision current){
		if(current.gameObject.tag=="Ball"){
			ballState.Explosion();
			ballState.DestroyBall();
		}
	}
}
