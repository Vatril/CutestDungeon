using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

	public Transform toFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (toFollow != null) {
			this.transform.position = new Vector3 (toFollow.position.x, this.transform.position.y, toFollow.position.z);
		}
	}
}
