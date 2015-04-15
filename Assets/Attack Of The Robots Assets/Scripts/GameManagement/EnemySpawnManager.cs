using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {
	public int maxNumEnemies = 10;
	public GameObject enemyPrefab;


	private GameObject[] spawners;
	private int currentNumEnemies = 0;
	private int spawnerIndex = 0;
	// Use this for initialization
	void Start () {
		spawners = GameObject.FindGameObjectsWithTag ("EnemySpawn");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentNumEnemies < maxNumEnemies) {
			Instantiate (enemyPrefab, spawners[spawnerIndex++].transform.position, enemyPrefab.transform.rotation);
            currentNumEnemies++;

			if(spawnerIndex == spawners.Length){ //reset the spawners
				spawnerIndex = 0;
			}
        }
	}

	public void EnemyDied(){
		currentNumEnemies--;
	}
}
