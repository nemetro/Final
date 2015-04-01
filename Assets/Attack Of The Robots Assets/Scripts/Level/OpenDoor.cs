using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	public float dist;
	public float speed;
	public bool open = false;
	public GameObject door;
	
	private float startpos;
	private bool move = false;
	
	
	void Start(){
		startpos = door.transform.position.y;
	}
	
	void FixedUpdate(){
		if(move && !open){
			door.transform.Translate(Vector3.up * speed);
			if(door.transform.position.y - startpos >= dist){
				move = false;
				open = true;
			}
		}
		else if(move && open){
			door.transform.Translate(Vector3.down * speed);
			if(door.transform.position.y - startpos <= 0){
				move = false;
				open = false;
			}
		}
	}
	
	void OnTriggerStay (Collider other) 
	{
		if(other.gameObject.tag == "Player"){
			if(other.GetComponent<MouseLookController>().controller.Action1.WasPressed)
			{
				move = true;
			}
		}
	}
}
