using UnityEngine;
using System.Collections;

public class PointBallSpawnManager : MonoBehaviour {
	public GameObject PointBallPrefab;
	public int maxPointBallCount = 3;
	
	private int pointBallCount = 0;
	private ArrayList pointBallPositions = new ArrayList();
	// Use this for initialization
	void Start () {
		GameObject[] pointBallGameObjects = GameObject.FindGameObjectsWithTag ("PointBall");

		foreach (GameObject pointBall in pointBallGameObjects) {
			pointBallPositions.Add(pointBall.transform.position);
			GameObject.Destroy(pointBall);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(pointBallCount != maxPointBallCount){
			int index = Random.Range(0, pointBallPositions.Count);
			Instantiate(PointBallPrefab, (Vector3)pointBallPositions[index], Quaternion.identity);
			pointBallCount++;
		}
	}

	public void BallCollected(){
		pointBallCount--;
	}
}
