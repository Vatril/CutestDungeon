using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannonJeffController : BossController
{

	private Animator ani;
	private ShootPoint sp;
	private ParticleSystem.Particle[] pl;

	//public int health;

	protected override void Start ()
	{
		base.Start ();
		this.health = 1300;
		ani = GetComponent<Animator> ();
		sp = GetComponentInChildren<ShootPoint> ();
		pl = new ParticleSystem.Particle[500];
	}

	public override string GetName (){
		return "Bannon";
	}

	protected override void Update ()
	{
		base.Update ();
		if (nma.hasPath) {
			ani.SetBool ("walking", true);
		} else {
			ani.SetBool ("walking", false);
		}

		RaycastHit hit;
		if (Physics.Raycast (transform.position, player.GetPlayer ().transform.position - transform.position, out hit, 10, ((1 << 8) | (1 << 9)))) {
			Debug.DrawLine (transform.position, hit.point, Color.red);
			if (hit.collider.gameObject.GetComponent<PlayerModelController> () != null) {
				sp.SetEmitterRunning (true);
			} else {
				sp.SetEmitterRunning (false);
			}
		} else {
			sp.SetEmitterRunning (false);
		}
	}


	void LateUpdate ()
	{	
		int length = sp.GetParticles (pl);
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
		sp.SetParticles (pl, length);
	}


	private class PlayerDamager : IDamaging
	{

		public Vector2 GetDamageRange ()
		{
			return new Vector2 (4, 8);
		}

	}

	public override void Die(){
		if (dead)
			return;
		dead = true;
		Instantiate (blood, this.transform.position,Quaternion.identity);
		Destroy (this.gameObject);
	}


}
