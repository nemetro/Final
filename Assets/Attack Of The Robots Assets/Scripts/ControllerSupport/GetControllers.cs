using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using InControl;

public class GetControllers : MonoBehaviour {

	public InputDevice[] players = new InputDevice[4];
	
	public int numPlayers;
	
	bool assigned = false;
	int cur_player;	
	GameObject prompt;
	InputDevice[] controllersAssigned = new InputDevice[4];

	void Awake()
	{
		DontDestroyOnLoad(this);
		prompt = GameObject.Find("UserPrompt");
	}

	// Update is called once per frame
	void Update () 
	{	
		if (numPlayers != 0 && !assigned & cur_player < numPlayers)
		{
			if (prompt.GetComponent<Text>().text == "")
			{
				prompt.GetComponent<Text>().text = "Press button on controller for player " + cur_player;
			}
			InputDevice controller = getNextController();
			if (controller != null)
			{
				for (int i = 0; i < controllersAssigned.Length; i++)
				{
					if (controllersAssigned[i] == controller)
					{
						prompt.GetComponent<Text>().text = "That device has already been assigned. Please try another.";
						return;
					}
				}
				players[cur_player] = controller;
				controllersAssigned[cur_player] = controller;			
				cur_player++;
				prompt.GetComponent<Text>().text = "";
			}
		}

	}
	
	InputDevice getNextController()
	{
		foreach (InputDevice dev in InputManager.Devices)
		{
			if (dev.AnyButton.WasPressed)
			{
				return dev;
			}
		}
		return null;
	}
	
	public void setNumPlayers(int num)
	{
		if (num > InputManager.Devices.Count)
		{
			prompt.GetComponent<Text>().text = "Not enough devices are attached for number of players!";
			return;
		}
		else
		{
			prompt.GetComponent<Text>().text = "";
			//clear controllersassigned array
			for (int i = 0; i < controllersAssigned.Length; i++)
			{
				controllersAssigned[i] = null;
			}
			numPlayers = num;
			assigned = false;
			cur_player = 0;
		}
	}
}
