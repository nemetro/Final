using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]

public class PlayerAnimationMovement : MonoBehaviour
{
	public AudioClip shoutingClip;		// Audio clip of the player shouting.
	public float turnSmoothing = 15f;	// A smoothing value for turning the player.
	public float speedDampTime = 0.1f;	// The damping for the speed parameter
	public float moveSidewaysSpeed = 3f;
	public AnimationClip walk;
	public AnimationClip run;
	
	private Animator anim;				// Reference to the animator component.
	private AnimatorHashIDs hash;			// Reference to the HashIDs.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(InGameTags.gameController).GetComponent<AnimatorHashIDs>();

		// Set the weight of the shouting layer to 1.
		anim.SetLayerWeight(1, 1f);
	}
	
	
	void FixedUpdate ()
	{
		// Cache the inputs.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool sneak = Input.GetButton("Sneak");
		
		MovementManagement(h, v, sneak);
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
			anim.speed = vertical;

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
			if(!audio.isPlaying)
				// ... play them.
				audio.Play();
		}
		else
			// Otherwise stop the footsteps.
			audio.Stop();
		
		// If the shout input has been pressed...
		if(shout)
			// ... play the shouting clip where we are.
			AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
	}
}
