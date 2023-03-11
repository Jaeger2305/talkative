using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    crystal,
}

[CreateAssetMenu(fileName = "Resource", menuName = "GameData/Resource", order = 1)]
public class ItemData : ScriptableObject
{
    public System.Guid id;
    public ItemType itemType;
    public int repairValue; // the ship will be repaired with items, and this indicates how much it will be repaired.
}
