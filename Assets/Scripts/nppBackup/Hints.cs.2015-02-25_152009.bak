using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Hints : MonoBehaviour {

	public Text HintText;
	public bool printText;

	// Use this for initialization
	void Start () {
		printText = false;
		//HintText = transform.Find("HintPrinter").gameObject.GetComponent<Text>();
	}

	void Update() {
		if(!printText) {
			HintText.text = "";
		}
	}

	void OnTriggerEnter(Collider other) {
		printText = true;
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.name == "Hint1"){
			HintText.text = "Shoot the blue box with the blue gun! Press Q to swap weapons.";
		}
	}

	void OnTriggerExit(Collider other) {
		HintText.text = "";
		printText = false;
	}
}
