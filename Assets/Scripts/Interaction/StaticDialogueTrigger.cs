using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticDialogueTrigger : Interactable
{
	[SerializeField]
	private DialogueManager dialogues;
	[SerializeField]
	private string speakerName;
	[SerializeField]
	[TextArea(3, 10)]
	private string dialogueText;

	[SerializeField]
	private UnityEvent OnDone;

	public override void Interact(PlayerInteractionManager manager)
	{
		dialogues.ShowText(this.dialogueText, this.speakerName).ContinueWithOnMainThread(task =>
		{
			try
			{
				Debug.Log("continue 2");
				if (task.Result)
				{
					Debug.Log("continue 2");
					this.OnDone.Invoke();
				}
			}
			catch (System.Exception exception)
			{
				Debug.LogException(exception);
			}
		});
	}
}
