using UnityEngine;
using System.Collections;

public class FootprintDirection : MonoBehaviour {
	public Vector3 nextFootprintPos = Vector3.zero;
	public bool hasBeenSeen = false;

	private float countdownTillDeath = 10f;
	void Update() {
		countdownTillDeath -= Time.deltaTime;

		if(countdownTillDeath <= 0) {
			Destroy(gameObject);
		}
	}
}
