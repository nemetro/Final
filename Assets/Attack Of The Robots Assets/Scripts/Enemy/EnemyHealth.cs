using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public GameObject splatter;
	public string player_shot;

	private bool dead = false;
	public float health = 1.0f; //100 health points
	private Rigidbody impactTarget;
	private Vector3 impact;
	private float impactEndTime = 0;
	private Enemy enemy;
	private EnemySpawnManager spawnManager;
	
	void Awake(){
		enemy = GetComponent<Enemy> ();
		spawnManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<EnemySpawnManager> ();
	}

	void Start(){
		//add a damage helper to any component that has a collider
		Collider[] colliders = gameObject.GetComponentsInChildren<Collider> ();
		foreach (Collider col in colliders) {
			EnemyDamageHelper dmgHelp = col.gameObject.AddComponent<EnemyDamageHelper>();
			dmgHelp.enemy = enemy;
			string colName = col.gameObject.name;
			if(colName.ToLower().Contains("head")){
				dmgHelp.damageModifier = 2.0f;
			} else if(colName.ToLower().Contains("spine")){
				dmgHelp.damageModifier = 0.75f;
			} else if(colName.ToLower().Contains("hips")){
				dmgHelp.damageModifier = 0.5f;
			} else if(colName.ToLower().Contains("leg")){
				dmgHelp.damageModifier = 0.25f;
			} else if(colName.ToLower().Contains("arm")){
				dmgHelp.damageModifier = 0.25f;
			} else {
				dmgHelp.damageModifier = 0.25f;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//Check if we need to apply an impact
		if (dead && Time.time<impactEndTime)
		{
			impactTarget.AddForce(impact,ForceMode.VelocityChange);
		}
	}

	public void BulletDamage(float damage){
//		print ("enemy shot with damage: " + damage);
		health -= damage;

		if(health <= 0f && !dead){
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, Vector3.down, out hitInfo)){ //if raycast hits something
				Instantiate(splatter, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			}
			
			enemy.Disable();
			
			//the impact will be reapplied for the next 250ms
			//to make the connected objects follow even though the simulated body joints
			//might stretch
			impactEndTime=Time.time+0.25f;
			spawnManager.EnemyDied();
			dead = true;
			
//			GameObject.FindGameObjectWithTag(InGameTags.player).GetComponent<ScoreTrack>().numKills++;//increases kill count of player who did killing
		}
	}

	public void ApplyForce(Rigidbody target, Vector3 force){
		if (target == null || dead) {
			return;
		}
		//set the impact target to whatever the ray hit
		impactTarget = target;
		
		//impact direction
		impact = force;
	}
}
