using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonBuilder2 : MonoBehaviour
{

	public Vector2 maxminSize;
	public DungeonPart2 template;
	public Link link;

	private LinkedList<DungeonPart2> map = new LinkedList<DungeonPart2> ();

	// Use this for initialization
	void Start ()
	{
		bool done;
		do {
			done = Init ();
		} while(!done);
		LinkTiles ();
		ActivateLinks ();
		var nms = map.First.Value.GetComponentInChildren<NavMeshSurface> ();
		nms.BuildNavMesh ();
		StartCoroutine (SpawnAfterTime ());
		StartCoroutine (SpawnOverTime ());
		StartCoroutine (PositionCameraAfterTime());

	}

	private bool Init ()
	{
		GenerateMap ();
		return true;
	}

	private void GenerateMap ()
	{
		bool done = false;
		bool toBig = false;

		var origin = BuildPart ();
		origin.name = "origin";
		map.AddFirst (origin);
		do {
			done = true;
			LinkedList<DungeonPart2> tmap = new LinkedList<DungeonPart2> ();
			foreach (DungeonPart2 part in map) {
				if (!part.IsDone ()) {
					done = false;
					if (!part.IsDone (toCheck: DungeonPart2.Direction.NORTH)) {
						var nextPos = new Vector2 (part.logicalPosition.x, part.logicalPosition.y + 1);
						var newPart = GetDungeonPartByPos (nextPos, new LinkedList<DungeonPart2>[]{ map, tmap });
						if (newPart == null) {
							newPart = toBig ? BuildPart (southopen: 0, northopen: 1, westopen: 1, eastopen: 1) : BuildPart (southopen: 0);
							newPart.logicalPosition = nextPos;
							newPart.UpdatePos ();
							tmap.AddLast (newPart);
						}
						newPart.neighbors.southPart = part;
						part.neighbors.northPart = newPart;
					} else if (!part.IsDone (toCheck: DungeonPart2.Direction.SOUTH)) {
						var nextPos = new Vector2 (part.logicalPosition.x, part.logicalPosition.y - 1);
						var newPart = GetDungeonPartByPos (nextPos, new LinkedList<DungeonPart2>[]{ map, tmap });
						if (newPart == null) {
							newPart = toBig ? BuildPart (southopen: 1, northopen: 0, westopen: 1, eastopen: 1) : BuildPart (northopen: 0);
							newPart.logicalPosition = nextPos;
							newPart.UpdatePos ();
							tmap.AddLast (newPart);
						}
						newPart.neighbors.northPart = part;
						part.neighbors.southPart = newPart;
					} else if (!part.IsDone (toCheck: DungeonPart2.Direction.WEST)) {
						var nextPos = new Vector2 (part.logicalPosition.x - 1, part.logicalPosition.y);
						var newPart = GetDungeonPartByPos (nextPos, new LinkedList<DungeonPart2>[]{ map, tmap });
						if (newPart == null) {
							newPart = newPart = toBig ? BuildPart (southopen: 1, northopen: 1, westopen: 1, eastopen: 0) : BuildPart (eastopen: 0);
							newPart.logicalPosition = nextPos;
							newPart.UpdatePos ();
							tmap.AddLast (newPart);
						}
						newPart.neighbors.eastPart = part;
						part.neighbors.westPart = newPart;
					} else if (!part.IsDone (toCheck: DungeonPart2.Direction.EAST)) {
						var nextPos = new Vector2 (part.logicalPosition.x + 1, part.logicalPosition.y);
						var newPart = GetDungeonPartByPos (nextPos, new LinkedList<DungeonPart2>[]{ map, tmap });
						if (newPart == null) {
							newPart = newPart = toBig ? BuildPart (southopen: 1, northopen: 1, westopen: 0, eastopen: 1) : BuildPart (westopen: 0);
							newPart.logicalPosition = nextPos;
							newPart.UpdatePos ();
							tmap.AddLast (newPart);
						}
						newPart.neighbors.westPart = part;
						part.neighbors.eastPart = newPart;


					}
				}

			}
			foreach (DungeonPart2 part in tmap) {
				map.AddLast (part);
			}
			if (map.Count >= maxminSize.y) {
				toBig = true;
			}
		} while(!done);
		if (map.Count < maxminSize.x) {
			Debug.LogError ("remaking map because to small");

			while (map.Count > 0) {
				Destroy (map.First.Value.gameObject);
				map.RemoveFirst ();
			}
			GenerateMap ();

		}

	}

	private DungeonPart2 BuildPart (
		int northopen = -1,
		int southopen = -1,
		int westopen = -1,
		int eastopen = -1
	)
	{
		if (northopen < 0) {
			northopen = Random.value > 0.5 ? 1 : 0;
		}
		if (southopen < 0) {
			southopen = Random.value > 0.5 ? 1 : 0;
		}
		if (westopen < 0) {
			westopen = Random.value > 0.5 ? 1 : 0;
		}
		if (eastopen < 0) {
			eastopen = Random.value > 0.5 ? 1 : 0;
		}
		var part = Instantiate (template, this.transform);

		if (northopen < 1) {
			part.walls.northWall.SetActive (false);
		}
		if (southopen < 1) {
			part.walls.southWall.SetActive (false);
		}
		if (westopen < 1) {
			part.walls.westWall.SetActive (false);
		}
		if (eastopen < 1) {
			part.walls.eastWall.SetActive (false);
		}

		return part;
	}

	private DungeonPart2 GetDungeonPartByPos (Vector2 pos, LinkedList<DungeonPart2>[] lists)
	{
		foreach (LinkedList<DungeonPart2> l in lists) {
			foreach (DungeonPart2 part in l) {
				if (part.logicalPosition == pos) {
					return part;
				}
			}
		}
		return null;
	}

	private void LinkTiles ()
	{
		foreach (DungeonPart2 part in map) {
			if (!part.walls.northWall.activeSelf && part.neighbors.northPart != null) {
				if (!part.neighbors.northPart.walls.southWall.activeSelf) {
					for (int i = -8; i <= 8; i += 2) {
						var l = Instantiate (link, part.transform);
						l.transform.localPosition = new Vector3 (i, 0f, 2f);
						l.end.transform.localPosition = new Vector3 (0f, 0f, 6f);
					}
				}
			}
			if (!part.walls.southWall.activeSelf && part.neighbors.southPart != null) {
				if (!part.neighbors.southPart.walls.northWall.activeSelf) {
					for (int i = -8; i <= 8; i += 2) {
						var l = Instantiate (link, part.transform);
						l.transform.localPosition = new Vector3 (i, 0f, -2f);
						l.end.transform.localPosition = new Vector3 (0f, 0f, -6f);
					}
				}
			}
			if (!part.walls.eastWall.activeSelf && part.neighbors.westPart != null) {
				if (!part.neighbors.eastPart.walls.westWall.activeSelf) {
					for (int i = -3; i <= 3; i += 2) {
						var l = Instantiate (link, part.transform);
						l.transform.localPosition = new Vector3 (6, 0f, i);
						l.end.transform.localPosition = new Vector3 (6f, 0f, 0f);
					}
				}
			}
			if (!part.walls.westWall.activeSelf && part.neighbors.eastPart != null) {
				if (!part.neighbors.westPart.walls.eastWall.activeSelf) {
					for (int i = -3; i <= 3; i += 2) {
						var l = Instantiate (link, part.transform);
						l.transform.localPosition = new Vector3 (-6, 0f, i);
						l.end.transform.localPosition = new Vector3 (-6f, 0f, 0f);
					}
				}
			}
		}
	}

	private void ActivateLinks ()
	{
		foreach (DungeonPart2 part in map) {
			foreach (Link l in part.GetComponentsInChildren<Link>()) {
				l.gameObject.SetActive (false);
				StartCoroutine (ActivateAfterTime (l));
			}
		}
	}

	IEnumerator ActivateAfterTime (Link l)
	{
		yield return new WaitForSeconds (1);
		l.gameObject.SetActive (true);
	}

	IEnumerator SpawnAfterTime ()
	{
		yield return new WaitForSeconds (2);
		GetComponent<EnemySpawner2> ().Spawn (map);
	}

	IEnumerator PositionCameraAfterTime ()
	{
		yield return new WaitForSeconds (1.5f);
		FindObjectOfType<MapCamController> ().PositionCam ();
		FindObjectOfType<PlayerController> ().SetCharacter (GameController.maincontroller.playerSelected);
	}



	IEnumerator SpawnOverTime ()
	{
		while (this.gameObject.activeInHierarchy) {
			yield return new WaitForSeconds (Random.Range (3, 15));
			PlayerModelController player = FindObjectOfType<PlayerModelController> ();
			if (player.gameObject.activeInHierarchy) {
				RaycastHit hit;
				if (Physics.Raycast (new Ray (player.transform.position, Vector3.down), out hit, 15, (1 << 9)|(1<<13))) {
					var dp = hit.collider.GetComponentInParent<DungeonPart2> ();
					if (dp == null) {
						dp = hit.collider.GetComponentInChildren<DungeonPart2> ();
					}
					if (dp == null) {
						Debug.LogError ("Failed to get player logical position");
						continue;
					}
					GetComponent<EnemySpawner2> ().Spawn (map, lrounds: 1, showParticles: true ,excluded: new Vector2[] {
						dp.logicalPosition,
						new Vector2 (dp.logicalPosition.x + 1, 	dp.logicalPosition.y + 1),
						new Vector2 (dp.logicalPosition.x - 1, 	dp.logicalPosition.y - 1),
						new Vector2 (dp.logicalPosition.x - 1, 	dp.logicalPosition.y),
						new Vector2 (dp.logicalPosition.x , 	dp.logicalPosition.y - 1),
						new Vector2 (dp.logicalPosition.x + 1, 	dp.logicalPosition.y),
						new Vector2 (dp.logicalPosition.x , 	dp.logicalPosition.y + 1),
						new Vector2 (dp.logicalPosition.x + 1, 	dp.logicalPosition.y - 1),
						new Vector2 (dp.logicalPosition.x - 1, 	dp.logicalPosition.y + 1),
					});
				} else {
					Debug.LogError ("Failed to get player logical position");
				}
			}
		}
	}
}
