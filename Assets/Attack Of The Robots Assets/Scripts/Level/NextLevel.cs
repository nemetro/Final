using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {

	public void Next()
	{
		int numPlayers = GameObject.Find("PlayerAssign").GetComponent<GetControllers>().numPlayers;
		string prompt = GameObject.Find("UserPrompt").GetComponent<Text>().text;
		if (numPlayers < 1 || prompt != "")
		{
			GameObject.Find("UserPrompt").GetComponent<Text>().text = "Please select a number of players and assign their controllers.";
		}
		else
		{
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
