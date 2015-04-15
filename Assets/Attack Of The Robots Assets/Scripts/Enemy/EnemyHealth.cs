using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public GameObject splatter;

	private bool dead = false;
	public float health = 1f; //100%
	private Rigidbody impactTarget;
	private Vector3 impact;
	private float impactEndTime = 0;
	// Update is called once per frame
	void Update () {
		if (health < 0 && !dead) {
			//paint splatter effect
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, Vector3.down, out hitInfo)){ //if raycast hits something
				Instantiate(splatter, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			}

			//Ragdoll effect
			RagdollHelper_noah helper=transform.root.GetComponent<RagdollHelper_noah>();//GetComponent<RagdollPartScript_noah>().mainScript;
			helper.ragdolled=true;

			//disable enemy tracking
			//print ("asdf");
			print ("robot disabled");
			transform.root.GetComponent<EnemyAI>().enabled = false;
			transform.root.GetComponent<EnemyAnimation>().enabled = false;
			transform.root.GetComponent<EnemyDetectPlayer>().Stop ();
			transform.root.GetComponent<EnemyShootingRaycast>().Stop();
			transform.root.GetComponent<NavMeshAgent>().enabled = false;

			//the impact will be reapplied for the next 250ms
			//to make the connected objects follow even though the simulated body joints
			//might stretch
			impactEndTime=Time.time+0.25f;
			dead = true;
		}

		//Check if we need to apply an impact
		if (dead && Time.time<impactEndTime)
		{
			impactTarget.AddForce(impact,ForceMode.VelocityChange);

		}
	}

	public void BulletDamage(float damage){
		print ("enemy shot!");
		health -= damage;
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
