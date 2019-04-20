using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IDamaging {

	public Vector2 GetDamageRange(){
		return new Vector2 (1,2);
	}

}
