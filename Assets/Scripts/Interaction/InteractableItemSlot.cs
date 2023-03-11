using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemSlot : Interactable
{
	[SerializeField]
	private Transform itemSlot;
	[SerializeField]
	private Item containedItem;

	[SerializeField]
	private bool canPickItem = true;
	[SerializeField]
	private bool canPlaceItem = true;

	public override void Interact(PlayerInteractionManager manager)
	{
		var itemHolder = manager.GetComponent<PlayerHeldItem>();
		if (itemHolder != null)
		{
			if (!itemHolder.HoldsItem)
			{
				if (this.canPickItem && this.containedItem != null)
				{
					itemHolder.PickItem(this.containedItem);
					this.containedItem = null;
				}
			}
			else
			{
				if (this.canPlaceItem && this.containedItem == null)
				{
					this.containedItem = itemHolder.PlaceItem(this.itemSlot);
				}
			}
		}
	}
}
