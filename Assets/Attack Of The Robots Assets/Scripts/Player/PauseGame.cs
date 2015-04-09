using UnityEngine;
using System.Collections;
using InControl;

public class PauseGame : MonoBehaviour {

	InputDevice controller;
	bool paused;

	void Awake()
	{
		paused = false;
	}

	void Update () 
	{
		controller = InputManager.ActiveDevice;
		
		if (controller.MenuWasPressed && !paused)
		{
			Time.timeScale = 0;
			paused = true;
		}
		else if (controller.MenuWasPressed && paused)
		{
			Time.timeScale = 1;
			paused = false;
		}
	}
}
