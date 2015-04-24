using UnityEngine;
using System.Collections;

public class EnemyShootingRaycast: MonoBehaviour
{
	public float maximumDamage = 10f;					// The maximum potential damage per shot.
	public float minimumDamage = 5f;					// The minimum potential damage per shot.
	public AudioClip shotClip;							// An audio clip to play when a shot happens.
	public float flashIntensity = 3f;					// The intensity of the light when the shot happens.
	public float fadeSpeed = 10f;						// How fast the light will fade after the shot.
	public GameObject gun;
	public float accuracyRadius = 1.0f;	

	private Animator anim;								// Reference to the animator.
	private AnimatorHashIDs hash;						// Reference to the HashIDs script.
	private LineRenderer laserShotLine;					// Reference to the laser shot line renderer.
	private Light laserShotLight;						// Reference to the laser shot light.
//	private SphereCollider col;							// Reference to the sphere collider.
	private Enemy enemy;
	private bool shooting;								// A bool to say whether or not the enemy is currently shooting.
//	private float scaledDamage;							// Amount of damage that is scaled by the distance from the player.
	private vp_Shooter m_Shooter;
	private GameObject player;

	void Awake () {
		// Setting up the references.
		enemy = GetComponent<Enemy>();
		anim = GetComponent<Animator>();
		laserShotLine = GetComponentInChildren<LineRenderer>();
		laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
//		col = GetComponent<SphereCollider>();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();
		// The line renderer and light are off to start.
		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		player = GameObject.FindGameObjectWithTag (InGameTags.player);
		
		// The scaledDamage is the difference between the maximum and the minimum damage.
//		scaledDamage = maximumDamage - minimumDamage;
		m_Shooter = GetComponentInChildren<vp_Shooter> ();
//		stop = false;
	}
	
	
	void Update (){
		// Cache the current value of the shot curve.
		float shot = anim.GetFloat(hash.shotFloat);
		
		// If the shot curve is peaking and the enemy is not currently shooting...
		if (shot > 0.5f && !shooting) {
			// ... shoot
			m_Shooter.TryFire();
		}
		
		// If the shot curve is no longer peaking...
		if(shot < 0.5f)	{
			// ... the enemy is no longer shooting and disable the line renderer.
			shooting = false;
			laserShotLine.enabled = false;
		}
		
		// Fade the light out.
		laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
	}
	
	
	void OnAnimatorIK (int layerIndex)
	{
		// Cache the current value of the AimWeight curve.
		float aimWeight = anim.GetFloat(hash.aimWeightFloat);
		
		// Set the IK position of the right hand to the player's centre.
		if(enemy.enemyDetectPlayer.playerInSight){
			anim.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position + Vector3.up * 1.5f);
		}

		// Set the weight of the IK compared to animation to that of the curve.
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}
	
//	
//	void Shoot ()
//	{
//		// The enemy is shooting.
//		shooting = true;
//
//		RaycastHit hit;
//
//		Vector3 shootDirection = gun.transform.forward;
//
//		if(enemyDetectPlayer.GetTargetedPlayer() != null){
//			shootDirection = enemyDetectPlayer.GetTargetedPlayer ().transform.position - gun.transform.position;
//			shootDirection += Random.insideUnitSphere * Random.Range(0, accuracyRadius);
//		}
//
//		if(Physics.Raycast (gun.transform.position, shootDirection.normalized, out hit, 100f)) {
//			if(hit.collider.tag == InGameTags.player){
//				// The fractional distance from the player, 1 is next to the player, 0 is the player is at the extent of the sphere collider.
//				float fractionalDistance = (col.radius - Vector3.Distance(transform.position, enemyDetectPlayer.GetTargetedPlayer ().transform.position)) / col.radius;
//				
//				// The damage is the scaled damage, scaled by the fractional distance, plus the minimum damage.
//				float damage = scaledDamage * fractionalDistance + minimumDamage;
//				
//				// The player takes damage.
//				hit.transform.gameObject.SendMessage("Damage", damage);//, SendMessageOptions.DontRequireReceiver);
//			}
//		}
//
//		// Display the shot effects.
//		if (hit.collider != null) {
//			ShotEffects (hit.point);
//		} else {
//			ShotEffects (gun.transform.position + gun.transform.forward * 100f);
//		}
//	}
//	
//	
	void ShotEffects (Vector3 point) {
		// Set the initial position of the line renderer to the position of the muzzle.
		laserShotLine.SetPosition(0, laserShotLine.transform.position);
		
		// Set the end position of the player's centre of mass.
		laserShotLine.SetPosition(1, point);
		
		// Turn on the line renderer.
		laserShotLine.enabled = true;
		
		// Make the light flash.
		laserShotLight.intensity = flashIntensity;
		
		// Play the gun shot clip at the position of the muzzle flare.
		AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
	}

	public void Stop(){
		laserShotLight.enabled = false;
		laserShotLine.enabled = false;
		this.enabled = false;
	}
}
