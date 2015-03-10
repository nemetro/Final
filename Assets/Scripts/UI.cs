using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {

	public enum split {horizontal, vertical};
	public split splitSetting = split.horizontal;

	private bool init = false;
	
	private List<Text> healthTxts = new List<Text>();
	private List<RawImage> crossImgs = new List<RawImage>();
	
	private List<Rect> cameraRects = new List<Rect>();
	
	private List<Vector2> crossMins = new List<Vector2>();
	private List<Vector2> crossMaxs = new List<Vector2>();
	private List<Vector3> crossScale = new List<Vector3>();
	
	private List<Vector2> healthMins = new List<Vector2>();
	private List<Vector2> healthMaxs = new List<Vector2>();
	

	void Start () {
		init = false;
		for(int i=0; i<4; i++){
			Text tempH = transform.Find("Health"+i.ToString()).gameObject.GetComponent<Text>();
			print (tempH.name);
			if(tempH != null)
				healthTxts.Add(tempH);

			RawImage tempC = transform.Find("Crosshair"+i.ToString()).gameObject.GetComponent<RawImage>();
			if(tempC != null)
				crossImgs.Add(tempC);
		}
	}
	

	void Update () {
		int p=0;
		foreach(Player player in Level.players){
			
			string newhealth = "";//"Player " + (p+1).ToString() + ": ";
						
			for(int i = 0; i<player.health; i++){
				newhealth += '-';
			}
			
			healthTxts[p].text = newhealth;

			if(player.health < player.MAXHEALTH/3)
				healthTxts[p].color = Color.red;
			else healthTxts[p].color = Color.white;
			
			p++;
		}
		
		if(p == 4 || init)
			return;
		
		//vars set for 1 player	
		Rect tempCam = new Rect(0f, 0f, 1f, 1f);
		Vector2 tempXMin = new Vector2(0f,0f);
		Vector2 tempXMax = new Vector2(1f,1f);
		Vector3 tempXScale = new Vector3(0.09f, 0.16f, 1f);
		Vector2 tempHMin = new Vector2(0.25f,0.9f);
		Vector2 tempHMax = new Vector2(0.75f,1f);
		
		switch(p){
		case 1:
			print("1 Player");
			//cam
			cameraRects.Add(tempCam);
			//cross
			crossMins.Add(tempXMin);
			crossMaxs.Add(tempXMax);
			crossScale.Add(tempXScale);
			//health
			healthMins.Add(tempHMin);
			healthMaxs.Add(tempHMax);
			break;
		case 2:
			print("2 Players");
			if(splitSetting == split.vertical){
				//cam1
				tempCam = new Rect(-0.5f, 0f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam2
				tempCam = new Rect(0.5f, 0f, 1f, 1f);
				cameraRects.Add(tempCam);
				
				//cross1
				tempXMin = new Vector2(0f,0f);
				tempXMax = new Vector2(0.5f,1f);
				tempXScale = new Vector3(0.09f, 0.08f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross2
				tempXMin = new Vector2(0.5f,0f);
				tempXMax = new Vector2(1f,1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				
				//health1
				tempHMin = new Vector2(0f,0.9f);
				tempHMax = new Vector2(0.5f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health2
				tempHMin = new Vector2(0.5f,0.9f);
				tempHMax = new Vector2(1f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
			}
			//horizontal split
			else{
				//cam1
				tempCam = new Rect(0f, 0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam2
				tempCam = new Rect(0f, -0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				
				//cross1
				tempXMin = new Vector2(0f,0.5f);
				tempXMax = new Vector2(1f,1f);
				tempXScale = new Vector3(0.045f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross2
				tempXMin = new Vector2(0f,0f);
				tempXMax = new Vector2(1f,0.5f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				
				//health1
				tempHMin = new Vector2(0.25f,0.9f);
				tempHMax = new Vector2(0.75f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health2
				tempHMin = new Vector2(0.25f,0.4f);
				tempHMax = new Vector2(0.75f,0.5f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
			}
			break;
		case 3:
			print("3 Players");
			if(splitSetting == split.vertical){
				//cam1
				tempCam = new Rect(-0.5f, 0f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam2
				tempCam = new Rect(0.5f, 0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam3
				tempCam = new Rect(0.5f, -0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				
				//cross1
				tempXMin = new Vector2(0f,0f);
				tempXMax = new Vector2(0.5f,1f);
				tempXScale = new Vector3(0.09f, 0.08f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross2
				tempXMin = new Vector2(0.5f,0.5f);
				tempXMax = new Vector2(1f,1f);
				tempXScale = new Vector3(0.09f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross3
				tempXMin = new Vector2(0.5f,0f);
				tempXMax = new Vector2(1f,0.5f);
				tempXScale = new Vector3(0.09f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				
				//health1
				tempHMin = new Vector2(0f,0.9f);
				tempHMax = new Vector2(0.5f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health2
				tempHMin = new Vector2(0.5f,0.9f);
				tempHMax = new Vector2(1f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health3
				tempHMin = new Vector2(0.5f,0.4f);
				tempHMax = new Vector2(1f,0.5f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
			}
			//horizontal split
			else{
				//cam1
				tempCam = new Rect(0f, 0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam2
				tempCam = new Rect(-0.5f, -0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				//cam3
				tempCam = new Rect(0.5f, -0.5f, 1f, 1f);
				cameraRects.Add(tempCam);
				
				//cross1
				tempXMin = new Vector2(0f,0.5f);
				tempXMax = new Vector2(1f,1f);
				tempXScale = new Vector3(0.045f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross2
				tempXMin = new Vector2(0f,0f);
				tempXMax = new Vector2(0.5f,0.5f);
				tempXScale = new Vector3(0.09f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				//cross3
				tempXMin = new Vector2(0.5f,0f);
				tempXMax = new Vector2(1f,0.5f);
				tempXScale = new Vector3(0.09f, 0.16f, 1f);
				crossMins.Add(tempXMin);
				crossMaxs.Add(tempXMax);
				crossScale.Add(tempXScale);
				
				//health1
				tempHMin = new Vector2(0.25f,0.9f);
				tempHMax = new Vector2(0.75f,1f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health2
				tempHMin = new Vector2(0f,0.4f);
				tempHMax = new Vector2(0.5f,0.5f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
				//health3
				tempHMin = new Vector2(0.5f,0.4f);
				tempHMax = new Vector2(1f,0.5f);
				healthMins.Add(tempHMin);
				healthMaxs.Add(tempHMax);
			}
			break;
		default:
			print ("Poop");
			break;
		}
		int n = 0;
		foreach(Player player in Level.players){
			//cam
			player.cam.rect = cameraRects[n];
			
			//cross			
			//position = (Left, PosY, PosZ)
			//sizeDelta = (Right, Height)
			//crossImgs[n].rectTransform.position = new Vector3(0,0,0);
			//crossImgs[n].rectTransform.sizeDelta = new Vector2(0,0);
			crossImgs[n].rectTransform.anchorMin = crossMins[n];
			crossImgs[n].rectTransform.anchorMax = crossMaxs[n];
			crossImgs[n].rectTransform.localScale = crossScale[n];
			
			//health
			healthTxts[n].rectTransform.anchorMin = healthMins[n];
			healthTxts[n].rectTransform.anchorMax = healthMaxs[n];
			
			n++;
		}
		for(int i=p; i<4; i++){
			crossImgs[i].enabled = false;
			healthTxts[i].text = "";
		}
		
		init = true;
	}
}
