using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public string startLevel;

	void Update(){
		if(Input.anyKey){
<<<<<<< HEAD
			Application.LoadLevel (startLevel);
=======
//			Application.LoadLevel (startLevel);
>>>>>>> master
		}
	}
}
