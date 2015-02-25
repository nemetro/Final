using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	private Text health;
	public Player player;

	// Use this for initialization
	void Start () {
		health = transform.Find("Health").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//life
		string newhealth = "";
		
		for(int i = 0; i<Player.health; i++){
			newhealth += '-';
		}
		health.text = newhealth;

		if(Player.health < player.MAXHEALTH/3)
			health.color = Color.red;
		else health.color = Color.white;

	}
}
