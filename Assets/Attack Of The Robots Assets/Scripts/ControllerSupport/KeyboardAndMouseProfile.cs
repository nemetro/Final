using System;
using System.Collections;
using UnityEngine;
using InControl;

//Taken and modified from InControl Asset
public class KeyboardAndMouseProfile : UnityInputDeviceProfile
{
	public KeyboardAndMouseProfile()
	{
		Name = "Keyboard/Mouse";
		Meta = "A keyboard and mouse combination profile appropriate for FPS.";
		
		// This profile only works on desktops.
		SupportedPlatforms = new[]
		{
			"Windows",
			"Mac",
			"Linux"
		};
		
		Sensitivity = 1.0f;
		LowerDeadZone = 0.0f;
		UpperDeadZone = 1.0f;
		
		ButtonMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "Fire - Mouse",
				Target = InputControlType.RightTrigger,
				Source = MouseButton0
			},
			new InputControlMapping
			{
				Handle = "Switch Weapons",
				Target = InputControlType.Action4,
				Source = KeyCodeButton( KeyCode.Q )
			},
			new InputControlMapping
			{
				Handle = "Reload",
				Target = InputControlType.Action3,
				Source = KeyCodeButton( KeyCode.R )
			},
			new InputControlMapping
			{
				Handle = "Sprint",
				Target = InputControlType.RightBumper,
				Source = KeyCodeButton( KeyCode.LeftShift )
			},
			new InputControlMapping
			{
				Handle = "Grenades",
				Target = InputControlType.LeftTrigger,
				Source = MouseButton1
			},
			new InputControlMapping
			{
				Handle = "Jump",
				Target = InputControlType.Action1,
				Source = KeyCodeButton( KeyCode.Space )
			},
			new InputControlMapping
			{
				Handle = "Pause",
				Target = InputControlType.Menu,
				Source = KeyCodeButton( KeyCode.P )
			},
			new InputControlMapping
			{
				Handle = "Crouch",
				Target = InputControlType.LeftStickButton,
				Source = KeyCodeButton( KeyCode.LeftControl )
			},
			new InputControlMapping
			{
				Handle = "Interact",
				Target = InputControlType.Action2,
				Source = KeyCodeButton( KeyCode.F )
			}
		};
		
		AnalogMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "Move X",
				Target = InputControlType.LeftStickX,
				// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
				Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
			},
			new InputControlMapping
			{
				Handle = "Move Y",
				Target = InputControlType.LeftStickY,
				// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
				Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
			},
			new InputControlMapping {
				Handle = "Move X Alternate",
				Target = InputControlType.LeftStickX,
				Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
			},
			new InputControlMapping {
				Handle = "Move Y Alternate",
				Target = InputControlType.LeftStickY,
				Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
			},
			new InputControlMapping
			{
				Handle = "Look X",
				Target = InputControlType.RightStickX,
				Source = MouseXAxis,
				Raw    = true,
				Scale  = 1f
			},
			new InputControlMapping
			{
				Handle = "Look Y",
				Target = InputControlType.RightStickY,
				Source = MouseYAxis,
				Raw    = true,
				Scale  = 1f
			}
		};
	}
}

