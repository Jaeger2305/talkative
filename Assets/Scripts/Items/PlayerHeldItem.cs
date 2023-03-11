using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldItem : MonoBehaviour
{
	[SerializeField]
	private Transform itemParent;
	[SerializeField]
	private float heldScaleFactor = 0.25f;

	private Item heldItem;
	public bool HoldsItem => this.heldItem != null;

	public void PickItem(Item item)
	{
		if (this.heldItem == null)
		{
			this.heldItem = item;
			if (this.itemParent != null)
			{
				item.transform.SetParent(this.itemParent, worldPositionStays: false);
				item.transform.localScale *= this.heldScaleFactor;
				item.gameObject.SetActive(true);
			}
			else
			{
				item.gameObject.SetActive(false);
			}
			item.HandleItemPick();
		}
	}

	public Item PlaceItem(Transform itemTarget)
	{
		if (itemTarget != null && this.heldItem != null)
		{
			var item = this.heldItem;
			this.heldItem = null;
			item.transform.SetParent(itemTarget, worldPositionStays: false);
			item.transform.localScale /= this.heldScaleFactor;
			item.HandleItemPlacement();
			return item;
		}
		else
		{
			return null;
		}
	}

	public void DestroyItem()
	{
		if (this.heldItem != null)
		{
			Object.Destroy(this.heldItem);
			this.heldItem = null;
		}
	}
}
