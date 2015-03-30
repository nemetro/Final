using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerHealth>().health = 50f;//max health
		}
	}
}
