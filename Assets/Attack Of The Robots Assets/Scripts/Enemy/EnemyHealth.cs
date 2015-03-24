using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public GameObject ragdoll;
	public GameObject splatter;

	private int health = 100;

	// Update is called once per frame
	void Update () {
		if (health < 0) {
			//paint splatter effect
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, Vector3.down, out hitInfo)){ //if raycast hits something
				Instantiate(splatter, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			}

			//Ragdoll effect
			Instantiate(ragdoll, transform.position, transform.rotation);
			GameObject.Destroy(gameObject);
			this.enabled = false;
		}
	}

	public void ApplyDamage(int damage){
		health -= damage;
	}
}
