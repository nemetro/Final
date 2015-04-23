using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {

	public void Next()
	{
		int numPlayers = GameObject.Find("PlayerAssign").GetComponent<GetControllers>().numPlayers;
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}
