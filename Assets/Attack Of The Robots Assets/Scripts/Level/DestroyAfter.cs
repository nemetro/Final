using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {
	
	private bool broken = false;
	public float lifetime = 10f;
	private float timer = 0;
	
	void FixedUpdate(){
		timer += Time.deltaTime;
		if(timer >= lifetime)
			Destroy(gameObject);
		
	}
}