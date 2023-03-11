using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotCondition : Condition
{
	[SerializeField]
	private InteractableItemSlot itemSlot;
	[SerializeField]
	private ConditionType conditionType;
	[SerializeField]
	private List<ItemType> desiredItem;

	public override bool IsFullfilled
	{
		get
		{
			switch (this.conditionType)
			{
				case ConditionType.HasNoItem:
					return this.itemSlot.ContainedItemType == null;
				case ConditionType.HasAnyItem:
					return this.itemSlot.ContainedItemType != null;
				case ConditionType.HasOneOfItem:
				default:
					return this.itemSlot.ContainedItemType != null && this.desiredItem.Contains(this.itemSlot.ContainedItemType.Value);
			}
		}
	}

	void OnValidate()
	{
		if (this.itemSlot == null)
		{
			var itemSlot = this.GetComponent<InteractableItemSlot>();
			if (itemSlot != null)
			{
				this.itemSlot = itemSlot;
#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(this);
#endif
			}
		}
	}

	private enum ConditionType
	{
		HasNoItem,
		HasAnyItem,
		HasOneOfItem,
	}
}
