using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class respawns : MonoBehaviour {

	public List<GameObject> spawnSpots = new List<GameObject>();
	private Vector3 curSpawn;
	public Player player;
	public int spawnNum = 0;

	void Start(){
		curSpawn = new Vector3(spawnSpots[spawnNum].transform.position.x,
		                       spawnSpots[spawnNum].transform.position.y,
		                       spawnSpots[spawnNum].transform.position.z);
		player.transform.position = curSpawn;
	}

	// Update is called once per frame
	void Update () {
		foreach(GameObject ss in spawnSpots){
			if(player.transform.position.z > ss.transform.position.z)
				curSpawn = ss.transform.position;
		}
		if(Player.health <= 0 || Input.GetKeyUp(KeyCode.R)){
			player.GetComponent<Rigidbody>().velocity = Vector3.zero;
			player.transform.position = curSpawn;
			Player.health = player.MAXHEALTH;
		}
	}
}
