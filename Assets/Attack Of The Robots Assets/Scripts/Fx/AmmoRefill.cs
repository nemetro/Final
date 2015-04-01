using UnityEngine;
using System.Collections;

public class AmmoRefill : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<WeaponMechanics>().numBullets = col.gameObject.GetComponent<WeaponMechanics>().maxBullets;
//			col.gameObject.GetComponent<WeaponMechanics>().numGnades = col.gameObject.GetComponent<WeaponMechanics>().maxGnades;
		}
	}
}
