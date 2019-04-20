using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlicker : MonoBehaviour {

	private Light flight;

	private float targetIntesity;
	private float targetGreen;


	private float oldIntensity;
	private float oldGreen;

	private float tenstick;
	private float greentick;

	// Use this for initialization
	void Start () {
		flight = GetComponentInChildren<Light> ();
		oldIntensity = flight.intensity;
		oldGreen = flight.color.g;
		targetGreen = (float)Random.Range (190,250)/255f;
		targetIntesity = (float)Random.Range (180,250)/100f;
	}
	


	void Update(){
		tenstick += Time.deltaTime;
		greentick += Time.deltaTime;

		flight.intensity = Mathf.Lerp (oldIntensity,targetIntesity,tenstick);
		flight.color = new Color (1f,Mathf.Lerp (oldGreen,targetGreen,greentick),0.47f);

		if (greentick >= 1) {
			greentick = 0;
			oldGreen = targetGreen;
			targetGreen = (float)Random.Range (190,250)/255f;
		}
		if (tenstick >= 1) {
			tenstick = 0;
			oldIntensity = targetIntesity;
			targetIntesity = (float)Random.Range (180,250)/100f;
		}
	}
}
