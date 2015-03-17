using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class GetControllers : MonoBehaviour {

	public InputDevice[] players = new InputDevice[4];
	
	//InputDevice controller0, controller1, controller2, controller3;
	//InputDevice[] controllers;
	public int numPlayers;
	bool assigned = false;
	int cur_player;	

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update () 
	{	
		if (numPlayers != 0 && !assigned & cur_player < numPlayers)
		{
			//print (numPlayers);
			
			for (int i = 0; i < numPlayers; i++)
			{
				/*if (players[i].GetComponent<CharacterControls>().controller == null)
				{
					cur_player = i;
					i = numPlayers;
				}*/
				/*if (players[i] == null)
				{
					cur_player = i;
				}*/
			}
			
			/*else
			{*/
				print ("Press button on controller for player " + cur_player);
				InputDevice controller = getNextController();
				if (controller != null)
				{
					players[cur_player] = controller;				
					cur_player++;
				}
			
/*				if (controller != null)
				{
					players[cur_player].GetComponentInChildren<GunMechanics>().controller = controller;
					players[cur_player].GetComponentInChildren<GunMechanics>().enabled = true;
					players[cur_player].GetComponent<CharacterControls>().controller = controller;
					players[cur_player].GetComponent<CharacterControls>().enabled = true;
					MouseLook[] look = players[cur_player].GetComponentsInChildren<MouseLook>();
					foreach (MouseLook member in look)
					{
						member.controller = controller;
						member.enabled = true;
					}
					players[cur_player].GetComponentInChildren<Player_Raycast>().controller = controller;
					players[cur_player].GetComponentInChildren<Player_Raycast>().enabled = true;				
					print (cur_player + " assigned!");
					if (cur_player == numPlayers)
					{
						assigned = true;
					}
				}*/
			//}
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
				print ("Not enough devices are attached for number of players!");
				return;
		}
		else
		{
			numPlayers = num;
			assigned = false;
			cur_player = 0;
		}
	}
}
