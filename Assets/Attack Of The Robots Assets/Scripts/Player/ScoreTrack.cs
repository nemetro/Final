using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTrack : MonoBehaviour {

	public int numKills, longestLife;
	
	private float cur_life;
	
	void Update()
	{
		cur_life += Time.deltaTime;
	}
	
	public void Die()
	{
		longestLife = (int)cur_life;
		GameObject.Find("NumKills").GetComponent<Text>().text = "Number of Kills: " + numKills;
		GameObject.Find("TimeSurvived").GetComponent<Text>().text = "Time Survived: " + longestLife; 
	}
	
}
