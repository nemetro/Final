using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProxFader : MonoBehaviour {
	
	public float radius = 10f;
	public float scanInterval = 0.1f;
	public RawImage gui;
	
	private int playerNum = -1;
	private float scanTimer = 0;
	private LayerMask enemyMask;
	private float scale;
	private bool set = false;
	
	public Texture PainTexture = null;

	void Start () {
		enemyMask = LayerMask.GetMask("Enemy");
		gui.color = new Color(255f, 255f, 255f, 0f);
		gui.texture = PainTexture;
		
		scale = 0.75f/(radius*radius);
	}
	
	void Update (){
	
		if(playerNum == -1)
			playerNum = this.GetComponent<Player>().num;
		
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
		
		if(set) return;
		
		set = true;
		
		gui.rectTransform.anchorMin = new Vector2(ScreenConstants.currentFaders[playerNum].x, 
		                                         ScreenConstants.currentFaders[playerNum].y);
		gui.rectTransform.anchorMax = new Vector2(ScreenConstants.currentFaders[playerNum].z, 
		                                         ScreenConstants.currentFaders[playerNum].w);
		
	}
}
