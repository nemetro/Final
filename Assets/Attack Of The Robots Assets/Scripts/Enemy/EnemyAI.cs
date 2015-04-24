using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;							// The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;							// The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;						// The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 1f;						// The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;						// An array of transforms for the patrol route.
	public bool patroling = false;							//debugging

	private Enemy enemy;
	private float chaseTimer;								// A timer for the chaseWaitTime.
	public float patrolTimer;								// A timer for the patrolWaitTime.
	public int wayPointIndex;								// A counter for the way point array.

	private GlobalLastPlayerSighting globalTarget;

	void Awake () {
		enemy = GetComponent<Enemy> ();
		globalTarget = GameObject.FindGameObjectWithTag (InGameTags.gameController).GetComponent<GlobalLastPlayerSighting> ();
	}
	
	
	void Update () {
		Chasing ();
	}
	
	void Chasing () {
		patroling = false;

		if (Vector3.SqrMagnitude (globalTarget.position - this.transform.position) > 4) {
			enemy.nav.destination = globalTarget.position;
			enemy.nav.speed = chaseSpeed;
			enemy.nav.Resume ();
		} else {
			enemy.nav.Stop();
		}
	}
}
