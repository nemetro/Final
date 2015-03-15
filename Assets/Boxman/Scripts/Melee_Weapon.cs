using UnityEngine;
using System.Collections;

public class Melee_Weapon : MonoBehaviour {
	public GameObject ragdoll;
	public GameObject splatter;

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Enemy") {
			//paint splatter effect
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, Vector3.down, out hitInfo)){ //if raycast hits something
				Instantiate(splatter, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			}
			
			//ragdoll effect
			Vector3 pos = other.transform.position;
			Quaternion rotation = other.transform.rotation;
			GameObject.Destroy(other.gameObject);
			Instantiate(ragdoll, pos, rotation);
		}
	}
}
