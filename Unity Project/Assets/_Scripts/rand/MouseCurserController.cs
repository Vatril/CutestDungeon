using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCurserController : MonoBehaviour {

	private MouseCurserController instance;

	private Vector2 hotspot = new Vector2((float)12.5f,12.5f);

	public Texture2D curser;

	void Awake(){
		Cursor.SetCursor (curser,hotspot, CursorMode.Auto);
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}
	}

	void Start(){
		Cursor.SetCursor (curser,hotspot, CursorMode.Auto);

	}

}
