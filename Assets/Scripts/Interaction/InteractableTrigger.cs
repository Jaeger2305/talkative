using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
	[SerializeField]
	private Interactable interactable;
	private readonly List<PlayerInteractionManager> containedManagers = new();


	void OnTriggerEnter(Collider collider)
	{
		var manager = collider.GetComponent<PlayerInteractionManager>();
		if (manager != null && !this.containedManagers.Contains(manager))
		{
			this.containedManagers.Add(manager);
			this.interactable.TriggerEntered(this, manager);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		var manager = collider.GetComponent<PlayerInteractionManager>();
		if (manager != null && this.containedManagers.Contains(manager))
		{
			this.containedManagers.Remove(manager);
			this.interactable.TriggerExited(this, manager);
		}
	}
}
