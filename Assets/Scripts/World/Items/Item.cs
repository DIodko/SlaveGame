using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    public Inventory inventory;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    public Vector2 positionItem;
    public ItemScriptableObject itemSO;
    public Cell prevCell;
    public int placedCellX;
    public int placedCellY;

    public Image itemDiscriptionImage;
    public TMP_Text itemName;
    public TMP_Text itemDiscription;


    public List<Cell> occupied_Cells = new List<Cell>();
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if(itemDiscriptionImage.gameObject.activeInHierarchy)
        {
            itemDiscriptionImage.transform.SetParent(inventory.transform);
            if (rectTransform.anchoredPosition.y > 0)
            {
                itemDiscriptionImage.rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 160);
            }
            else
            {
                itemDiscriptionImage.rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 160);
            }
        }
    }
    public void positioningitem(Vector2 vector2)
    {
        rectTransform.anchoredPosition = vector2;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventory.draggedItem = this;
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        OccupiedCells(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        positionItem = Input.mousePosition;
        positionItem.x -= Screen.width / 2;
        positionItem.y -= Screen.height / 2;
        rectTransform.anchoredPosition = positionItem;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        inventory.draggedItem = null;
        if (Mathf.Abs(positionItem.x) > 370)
        {
            PlacementCells(this.gameObject, this.prevCell);
        }
        else if (Mathf.Abs(positionItem.y) > 420)
        {
            PlacementCells(this.gameObject, this.prevCell);
        }
        else
        {
            OccupiedCells(false);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        dragItem.PlacementCells(dragItem.gameObject, dragItem.prevCell);
    }
    public void PlacementCells(GameObject Item, Cell prevCell)
    {   
        if (Item.GetComponent<Item>().itemSO.itemSizeX > 1)
        {
            Debug.Log(prevCell.x);
            Debug.Log(prevCell.y);
            Item.GetComponent<Item>().rectTransform.anchoredPosition = new Vector2(-256 + placedCellX * 128, 364 - placedCellY * 120);
            prevCell.cellImage.sprite = prevCell.cellLeft;
            inventory.cells[placedCellX + 1, placedCellY].cellImage.sprite = prevCell.cellRight;
            Item.GetComponent<Item>().occupied_Cells.Add(inventory.cells[placedCellX + 1, placedCellY]);
            prevCell.isFree = false;
            Item.GetComponent<Item>().occupied_Cells.Add(inventory.cells[placedCellX, placedCellY]);
            Item.transform.SetParent(inventory.transform);
        }
        else
        {
            prevCell.cellImage.sprite = prevCell.cellSingleFilled;
            Item.GetComponent<Item>().rectTransform.anchoredPosition = new Vector2(-320 + placedCellX * 128, 364 - placedCellY * 120);
            prevCell.isFree = false;
            Item.GetComponent<Item>().occupied_Cells.Add(inventory.cells[placedCellX, placedCellY]);
            Item.transform.SetParent(inventory.transform);
        }
        
    }    
    public void OccupiedCells(bool comand)
    {
        if (comand)
        {
            foreach (var cell in occupied_Cells)
            {
                cell.isFree = true;
                cell.cellImage.sprite = cell.cellSingle;
            }
            occupied_Cells.Clear();
        }
        else
        {
            foreach (var cell in occupied_Cells)
            {
                cell.isFree = false;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(itemDiscriptionImage.gameObject.activeInHierarchy)
            {
                itemDiscriptionImage.transform.SetParent(transform);
                itemDiscriptionImage.gameObject.SetActive(false);
            }    
            else
            {
                itemDiscriptionImage.gameObject.SetActive(true);
                itemName.text = itemSO.itemName;
                itemDiscription.text = itemSO.itemDiscription;
            }
        }
    }
}
