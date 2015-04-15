using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenConstants : MonoBehaviour {
	
	public static List<Rect> oneCam = new List<Rect>();
	public static List<Rect> twoVertCam = new List<Rect>();
	public static List<Rect> twoHorCam = new List<Rect>();
	public static List<Rect> threeVertCam = new List<Rect>();
	public static List<Rect> threeHorCam = new List<Rect>();
	public static List<Rect> fourCam = new List<Rect>();
	
	public enum split {horizontal, vertical};
	public static split splitSetting = split.horizontal;
	
	 
	void Start(){
		ScreenConstants.oneCam.Add(new Rect(0,0,1f,1f));
//		oneCam.Add(new Rect(-0.5f, 0f, 1f, 1f));
		
		ScreenConstants.twoVertCam.Add(new Rect(-0.5f, 0f, 1f, 1f));
		ScreenConstants.twoVertCam.Add(new Rect(0.5f, 0f, 1f, 1f));

		ScreenConstants.twoHorCam.Add(new Rect(0f, 0.5f, 1f, 1f));
		ScreenConstants.twoHorCam.Add(new Rect(0f, -0.5f, 1f, 1f));
		
		ScreenConstants.threeVertCam.Add(new Rect(-0.5f, 0f, 1f, 1f));
		ScreenConstants.threeVertCam.Add(new Rect(0.5f, 0.5f, 1f, 1f));
		ScreenConstants.threeVertCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));

		ScreenConstants.threeHorCam.Add(new Rect(0f, 0.5f, 1f, 1f));
		ScreenConstants.threeHorCam.Add(new Rect(-0.5f, -0.5f, 1f, 1f));
		ScreenConstants.threeHorCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));
		
		ScreenConstants.fourCam.Add(new Rect(-0.5f, 0.5f, 1f, 1f));
		ScreenConstants.fourCam.Add(new Rect(0.5f, 0.5f, 1f, 1f));
		ScreenConstants.fourCam.Add(new Rect(0f, 0f, 0.5f, 0.5f));
		ScreenConstants.fourCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));
	}
	
}
