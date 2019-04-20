using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Obsolete("DungeonPart is deprecated, please use DungeonPart2 instead.")]
public class DungeonPart : MonoBehaviour {

	[System.Serializable]
	public class Neighbors
	{
		public NeighborPair northwall;
		public NeighborPair southwall;
		public NeighborPair eastwall;
		public NeighborPair westwall;
	}

	[System.Serializable]
	public class NeighborPair
	{
		public bool hasWall;
		public DungeonPart node;
	}

	public Neighbors neighbors;
	public Vector2 logicalPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public bool IsDone(){
		bool done = true;

		if (!neighbors.eastwall.hasWall && neighbors.eastwall.node == null) {
			return false;
		}
		if (!neighbors.southwall.hasWall && neighbors.southwall.node == null) {
			return false;
		}
		if (!neighbors.westwall.hasWall && neighbors.westwall.node == null) {
			return false;
		}
		if (!neighbors.northwall.hasWall && neighbors.northwall.node == null) {
			return false;
		}

		return done;
	}

	public void UpdatePos(){
		transform.position = new Vector3 ((logicalPosition.x * 20),0,(logicalPosition.y * 10));
	}
}
