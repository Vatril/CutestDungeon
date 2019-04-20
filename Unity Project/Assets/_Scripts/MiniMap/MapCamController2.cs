using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamController2 : MonoBehaviour
{

	private Camera cam;
	private bool shouldPositionOnStartup = false;
	private Rect camrect;


	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera> ();
		camrect = cam.rect;
	}


	void Update(){
		if (InputManager.GetMap()) {
			cam.rect = new Rect (.1f, .1f, .8f, .8f);
		} else {
			cam.rect = camrect;
		}
	}


	public void PositionCam (LinkedList<DungeonPart2> map)
	{

	}

	private DungeonPart2 GetLargest(DungeonPart2.Direction dir, LinkedList<DungeonPart2> map){
		var temp = map.First;
		var largest = temp.Value;
		while (temp != null) {
			switch (dir) {
			case DungeonPart2.Direction.NORTH:
				if (temp.Value.logicalPosition.x > largest.logicalPosition.x)
					largest = temp.Value;
				break;
			case DungeonPart2.Direction.SOUTH:
				if (temp.Value.logicalPosition.x < largest.logicalPosition.x)
					largest = temp.Value;
				break;
			case DungeonPart2.Direction.EAST:
				if (temp.Value.logicalPosition.y > largest.logicalPosition.y)
					largest = temp.Value;
				break;
			case DungeonPart2.Direction.WEST:
				if (temp.Value.logicalPosition.y < largest.logicalPosition.y)
					largest = temp.Value;
				break;
			}

			temp = temp.Next;
		}
		return largest;
	}
}
