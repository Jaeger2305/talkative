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

	[SerializeField]
	private bool limitPlaceableItems = false;
	[SerializeField]
	private List<ItemType> allowedItemTypes = new();

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
				if (this.containedItem == null && this.IsPlacementAllowed(itemHolder.HeldItemType.Value))
				{
					this.containedItem = itemHolder.PlaceItem(this.itemSlot);
				}
			}
		}
	}

	private bool IsPlacementAllowed(ItemType itemType)
	{
		return this.canPlaceItem && (!this.limitPlaceableItems || this.allowedItemTypes.Contains(itemType));
	}
}
