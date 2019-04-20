using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionParticles : MonoBehaviour {

	public PotionSystem regen;
	public PotionSystem speed;
	public PotionSystem strength;
	public PotionSystem invinc;

	// Use this for initialization
	void Start () {
		var m = regen.system.main;
		m.startColor = regen.color;
		m = speed.system.main;
		m.startColor = speed.color;
		m = strength.system.main;
		m.startColor = strength.color;
		m = invinc.system.main;
		m.startColor = invinc.color;

	}



	public void PlayRegen(){
		if (!regen.system.isPlaying) {
			regen.system.Play ();
		}	
	}
	public void PlayStrength(){
		if (!strength.system.isPlaying) {
			strength.system.Play ();
		}	
	}
	public void PlaySpeed(){
		if (!speed.system.isPlaying) {
			speed.system.Play ();
		}	
	}
	public void PlayInvinc(){
		if (!invinc.system.isPlaying) {
			invinc.system.Play ();
		}	
	}

	public void StopRegen(){
		regen.system.Stop ();
	}
	public void StopSpeed(){
		speed.system.Stop ();
	}
	public void StopStrength(){
		strength.system.Stop ();
	}
	public void StopInvinc(){
		invinc.system.Stop ();
	}


	[System.Serializable]
	public class PotionSystem{
		public ParticleSystem system;
		public Color color;
	}
}
