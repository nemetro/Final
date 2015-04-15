using UnityEngine;
using System.Collections;

public class enemyDetector : MonoBehaviour {

	public float radius = 10f;
	public float scanInterval = 0.1f;
	public AudioClip beeper;
	public float angleThresholdLower = 0;
	public float angleThreshholdHigher = 50;
	public float speed1 = .5f;
	public float speed2 = .35f;
	public float speed3 = .2f;

	private float scanTimer = 0;
	private LayerMask enemyMask;
	// Use this for initialization
	void Start () {
		enemyMask = LayerMask.GetMask("Enemy");
	}

	// Update is called once per frame
	void Update () {
		scanTimer += Time.deltaTime;


		if (scanTimer >= scanInterval) {
			Collider[] enemies = Physics.OverlapSphere (this.transform.position, radius, enemyMask);
			foreach (Collider enemy in enemies) {
				if(enemy.transform.root.GetComponent<vp_DamageHandlerRagdoll>().CurrentHealth > 0) {
					//print((enemy.transform.position - this.transform.position).sqrMagnitude);
					//print (Vector3.DistanceSquared(this.transform.position, enemy.transform.position));

					float distAngle = Vector3.Angle(this.transform.forward, (enemy.transform.position-this.transform.position)); 
					if(distAngle > angleThresholdLower && distAngle < angleThreshholdHigher) {
						if(distAngle < 51 && distAngle >= 34) {
							speed1 -= Time.deltaTime;
							if(speed1 <= 0) {
								AudioSource.PlayClipAtPoint(beeper, transform.position, 1f);
								speed1 = 0.5f;
							}
						} else if(distAngle < 34 && distAngle >= 17) {
							speed2 -= Time.deltaTime;
							if(speed2 <= 0) {
								AudioSource.PlayClipAtPoint(beeper, transform.position, 1f);
								speed2 = 0.35f;
							}
						} else if(distAngle < 17 && distAngle >= 0) {
							speed3 -= Time.deltaTime;
							if(speed3 <= 0) {
								AudioSource.PlayClipAtPoint(beeper, transform.position, 1f);
								speed3 = 0.2f;
							}
						}
					}
				}
				//calculate closest enemy with sqmagnitude
				//calculate angle between transform.front(transform) && enemy Vector3.angle(position1, position2);
				//beep louder if angle less than 60 or greater than 300
			}
			scanTimer = 0;
		}
	}
}
