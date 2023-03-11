using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDialogueTrigger : Interactable
{
	[SerializeField]
	private DialogueManager dialogues;
	[SerializeField]
	private string speakerName;
	[SerializeField]
	[TextArea(3, 10)]
	private string dialogueText;

	public override void Interact(PlayerInteractionManager manager)
	{
		dialogues.ShowText(this.dialogueText, this.speakerName);
	}
}
