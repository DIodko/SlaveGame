using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypeItem", menuName = "Inventory/Item/New Weapon Type Item")]
public class WeaponType : ItemScriptableObject
{
    private void Start()
    {
        itemType = ItemType.Weapon;
    }
}
