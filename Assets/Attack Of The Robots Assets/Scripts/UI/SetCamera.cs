using UnityEngine;
using System.Collections;

public class SetCamera : MonoBehaviour {
	
	private Camera[]cams = null;
	private Player player = null;
	
	void Start () {
		
		cams = this.GetComponentsInChildren<Camera>();
		
		player = this.GetComponentInParent<Player>();
		
		if(cams == null)
			print ("NULL");	
	}
	
	void Update () {
		foreach(Camera cam in cams){
		if(ScreenConstants.splitSetting == ScreenConstants.split.horizontal){
			switch(Level.players.Count){
			case 1:
				cam.rect = ScreenConstants.oneCam[player.num];

				break;
			case 2:
				print (ScreenConstants.twoHorCam.Count);
				cam.rect = ScreenConstants.twoHorCam[player.num];
				break;
//			case 3:
//				fpsCam.rect = ScreenConstants.threeHorCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			case 4:
//				fpsCam.rect = ScreenConstants.fourCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			default: 
//				print ("OOPS");
//				break;
			}
		}
//		else{
//			switch(Level.players.Count){
//			case 1:
//				fpsCam.rect = ScreenConstants.oneCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			case 2:
//				fpsCam.rect = ScreenConstants.twoVertCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			case 3:
//				fpsCam.rect = ScreenConstants.threeVertCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			case 4:
//				fpsCam.rect = ScreenConstants.fourCam[player.num];
//				weaponCam.rect = ScreenConstants.
//				break;
//			default: 
//				print ("OOPS");
//				break;
//			}
//		}
	}
	}
}
