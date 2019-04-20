using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

//[System.Obsolete("MapCamController is deprecated, please use MapCamController2 instead.")]
public class MapCamController : MonoBehaviour
{

	private Camera cam;
	private bool shouldPositionOnStartup = false;
	private Rect camrect;


	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera> ();
		camrect = cam.rect;
	}


	void Update(){
		if (InputManager.GetMap()) {
			cam.rect = new Rect (.1f, .1f, .8f, .8f);
		} else {
			cam.rect = camrect;
		}
	}
				

	public void PositionCam ()
	{
		if(cam == null){
			shouldPositionOnStartup = true;
			return;
		}
		while (Scan ()) {
			cam.orthographicSize++;
		}

		bool[] hits = new bool[]{ false, false, false, false };
		do {
			cam.transform.Translate (new Vector3 (0, 0, 1), Space.World);
			Scan (hits);
		} while(!hits [0]);
		int xoff = 0;
		do {
			xoff++;
			cam.transform.Translate (new Vector3 (0, 0, -1), Space.World);
			Scan (hits);
		} while(!hits [2]);
		cam.transform.Translate (new Vector3 (0, 0, xoff / 2), Space.World);
		do {
			cam.transform.Translate (new Vector3 (1, 0, 0), Space.World);
			Scan (hits);
		} while(!hits [1]);
		int yoff = 0;
		do {
			yoff++;
			cam.transform.Translate (new Vector3 (-1, 0, 0), Space.World);
			Scan (hits);
		} while(!hits [3]);
		cam.transform.Translate (new Vector3 (yoff / 2, 0, 0), Space.World);
		while (!Scan ()) {
			cam.orthographicSize--;
		}
		cam.orthographicSize++;
	}

	private bool Scan (bool[] hits = null)
	{
		hits = hits ?? new bool[]{ false, false, false, false };
		bool ret = false;
		for (float x = 0f; x <= 1; x += 0.01f) {
			Ray ray = cam.ViewportPointToRay (new Vector2 (x, 0));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.DrawLine (ray.origin, hit.point, Color.cyan, duration: 5);
				hits [0] = true;
				ret = true;
			}
		}
		for (float y = 0f; y <= 1; y += 0.01f) {
			Ray ray = cam.ViewportPointToRay (new Vector2 (0, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.DrawLine (ray.origin, hit.point, Color.green, duration: 5);
				hits [1] = true;
				ret = true;
			}
		}
		for (float x = 0f; x <= 1; x += 0.01f) {
			Ray ray = cam.ViewportPointToRay (new Vector2 (x, 1));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.DrawLine (ray.origin, hit.point, Color.red, duration: 5);
				hits [2] = true;
				ret = true;
			}
		}
		for (float y = 0f; y <= 1; y += 0.01f) {
			Ray ray = cam.ViewportPointToRay (new Vector2 (1, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Debug.DrawLine (ray.origin, hit.point, Color.yellow, duration: 5);
				hits [3] = true;
				ret = true;
			}
		}
		return ret;
	}

	//[CustomEditor(typeof(MapCamController))]
	//public class ObjectBuilderEditor : Editor
	//{
	//	public override void OnInspectorGUI()
	//	{
	//		DrawDefaultInspector();
	//
	//		MapCamController mcc = (MapCamController)target;
	//		if(GUILayout.Button("Position Camera"))
	//		{
	//			mcc.PositionCam();
	//		}
	//	}
	//}
}
