﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public List<GameObject> spawnSpots = new List<GameObject>();
	public static List<Player> players = new List<Player>();
	private Vector3 curSpawn;
	private bool onstart = true;
	public int spawnNum = 0;

	void Start(){
		curSpawn = new Vector3(spawnSpots[spawnNum].transform.position.x,
		                       spawnSpots[spawnNum].transform.position.y,
		                       spawnSpots[spawnNum].transform.position.z);
		foreach(Player player in players)
			player.transform.position = curSpawn;
	}

	// Update is called once per frame
	void Update () {
		if(onstart){
			onstart = false;
			int i = 0;
			foreach(Player player in players){
				print ("SET PLAYER " + i);
				player.num = i;
				i++;
			}
		}

		foreach(Player player in players){
			if(player.health <= 0 || Input.GetKeyUp(KeyCode.R)){
				print ("move to " + curSpawn);
				player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				player.transform.position = curSpawn;
				player.health = player.MAXHEALTH;
			}
			foreach(GameObject ss in spawnSpots){
				if(player.transform.position.z > ss.transform.position.z){
					curSpawn = ss.transform.position;
				}
			}
		}
	}
}
