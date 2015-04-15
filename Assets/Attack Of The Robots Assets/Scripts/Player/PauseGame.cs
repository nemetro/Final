using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class PauseGame : MonoBehaviour {
	
	public Canvas pauseMenu;
	
	public GameObject[] players;
	GameObject sens;
	InputDevice controller;
	bool paused = false;
	
	void Start()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		sens = GameObject.Find("SensitivityTracker");
	}
	
	public void pause () 
	{
		controller = InputManager.ActiveDevice;	
		pauseMenu.gameObject.SetActive(true);
	}
	
	public void unpause () 
	{
		controller = InputManager.ActiveDevice;	
		sens.GetComponent<SensitivityTracker>().UpdateSensitivites();
		pauseMenu.gameObject.SetActive(false);
	}
}
