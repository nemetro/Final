using UnityEngine;
using System.Collections;
using InControl;

public class BacktoMain : MonoBehaviour {

	InputDevice controller;

	// Update is called once per frame
	void Update () 
	{
		controller = InputManager.ActiveDevice;
		if (controller.AnyButton.WasPressed)
		{
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				if (obj != this)
				{
					Destroy	(obj);
				}
			}
			Cursor.visible = true;
			Application.LoadLevel(0);
		}
	}
}
