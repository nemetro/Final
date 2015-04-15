using UnityEngine;
using System.Collections;

public class paintFootprints : MonoBehaviour {
	public bool walkingOnPaint;
	public bool moving;
	public int paintSteps = 0;
	public GameObject footprint;
	
	private int maxNumPaintSteps = 20;
	private Vector3 currLoc;
	private Vector3 prevLoc;
	private float stepTimer = 0.2f;
	private FootprintDirection lastFootprint;
	private GameObject ftprint;
	// Use this for initialization
	void Start () {
		currLoc = transform.position;
		prevLoc = transform.position;
		walkingOnPaint = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		moving = false;
		prevLoc = currLoc;
		currLoc = transform.position;

		if (prevLoc != currLoc) {
			moving = true;
		}

		if (walkingOnPaint) {
			paintSteps = maxNumPaintSteps;
		}
		
		if (!walkingOnPaint && paintSteps != 0 && stepTimer < 0f && moving){ //if not walking on paint and we have paint on our feet
			RaycastHit hitInfo;
			LayerMask floorMask = LayerMask.GetMask("Environment");
			if(Physics.Raycast(transform.position + transform.up, transform.up * -1, out hitInfo, floorMask)){
				if(this.gameObject.tag == "Player") {
					ftprint = (GameObject)Instantiate(footprint, hitInfo.point + hitInfo.normal * 0.001f, transform.rotation);
				} else if(this.gameObject.tag == "Enemy") {
					Vector3 enemyFeet = new Vector3(hitInfo.point.x, hitInfo.point.y - .8f, hitInfo.point.z);
					ftprint = (GameObject)Instantiate(footprint, enemyFeet + hitInfo.normal * 0.001f, transform.rotation);
				}
				ftprint.transform.forward = -1*transform.forward;
				if(lastFootprint != null){
					lastFootprint.nextFootprintPos = hitInfo.point;
				}
				lastFootprint = ftprint.GetComponent<FootprintDirection>();
				paintSteps--;
				stepTimer = 0.2f;
			}
		}
		
		if (paintSteps == 0) { //start a new trail
			lastFootprint = null;
		}
		
		if (stepTimer > 0f) {
			stepTimer -= Time.fixedDeltaTime;
		}
		walkingOnPaint = false;
	}
}
