using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Food, Berry, Weapon, Armour}

public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int maxAmount;
    public int itemSizeX;
    public int itemSizeY;
    public Sprite itemSprite;
    public string itemDiscription;
}
