﻿using UnityEngine;
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

	public Material cloakedMaterial;

	//grab these from transform
	private Material robotBodyMat;
	private Material scifiGunMat;

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

		robotBodyMat = enemyMeshRenderer.material;
		scifiGunMat = gunMeshRenderer.material;
	}

	void Start(){
		enemyMeshRenderer.material = cloakedMaterial;
		gunMeshRenderer.material = cloakedMaterial;
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
		enemyMeshRenderer.material = robotBodyMat;
		gunMeshRenderer.material = scifiGunMat;
	}
}
