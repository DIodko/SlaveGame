using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BerryTypeItem", menuName = "Inventory/Item/New Berry Type Item")]
public class BerryType : ItemScriptableObject
{
    private void Start()
    {
        itemType = ItemType.Berry;
    }
}
