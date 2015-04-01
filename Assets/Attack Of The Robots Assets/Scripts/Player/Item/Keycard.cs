using UnityEngine;
using System.Collections;

public class Keycard : MonoBehaviour {
	

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			//print ("ok");
			Destroy (gameObject);
		}
	}
}
