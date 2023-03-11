using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionalDialogue : Interactable
{
	[SerializeField]
	private DialogueManager dialogueManager;
	[SerializeField]
	private Condition condition;

	[SerializeField]
	private string speakerName;
	[SerializeField]
	[TextArea]
	private string wrongItemText;
	[SerializeField]
	[TextArea]
	private string correctItemText;

	[SerializeField]
	private UnityEvent OnDone;

	public override void Interact(PlayerInteractionManager manager)
	{
		if (this.condition.IsFullfilled)
		{
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
