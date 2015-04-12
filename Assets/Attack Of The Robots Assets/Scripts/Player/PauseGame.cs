using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class PauseGame : MonoBehaviour {
	
	public Canvas normUI, pauseMenu;
	
	public GameObject[] players;
	GameObject sens;
	InputDevice controller;
	bool paused = false;
	
	void Start()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		sens = GameObject.Find("SensitivityTracker");
	}
	
	void Update () 
	{
		controller = InputManager.ActiveDevice;
		
		if (controller.MenuWasPressed && !paused)
		{
			Time.timeScale = 0;
			normUI.gameObject.SetActive(false);
			pauseMenu.gameObject.SetActive(true);
			//sens.SetActive(true);
			for (int i = 0; i < players.Length; i++)
			{
				players[i].SetActive(false);
			}
			paused = true;
		}
		else if (controller.MenuWasPressed && paused)
		{
			Time.timeScale = 1;
			normUI.gameObject.SetActive(true);
			sens.GetComponent<SensitivityTracker>().UpdateSensitivites();
			pauseMenu.gameObject.SetActive(false);
			//sens.SetActive(false);
			/*for (int i = 0; i < players.Length; i++)
			{
				players[i].SetActive(true);
			}*/
			paused = false;
		}
	}
}
