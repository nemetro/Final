using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	private static ScoreManager instance = null;

	public static int currentScore = 0;
	private static ArrayList scores;

	public static ScoreManager Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		if (scores == null) {
			scores = new ArrayList();
		}
		scores.Add (currentScore); //store score for later
		currentScore = 0;
	}

	public static void AddPoints(int points){
		currentScore += points;
	}
}
