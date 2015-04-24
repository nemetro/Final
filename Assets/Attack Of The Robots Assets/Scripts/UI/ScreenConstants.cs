using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenConstants : MonoBehaviour {
//CAMERA	
	private List<Rect> oneCam = new List<Rect>();
	private List<Rect> twoVertCam = new List<Rect>();
	private List<Rect> twoHorCam = new List<Rect>();
	private List<Rect> threeVertCam = new List<Rect>();
	private List<Rect> threeHorCam = new List<Rect>();
	private List<Rect> fourCam = new List<Rect>();
	
//FADER
	private List<Vector4> oneFader = new List<Vector4>();
	private List<Vector4> twoVertFader = new List<Vector4>();
	private List<Vector4> twoHorFader = new List<Vector4>();
	
//CROSSHAIRS	
	public Texture crosshair = null;
	private List<Rect> oneCross = new List<Rect>();
	private List<Rect> twoVertCross = new List<Rect>();
	private List<Rect> twoHorCross = new List<Rect>();
	
//HEALTH
	private List<Vector4> oneHealth = new List<Vector4>();
	private List<Vector4> twoHorHealth = new List<Vector4>();
	
//AMMO
	private List<Vector4> oneAmmo = new List<Vector4>();
	private List<Vector4> twoHorAmmo = new List<Vector4>();
	
	
//STATIC
	public static split splitSetting = split.horizontal;
	public static List<Rect> currentCams = null;
	public static List<Vector4> currentFaders = null;
	public static List<Rect> currentCross = null;
	public static List<Vector4> currentHealths = null;
	public static List<Vector4> currentAmmos = null;

	private int set = 0;
	
	public enum split {horizontal, vertical};
	 
	void Start(){
	//CAMERA
		oneCam.Add(new Rect(0,0,1f,1f));
		
		twoVertCam.Add(new Rect(-0.5f, 0f, 1f, 1f));
		twoVertCam.Add(new Rect(0.5f, 0f, 1f, 1f));

		twoHorCam.Add(new Rect(0f, 0.5f, 1f, 1f));
		twoHorCam.Add(new Rect(0f, -0.5f, 1f, 1f));
		
		threeVertCam.Add(new Rect(-0.5f, 0f, 1f, 1f));
		threeVertCam.Add(new Rect(0.5f, 0.5f, 1f, 1f));
		threeVertCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));

		threeHorCam.Add(new Rect(0f, 0.5f, 1f, 1f));
		threeHorCam.Add(new Rect(-0.5f, -0.5f, 1f, 1f));
		threeHorCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));
		
		fourCam.Add(new Rect(-0.5f, 0.5f, 1f, 1f));
		fourCam.Add(new Rect(0.5f, 0.5f, 1f, 1f));
		fourCam.Add(new Rect(0f, 0f, 0.5f, 0.5f));
		fourCam.Add(new Rect(0.5f, -0.5f, 1f, 1f));
		
	//FADER
		oneFader.Add(new Vector4(0f, 0f, 1f, 1f));
		
		twoVertFader.Add(new Vector4(0f, 0f, 0.5f, 1f));
		twoVertFader.Add(new Vector4(0.5f, 0f, 1f, 1f));
		
		twoHorFader.Add(new Vector4(0f, 0.5f, 1f, 1f));
		twoHorFader.Add(new Vector4(0f, 0f, 1f, 0.5f));
		//TODO
		
	//CROSSHAIRS
		oneCross.Add(new Rect((Screen.width * 0.5f) - (crosshair.width * 0.5f),
		                         (Screen.height * 0.5f) - (crosshair.height * 0.5f), 
		                         crosshair.width, crosshair.height));
		                         
		twoHorCross.Add(new Rect((Screen.width * 0.5f) - (crosshair.width * 0.5f),
		                             (Screen.height * 0.75f) - (crosshair.height * 0.5f), 
		                             crosshair.width, crosshair.height));
		twoHorCross.Add(new Rect((Screen.width * 0.5f) - (crosshair.width * 0.5f),
		                         (Screen.height * 0.25f) - (crosshair.height * 0.5f), 
		                         crosshair.width, crosshair.height));
		
		twoVertCross.Add(new Rect((Screen.width * 0.25f) - (crosshair.width * 0.5f),
		                          (Screen.height * 0.5f) - (crosshair.height * 0.5f), 
		                          crosshair.width, crosshair.height));
		twoVertCross.Add(new Rect((Screen.width * 0.75f) - (crosshair.width * 0.5f),
		                          (Screen.height * 0.5f) - (crosshair.height * 0.5f), 
		                          crosshair.width, crosshair.height));
		//TODO
		
	//HEALTH
		oneHealth.Add(new Vector4(0f, 0.05f, 0.15f, 0.15f));
		
		twoHorHealth.Add(new Vector4(0f, 0.55f, 0.15f, 0.65f));
		twoHorHealth.Add(new Vector4(0f, 0.05f, 0.15f, 0.15f));
		//TODO
		
	//AMMO
		oneAmmo.Add(new Vector4(0f, 0.05f, 0.15f, 0.15f));
		
		twoHorAmmo.Add(new Vector4(0f, 0.55f, 0.15f, 0.65f));
		twoHorAmmo.Add(new Vector4(0f, 0.05f, 0.15f, 0.15f));
		//TODO
	}
	
	void Update()
	{
		if(set == Level.players.Count) return;
		
		set = Level.players.Count;
		switch(set){
		case 1:
			currentCams = oneCam;
			currentFaders = oneFader;
			currentCross = oneCross;
			currentHealths = oneHealth;
			currentAmmos = oneAmmo;
			break;
//		case 2:
//			if(splitSetting == split.horizontal){
//				currentCams = twoHorCam;
//				currentFaders = twoHorFader;
//				currentCross = twoHorCross;
//				currentHealths = twoHorHealth;
//				currentAmmos = twoHorAmmo;
//			}
//			else{
//				currentCams = twoVertCam;
//				currentFaders = twoVertFader;
//				currentCross = twoVertCross;
//			}
//			break;
//		case 3:
//			if(splitSetting == split.horizontal){
//				currentCams = threeHorCam;
//			}
//			else{
//				currentCams = threeVertCam;
//			}
//			break;
//		case 4:
//			currentCams = fourCam;
//			break;
		default:
			currentCams = oneCam;
			currentFaders = oneFader;
			currentCross = oneCross;
			currentHealths = oneHealth;
			currentAmmos = oneAmmo;
			break;
		}
	}
	
}
