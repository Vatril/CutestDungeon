using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenItem: MonoBehaviour
{
	public abstract bool ActOnPlayer(PlayerModelController pmc);

	public abstract string GetName ();
}


