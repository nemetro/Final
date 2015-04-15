using UnityEngine;
using System.Collections;

/**
 * This class is a convienence class. It holds commonly used classes for the enemy guard.
 **/
public class Enemy : MonoBehaviour {
	public EnemyAI enemyAI;
	public EnemyAnimation enemyAnim;
	public EnemyDetectPlayer enemyDetectPlayer;
	public EnemyHealth enemyHealth;
	public EnemyShootingRaycast enemyShootingRaycast;
	public NavMeshAgent nav;
	public RagdollHelper_noah ragHelper;
	public EnemyDestroy enemyDestroy;

	private Renderer enemyMeshRenderer;
	private Renderer gunMeshRenderer;

	void Awake () {
		enemyAI = GetComponentInChildren<EnemyAI> ();
		enemyAnim = GetComponentInChildren<EnemyAnimation> ();
		enemyDetectPlayer = GetComponentInChildren<EnemyDetectPlayer> ();
		enemyHealth = GetComponentInChildren<EnemyHealth> ();
		enemyShootingRaycast = GetComponentInChildren<EnemyShootingRaycast> ();
		ragHelper = GetComponent<RagdollHelper_noah> ();
		nav = GetComponentInChildren<NavMeshAgent> ();
		enemyMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
		gunMeshRenderer = GetComponentInChildren<MeshRenderer> ();
		enemyDestroy = GetComponentInChildren<EnemyDestroy> ();
	}

	void Start(){
		enemyMeshRenderer.enabled = false;
		gunMeshRenderer.enabled = false;
	}

	public void Disable(){
		enemyAI.enabled = false;
		enemyAnim.enabled = false;
		enemyHealth.enabled = false;
		enemyDestroy.enabled = true;
		nav.enabled = false;

		enemyDetectPlayer.Stop ();
		enemyShootingRaycast.Stop ();

		ragHelper.ragdolled=true; //ragdoll enemy
	}

	public void MakeEnemyVisible(){
		enemyMeshRenderer.enabled = true;
		gunMeshRenderer.enabled = true;
	}
}
