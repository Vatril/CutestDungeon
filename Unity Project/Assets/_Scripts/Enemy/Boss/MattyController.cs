using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattyController : BossController
{

	private Animator ani;
	private ParticleSystem ps;
	private ParticleSystem.Particle[] pl;
	private bool cooling;

	//public int health;

	protected override void Start ()
	{
		base.Start ();
		this.health = 2000;
		ani = GetComponent<Animator> ();
		ps = GetComponent<ParticleSystem> ();
		pl = new ParticleSystem.Particle[ps.main.maxParticles];
		StartCoroutine (Cooler ());
	}



	protected override void Update ()
	{
		base.Update ();
		if (nma.hasPath) {
			ani.SetBool ("walking", true);
		} else {
			ani.SetBool ("walking", false);
		}

		if (!cooling) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, player.GetPlayer ().transform.position - transform.position, out hit, 10, ((1 << 8) | (1 << 9)))) {
				if (hit.collider.gameObject.GetComponent<PlayerModelController> () != null) {
					SetEmitterRunning (true);
				} else {
					SetEmitterRunning (false);
				}
			} else {
				SetEmitterRunning (false);
			}
		} else {
			SetEmitterRunning (false);
		}

		ani.SetBool ("shooting", ps.isPlaying);
	}

	private void SetEmitterRunning (bool state)
	{
		if (ps.isPlaying && !state) {
			ps.Stop ();
		} else if (ps.isStopped && state) {
			ps.Play ();
		}
	}

	void LateUpdate ()
	{	
		int length = ps.GetParticles (pl);
		for (int i = 0; i < length; i++) {
			RaycastHit[] hits;

			var ppos = Quaternion.Euler (new Vector3 (0, transform.localRotation.eulerAngles.y, 0)) * pl [i].position + transform.position;
			ppos.y = 200;
			hits = Physics.RaycastAll (ppos, Vector3.down, Mathf.Infinity, ~((1<<11)|(1<<13)), QueryTriggerInteraction.Collide);
			bool wasHit = false;
			foreach (RaycastHit hit in hits) {
				var ec = hit.collider.GetComponentInChildren<PlayerModelController> ();
				if (ec == null) {
					ec = hit.collider.GetComponentInParent<PlayerModelController> ();
				}
				if (ec != null) {
					ec.OnDamageTouch (new PlayerDamager ());
					wasHit = true;
				}
				if (!hit.collider.isTrigger) {
					wasHit = true;
					break;
				}
			}
			if (wasHit) {
				pl [i].remainingLifetime = 0;
			}
		}
		ps.SetParticles (pl, length);
	}


	private class PlayerDamager : IDamaging
	{
				
		public Vector2 GetDamageRange ()
		{
			return new Vector2 (2, 6);
		}

	}

	public override string GetName (){
		return "Matty Lionheart";
	}

	public override void Die(){
		if (dead)
			return;
		dead = true;
		Instantiate (blood, this.transform.position,Quaternion.identity);
		Destroy (this.gameObject);
	}

	public IEnumerator Cooler(){
		while (health > 0) {
			yield return new WaitForSeconds (Random.Range(2,13));
			cooling = true;
			yield return new WaitForSeconds (Random.Range(1,2));
			cooling = false;
		}
	}


}
