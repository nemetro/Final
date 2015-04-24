using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SensitivityTracker : MonoBehaviour {
	
	public float[] playerSens;
	
	// Use this for initialization
	void Start () 
	{
		//DontDestroyOnLoad(this);
	}
	
	public void UpdateSensitivites() 
	{
		GameObject slider = GameObject.Find("P1Slider");
		if (slider != null)
		{
			playerSens[0] = slider.GetComponent<Slider>().value;
		}
		GameObject assign = GameObject.Find("AssignConts");
		if (assign != null)
		{
			GameObject.Find("AssignConts").GetComponent<AssignControllers>().setSens(this.gameObject);
		}
	}
}
