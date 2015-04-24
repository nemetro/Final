//using UnityEngine;
//using System.Collections;
//
//public class EnemyDetectPlayer: MonoBehaviour{
//	public static Vector3 resetPosition = new Vector3(1000, 1000, 1000);
//
//	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
//	public bool playerInSight;							// Whether or not the player is currently sighted.
//	public GameObject gun;								// reference to enemy's gun
//	public Vector3 personalLastKnownLocation;
//	public bool alwaysPlayerAware = false;
//
//	public Enemy enemy;
//	private SphereCollider col;							// Reference to the sphere collider trigger component.
//	private Animator anim;								// Reference to the Animator.
//	private Animator playerAnim;						// Reference to the player's animator component.	
//	private AudioSource audioSrc;						// Reference to enemy audio source 
//	private AnimatorHashIDs hash;						// Reference to the HashIDs.
//	private GameObject target;							// Reference to target gameobject
//	private GlobalLastPlayerSighting globalLastPlayerSighting;
//
//	public GameObject permanentTarget;
//
//	void Awake () {
//		enemy = transform.parent.GetComponent<Enemy> ();
//		col = transform.GetComponent<SphereCollider>();
//		anim = transform.parent.GetComponent<Animator>();
//		audioSrc = transform.parent.GetComponent<AudioSource> ();
//		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();
//		personalLastKnownLocation = resetPosition;
//		globalLastPlayerSighting = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<GlobalLastPlayerSighting>();
//	}
//
//	void Update () {
//		//if the player is dead then reset the target
//		if (target != null && GetTargetPlayerHeath () <= 0f) { //TODO this seems like it is out of place maybe should be moved
//			target = null;
//		}
//
//		// If the player is alive...
//		if(playerInSight && target != null &&  GetTargetPlayerHeath() > 0f){
//			anim.SetBool (hash.playerInSightBool, playerInSight); // ... set the animator parameter to whether the player is in sight or not.
//		} else {
//			anim.SetBool (hash.playerInSightBool, false); // ... set the animator parameter to false.
//		}
//	}
//
//	void FixedUpdate (){
//		if (target != null && CalculatePathLength(target.transform.position) <= col.radius) {
//			if(Vector3.Distance(transform.position, target.transform.position) < 3f) {
//				enemy.nav.Stop ();
//			}
//			//since the player is within range update the last known position
//			personalLastKnownLocation = target.transform.position;
//
//			// Create a vector from the enemy to the player and store the angle between it and forward.
//			Vector3 direction = target.transform.position - transform.position;
//			float angle = Vector3.Angle(direction, transform.forward);
//			// If the angle between forward and where the player is, is less than half the angle of view...
//			if (angle < fieldOfViewAngle * 0.5f) {
//				RaycastHit hit;
//				
//				// ... and if a raycast towards the player from the gun and the head hits something...
//				LayerMask physicalLayers = LayerMask.GetMask("Environment", "Default", "PlayArea");
//				bool gunCanSeeTarget = Physics.Raycast (gun.transform.position, direction.normalized, out hit, col.radius/2.0f, physicalLayers);
//				bool headCanSeeTarget = Physics.Raycast (transform.position + transform.up, direction.normalized, out hit, col.radius/2.0f, physicalLayers);
//	
//				if (target.gameObject.tag == InGameTags.player && gunCanSeeTarget && headCanSeeTarget) {
//					if (hit.collider.tag == InGameTags.player) { // ... and if the raycast hits the player...
//						playerInSight = true;// ... the player is in sight.
//		
//					}
//				}
//			}
//		}
//	}
//
////TODO probably can do some neat things with linked lists to keep track of players
//	void OnTriggerEnter (Collider other){
//		if (other.gameObject.tag == InGameTags.player && other.gameObject.GetComponent<vp_FPPlayerDamageHandler>().CurrentHealth > 0 && target == null) {// If the player enters the trigger zone
//			target = other.gameObject;
//			personalLastKnownLocation = target.transform.position;
//		}
//	}
//
//	void OnTriggerExit (Collider other){
//		if (other.gameObject.tag == InGameTags.player && other.gameObject == target) {// If the player enters the trigger zone
//			target = null;
//		}
//	}
//	
//	void OnTriggerStay (Collider other){
//		if (other.tag == InGameTags.enemy && personalLastKnownLocation != resetPosition) {
//			EnemyDetectPlayer otherEnemyDetectPlayer = other.transform.root.GetComponentInChildren<EnemyDetectPlayer>();
//			if(otherEnemyDetectPlayer.playerInSight == false || otherEnemyDetectPlayer.personalLastKnownLocation == resetPosition){
//				if(CalculatePathLength(other.transform.position) <= col.radius/1.2f){ //make sure the other robot is in range
//					otherEnemyDetectPlayer.personalLastKnownLocation = personalLastKnownLocation;
//					otherEnemyDetectPlayer.playerInSight = true;
//				}
//			}
//		}
//	}
//
//	float CalculatePathLength (Vector3 targetPosition){
//		// Create a path and set it based on a target position.
//		NavMeshPath path = new NavMeshPath();
//		if (enemy.nav.enabled) {
//			enemy.nav.CalculatePath (targetPosition, path);
//		}
//		
//		// Create an array of points which is the length of the number of corners in the path + 2.
//		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];
//		
//		// The first point is the enemy's position.
//		allWayPoints[0] = transform.position;
//		
//		// The last point is the target position.
//		allWayPoints[allWayPoints.Length - 1] = targetPosition;
//		
//		// The points inbetween are the corners of the path.
//		for(int i = 0; i < path.corners.Length; i++){
//			allWayPoints[i + 1] = path.corners[i];
//		}
//		
//		// Create a float to store the path length that is by default 0.
//		float pathLength = 0;
//		
//		// Increment the path length by an amount equal to the distance between each waypoint and the next.
//		for(int i = 0; i < allWayPoints.Length - 1; i++){
//			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
//		}
//		
//		return pathLength;
//	}
//
//	public GameObject GetTargetedPlayer(){
//		if (permanentTarget != null) {
//			return permanentTarget;
//		}
//
//		return target;
//	}
//
//	float GetTargetPlayerHeath(){
//		return target.GetComponent<vp_FPPlayerDamageHandler> ().CurrentHealth;
//	}
//
//	public bool HasValidTarget(){
//		return personalLastKnownLocation != resetPosition || permanentTarget != null;
//	}
//
//	public bool CanSeeTarget(){
//		return playerInSight;
//	}
//
//
//}

