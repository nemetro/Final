using UnityEngine;
using System.Collections;

public class EnemyDetectPlayer: MonoBehaviour{
	public static Vector3 resetPosition = new Vector3(1000, 1000, 1000);

	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;							// Whether or not the player is currently sighted.
	public GameObject gun;								// reference to enemy's gun
	public Vector3 personalLastKnownLocation;

	private NavMeshAgent nav;							// Reference to the NavMeshAgent component.
	private SphereCollider col;							// Reference to the sphere collider trigger component.
	private Animator anim;								// Reference to the Animator.
	private Animator playerAnim;						// Reference to the player's animator component.	
	private AudioSource audioSrc;							// Reference to enemy audio source 
	private AnimatorHashIDs hash;						// Reference to the HashIDs.
	private GameObject target;							// Reference to target gameobject
//	private int numDetectedPlayers = 0;					// Number of players in range
	private GlobalLastPlayerSighting globalLastPlayerSighting;

	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
		audioSrc = GetComponent<AudioSource> ();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();
		personalLastKnownLocation = resetPosition;
		globalLastPlayerSighting = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<GlobalLastPlayerSighting>();
	}

	void Update () {
		//if the player is dead then reset the target
		if (target != null && GetTargetPlayerHeath () <= 0f) { //TODO this seems like it is out of place maybe should be moved
			target = null;
		}

		// If the player is alive...
		if(playerInSight && target != null &&  GetTargetPlayerHeath() > 0f){
			anim.SetBool (hash.playerInSightBool, playerInSight); // ... set the animator parameter to whether the player is in sight or not.
		} else {
			anim.SetBool (hash.playerInSightBool, false); // ... set the animator parameter to false.
		}
	}

	void FixedUpdate (){
		if (target != null && CalculatePathLength(target.transform.position) <= col.radius) {
			//since the player is within range update the last known position
			personalLastKnownLocation = target.transform.position;

			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = target.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			print("angle: " + angle);
			print ("fieldOfView: " + fieldOfViewAngle * 0.5f);
			// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) {
				print ("accepted");
				RaycastHit hit;
				
				// ... and if a raycast towards the player from the gun and the head hits something...
				LayerMask physicalLayers = LayerMask.GetMask("Environment", "Default", "PlayArea");
				bool gunCanSeeTarget = Physics.Raycast (gun.transform.position, direction.normalized, out hit, col.radius/2.0f, physicalLayers);
				bool headCanSeeTarget = Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius/2.0f, physicalLayers);
	
				if (target.gameObject.tag == InGameTags.player && gunCanSeeTarget && headCanSeeTarget) {
					if (hit.collider.tag == InGameTags.player) { // ... and if the raycast hits the player...
						playerInSight = true;// ... the player is in sight.
						globalLastPlayerSighting.position = personalLastKnownLocation; //TODO make this better (should be a bool and let local enemies activate eachother)
						audioSrc.Play ();
					}
				}
			} else {
				print ("declined");
			}
		}
	}

//TODO probably can do some neat things with linked lists to keep track of players
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == InGameTags.player && other.gameObject.GetComponent<PlayerHealth>().health > 0 && target == null) {// If the player enters the trigger zone
			target = other.gameObject;
			personalLastKnownLocation = target.transform.position;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == InGameTags.player && other.gameObject == target) {// If the player enters the trigger zone
			target = null;
		}
	}
	
	void OnTriggerStay (Collider other){
		if (other.tag == InGameTags.enemy && personalLastKnownLocation != resetPosition) {
			if(other.transform.root.GetComponent<EnemyDetectPlayer>().playerInSight == false 
			   || other.transform.root.GetComponent<EnemyDetectPlayer>().personalLastKnownLocation == resetPosition){

				if(CalculatePathLength(other.transform.position) <= col.radius/1.2f){ //make sure the other robot is in range
					other.transform.root.GetComponent<EnemyDetectPlayer>().personalLastKnownLocation = personalLastKnownLocation;
					other.transform.root.GetComponent<EnemyDetectPlayer>().playerInSight = true;
				}
			}
		}
	}

	float CalculatePathLength (Vector3 targetPosition){
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

	float GetTargetPlayerHeath(){
		return target.GetComponent<PlayerHealth> ().health;
	}

	public bool HasValidTarget(){
		return personalLastKnownLocation != resetPosition;
	}

	public bool CanSeeTarget(){
		return playerInSight;
	}

	public void Stop(){
		globalLastPlayerSighting.position = globalLastPlayerSighting.resetPosition;
		this.enabled = false;
	}
}
