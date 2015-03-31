using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	private LineRenderer laser;
	// Use this for initialization
	void Start () {
		laser = GetComponent<LineRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		laser.SetPosition (0, transform.position);

		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, transform.forward, out hitInfo)) {
			laser.SetPosition (1, hitInfo.point);
		} else {
			laser.SetPosition (1, transform.position + (transform.forward * 100));
		}
	}
}
