﻿using UnityEngine;
using System.Collections;

public class PaintSplatter : MonoBehaviour {

	public float offTimer = 5.0f; //automatically off in 5 seconds
	
	void Update () {
		GetComponent<ParticleSystem>().enableEmission = offTimer > 0.0f;
		offTimer -= Time.deltaTime;

		if (offTimer < 0.0f) {
			this.enabled = false; //once the timer is done turn off this script
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player" || other.tag == "Enemy") {
			if(other.tag == "Enemy") {
				other.transform.root.GetComponent<paintFootprints>().walkingOnPaint = true;
			} else {
				other.gameObject.GetComponent<paintFootprints>().walkingOnPaint = true;
			}
		}
	}
}
