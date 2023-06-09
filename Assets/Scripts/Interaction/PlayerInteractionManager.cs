using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteractionManager : MonoBehaviour
{
	private readonly List<Interactable> interactables = new();
	private Interactable FirstInteractable => this.interactables.FirstOrDefault(interactable => interactable != null && interactable.isActiveAndEnabled);
	public bool HasInteractable => FirstInteractable != null;
	public event System.Action<bool> OnHasInteractableChanged;
	private bool previousHasInteractable = false;
	public bool AreInteractionsAllowed { get; set; } = true;

	public void RegisterInteractable(Interactable interactable)
	{
		if (!this.interactables.Contains(interactable))
		{
			this.interactables.Add(interactable);
			this.CheckHasInteractableChange();
		}
	}

	void Update()
	{
		// Remove null entries
		this.interactables.RemoveAll(interactable => interactable == null);
		this.CheckHasInteractableChange();

		// TODO(rw): refactor to propery input handling
		if (this.AreInteractionsAllowed && Input.GetKeyDown(KeyCode.E))
		{
			this.Interact();
		}
	}
	private void CheckHasInteractableChange()
	{
		var hasInteractable = this.HasInteractable;
		if (hasInteractable != this.previousHasInteractable)
		{
			this.previousHasInteractable = hasInteractable;
			this.OnHasInteractableChanged?.Invoke(hasInteractable);
		}
	}

	public void UnregsiterInteractable(Interactable interactable)
	{
		this.interactables.Remove(interactable);
		this.CheckHasInteractableChange();
	}

	public void Interact()
	{
		var interactable = this.FirstInteractable;
		if (interactable != null)
		{
			interactable.Interact(this);
		}
	}
}
