using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent (typeof (Animator))]

public class PlayerAnimationMovement : MonoBehaviour
{
	public AudioClip shoutingClip;		// Audio clip of the player shouting.
	public AudioClip stepInPaintClip;
	public float turnSmoothing = 15f;	// A smoothing value for turning the player.
	public float speedDampTime = 0.1f;	// The damping for the speed parameter
	public float moveSidewaysSpeed = 3f;
	public AnimationClip walk;
	public AnimationClip run;
	public GameObject footprint;
	public int maxNumPaintSteps = 30;
	public InputDevice controller;
	public bool walkingOnPaint = false;
	
	private Animator anim;				// Reference to the animator component.
	private AnimatorHashIDs hash;			// Reference to the HashIDs.
	private int paintSteps = 0;
	private float stepTimer = 0.2f;
	private FootprintDirection lastFootprint;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();

		// Set the weight of the shouting layer to 1.
		anim.SetLayerWeight(1, 1f);
		maxNumPaintSteps = 30;
	}
	
	
	void FixedUpdate ()
	{
		// Cache the inputs.
		if (controller == null)
		{
			controller = InputManager.Devices[0];
		}
		
		float h = controller.LeftStickX;//Input.GetAxis("Horizontal");
		float v = controller.LeftStickY;//Input.GetAxis("Vertical");
		bool sneak = Input.GetButton("Sneak");
		
		MovementManagement(h, v, sneak);
		bool moving = h != 0 || v != 0;
		if (walkingOnPaint && moving) {
			if(paintSteps != maxNumPaintSteps){
				GetComponent<AudioSource>().PlayOneShot(stepInPaintClip);
			}
			paintSteps = maxNumPaintSteps;
		}

		if (!walkingOnPaint && paintSteps != 0 && stepTimer < 0f && moving){ //if not walking on paint and we have paint on our feet
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position + transform.up, transform.up * -1, out hitInfo)){
				GameObject ftprint = (GameObject)Instantiate(footprint, hitInfo.point + hitInfo.normal * 0.001f, transform.rotation);
				ftprint.transform.forward = -1*transform.forward;
				if(lastFootprint != null){
					lastFootprint.nextFootprintPos = hitInfo.point;
				}
				lastFootprint = ftprint.GetComponent<FootprintDirection>();
				paintSteps--;
				stepTimer = 0.2f;
			}
		}

		if (paintSteps == 0) { //start a new trail
			lastFootprint = null;
		}

		if (stepTimer > 0f) {
			stepTimer -= Time.fixedDeltaTime;
		}
		walkingOnPaint = false;
	}
	
	
	void Update ()
	{
		// Cache the attention attracting input.
		bool shout = Input.GetButtonDown("Attract");
		
		// Set the animator shouting parameter.
		anim.SetBool(hash.shoutingBool, shout);
		
		AudioManagement(shout);
	}
	
	
	void MovementManagement (float horizontal, float vertical, bool sneaking)
	{
		// Set the sneaking parameter to the sneak input.
		anim.SetBool(hash.sneakingBool, sneaking);

		// If there is some axis input...
		if (horizontal != 0f) {
			transform.position = transform.position + horizontal * moveSidewaysSpeed * Time.deltaTime * transform.right;
		}


		if (vertical != 0f) {// controls running
			if(Input.GetKey(KeyCode.Space)) {
				anim.speed = 1.5f;
			} else {
				anim.speed = 1;
			}

			//set the speed parameter to 5.5f.
			anim.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		} else {
			// Otherwise set the speed parameter to 0.
			anim.SetFloat (hash.speedFloat, 0.0f, speedDampTime, Time.deltaTime);
		}
	}

	
	void AudioManagement (bool shout)
	{
		// If the player is currently in the run state...
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState)
		{
			// ... and if the footsteps are not playing...
			if(!GetComponent<AudioSource>().isPlaying)
				// ... play them.
				GetComponent<AudioSource>().Play();
		}
		else
			// Otherwise stop the footsteps.
			GetComponent<AudioSource>().Stop();
		
		// If the shout input has been pressed...
		if(shout)
			// ... play the shouting clip where we are.
			AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
	}
}
