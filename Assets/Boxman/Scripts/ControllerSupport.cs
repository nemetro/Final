using UnityEngine;
using System.Collections;

public class ControllerSupport : MonoBehaviour {

	CharacterController character;
	Camera cam;
	Vector3 speed = new Vector3(0, 0, 3);
	
	public enum RotationAxes { X = 0, Y = 1 }
	public RotationAxes axes = RotationAxes.X;
	
	public GameObject redBull, blueBull;

	void Start()
	{
		character = this.GetComponent<CharacterController>();
	}

	void Update () 
	{
		if (tag != "MainCamera")
		{
			//Vector3 move = new Vector3();
			float hor = Input.GetAxis("Horizontal");
			float vert = Input.GetAxis("Vertical");
			float jump = -.5f;
			if (Input.GetButtonDown("A"))
			{
				jump = 15f;
			}
			Vector3 facing = transform.forward.normalized;
			Vector3 up = Vector3.up;
			Vector3 perp = Vector3.Cross(up, facing);
			//Vector3 move = new Vector3(facing.x  hor, facing.y + vert, jump);
			character.Move(facing * vert);
			character.Move(perp * hor);
			character.Move(up * jump);
			Shoot();
		}
		Look();
	}
	
	void Shoot()
	{	
		if (Input.GetAxis("Red") == 1)
		{
			GameObject inst = (GameObject)Instantiate(redBull);
			inst.rigidbody.velocity = speed;
		}
		if (Input.GetAxis("Blue") == 1)
		{
			GameObject inst = (GameObject) Instantiate(blueBull);
			inst.rigidbody.velocity = speed;
		}
	}
	
	void Look()
	{
	/*	Quaternion rotate = new Quaternion(, );
		rotate.y = ;
		rotate.x = ;
		print(rotate);*/
		//cam.transform.Rotate(Input.GetAxis("LookLeftRight"), Input.GetAxis("LookUpDown"), -transform.eulerAngles.z);
		//transform.GetChild(1).localRotation.z = 0;
		
		if (axes == RotationAxes.X)
		{
			transform.Rotate(0, Input.GetAxis("LookUpDown"), 0);
		}
		else
		{
			float rotationY = Input.GetAxis("LookLeftRight");
			//rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.Rotate(rotationY, 0 ,0);
			//transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
		
	}
}
