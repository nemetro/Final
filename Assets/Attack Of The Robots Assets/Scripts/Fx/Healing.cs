using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if(col.gameObject.GetComponent<PlayerHealth>().player.health < col.gameObject.GetComponent<PlayerHealth>().player.MAXHEALTH)
			{
				col.gameObject.GetComponent<PlayerHealth>().health = col.gameObject.GetComponent<PlayerHealth>().player.MAXHEALTH;//max health
				col.gameObject.GetComponent<PlayerHealth>().player.health = col.gameObject.GetComponent<PlayerHealth>().player.MAXHEALTH;
				Destroy(this.gameObject);
			}
		}
	}
}
