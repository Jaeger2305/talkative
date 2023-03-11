using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/** Often, the trigger is on a child element, so we can have one for a "trigger" zone, and one for "rigidbodies".
 * This quick conversion allows us to use the interface to communicate within a GO hierarchy, like prefabs */
public class TriggerToUnityEvent : MonoBehaviour
{

    [SerializeField] private List<string> tagsOfInterest;
    public UnityEvent<Collider> onTriggerEnter;

    public void OnTriggerEnter(Collider incoming)
    {
        if (tagsOfInterest.Contains(incoming.tag)) onTriggerEnter.Invoke(incoming);
    }
}
