using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(PlayerInteractionManager))]
public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField]
	private DialogueManager dialogueManager;

	private FirstPersonController playerController;
	private PlayerInteractionManager interactionManager;

	void Awake()
	{
		this.playerController = this.GetComponent<FirstPersonController>();
		this.interactionManager = this.GetComponent<PlayerInteractionManager>();

		if (this.dialogueManager == null)
		{
			this.dialogueManager = Object.FindObjectOfType<DialogueManager>(includeInactive: true);
		}
	}

	void Update()
	{
		bool canMove = true;
		bool canInteract = true;

		if (dialogueManager.IsDialogueOpen)
		{
			canMove = false;
			canInteract = false;
		}

		this.playerController.playerCanMove = canMove;
		this.playerController.cameraCanMove = canMove;
		this.interactionManager.AreInteractionsAllowed = canInteract;
	}
}
