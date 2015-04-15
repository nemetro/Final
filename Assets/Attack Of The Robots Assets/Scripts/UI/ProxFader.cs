using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProxFader : MonoBehaviour {
	
	public float radius = 10f;
	public float scanInterval = 0.1f;
	public RawImage gui;
	
	private float scanTimer = 0;
	private LayerMask enemyMask;
	private float scale;
	
	public Texture PainTexture = null;

	void Start () {
		enemyMask = LayerMask.GetMask("Enemy");
		gui.color = new Color(255f, 255f, 255f, 0f);
		gui.texture = PainTexture;
		
		scale = 0.75f/(radius*radius);
	}
	
	void Update (){
		scanTimer += Time.deltaTime;
		
		if (scanTimer >= scanInterval) {
			
			float alpha = 0f;
			
			Collider[] enemies = Physics.OverlapSphere (this.transform.position, radius, enemyMask);
			foreach (Collider enemy in enemies) {
				if(enemy.transform.root.GetComponent<vp_DamageHandlerRagdoll>().CurrentHealth > 0){
					
					float tempAlpha = scale*(radius*radius - ((enemy.transform.position - this.transform.position).sqrMagnitude));
					if(tempAlpha > alpha)
						alpha = tempAlpha;
				}
			}
			if(enemies.Length > 0){
				
				gui.color = new Color(255f, 255f, 255f, alpha);
				
			}
			else{
				gui.color = new Color(255f, 255f, 255f, 0f);;
			}
			scanTimer = 0;
		}
	}
}
