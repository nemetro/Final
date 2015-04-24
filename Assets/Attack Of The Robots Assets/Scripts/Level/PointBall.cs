using UnityEngine;
using System.Collections;

public class PointBall : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.tag == InGameTags.player) {
			ScoreManager.AddPoints(1000);
		}
	}
}
