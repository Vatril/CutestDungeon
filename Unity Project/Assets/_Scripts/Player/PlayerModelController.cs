using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class PlayerModelController : MonoBehaviour, IDamager
{

	private Animator anim;
	private Portal portal;
	private float lastHit = 0;
	//private float sweep = 0;

	protected float progress = 0;
	protected float mana = 100f;
	protected float manaregenrate = 1;
	protected int health;
	protected int maxhealth = 12;
	protected float maxmana = 100f;
	protected float damageMulti = 1f;
	protected Vector2 damage = new Vector2 (4, 12);
	private int money = 0;
	private bool warping;
	private bool warpingToShop;
	private float warpProgress;
	private float baseSpeed;
	private float baseDamage;
	private AudioSource source;

	private float speedLeft = 0;
	private float strengthLeft = 0;
	private float regenLeft = 0;
	private float invincLeft = 0;

	private float timeAtLastRegen;

	public AudioClip[] grunts;

	private PotionParticles particles;
	private ParticleSystem.Particle[] pl;
	private ShootPoint shootpoint;
	private UIController uic;

	public bool alive = true;
	public bool isMoving;
	public Color color;
	public float speedMulti = 1f;

	void OnEnable(){
		portal.Stop ();
	}

	// Use this for initialization
	protected virtual void Start ()
	{
		Debug.LogError ("Starting");
		DontDestroyOnLoad (gameObject);
		mana = maxmana;
		uic = FindObjectOfType<UIController> ();
		health = maxhealth;
		anim = GetComponent<Animator> ();
		shootpoint = GetComponentInChildren<ShootPoint> ();
		portal = GetComponentInChildren<Portal> ();
		particles = GetComponentInChildren<PotionParticles> ();
		baseSpeed = speedMulti;
		baseDamage = damageMulti;
		uic.DisplayHearts (this.health, this.maxhealth);
		pl = new ParticleSystem.Particle[200];
		source = GetComponent<AudioSource> ();
		source.volume = SoundManager.instance.GetSFXVolume ();
	}

	protected virtual void Update ()
	{
		if (Time.timeScale == 0)
			return;
		if (mana < maxmana && !InputManager.GetAttack ()) {
			mana+=manaregenrate;
		}

		if (speedLeft > 0) {
			particles.PlaySpeed ();
			speedLeft -= Time.deltaTime;
			speedMulti = baseSpeed * 2;
			if (speedLeft <= 0) {
				particles.StopSpeed ();
				speedMulti = baseSpeed;
				speedLeft = 0;
			}
		}

		if (strengthLeft > 0) {
			particles.PlayStrength ();
			strengthLeft -= Time.deltaTime;
			damageMulti = baseDamage * 2.5f;
			if (strengthLeft <= 0) {
				particles.StopStrength ();
				damageMulti = baseDamage;
				strengthLeft = 0;
			}
		}

		if (regenLeft > 0) {
			particles.PlayRegen ();
			regenLeft -= Time.deltaTime;
			if (timeAtLastRegen + 1 < Time.time) {
				timeAtLastRegen = Time.time;
				Heal (1);
			}
			if (regenLeft <= 0) {
				particles.StopRegen ();
				regenLeft = 0;
			}
		}

		if (invincLeft > 0) {
			particles.PlayInvinc ();
			invincLeft -= Time.deltaTime;
			if (invincLeft <= 0) {
				particles.StopInvinc ();
				invincLeft = 0;
			}
		}


		if (warping || warpingToShop) {
			if (InputManager.GetUse ()) {
				warpProgress += Time.deltaTime;
				portal.Play ();
				if (warpProgress >= 3f) {
					if (!GameController.maincontroller.finalRound || (GameController.maincontroller.finalRound && GameController.maincontroller.finalRoundEnded)) {
						if (warping) {
							
							if (GameController.maincontroller.finalRoundEnded) {
								GameObject.FindObjectOfType<LoadManager> ().Load ("EndScene");
								GameController.maincontroller.canPause = false;
							}
						
							if (SceneManager.GetActiveScene ().name == "Boss") {
								GameObject.FindObjectOfType<LoadManager> ().Load ("Dungeon");
							} else {
								AchievementTracker.instance.ChangeLevel ();
								GameObject.FindObjectOfType<LoadManager> ().Load ("Boss");
							}
							uic.holder.progress.value = 0;
							progress = 0;
							GameController.maincontroller.canPause = false;
						} else if (warpingToShop && SceneManager.GetActiveScene ().name == "Dungeon") {
							GameObject.FindObjectOfType<ShopController> ().StartShopping (this);
						}
					}
				}
			} else {
				warpProgress = 0;
				warping = false;
				warpingToShop = false;
				portal.Stop ();
			}
		}
	}

	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		if (alive) {
			HandleInput ();
			anim.SetBool ("walking", isMoving);
			anim.SetBool ("shooting", InputManager.GetAttack ());
		} else {
			shootpoint.SetEmitterRunning (false);
		}
	}

	public virtual void HandleInput ()
	{
		if (InputManager.GetAttack ()) {
			if (mana > 0) {
				mana -= .5f;
				shootpoint.SetEmitterRunning (true);
			} else {
				shootpoint.SetEmitterRunning (false);
			}
		} else {
			shootpoint.SetEmitterRunning (false);
		}


	}




	void LateUpdate ()
	{	
		int length = shootpoint.GetParticles (pl);
		for (int i = 0; i < length; i++) {
			RaycastHit[] hits;

			var ppos = Quaternion.Euler (new Vector3 (0, transform.localRotation.eulerAngles.y, 0)) * pl [i].position / 2 + transform.position;
			ppos.y = 200;
			hits = Physics.RaycastAll (ppos, Vector3.down, Mathf.Infinity, ~((1 << 8) | (1 << 13)), QueryTriggerInteraction.Collide);
			bool wasHit = false;
			foreach (RaycastHit hit in hits) {
				var ec = hit.collider.GetComponentInChildren<IDamagable> ();
				if (ec == null) {
					ec = hit.collider.GetComponentInParent<IDamagable> ();
				}
				if (ec != null) {
					ec.Damage ((int)(Random.Range (damage.x, damage.y) * damageMulti * (1/GameController.maincontroller.difficutlyScale)));
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
		shootpoint.SetParticles (pl, length);
	}

	private void Grunt(){
		if (!source.isPlaying) {
			source.clip = grunts[Random.Range(0, grunts.Length)];
			source.Play ();
		}
	}

	void OnTriggerStay (Collider other)
	{
		OnTriggerSomething (other);
	}

	void OnTriggerEnter (Collider other)
	{
		OnTriggerSomething (other);
	}

	private void OnTriggerSomething (Collider other)
	{
		if (!enabled)
			return;
		var ec = other.GetComponent<IDamaging> ();
		if (ec != null) {
			OnDamageTouch (ec);
		}
		var pickup = other.GetComponent<Pickup> ();
		if (pickup != null) {
			pickup.Consume (this);
		}
	}

	public void OnDamageTouch (IDamaging ec)
	{
		if (invincLeft > 0) {
			return;
		}
		if (lastHit < Time.time - .75 && alive) {
			int damage = (int)Random.Range (ec.GetDamageRange ().x*GameController.maincontroller.difficutlyScale, ec.GetDamageRange ().y*GameController.maincontroller.difficutlyScale + 1);
			AchievementTracker.instance.GetDamage (damage);
			this.health -= damage;
			if (damage > 0) {
				Grunt ();
			}
			if (health <= 0) {
				health = 0;
				if (alive) {
					Die ();
				}
			}
			uic.DisplayHearts (this.health, this.maxhealth);
			lastHit = Time.time;
		}
	}

	private void Die ()
	{
		alive = false;
		anim.applyRootMotion = false;
		anim.SetTrigger ("death");
		AchievementTracker.instance.Die ();
		GameController.maincontroller.canPause = false;
		StartCoroutine (ShowDeadAfterTime());
	}

	private IEnumerator ShowDeadAfterTime(){
		yield return new WaitForSeconds (1);
		FindObjectOfType<EscapeMenu> ().Dead ();
	}

	public int GetHealth ()
	{
		return this.health;
	}

	public int GetMaxHealth ()
	{
		return this.maxhealth;
	}

	public void ModMoney (int amount)
	{
		this.money += amount;
		uic.holder.money.text = "" + money;
	}

	public bool CanAfford (int cost)
	{
		return this.money >= cost;
	}

	public virtual Vector2 GetDamageRange ()
	{
		return this.damage * damageMulti;
	}

	public float GetManaPercent ()
	{
		return mana / maxmana * 100;
	}

	public void Heal (int amount)
	{
		if (alive) {
			this.health = (this.health + amount > this.maxhealth) ? this.maxhealth : this.health + amount;
			uic.DisplayHearts (this.health, this.maxhealth);
		}
	}

	public void AddProgress (float amount)
	{
		AchievementTracker.instance.EnemyKilled ();
		progress += amount;
		if (progress >= 100) {
			progress = 100;
		}
		uic.holder.progress.value = progress;
	}

	public void ToNextLevel ()
	{
		if (progress <= 99f) {
			uic.holder.progress.GetComponentInChildren<BlinkImage> ().Blick ();
			return;
		}
		warping = true;
	}

	public void ToShop ()
	{
		warpingToShop = true;
	}

	public void ModMaxHealth (int hearts)
	{
		maxhealth += hearts * 4;
		uic.DisplayHearts (this.health, this.maxhealth);
		AchievementTracker.instance.CheckHearts (this.maxhealth);
	}

	public void HealMana (int amount)
	{
		this.mana += amount;
		if (this.mana > this.maxmana) {
			this.mana = this.maxmana;
		}
	}

	public void AddRegenTime (float time)
	{
		regenLeft += time;
	}

	public void AddStrengthTime (float time)
	{
		strengthLeft += time;
	}

	public void AddSpeedTime (float time)
	{
		speedLeft += time;
	}

	public void AddInvincTime (float time)
	{
		invincLeft += time;
	}
}
