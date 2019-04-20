using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour {

	private ParticleSystem ps;


	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}


	public void SetEmitterRunning(bool state){
		if (ps.isPlaying && !state) {
			ps.Stop ();
		}else if(ps.isStopped && state){
			ps.Play();
		}
	}

	public int GetParticles(ParticleSystem.Particle[] pl){
		if (ps == null)
			return 0;
		return ps.GetParticles (pl);
	}

	public void SetParticles(ParticleSystem.Particle[] pl, int length){
		if (ps == null)
			return;
		ps.SetParticles (pl, length);
	}


}
