using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int x;
    public int y;
    public bool isFree;
    public Inventory inventory;
    public ItemScriptableObject itemInfo;
    public Sprite cellSingleFilled;
    public Sprite cellSingle;
    public Sprite cellLeft;
    public Sprite cellRight;
    public Image cellImage;
    public Vector2 positionCell;

    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        cellImage = GetComponent<Image>();
    }
    public void Update()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        itemInfo = dragItem.itemSO;
        if (isFree)
        {
            if (dragItem.GetComponent<RectTransform>().anchoredPosition.x < 385 && !inventory.itemsList.Contains(dragItem.gameObject))
            {
                inventory.itemsList.Remove(dragItem.gameObject);
                dragItem.inventory = inventory;
                inventory.itemsList.Add(dragItem.gameObject);
            }
            else if (dragItem.GetComponent<RectTransform>().anchoredPosition.x >= 385 && !inventory.itemsList.Contains(dragItem.gameObject))
            {
                inventory.itemsList.Remove(dragItem.gameObject);
                dragItem.inventory = inventory;
                inventory.itemsList.Add(dragItem.gameObject);
            }
            dragItem.transform.SetParent(transform);
            dragItem.transform.localPosition = Vector2.zero;
            var newPos = dragItem.transform.localPosition;
            if (dragItem.itemSO.itemSizeX > 1)
            {
                if (x < (inventory.sizeX - 1))
                {
                    Debug.Log("Меньше");
                    newPos.x += dragItem.itemSO.itemSizeX * 8;
                    dragItem.transform.localPosition = newPos;
                    cellImage.sprite = cellLeft;
                    inventory.cells[x + 1, y].cellImage.sprite = cellRight;
                    dragItem.occupied_Cells.Add(inventory.cells[x + 1, y]);
                    dragItem.occupied_Cells.Add(this);
                    dragItem.prevCell = this;
                    isFree = false;
                    dragItem.transform.SetParent(inventory.transform);
                    dragItem.placedCellX = x;
                    dragItem.placedCellY = y;
                }
                else
                {
                    dragItem.transform.SetParent(inventory.transform);
                    dragItem.PlacementCells(dragItem.gameObject, dragItem.prevCell);
                }
            }
            else if (dragItem.itemSO.itemSizeY > 1)
            {
                newPos.y -= dragItem.itemSO.itemSizeY * 8;
                dragItem.transform.localPosition = newPos;
            }
            else
            {
                dragItem.prevCell = this;
                isFree = false;
                dragItem.occupied_Cells.Add(this);
                dragItem.transform.SetParent(inventory.transform);
                dragItem.placedCellX = x;
                dragItem.placedCellY = y;
            }
        }
        else
        {
            dragItem.PlacementCells(dragItem.gameObject, dragItem.prevCell);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventory.draggedItem)
        {
            if(isFree)
            {
                var dragItem = eventData.pointerDrag.GetComponent<Item>();
                if (dragItem.itemSO.itemSizeX > 1 && x < (inventory.sizeX - 1))
                {
                    cellImage.sprite = cellLeft;
                    inventory.cells[x + 1, y].cellImage.sprite = cellRight;
                }
                else if (dragItem.itemSO.itemSizeX == 1)
                {
                    cellImage.sprite = cellSingleFilled;
                }
            }    
        }    
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventory.draggedItem)
        {
            if (isFree)
            {
                var dragItem = eventData.pointerDrag.GetComponent<Item>();
                if (dragItem.itemSO.itemSizeX > 1 && x < (inventory.sizeX - 1))
                {
                    cellImage.sprite = cellSingle;
                    inventory.cells[x + 1, y].cellImage.sprite = cellSingle;
                }
                else if (dragItem.itemSO.itemSizeX == 1)
                {
                    cellImage.sprite = cellSingle;
                }
            }
        }
    }
}
