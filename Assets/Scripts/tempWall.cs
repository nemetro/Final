using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum tempType {unblock, build, push};

public class tempWall : MonoBehaviour {

	public List<GameObject> dependants = new List<GameObject>();
	public bool done = false, built = false;
	public tempType type;
	public GameObject buildTemp;
	public GameObject buildPref;
	public bool pushEnemy = false;

	// Update is called once per frame
	void Update () {

		//end
		if(done && !built){
			switch(type){
			case tempType.unblock:
				Destroy(this.gameObject);
				break;
			case tempType.build:
				Instantiate(buildPref, new Vector3(buildTemp.transform.position.x,
				                                   buildTemp.transform.position.y,
				                                   buildTemp.transform.position.z),
				            Quaternion.Euler(0,0,0));
				built = true;
				break;
			case tempType.push:
				Destroy(this.gameObject);
				break;
			default:
				break;
			}
		}

		//check
		done = true;
		foreach(GameObject go in dependants){

			switch(type){
			default:
				if(go != null){
					done = false;
				}
				break;
			}
		}
	}

	void OnCollisionEnter(Collision other){
		print (other.gameObject.name);

		switch(type){
		case tempType.push:
				done = true;
			if(other.gameObject.tag != "Player")
				Destroy(other.gameObject);
			break;
		default:
			break;
		}
	}
}
