using UnityEngine;
using System.Collections;

public class ScoreTrack : MonoBehaviour {

	public int numKills, timesKilled, longestLife;
	
	private float cur_life;
	private bool alive;
	
	void Update()
	{
		cur_life += Time.deltaTime;
	}
	
	public void Die()
	{
		if (cur_life > longestLife)
		{
			longestLife = (int)cur_life;
		}
		cur_life = 0;
		timesKilled++;
		print (timesKilled);
		print (longestLife);
	}
	
}
