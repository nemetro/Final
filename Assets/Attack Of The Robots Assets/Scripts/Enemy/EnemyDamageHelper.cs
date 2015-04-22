using UnityEngine;
using System.Collections;

//do positional damage
public class EnemyDamageHelper : MonoBehaviour {
	public Enemy enemy;
	public float damageModifier = 1.0f;

	public void Damage(float damage){
		enemy.enemyHealth.BulletDamage(damage * damageModifier);
		enemy.MakeEnemyVisible ();
	}
}
