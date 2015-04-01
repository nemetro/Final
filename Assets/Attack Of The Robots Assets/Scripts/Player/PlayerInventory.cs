using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
	public bool hasKey;			// Whether or not the player has the key.
	public AudioClip pickupSound;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Keycard")
		{
			hasKey = true;
			AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);
			print ("You got a keycard!");
		}
	}
}
