using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public void Next(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}
