using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

	private MainUIHolder holder;

	public ScreenItem portalImage;

	private const string infinity = "∞";

	private LinkedList <ScreenItem> items;
	private LinkedListNode<ScreenItem> current;
	private float last;

	private static ItemManager instance;

	void Awake(){
		if (instance == null) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
		}

	}




	// Use this for initialization
	void Start ()
	{
		holder = FindObjectOfType<MainUIHolder> ();
		items = new LinkedList<ScreenItem> ();
		items.AddLast (portalImage);
		SetItem (items.Last);

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (holder == null) {
			holder = FindObjectOfType<MainUIHolder> ();
		}
		if (GetComponentInChildren<PlayerModelController> () == null) {
			//Debug.LogError ("ItemManager cant find Player");
			return;
		}

		if (InputManager.GetItemDown ()) {
			SetPrevs ();
		}

		if (InputManager.GetItemUp ()) {
			SetNext ();
		}

		if (InputManager.GetUse () && GetComponentInChildren<PlayerModelController> ().enabled) {
			if (last + .5f < Time.time) {
				if (current.Value.ActOnPlayer (GetComponentInChildren<PlayerModelController> ())) {
					var oldcur = current;

					if (current.Previous != null && current.Previous.Value.GetName ().CompareTo (current.Value.GetName ()) == 0) {
						SetItem (current.Previous);
					} else if (current.Next != null) {
						SetItem (current.Next);
					} else {
						SetItem (items.First);
					}
					Destroy (oldcur.Value);
					items.Remove (oldcur);
				}
				last = Time.time;
			}
		}
	}

	public void AddItem (ScreenItem item)
	{
		DontDestroyOnLoad (item.gameObject);
		items.AddLast (item);
		SortLinkedList (items);
		SetItem (current);
	}

	public bool Take (string itemName)
	{

		var temp = items.First;
		LinkedListNode<ScreenItem> toRemove = null;
		while (temp != null) {
			if (temp.Value.GetName ().Equals (itemName)) {
				toRemove = temp;
				break;
			}
			temp = temp.Next;
		}

		if (toRemove == null) {
			return false;
		}

		if (toRemove.Previous != null && toRemove.Previous.Value.GetName ().CompareTo (toRemove.Value.GetName ()) == 0) {
			SetItem (toRemove.Previous);
		} else if (toRemove.Next != null) {
			SetItem (toRemove.Next);
		} else {
			SetItem (items.First);
		}
		Destroy (toRemove.Value);
		items.Remove (toRemove);
		return true;
	}

	private void SetNext ()
	{
		LinkedListNode<ScreenItem> temp = current;
		while ((temp = temp.Next) != null) {
			if (!string.Equals (temp.Value.GetName (), current.Value.GetName ())) {
				SetItem (temp);
				return;
			}
		}

		SetItem (items.First);
	}

	private void SetPrevs ()
	{
		LinkedListNode<ScreenItem> temp = current;
		while ((temp = temp.Previous) != null) {
			if (!string.Equals (temp.Value.GetName (), current.Value.GetName ())) {
				SetItem (temp);
				return;
			}
		}

		SetItem (items.Last);
	}

	private void SetItem (LinkedListNode<ScreenItem> item)
	{
		current = item;
		while (holder.container.transform.childCount != 0) {
			var child = holder.container.transform.GetChild (0);
			child.transform.parent = null;
			Destroy (child.gameObject);

		}
		Instantiate (item.Value, holder.container.transform).gameObject.SetActive (true);
		holder.nameText.text = item.Value.GetName ();
		if (item.Value == portalImage) {
			holder.amountText.text = infinity;
		} else {
			int amount = 0;
			foreach (ScreenItem itm in items) {
				if (string.Equals (itm.GetName (), item.Value.GetName ())) {
					amount++;
				}
			}
			holder.amountText.text = string.Format ("{0}", amount);
		}
	}

	private void SortLinkedList (LinkedList<ScreenItem> toSort)
	{
		ScreenItem[] tempItems = new ScreenItem[toSort.Count];
		toSort.CopyTo (tempItems, 0);
		ScreenItem temp;
		string tempcur = current.Value.GetName ();
		for (int i = 0; i < tempItems.Length; i++) {
			for (int j = i; j < tempItems.Length; j++) {
				if (tempItems [i].GetName ().CompareTo (tempItems [j].GetName ()) < 0) {
					temp = tempItems [i];
					tempItems [i] = tempItems [j];
					tempItems [j] = temp;
				}
			}
		}

		toSort.Clear ();

		foreach (ScreenItem item in tempItems) {
			toSort.AddLast (item);
			if (string.Equals (item.GetName (), tempcur)) {
				current = toSort.Last;
			}
		}


	}
}
