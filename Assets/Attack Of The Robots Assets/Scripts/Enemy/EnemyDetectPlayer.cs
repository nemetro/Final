using UnityEngine;
using System.Collections;

public class EnemyDetectPlayer: MonoBehaviour{
	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;							// Whether or not the player is currently sighted.
	public GameObject gun;								// reference to enemy's gun
	public Vector3 personalLastSighting;

	private NavMeshAgent nav;							// Reference to the NavMeshAgent component.
	private SphereCollider col;							// Reference to the sphere collider trigger component.
	private Animator anim;								// Reference to the Animator.
	private Animator playerAnim;						// Reference to the player's animator component.				
	private AnimatorHashIDs hash;						// Reference to the HashIDs.
	private GameObject target;							// Reference to target gameobject
	private int numDetectedPlayers = 0;					// Number of players in range
	
	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();
	}

	void Update () {
		// If the player is alive...
		if(playerInSight && GetTargetPlayerHeath() > 0f){
			anim.SetBool (hash.playerInSightBool, playerInSight); // ... set the animator parameter to whether the player is in sight or not.
		} else {
			anim.SetBool (hash.playerInSightBool, false); // ... set the animator parameter to false.
		}
	}

	void FixedUpdate() {
		playerInSight = false; // by default player is not in sight

		if (target == null || target.tag == InGameTags.footprint) { //if no target or target is a footprint then player cannot be in sight
			return;
		}
		
		// Create a vector from the enemy to the player and store the angle between it and forward.
		Vector3 direction = target.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		
		// If the angle between forward and where the player is, is less than half the angle of view...
		if (angle < fieldOfViewAngle * 0.5f) {
			RaycastHit hit;
			
			// ... and if a raycast towards the player from the gun and the head hits something...
			bool gunCanSeeTarget = Physics.Raycast (gun.transform.position + transform.up, direction.normalized, out hit, col.radius);
			bool headCanSeeTarget = Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius);

			if (target.gameObject.tag == InGameTags.player && gunCanSeeTarget && headCanSeeTarget) {
				if (hit.collider.tag == InGameTags.player) { // ... and if the raycast hits the player...
					playerInSight = true;// ... the player is in sight.
				}
			}
//				else if (other.gameObject.tag == InGameTags.footprint){
//				FootprintDirection footDir = other.gameObject.GetComponent<FootprintDirection>();
//				if(footDir.hasBeenSeen == false && footDir.nextFootprintPos != Vector3.zero){
//					personalLastSighting = footDir.nextFootprintPos;
//				}
//				footDir.hasBeenSeen = true;
//			}
		}
		
		// Store the name hashes of the current states.
		int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
		int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
		

		// If the player is running
//		if(playerLayerZeroStateHash == hash.locomotionState){
		if(CalculatePathLength(target.transform.position) <= col.radius){ // ... and if the player is within hearing range...
			personalLastSighting = target.transform.position; // ... set the last personal sighting of the player to the player's current position.
		}
//		}
	}
	
	
	void OnTriggerStay (Collider other){
		// if already have a target
		if (target != null) {
			return;
		}

		//if no target and a player enters the trigger area
		if (other.gameObject.tag == InGameTags.player) {
			target = other.gameObject;
		} else if (other.gameObject.tag == InGameTags.footprint) {
			//TODO reimplement footprints	
		}
	}

//TODO probably can do some neat things with linked lists to keep track of players
//	void OnTriggerEnter (Collider other){
//		if (other.gameObject.tag == InGameTags.player) {// If the player enters the trigger zone
//			numDetectedPlayers++; //increment number of players in the trigger zone
//			playerInSight = true; //a player is in trigger zone
//		}
//	}
//
//	void OnTriggerExit (Collider other)
//	{
//		if (other.gameObject.tag == InGameTags.player) {// If the player enters the trigger zone
//			numDetectedPlayers--; //increment number of players in the trigger zone
//			playerInSight = numDetectedPlayers > 0; //update players in sight.
//		}
//	}
	
	
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if (nav.enabled) {
			nav.CalculatePath (targetPosition, path);
		}
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++){
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++){
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}

	public GameObject GetTargetedPlayer(){
		return target;
	}

	public float GetTargetPlayerHeath(){
		return target.GetComponent<PlayerHealth> ().health;
	}
}
