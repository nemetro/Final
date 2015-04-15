using UnityEngine;
using System.Collections;

public class enemyDetector : MonoBehaviour {

	public float radius = 10f;
	public float scanInterval = 0.1f;
	public float angleThreshhold = 50;
	public float maxPitch = 1.2f;
	public float minPitch = .8f;
	public float maxVol = 1;
	public float minVol = 0.2f;

	private float scanTimer = 0;
	private LayerMask enemyMask;
	private AudioSource beeper;
	private float pitchModifier;
	private float volumeModifier;
	// Use this for initialization

	LayerMask physicalLayers;
	void Start () {
		enemyMask = LayerMask.GetMask("Enemy");
		beeper = GetComponent<AudioSource> ();
		pitchModifier = maxPitch - minPitch;
		volumeModifier = maxVol - minVol;
		physicalLayers = LayerMask.GetMask("Environment", "Default", "PlayArea", "Enemy");
	}

	// Update is called once per frame
	void FixedUpdate () {
		//beeper.enabled = false;
		scanTimer += Time.deltaTime;


		if (scanTimer >= scanInterval) {
			Collider[] enemies = Physics.OverlapSphere (this.transform.position, radius, enemyMask);

			//print (enemies.Length)

			float angleRatio = -1;
			foreach (Collider enemy in enemies) {
				if(enemy.transform.root.GetComponent<EnemyHealth>().health > 0) {
					float distAngle = Vector3.Angle(this.transform.forward, (enemy.transform.position-this.transform.position)); 

					if(distAngle < angleThreshhold) {
						angleRatio = 1-(distAngle/angleThreshhold);
					}
				}
			}

			print ("what the fuck: " + angleRatio);

			if(angleRatio != -1) {
				float ratioScale = Mathf.Pow (angleRatio, 3);

				RaycastHit hit;
				//if aimed directly at enemy then ratioScale should be 1.0f
				if(Physics.Raycast (transform.position, transform.forward, out hit, 100f) && hit.collider.gameObject.tag == InGameTags.enemy){
					ratioScale = 1.0f;
				}
				print(ratioScale);
				beeper.pitch = minPitch + pitchModifier*ratioScale;
				beeper.volume = minVol + volumeModifier*ratioScale;
			} else {
				beeper.volume = minVol;
				beeper.pitch = minPitch;
			}
			scanTimer = 0;
		}
	}
}
