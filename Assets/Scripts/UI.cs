using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	private Text health1;
	private Text health2;	
	private Text playerl;
	private Text playerr;
	public Player playerL;
	public Player playerR;

	// Use this for initialization
	void Start () {
		health1 = transform.Find("Health1").gameObject.GetComponent<Text>();
		health2 = transform.Find("Health2").gameObject.GetComponent<Text>();
		playerl = transform.Find("PlayerL").gameObject.GetComponent<Text>();
		playerr = transform.Find("PlayerR").gameObject.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		int p=0;
		foreach(Player player in Level.players){
			if(p == 0)
				playerl.text = "Player " + (player.num+1).ToString();
			else
				playerr.text = "Player " + (player.num+1).ToString();
			p++;
		}

		//life
		string newhealth1 = "Player 1: ";
		
		for(int i = 0; i<playerL.health; i++){
			newhealth1 += '-';
		}
		health1.text = newhealth1;

		if(playerL.health < playerL.MAXHEALTH/3)
			health1.color = Color.red;
		else health1.color = Color.white;

		string newhealth2 = "Player 2: ";
		
		for(int i = 0; i<playerR.health; i++){
			newhealth2 += '-';
		}
		health2.text = newhealth2;
		
		if(playerR.health < playerR.MAXHEALTH/3)
			health2.color = Color.red;
		else health2.color = Color.white;

	}
}
