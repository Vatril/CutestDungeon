using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class InputSettingsController : MonoBehaviour {

	public Button attack;
	public Button use;
	public Button map;
	public Button iup;
	public Button idown;
	public Movment movment;
	public Button menu;

	private bool listening;
	private Button listenFor;

	[System.Serializable]
	public struct Movment
	{
		public Button forward;
		public Button backwards;
		public Button left;
		public Button right;
	}

	public class InputNames{
		public const string attack = "Attack";
		public const string use = "Use";
		public const string map = "Map";
			   
		public const string iup = "IUp";
		public const string idown = "IDown";
			   
		public const string forward = "Forward";
		public const string backwards = "Backwards";
		public const string left = "Left";
		public const string right = "Right";
			   
		public const string menu = "Menu";
			 


		public const string attacktype = "AttackType";
		public const string usetype = "UseType";
		public const string maptype = "MapType";
			   
		public const string iuptype = "IUpType";
		public const string idowntype = "IDownType";
			   
		public const string forwardtype = "ForwardType";
		public const string backwardstype = "BackwardsType";
		public const string lefttype = "LeftType";
		public const string righttype = "RightType";

		public const string menutype = "MenuType";
	
		public const string mousewheel = "MouseWheel";
	}

	public class InputTypes{
		public const int key = 0;
		public const int axis = 1;
		public const int mouse = 2;
	}


	public class Axis
	{
		private string axisname;
		private int direction;

		public string Axisname{get { return axisname;} set {axisname = value;}}
		public int Direction{get { return direction;} set {direction = value;}}

		public Axis(string name, int dir){
			axisname = name;
			direction = dir;
		}
	}


	// Use this for initialization
	void Start () {
		SetButtonText ();
	}
	
	// Update is called once per frame
	void Update () {
		if (listening) {
			foreach (KeyCode k in Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKey (k)) {
					SetNewInput (code: k);
				}
			}
		}



		if (Input.GetAxisRaw (InputNames.mousewheel) != 0) {
			SetNewInput (axis: new Axis(InputNames.mousewheel, Input.GetAxisRaw (InputNames.mousewheel)<0?1:2));
		}

		for(int i = 0; i <= 2; i++){
			if (Input.GetMouseButton (i)) {
				SetNewInput (btn: i);
			}
		}
	}

	private void SetNewInput(int btn = -1, Axis axis = null, KeyCode code = KeyCode.F15){
		if (!listening)
			return;
		listening = false;
		if (listenFor.GetHashCode () == attack.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.attacktype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.attack, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.attacktype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.attack + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.attack, axis.Direction);

			}else {
				PlayerPrefs.SetInt (InputNames.attacktype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.attack, code.ToString());
			}
		}else if (listenFor.GetHashCode () == use.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.usetype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.use, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.usetype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.use + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.use, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.usetype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.use, code.ToString());
			}
		}else if (listenFor.GetHashCode () == map.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.maptype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.map, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.maptype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.map + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.map, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.maptype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.map, code.ToString());
			}
		}else if (listenFor.GetHashCode () == movment.forward.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.forwardtype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.forward, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.forwardtype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.forward + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.forward, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.forwardtype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.forward, code.ToString());
			}
		}else if (listenFor.GetHashCode () == movment.backwards.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.backwardstype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.backwards, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.backwardstype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.backwards + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.backwards, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.backwardstype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.backwards, code.ToString());
			}
		}else if (listenFor.GetHashCode () == movment.left.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.lefttype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.left, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.lefttype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.left + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.left, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.lefttype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.left, code.ToString());
			}
		}else if (listenFor.GetHashCode () == movment.right.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.righttype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.right, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.righttype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.right + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.right, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.righttype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.right, code.ToString());
			}
		}else if (listenFor.GetHashCode () == menu.GetHashCode ()) {
			if (btn >= 0) {
				PlayerPrefs.SetInt (InputNames.menutype, InputTypes.mouse);
				PlayerPrefs.SetInt (InputNames.menu, btn);
			} else if (axis != null) {
				PlayerPrefs.SetInt (InputNames.menutype, InputTypes.axis);
				PlayerPrefs.SetString (InputNames.menu + InputTypes.axis, axis.Axisname);
				PlayerPrefs.SetInt (InputNames.menu, axis.Direction);
			}else {
				PlayerPrefs.SetInt (InputNames.menutype, InputTypes.key);
				PlayerPrefs.SetString (InputNames.menu, code.ToString());
			}
		}

		listenFor = null;
		SetButtonText ();
	}
		

	public void Back(){
		SceneManager.LoadScene ("Main Menu");
	}

	public void Reset(){
		
		Init ();

		SetButtonText ();
	}

	public static void Init(){
		PlayerPrefs.SetInt (InputNames.attacktype, InputTypes.mouse);
		PlayerPrefs.SetInt (InputNames.usetype, InputTypes.mouse);
		PlayerPrefs.SetInt (InputNames.maptype, InputTypes.mouse);

		PlayerPrefs.SetInt (InputNames.iuptype, InputTypes.axis);
		PlayerPrefs.SetInt (InputNames.idowntype, InputTypes.axis);

		PlayerPrefs.SetInt (InputNames.forwardtype, InputTypes.key);
		PlayerPrefs.SetInt (InputNames.backwardstype, InputTypes.key);
		PlayerPrefs.SetInt (InputNames.lefttype, InputTypes.key);
		PlayerPrefs.SetInt (InputNames.righttype, InputTypes.key);

		PlayerPrefs.SetInt (InputNames.menutype, InputTypes.key);



		PlayerPrefs.SetString (InputNames.attack, KeyCode.Q.ToString());
		PlayerPrefs.SetString (InputNames.use, KeyCode.E.ToString());
		PlayerPrefs.SetString (InputNames.map, KeyCode.Space.ToString());

		PlayerPrefs.SetString (InputNames.iup + InputTypes.axis, InputNames.mousewheel);
		PlayerPrefs.SetString (InputNames.idown + InputTypes.axis, InputNames.mousewheel);

		PlayerPrefs.SetString (InputNames.forward, KeyCode.W.ToString());
		PlayerPrefs.SetString (InputNames.backwards, KeyCode.S.ToString());
		PlayerPrefs.SetString (InputNames.left, KeyCode.A.ToString());
		PlayerPrefs.SetString (InputNames.right, KeyCode.D.ToString());

		PlayerPrefs.SetString (InputNames.menu, KeyCode.Escape.ToString());




		PlayerPrefs.SetInt (InputNames.attack, 0);
		PlayerPrefs.SetInt (InputNames.use, 1);
		PlayerPrefs.SetInt (InputNames.map, 2);

		PlayerPrefs.SetInt (InputNames.iup, 2);
		PlayerPrefs.SetInt (InputNames.idown, 1);

		PlayerPrefs.SetInt (InputNames.forward, 0);
		PlayerPrefs.SetInt (InputNames.backwards, 1);
		PlayerPrefs.SetInt (InputNames.left, 0);
		PlayerPrefs.SetInt (InputNames.right, 1);

		PlayerPrefs.SetInt (InputNames.menu, 2);
	}

	public void SetInput(Button btn){
		btn.GetComponentInChildren<Text>().text = "?";
		listening = true;
		listenFor = btn;
	}



	private void SetButtonText(){
		attack.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.attacktype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.attack, KeyCode.Q.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.attacktype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.attack, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.attack + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.attack, 1)>1?"up":"down");

		use.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.usetype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.use, KeyCode.E.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.usetype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.use, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.use + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.use, 1)>1?"up":"down");

		map.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.maptype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.map, KeyCode.Space.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.maptype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.map, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.map + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.map, 1)>1?"up":"down");

		iup.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.iuptype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.iup, KeyCode.LeftControl.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.iuptype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.iup, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.iup + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.iup, 1)>1?"up":"down");

		idown.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.idowntype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.idown, KeyCode.LeftAlt.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.idowntype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.idown, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.idown + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.idown, 1)>1?"up":"down");

		movment.forward.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.forwardtype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.forward, KeyCode.W.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.forwardtype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.forward, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.forward + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.forward, 1)>1?"up":"down");

		movment.backwards.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.backwardstype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.backwards, KeyCode.S.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.backwardstype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.backwards, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.backwards + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.backwards, 1)>1?"up":"down");

		movment.left.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.lefttype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.left, KeyCode.A.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.lefttype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.left, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.left + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.left, 1)>1?"up":"down");

		movment.right.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.righttype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.right, KeyCode.D.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.righttype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.right, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.right + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.right, 1)>1?"up":"down");

		menu.GetComponentInChildren<Text> ().text = PlayerPrefs.GetInt (InputNames.menutype, InputTypes.mouse) == InputTypes.key ? 
			PlayerPrefs.GetString (InputNames.menu, KeyCode.Escape.ToString ()).ToUpper () : PlayerPrefs.GetInt (InputNames.menutype, InputTypes.mouse) == InputTypes.mouse ?
			string.Format ("Mouse {0}", PlayerPrefs.GetInt (InputNames.menu, 0)) : 
			string.Format ("{0} {1}", PlayerPrefs.GetString (InputNames.menu + InputTypes.axis, "None") ,PlayerPrefs.GetInt (InputNames.menu, 1)>1?"up":"down");
		

		
	}
}
