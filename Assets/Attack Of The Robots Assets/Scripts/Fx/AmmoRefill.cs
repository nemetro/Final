using UnityEngine;
using System.Collections;

public class AmmoRefill : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if(col.gameObject.GetComponent<WeaponMechanics>().numBullets < col.gameObject.GetComponent<WeaponMechanics>().maxBullets
			   || col.gameObject.GetComponent<WeaponMechanics>().numGnades < col.gameObject.GetComponent<WeaponMechanics>().maxGnades)
			{
				col.gameObject.GetComponent<WeaponMechanics>().numBullets = col.gameObject.GetComponent<WeaponMechanics>().maxBullets;
				col.gameObject.GetComponent<WeaponMechanics>().numGnades = col.gameObject.GetComponent<WeaponMechanics>().maxGnades;
				Destroy(this.gameObject);
			}
		}
	}
}
