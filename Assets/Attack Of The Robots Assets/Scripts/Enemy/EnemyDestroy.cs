using UnityEngine;
using System.Collections;

public class EnemyDestroy : MonoBehaviour {

	public float destroyTimer = 10.0f; //ten seconds
	private bool destroyed = false;

	// Update is called once per frame
	void Update () {
		destroyTimer -= Time.deltaTime;
		if (destroyTimer < 0 && !destroyed) {
			destroyed = true;
			GameObject.Destroy(gameObject);
		}
	}
}
