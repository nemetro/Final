using UnityEngine;
using System.Collections;

public class SetCamera : MonoBehaviour {
	
	private Camera[]cams = null;
	private int playerNum = -1;
	
	void Start () {
		cams = this.GetComponentsInChildren<Camera>();		
	}
	
	void Update () {
		if(playerNum == -1)
			playerNum = this.GetComponentInParent<Player>().num;
		foreach(Camera cam in cams){
			cam.rect = ScreenConstants.currentCams[playerNum];
		}
	}
}
