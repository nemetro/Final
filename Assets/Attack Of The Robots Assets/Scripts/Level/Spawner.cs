using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject roboPref;
	public Transform[] wayPoints;
	public float spawnTime;
	public bool spawn = false;
	public bool on = true;
	public float rad;
	public int totalSpawns;
	
	private int count = 0;
	private float time = 0f;
	private EnemyAI aiscript;
	
	void Start(){
		spawn = false;
		this.GetComponent<SphereCollider>().radius = rad;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(on == false || spawn == false)
			return;
		time += Time.deltaTime;
		if(time >= spawnTime){
			GameObject robo = Instantiate(roboPref, this.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
			aiscript = robo.GetComponent<EnemyAI>();
			aiscript.patrolWayPoints = wayPoints;
			print ("reset");
			time = 0f;
			count++;
			
			if(count >= totalSpawns)
				on = false;
		}
	}
	
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject.tag == "Player" && spawn == false){
			spawn = true;
			time = spawnTime;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.tag == "Player" && spawn == true)
			spawn = false;
	}
}
