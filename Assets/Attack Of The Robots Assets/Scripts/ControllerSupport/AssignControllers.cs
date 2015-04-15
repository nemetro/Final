using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class AssignControllers : MonoBehaviour {
	
	InputDevice[] controllers;
	public GameObject[] players;
	int numPlayers;
	
	void Awake()
	{
		if (GameObject.Find("PlayerAssign") == null)
		{
			//GameObject.Find("SensitivityTracker").SetActive(false);
			return;
		}
		controllers = GameObject.Find("PlayerAssign").GetComponent<GetControllers>().players;
		numPlayers = GameObject.Find("PlayerAssign").GetComponent<GetControllers>().numPlayers;
		for (int cur_player = 0; cur_player < numPlayers; cur_player++)
		{
			/*players[cur_player].gameObject.SetActive(true);
			players[cur_player].GetComponent<PlayerAnimationMovement>().controller = controllers[cur_player];
			MouseLookController[] look = players[cur_player].GetComponentsInChildren<MouseLookController>();
			foreach (MouseLookController member in look)
			{
				member.controller = controllers[cur_player];
				member.sensitivityX = GameObject.Find("SensitivityTracker").GetComponent<SensitivityTracker>().playerSens[cur_player];
				member.sensitivityY = GameObject.Find("SensitivityTracker").GetComponent<SensitivityTracker>().playerSens[cur_player];
				member.enabled = true;
			}
			players[cur_player].GetComponent<WeaponMechanics>().controller = controllers[cur_player];*/
			//	players[cur_player].GetComponentInChildren<Player_Raycast>().controller = controllers[cur_player];
			vp_FPInput look = players[cur_player].GetComponent<vp_FPInput>();
			look.MouseLookSensitivity.x = GameObject.Find("SensitivityTracker").GetComponent<SensitivityTracker>().playerSens[cur_player];
			look.MouseLookSensitivity.y = GameObject.Find("SensitivityTracker").GetComponent<SensitivityTracker>().playerSens[cur_player];
			players[cur_player].GetComponent<vp_FPInput>().controller = controllers[cur_player];
			players[cur_player].gameObject.SetActive(true);
			print (cur_player + " assigned controller" + controllers[cur_player].Name);
		}
		//GameObject.Find("SensitivityTracker").SetActive(false);
	}
	
	public void setSens(GameObject sens)
	{
		for (int cur_player = 0; cur_player < numPlayers; cur_player++)
		{
			//players[cur_player].gameObject.SetActive(true);
			vp_FPInput look = players[cur_player].GetComponent<vp_FPInput>();
			look.MouseLookSensitivity.x = sens.GetComponent<SensitivityTracker>().playerSens[cur_player];
			look.MouseLookSensitivity.y = sens.GetComponent<SensitivityTracker>().playerSens[cur_player];
		}
	}
}
