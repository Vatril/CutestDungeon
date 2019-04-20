using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Obsolete("DungeonBuilder is deprecated, please use DungeonBuilder2 instead.")]
public class DungeonBuilder : MonoBehaviour
{

	public Vector2 maxminSize;
	public DungeonPart[] northOpen;
	public DungeonPart[] eastOpen;
	public DungeonPart[] southOpen;
	public DungeonPart[] westOpen;
	public DungeonPart[] northOpenEnd;
	public DungeonPart[] eastOpenEnd;
	public DungeonPart[] southOpenEnd;
	public DungeonPart[] westOpenEnd;


	private LinkedList<DungeonPart> map = new LinkedList<DungeonPart> ();
	private EnemySpawner es;

	// Use this for initialization
	void Start ()
	{
		bool done;
		do{
			done = Init();
		}while(!done);
	}

	private bool Init(){
		GenerateMap ();
		NavMeshSurface nms = map.First.Value.gameObject.AddComponent (typeof(NavMeshSurface)) as NavMeshSurface;

		//nms.tileSize = 1;

		GameObject mapcam = GameObject.Find ("Map Cam");
		if (mapcam != null)
			mapcam.GetComponent<MapCamController> ().PositionCam ();
		es = GetComponent<EnemySpawner> ();
		List<EnemyController> ecs = es.Spawn (map);
		var ecla = new EnemyController[ecs.Count];
		ecs.CopyTo (ecla);
		nms.BuildNavMesh ();
		for (int i = 0; i < ecla.Length; i++) {
			ecla [i].gameObject.SetActive (true);
		}

		foreach (EnemyController ec1 in ecla) {
			foreach (EnemyController ec2 in ecla) {
				var nma = ec1.GetComponent<NavMeshAgent> () as NavMeshAgent;
				NavMeshPath path = new NavMeshPath ();
				nma.CalculatePath (ec1.transform.position, path);
				if (path.status != NavMeshPathStatus.PathComplete) {
					Debug.Log ("NavMesh error");
					return false;
				}
			}
		}
		return true;
	}

	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void GenerateMap ()
	{
		bool done = false;
		bool toBig = false;
		DungeonPart[] fg = new DungeonPart[][]{ northOpen, eastOpen, southOpen, westOpen } [Random.Range (0, 4)];
		DungeonPart first = Instantiate (fg [Random.Range (0, fg.Length)], transform.position, Quaternion.identity);
		first.transform.parent = this.transform;
		map.AddLast (first);

		int i = 0;

		do {

			done = true;
			foreach (DungeonPart part in map) {
				DungeonPart toAdd = null;
				if (!part.neighbors.eastwall.hasWall && part.neighbors.eastwall.node == null) {
					toAdd = GetDungeonPartByPos (new Vector2 (part.logicalPosition.x + 1, part.logicalPosition.y));
					if (toAdd == null) {
						toAdd = Instantiate ((toBig ? westOpenEnd : westOpen) [Random.Range (0, (toBig ? westOpenEnd : westOpen).Length)], transform.position, Quaternion.identity);
						toAdd.logicalPosition = new Vector2 (part.logicalPosition.x + 1, part.logicalPosition.y);
						toAdd.transform.parent = this.transform;
					}
					toAdd.neighbors.westwall.node = part;
					part.neighbors.eastwall.node = toAdd;
				} else if (!part.neighbors.southwall.hasWall && part.neighbors.southwall.node == null) {
					toAdd = GetDungeonPartByPos (new Vector2 (part.logicalPosition.x, part.logicalPosition.y - 1));
					if (toAdd == null) {
						toAdd = Instantiate ((toBig ? northOpenEnd : northOpen) [Random.Range (0, (toBig ? northOpenEnd : northOpen).Length)], transform.position, Quaternion.identity);
						toAdd.logicalPosition = new Vector2 (part.logicalPosition.x, part.logicalPosition.y - 1);
						toAdd.transform.parent = this.transform;
					}
					toAdd.neighbors.northwall.node = part;
					part.neighbors.southwall.node = toAdd;
				} else if (!part.neighbors.westwall.hasWall && part.neighbors.westwall.node == null) {
					toAdd = GetDungeonPartByPos (new Vector2 (part.logicalPosition.x - 1, part.logicalPosition.y));
					if (toAdd == null) {
						toAdd = Instantiate ((toBig ? eastOpenEnd : eastOpen) [Random.Range (0, (toBig ? eastOpenEnd : eastOpen).Length)], transform.position, Quaternion.identity);
						toAdd.logicalPosition = new Vector2 (part.logicalPosition.x - 1, part.logicalPosition.y);
						toAdd.transform.parent = this.transform;
					}
					toAdd.neighbors.eastwall.node = part;
					part.neighbors.westwall.node = toAdd;
				} else if (!part.neighbors.northwall.hasWall && part.neighbors.northwall.node == null) {
					toAdd = GetDungeonPartByPos (new Vector2 (part.logicalPosition.x, part.logicalPosition.y + 1));
					if (toAdd == null) {
						toAdd = Instantiate ((toBig ? southOpenEnd : southOpen) [Random.Range (0, (toBig ? southOpenEnd : southOpen).Length)], transform.position, Quaternion.identity);
						toAdd.logicalPosition = new Vector2 (part.logicalPosition.x, part.logicalPosition.y + 1);
						toAdd.transform.parent = this.transform;
					}
					toAdd.neighbors.southwall.node = part;
					part.neighbors.northwall.node = toAdd;
				}
				if (toAdd != null) {
					toAdd.UpdatePos ();
					done = false;
					map.AddLast (toAdd);
					if (i++ > maxminSize.y) {
						toBig = true;
					}
					break;
				}

			}
		} while(!done);

		if (toBig) {
			
		} else {
			Debug.Log ("remaking map");
			if (i < maxminSize.y) {
				while (map.Count > 0) {
					Destroy (map.First.Value.gameObject);
					map.RemoveFirst ();
				}
				GenerateMap ();
			}
		}
	}

	private DungeonPart GetDungeonPartByPos (Vector2 pos)
	{
		foreach (DungeonPart part in map) {
			if (part.logicalPosition == pos) {
				return part;
			}
		}
		return null;
	}
}
