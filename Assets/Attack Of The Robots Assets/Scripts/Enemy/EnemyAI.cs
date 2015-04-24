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
//		if (enemy.enemyDetectPlayer.HasValidTarget() || enemy.enemyDetectPlayer.alwaysPlayerAware) {// If enemy is aware of the player but not in sight
//			Chasing ();
//		} else { //enemy has not found the player
//			Patrolling (); 
//		}
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



		// Create a vector from the enemy to the last sighting of the player.
//		Vector3 sightingDeltaPos = enemy.enemyDetectPlayer.personalLastKnownLocation - transform.position;
//
//	//	if (sightingDeltaPos.sqrMagnitude > 4f) { // If the the last personal sighting of the player is not close...
////			print (enemyDetectPlayer.personalLastKnownLocation);
//			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
//			if(enemy.enemyDetectPlayer.alwaysPlayerAware){
//				enemy.nav.destination = GameObject.FindGameObjectWithTag(InGameTags.player).transform.position;
//			} else {
//				enemy.nav.destination = enemy.enemyDetectPlayer.personalLastKnownLocation;
//			}
//			enemy.nav.Resume();
//	//	} else {
//	//		enemy.nav.Stop();
////		}
//
//		enemy.nav.speed = chaseSpeed;// Set the appropriate speed for the NavMeshAgent.
//
//		if (enemy.nav.remainingDistance < enemy.nav.stoppingDistance) { // If near the last personal sighting...
//			chaseTimer += Time.deltaTime; // ... increment the timer.
//
//			if (chaseTimer >= chaseWaitTime) { // If the timer exceeds the wait time...
//				chaseTimer = 0f;// ... reset last global sighting, the last personal sighting and the timer.
//			}
//		} else {
//			chaseTimer = 0f;// If not near the last sighting personal sighting of the player, reset the timer.
//		}
	}

	
	void Patrolling (){
		patroling = true;
		// Set an appropriate speed for the NavMeshAgent.
		enemy.nav.speed = patrolSpeed;
		enemy.nav.Resume ();
		
		// If near the next waypoint...
		if (enemy.nav.remainingDistance < 1.0f) { //if within a meter
			patrolTimer += Time.deltaTime; // ... increment the timer.
			if (patrolTimer >= patrolWaitTime) { // If the timer exceeds the wait time...
				wayPointIndex++;

				if(wayPointIndex == patrolWayPoints.Length){ //wrap around waypoints when reach end
					wayPointIndex = 0;
				}
				patrolTimer = 0;// Reset the timer.
			}
		} else {
			patrolTimer = 0;// If not near a destination, reset the timer.
		}

		// Set the destination to the patrolWayPoint.
		if (patrolWayPoints.Length > 0) {
			enemy.nav.destination = patrolWayPoints [wayPointIndex].position;
		} else {
			enemy.nav.destination = transform.position;
		}
	}
}
