using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI repairValueText;
    public void UpdateInventory(List<ItemData> items)
    {
        int val = items.Sum(i => i.repairValue);
        repairValueText.SetText(val.ToString());
    }
}
