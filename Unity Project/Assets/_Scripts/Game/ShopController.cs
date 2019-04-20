using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	public GameObject shop;

	private PlayerModelController pmc;
	private GameObject curshop;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartShopping(PlayerModelController pmc){
		this.pmc = pmc;
		foreach (EnemyController ec  in GameObject.FindObjectsOfType<EnemyController>()) {
			ec.Pause (true);
		}
		pmc.GetComponentInParent<ItemManager> ().Take ("Shop Key");
		pmc.enabled = false;
		curshop = Instantiate (shop, new Vector3(10000,0,10000), Quaternion.identity);
		curshop.gameObject.GetComponentInChildren<ShopUI> ().pmc = pmc;
	}

	public void EndShopping(){
		Destroy (curshop);
		foreach (EnemyController ec  in GameObject.FindObjectsOfType<EnemyController>()) {
			ec.Pause (false);
		}
		pmc.enabled = true;
	}
}
