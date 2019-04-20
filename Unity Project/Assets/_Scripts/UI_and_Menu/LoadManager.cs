using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public LoadTransferer loadTransferer;

	// Use this for initialization
	void Start () {
		LoadTransferer l = FindObjectOfType<LoadTransferer> ();
		if (l != null) {
			StartCoroutine (LoadAsync(l));
		}
	}

	public void Load(string toLoad){
		LoadTransferer l =  Instantiate (loadTransferer, this.transform.position, Quaternion.identity) as LoadTransferer;
		l.sceneToLoad = toLoad;
		SceneManager.LoadScene ("Loading");
	}

	private IEnumerator LoadAsync(LoadTransferer l){
		AsyncOperation operation = SceneManager.LoadSceneAsync (l.sceneToLoad);
		Destroy (l.gameObject);
		while (!operation.isDone) {
			Debug.Log (operation.progress);
			yield return null;
		}
	}
}
