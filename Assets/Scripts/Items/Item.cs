using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[field: SerializeField]
	public ItemType ItemType { get; private set; }

	virtual public void HandleItemPick()
	{

	}
	virtual public void HandleItemPlacement()
	{

	}
}
