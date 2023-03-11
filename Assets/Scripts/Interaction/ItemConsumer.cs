using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemConsumer : Interactable
{
	[SerializeField]
	private DialogueManager dialogueManager;
	[SerializeField]
	private ItemType requiredItem;

	[SerializeField]
	private UnityEvent OnDone;

	[SerializeField]
	private string speakerName;
	[SerializeField]
	[TextArea]
	private string wrongItemText;
	[SerializeField]
	[TextArea]
	private string correctItemText;

	public override void Interact(PlayerInteractionManager manager)
	{
		var itemHolder = manager.GetComponent<PlayerHeldItem>();
		if (itemHolder)
		{
			if (itemHolder.HeldItemType != null && itemHolder.HeldItemType.Value == this.requiredItem)
			{
				itemHolder.DestroyItem();
				dialogueManager.ShowText(this.correctItemText, this.speakerName).ContinueWithOnMainThread(task =>
				{
					if (task.Result)
					{
						this.OnDone.Invoke();
					}
				});
			}
			else
			{
				dialogueManager.ShowText(this.wrongItemText, this.speakerName);
			}
		}
	}
}
