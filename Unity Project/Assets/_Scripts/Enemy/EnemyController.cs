using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public abstract class EnemyController : MonoBehaviour, IDamaging, IDamagable {

	protected PlayerController player;
	protected NavMeshAgent nma;
	protected int health;
	public float xp;
	protected bool dead = false;
	private float speed;
	private float lastHit;

	public SuicideAfter blood;

	// Use this for initialization
	protected virtual void Start () {
		player = GameObject.Find ("Player").gameObject.GetComponent<PlayerController> ();
		if (player == null) {
			Debug.LogError ("Player not found");
		}
		nma = GetComponent<NavMeshAgent> ();
		speed = nma.speed;

		RaycastHit info;
		if (!Physics.Raycast (new Ray(transform.position + new Vector3(0,5,0), Vector3.down), out info, Mathf.Infinity, 1<<13)) {
			if (GetComponent<BossController>() == null) {
				Destroy (this.gameObject);
			}
		}
		nma.SetDestination (this.transform.position);
	}

	// Update is called once per frame
	protected virtual void Update () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, player.GetPlayer().transform.position - transform.position, out hit,35, ((1<<8)|(1<<9)))) {
			Debug.DrawLine (transform.position, hit.point, Color.red);
			if (hit.collider.gameObject.GetComponent<PlayerModelController>() != null) {
				nma.SetDestination (player.GetPlayer().transform.position);
			}
		}


	}

	public void Pause(bool pause){
		if (pause) {
			nma.speed = 0;
		} else {
			nma.speed = speed;
		}
	}

	void OnEnable() {
		GetComponent<NavMeshAgent> ().enabled = true;
	}
		

	public virtual void Die(){
		if (dead)
			return;
		dead = true;
		player.GetPlayer ().AddProgress (xp);
		GetComponent<Dropper>().DropItem ();
		Instantiate (blood, this.transform.position,Quaternion.identity);
		Destroy (this.gameObject);
	}

	public virtual void Damage(int damage){
		if (lastHit + .1 < Time.time) {
			lastHit = Time.time;
			this.health -= damage;
			if (this.health <= 0) {
				Die ();
			}
		}
	}

	public abstract Vector2 GetDamageRange();


}
