using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosMarker : MonoBehaviour {

	private PlayerController player;
	private RectTransform trans;

	public RectTransform canvas;
	public Camera minimapcam;

	private Image image;

	// Use this for initialization
	void Start () {
		GameObject pobj = GameObject.Find ("Player");
		if (pobj == null) {
			Debug.LogError ("Player not found");
		}
		player = pobj.GetComponent<PlayerController>();
		trans = GetComponent<RectTransform> ();
		image = GetComponent<Image> ();
	}

	// Update is called once per frame
	void LateUpdate () {
		if (player.GetPlayer () != null) {
			image.color = player.GetPlayer ().color;
			float scale = canvas.sizeDelta.x * .0002f;
			trans.localScale = new Vector3 (scale,scale,1);
			Vector3 pos = minimapcam.WorldToViewportPoint(player.GetPlayer ().gameObject.transform.position);
			trans.anchoredPosition = new Vector2(pos.x * canvas.sizeDelta.x, pos.y * canvas.sizeDelta.y);
		}
	}
}
