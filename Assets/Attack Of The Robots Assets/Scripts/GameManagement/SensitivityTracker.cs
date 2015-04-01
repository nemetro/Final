using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SensitivityTracker : MonoBehaviour {

	public float[] playerSens;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	public void setSens() 
	{
		for (int i = 0; i < playerSens.Length; i++)
		{
			playerSens[i] = GameObject.Find("P" + (i + 1) + "Slider").GetComponent<Slider>().value;
		}
	}
}
