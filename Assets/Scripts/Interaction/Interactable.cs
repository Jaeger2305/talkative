using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	abstract public void Interact(PlayerInteractionManager manager);
	private readonly List<InteractableTrigger> triggersEntered = new();

	public void TriggerEntered(InteractableTrigger trigger, PlayerInteractionManager manager)
	{
		if (!this.triggersEntered.Contains(trigger))
		{
			this.triggersEntered.Add(trigger);
			manager.RegisterInteractable(this);
		}
	}

	public void TriggerExited(InteractableTrigger trigger, PlayerInteractionManager manager)
	{
		if (this.triggersEntered.Contains(trigger))
		{
			this.triggersEntered.Remove(trigger);
			manager.UnregsiterInteractable(this);
		}
	}
}
