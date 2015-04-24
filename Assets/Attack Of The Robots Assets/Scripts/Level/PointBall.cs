using UnityEngine;
using System.Collections;

public class PointBall : MonoBehaviour {
	PointBallSpawnManager pointBallSpawnManager;
	AudioClip pickupSound;

	void Start(){
		pointBallSpawnManager = GameObject.FindGameObjectWithTag (InGameTags.gameController).GetComponent<PointBallSpawnManager> ();
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == InGameTags.player) {
			ScoreManager.AddPoints(1000);
			AudioSource.PlayClipAtPoint(pickupSound, transform.position);
			pointBallSpawnManager.BallCollected();
			GameObject.Destroy(this.gameObject);
		}
	}
}
