
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    [SerializeField] private Health health;

    public void Repair(Collider incoming)
    {
        var incomingInventory = incoming.GetComponent<Inventory>();

        #nullable enable
        ItemData? item;
        #nullable disable
        do
        {
            item = incomingInventory.GetItem();
            if (item == null) continue;
            incoming.GetComponent<Inventory>().RemoveItem(item);

            health.GainHealth(item.repairValue);
        } while (item != null);
    }
}
