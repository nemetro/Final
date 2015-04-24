using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {
	public int maxNumEnemies = 10;
	public GameObject enemyPrefab;
	public int pointsForKillingEnemy = 100;


	private GameObject[] spawners;
	private int currentNumEnemies = 0;
	private int spawnerIndex = 0;
	private GameObject player;
	// Use this for initialization
	void Start () {
		spawners = GameObject.FindGameObjectsWithTag ("EnemySpawn");
		player = GameObject.FindGameObjectWithTag (InGameTags.player);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentNumEnemies < maxNumEnemies) {
			GameObject enemyClone = (GameObject) Instantiate (enemyPrefab, spawners[spawnerIndex++].transform.position, enemyPrefab.transform.rotation);
//			enemyClone.GetComponent<Enemy>().enemyDetectPlayer.alwaysPlayerAware = true;
//			enemyClone.GetComponent<Enemy>().enemyDetectPlayer.permanentTarget = player;

			currentNumEnemies++;

			if(spawnerIndex == spawners.Length){ //reset the spawners
				spawnerIndex = 0;
			}
        }
	}

	public void EnemyDied(){
		currentNumEnemies--;
		ScoreManager.AddPoints (pointsForKillingEnemy);
	}
}
