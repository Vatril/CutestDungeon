using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPart2 : MonoBehaviour {

	public Walls walls;
	public Neighbors neighbors = new Neighbors();
	public Vector2 logicalPosition;

	[System.Serializable]
	public class Walls{
		public GameObject westWall;
		public GameObject eastWall;
		public GameObject northWall;
		public GameObject southWall;
	}

	[System.Serializable]
	public class Neighbors{
		public DungeonPart2 westPart;
		public DungeonPart2 eastPart;
		public DungeonPart2 northPart;
		public DungeonPart2 southPart;
	}

	public enum Direction{
		ALL, SOUTH,NORTH,EAST,WEST
	}


	// Use this for initialization
	void Start () {
		var torches = GetComponentsInChildren<TorchFlicker> ();

		foreach (TorchFlicker torch in torches) {
			if (!torch.gameObject.activeInHierarchy || !torch.gameObject.activeSelf) {
				DestroyImmediate (torch.gameObject);
			}
		}
		torches = GetComponentsInChildren<TorchFlicker> ();

		if (torches.Length > 0) {

			foreach (TorchFlicker torch in torches) {
				torch.gameObject.SetActive (false);
			}

			for (int i = 0; i < Random.Range (0, 2); i++) {
				torches [Random.Range (0, torches.Length - 1)].gameObject.SetActive (true);
			}

			foreach (TorchFlicker torch in torches) {
				if (!torch.gameObject.activeSelf) {
					Destroy (torch.gameObject);
				}
			}
		}
	}
	
	public bool IsDone(Direction toCheck = Direction.ALL){
		bool done = true;

		if (!walls.eastWall.activeSelf && neighbors.eastPart == null
			&& (toCheck == Direction.ALL || toCheck == Direction.EAST)) {
			return false;
		}
		if (!walls.southWall.activeSelf && neighbors.southPart == null
			&& (toCheck == Direction.ALL || toCheck == Direction.SOUTH)) {
			return false;
		}
		if (!walls.westWall.activeSelf && neighbors.westPart == null
			&& (toCheck == Direction.ALL || toCheck == Direction.WEST)) {
			return false;
		}
		if (!walls.northWall.activeSelf && neighbors.northPart == null
			&& (toCheck == Direction.ALL || toCheck == Direction.NORTH)) {
			return false;
		}

		return done;
	}

	public void UpdatePos(){
		transform.position = new Vector3 ((logicalPosition.x * 20),0,(logicalPosition.y * 10));
	}
		
}
