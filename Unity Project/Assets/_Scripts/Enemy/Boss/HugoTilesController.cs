using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugoTilesController : BossController
{

	private Animator ani;


	//public int health;

	protected override void Start ()
	{
		base.Start ();
		this.health = 1000;
		ani = GetComponent<Animator> ();
	}

	public override string GetName (){
		return "Dimensiona";
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
		if (Physics.Raycast (transform.position, player.GetPlayer ().transform.position - transform.position, out hit, 5, ((1 << 8) | (1 << 9)))) {
			if (hit.collider.gameObject.GetComponent<PlayerModelController> () != null) {
				ani.SetBool ("ramming", true);
			} else {
				ani.SetBool ("ramming", false);
			}
		} else {
			ani.SetBool ("ramming", false);
		}
	}
		

	private class PlayerDamager : IDamaging
	{

		public Vector2 GetDamageRange ()
		{
			return new Vector2 (6, 16);
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
