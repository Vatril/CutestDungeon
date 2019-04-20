using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryManager : MonoBehaviour {

	public Summarizer sum;

	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
			if (hit.collider.CompareTag ("Player")) {
				sum.gameObject.SetActive (true);
				if (hit.collider.name == "Vatril" || hit.collider.gameObject.GetComponent<ModelGrayer> ().isUnlocked) {
					sum.SetDisplay (hit.collider.name);
				} else {
					sum.SetDisplay (null);
				}
			} else {
				sum.gameObject.SetActive (false);
			}
		} else {
			sum.gameObject.SetActive (false);
		}
	}
}
