using UnityEngine;
using System.Collections;

public class PointBall : MonoBehaviour {
	PointBallSpawnManager pointBallSpawnManager;
	public AudioClip pickupSound;
	public float restoreHPPercent = 0.25f;

	void Start(){
		pointBallSpawnManager = GameObject.FindGameObjectWithTag (InGameTags.gameController).GetComponent<PointBallSpawnManager> ();
	}


	void OnTriggerEnter(Collider other){
		if (other.tag == InGameTags.player) {
			//give the player some health
			vp_FPPlayerDamageHandler playerHp = other.transform.root.GetComponent<vp_FPPlayerDamageHandler>();
			playerHp.CurrentHealth += playerHp.MaxHealth*restoreHPPercent;

			//if health was too much then just set the HP to the max
			if(playerHp.CurrentHealth > playerHp.MaxHealth){
				playerHp.CurrentHealth = playerHp.MaxHealth;
			}

			//add points for picking up the ball
			ScoreManager.AddPoints(1000);

			//play sound to tell player they got the ball
			AudioSource.PlayClipAtPoint(pickupSound, transform.position);

			//let the manager know the ball was picked up 
			pointBallSpawnManager.BallCollected();

			//destroy the ball
			GameObject.Destroy(this.gameObject);
		}
	}
}
