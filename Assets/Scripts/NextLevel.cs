using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	void Update(){
		if(Input.anyKey){
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
