using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] ItemData itemData;

    public void Collect(Collider incoming)
    {
        Debug.Log(incoming);
        Destroy(gameObject); // for now, we just immediately destroy the item, but we probably want to add effects and more hooks here
        incoming.GetComponent<Inventory>().AddItem(itemData);
    }
}
