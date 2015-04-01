using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public List<GameObject> spawnSpots = new List<GameObject>();
	public static List<Player> players = new List<Player>();
	private float timer1, timer2, timer3, timer4;
	public float resetAfterDeathTime = 5f;
	private List<Vector3> curSpawns = new List<Vector3>();
	private bool onstart = true;
	public int spawnNum = 0;

	private GlobalLastPlayerSighting lastPlayerSighting;	// Reference to last global sighting of the player.

	void Start(){
		print ("Spawn " + spawnSpots[0]);
		foreach(GameObject ss in spawnSpots){
			Vector3 temp = new Vector3(ss.transform.position.x,
										ss.transform.position.y,
										ss.transform.position.z);
			curSpawns.Add(temp);
		}
//		foreach(Player player in players){
//			player.transform.position = curSpawn;
//		}
		lastPlayerSighting = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<GlobalLastPlayerSighting>();
	}

	// Update is called once per frame
	void Update () {
		if(onstart){
			onstart = false;
			int i = 0;
			foreach(Player player in players){
//				print ("SET PLAYER " + i);
				player.num = i;
				i++;
				player.health = 0;
			}
		}

		foreach(Player player in players){
			if(player.health <= 0 /*|| Input.GetKeyUp(KeyCode.R)*/)
			{
//				print ("move to " + curSpawns[player.num]);
				player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				player.transform.position = curSpawns[player.num];
				player.health = player.MAXHEALTH;
				//Reset the alarm status
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
			}
//			foreach(GameObject ss in spawnSpots){
//				if(player.transform.position.z > ss.transform.position.z)
//				{
//					curSpawn = ss.transform.position;
//				}
//			}
		}
	}
}
