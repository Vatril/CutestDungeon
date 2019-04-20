using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager
{

	public static float GetHorizontal ()
	{
		return
			(GetInput (InputSettingsController.InputNames.righttype, InputSettingsController.InputNames.right, InputSettingsController.InputTypes.key, KeyCode.D, InputSettingsController.InputNames.mousewheel, 0) ? 1 : 0) -
			(GetInput (InputSettingsController.InputNames.lefttype, InputSettingsController.InputNames.left, InputSettingsController.InputTypes.key, KeyCode.A, InputSettingsController.InputNames.mousewheel, 1) ? 1 : 0);
	}

	public static float GetVertical ()
	{
		return
			(GetInput (InputSettingsController.InputNames.forwardtype, InputSettingsController.InputNames.forward, InputSettingsController.InputTypes.key, KeyCode.W, InputSettingsController.InputNames.mousewheel, 1) ? 1 : 0) -
			(GetInput (InputSettingsController.InputNames.backwardstype, InputSettingsController.InputNames.backwards, InputSettingsController.InputTypes.key, KeyCode.S, InputSettingsController.InputNames.mousewheel, 0) ? 1 : 0);
	}

	public static bool GetAttack ()
	{
		return GetInput (InputSettingsController.InputNames.attacktype, InputSettingsController.InputNames.attack, InputSettingsController.InputTypes.mouse, KeyCode.Q, InputSettingsController.InputNames.mousewheel, 0);
	}

	public static bool GetUse ()
	{
		return GetInput (InputSettingsController.InputNames.usetype, InputSettingsController.InputNames.use, InputSettingsController.InputTypes.mouse, KeyCode.E, InputSettingsController.InputNames.mousewheel, 1);
	}

	public static bool GetMap ()
	{
		return GetInput (InputSettingsController.InputNames.maptype, InputSettingsController.InputNames.map, InputSettingsController.InputTypes.mouse, KeyCode.Space, InputSettingsController.InputNames.mousewheel, 2);
	}

	public static bool GetMenu ()
	{
		return GetInput (InputSettingsController.InputNames.menutype, InputSettingsController.InputNames.menu, InputSettingsController.InputTypes.key, KeyCode.Escape, InputSettingsController.InputNames.mousewheel, 0);
	}

	public static bool GetItemUp ()
	{
		return GetInput (InputSettingsController.InputNames.iuptype, InputSettingsController.InputNames.iup, InputSettingsController.InputTypes.axis, KeyCode.LeftControl, InputSettingsController.InputNames.mousewheel, 1);
	}

	public static bool GetItemDown ()
	{
		return GetInput (InputSettingsController.InputNames.idowntype, InputSettingsController.InputNames.idown, InputSettingsController.InputTypes.axis, KeyCode.LeftAlt, InputSettingsController.InputNames.mousewheel, 0);
	}


	private static bool GetInput (string nametype, string name, int defaultType, KeyCode defaultKey, string defaultAxis, int defaultButton)
	{
		switch (PlayerPrefs.GetInt (nametype, defaultType)) {
		case InputSettingsController.InputTypes.key:
			return Input.GetKey (ToKeyCode (PlayerPrefs.GetString (name, defaultKey.ToString ())));
		case InputSettingsController.InputTypes.axis:
			return PlayerPrefs.GetInt (name) == 2 ? 
				Input.GetAxisRaw (PlayerPrefs.GetString (name + nametype, defaultAxis)) > 0 : Input.GetAxisRaw (PlayerPrefs.GetString (name + nametype, defaultAxis)) < 0;
		case InputSettingsController.InputTypes.mouse:
			return Input.GetMouseButton (PlayerPrefs.GetInt (name, defaultButton));
		}
		return false;
	}


	private static KeyCode ToKeyCode (string code)
	{
		code = code.ToLower ();
		foreach (KeyCode k in Enum.GetValues(typeof(KeyCode))) {
			if (k.ToString ().ToLower ().Equals (code)) {
				return k;
			}
		}
		Debug.LogError ("Invalid Input");
		return KeyCode.Escape;
	}


}
