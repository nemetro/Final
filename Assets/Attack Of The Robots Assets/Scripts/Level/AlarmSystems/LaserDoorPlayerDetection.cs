using UnityEngine;
using System.Collections;

public class LaserDoorPlayerDetection : MonoBehaviour
{
	public AudioClip hurtPlayerSound;

    private GameObject player;								// Reference to the player.
    private GlobalLastPlayerSighting lastPlayerSighting;		// Reference to the global last sighting of the player

    void Awake ()
    {
		// Setting up references.
		player = GameObject.FindGameObjectWithTag(InGameTags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<GlobalLastPlayerSighting>();
    }


    void OnTriggerStay(Collider other)
    {
		// If the beam is on...
        if (GetComponent<Renderer>().enabled) {
			// ... and if the colliding gameobject is the player...
			if (other.gameObject == player){
				// ... set the last global sighting of the player to the colliding object's position.
				player.GetComponent<PlayerHealth>().TakeDamage(120);
//				lastPlayerSighting.position = other.transform.position; //turn on the alarm
				GetComponent<AudioSource>().PlayOneShot (hurtPlayerSound);
			}
		}
    }
}