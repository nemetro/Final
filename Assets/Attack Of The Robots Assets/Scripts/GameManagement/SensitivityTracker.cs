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
	
	public void UpdateSensitivites() 
	{
		for (int i = 0; i < playerSens.Length; i++)
		{
			GameObject slider = GameObject.Find("P" + (i + 1) + "Slider");
			if (slider != null)
			{
				playerSens[i] = slider.GetComponent<Slider>().value;
			}
		}
		GameObject assign = GameObject.Find("AssignConts");
		if (assign != null)
		{
			GameObject.Find("AssignConts").GetComponent<AssignControllers>().setSens(this.gameObject);
		}
	}
}