using UnityEngine;
using System.Collections;

public class EnemyDetectPlayer : MonoBehaviour
{
	public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;                      // Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;            // Last place this enemy spotted the player.
	
	
	private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
	private SphereCollider col;                     // Reference to the sphere collider trigger component.
	private Animator anim;                          // Reference to the Animator.
	private GlobalLastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
	private GameObject player;                      // Reference to the player.
//	private Animator playerAnim;                    // Reference to the player's animator component.
	private vp_FPPlayerDamageHandler playerHealth;              // Reference to the player's health script.
	private AnimatorHashIDs hash;                           // Reference to the HashIDs.
	private Vector3 previousSighting;               // Where the player was sighted last frame.
	
	
	void Awake ()
	{
		// Setting up the references.
		nav = transform.parent.GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = transform.parent.GetComponent<Animator>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<GlobalLastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag(InGameTags.player);
//		playerAnim = player.GetComponent<Animator>();
		playerHealth = player.GetComponent<vp_FPPlayerDamageHandler>();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();
		
		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}
	
	
	void Update ()
	{
		// If the last global sighting of the player has changed...
		if(lastPlayerSighting.position != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = lastPlayerSighting.position;
		
		// Set the previous sighting to the be the sighting from this frame.
		previousSighting = lastPlayerSighting.position;
		
		// If the player is alive...
		if(playerHealth.CurrentHealth > 0f)
			// ... set the animator parameter to whether the player is in sight or not.
			anim.SetBool(hash.playerInSightBool, playerInSight);
		else
			// ... set the animator parameter to false.
			anim.SetBool(hash.playerInSightBool, false);
	}
	
	
	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject.tag == InGameTags.player)
		{

			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			print ("player player player");
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f)
			{
				print ("player in field of view");
				RaycastHit hit;

				playerInSight = true;

				// ... and if a raycast towards the player hits something...
//				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
//				{
//					print ("player raycasted like a boss");
//					// ... and if the raycast hits the player...
//					if(hit.collider.gameObject.tag == InGameTags.player)
//					{
//						// ... the player is in sight.
//						playerInSight = true;
//						
//						// Set the last global sighting is the players current position.
//						lastPlayerSighting.position = player.transform.position;
//					} else {
//						print ("this other brotha got hit: " + hit.collider.tag);
//					}
//				}
			}
			
			// Store the name hashes of the current states.
//			int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
//			int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
//			
//			// If the player is running or is attracting attention...
//			if(playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
//			{
				// ... and if the player is within hearing range...
				if(CalculatePathLength(player.transform.position) <= col.radius)
					// ... set the last personal sighting of the player to the player's current position.
					personalLastSighting = player.transform.position;
			}
//		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if(other.gameObject == player)
			// ... the player is not in sight.
			playerInSight = false;
	}
	
	
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}

	public void Stop(){
		//		globalLastPlayerSighting.position = globalLastPlayerSighting.resetPosition;
		this.enabled = false;
	}
}
