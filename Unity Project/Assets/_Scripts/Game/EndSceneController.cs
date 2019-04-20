using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour {

	public Portal portal;
	public Camera cam;
	public PlayerModelController[] players;
	public Vector2 camrot;
	public Vector2 walkpos;
	public ParticleSystem pSystem;
	public RawImage fadeout;

	private bool walking;
	private bool scroll;
	private PlayerModelController player;
	private float time;

	public GameObject[] endscroll;
	// Use this for initialization
	void Start () {
		Instantiate (portal, new Vector3 (-7,0,0), Quaternion.identity).ActivateFor5 ();
		StartCoroutine (PlayAnimation());
	}


	private IEnumerator PlayAnimation(){
		Destroy (GameObject.FindObjectOfType<UIController> ().gameObject);
		Destroy (GameObject.FindObjectOfType<MainUIHolder> ().gameObject);
		fadeout.CrossFadeAlpha (0, .5f, true);
		yield return new WaitForSeconds (2);
		player = Instantiate (players[GameController.maincontroller.playerSelected], new Vector3(walkpos.x,1,0), Quaternion.FromToRotation(Vector3.forward,Vector3.right));
		player.enabled = false;
		foreach (ParticleSystem ps in player.gameObject.GetComponentsInChildren<ParticleSystem>()) {
			Destroy (ps);
		}
		Destroy (player.GetComponentInChildren<Portal>().gameObject);
		yield return new WaitForSeconds (2);
		walking = true;
		player.GetComponent<Animator> ().SetBool ("walking", true);
		yield return new WaitUntil (()=>{return time >= 1;});
		player.GetComponent<Animator> ().SetBool ("walking", false);
		yield return new WaitForSeconds (1);
		player.GetComponent<Animator> ().SetBool ("shooting", true);
		yield return new WaitForSeconds (0.5f);
		pSystem.Play ();
		yield return new WaitForSeconds (2);
		fadeout.CrossFadeAlpha (1, 2, true);
		yield return new WaitForSeconds (2);
		scroll = true;
		yield return new WaitUntil (()=>{return endscroll[3].transform.localPosition.y >= 0;});
		scroll = false;
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene ("StartScene");


		Destroy (GameObject.FindObjectOfType<AchievementTracker> ().gameObject);
		Destroy (GameController.maincontroller.gameObject);
		Destroy (this.gameObject);
	}

	void Update(){
		if (walking && time <= 1) {
			player.transform.position = new Vector3 (Mathf.Lerp(walkpos.x,walkpos.y,time),1,0);
			var rot = cam.transform.localRotation;
			rot.eulerAngles = new Vector3 (0,Mathf.Lerp(camrot.x,camrot.y,time),0);
			cam.transform.localRotation = rot;
			time += 0.15f * Time.deltaTime;
		}
		if (scroll) {
			foreach (GameObject go in endscroll) {
				go.transform.Translate (Vector3.up);
			}
		}
	}

}
