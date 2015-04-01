using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintPrompt : MonoBehaviour {

	 public string prompt;
	 public Text onScreen;
	
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			onScreen.text = prompt;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			onScreen.text = "";
		}
	}
}
