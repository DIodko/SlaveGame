using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform transformCells;
    public int sizeX;
    public int sizeY;
    public Cell cellPrefab;
    public Cell[,] cells;
    public Item draggedItem;
    public GameObject inventoryPanel;
    public List<GameObject> itemsList = new List<GameObject>();

    public Button button1;

    public List<Item> itemsWaitList = new List<Item>();

    void Start()
    {
        inventoryPanel.SetActive(true);
        button1.onClick.AddListener(but1);
        cells = new Cell[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                var newCell = Instantiate(cellPrefab, transformCells);
                newCell.x = x;
                newCell.y = y;
                newCell.isFree = true;
                cells[x, y] = newCell;
            }
        }
        if (itemsList.Count > 0)
        {
            SpawnItems();
        }
    }

    public void but1()
    {
        this.gameObject.SetActive(false);
    }

    public void SpawnItems()
    {
        for (int itemNumber = 0; itemNumber < itemsList.Count; itemNumber++)
        {
            bool spawn = false;
            for (int y = 0; y < sizeY && spawn == false; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    if(cells[x, y].isFree)
                    {
                        if (itemsList[itemNumber].GetComponent<Item>().itemSO.itemSizeX > 1)
                        {
                            if (x < sizeX && cells[x + 1, y].isFree)
                            {
                                GameObject spawnedItem = (GameObject)Instantiate(itemsList[itemNumber], cells[x, y].transform);
                                spawnedItem.transform.SetParent(this.transform);
                                Vector2 vectorNew1 = new Vector2(-((inventoryPanel.GetComponent<RectTransform>().sizeDelta.x * 4 - 16) / sizeX) * 2 + 128 * x, 364 - y * 120);
                                spawnedItem.GetComponent<RectTransform>().anchoredPosition = vectorNew1;
                                var spawnedItemScript = spawnedItem.GetComponent<Item>();
                                cells[x, y].cellImage.sprite = cells[x, y].cellLeft;
                                cells[x + 1, y].cellImage.sprite = cells[x + 1, y].cellRight;
                                if (!spawnedItemScript.occupied_Cells.Contains(cells[x, y]))
                                    spawnedItemScript.occupied_Cells.Add(cells[x, y]);
                                if (!spawnedItemScript.occupied_Cells.Contains(cells[x + 1, y]))
                                    spawnedItemScript.occupied_Cells.Add(cells[x + 1, y]);
                                spawnedItemScript.OccupiedCells(false);
                                spawnedItemScript.placedCellX = x;
                                spawnedItemScript.placedCellY = y;
                                spawnedItemScript.prevCell = cells[x, y];
                                spawn = true;
                                break;
                            }
                        }
                        else
                        {
                            GameObject spawnedItem = (GameObject)Instantiate(itemsList[itemNumber], cells[x, y].transform);
                            spawnedItem.transform.SetParent(this.transform);
                            Vector2 vectorNew = new Vector2(-320 + x * 128, 364 - y * 120);
                            Vector2 vectorNew1 = new Vector2(-((inventoryPanel.GetComponent<RectTransform>().sizeDelta.x * 4 - 16) / 2 / sizeX) * (sizeX - 1), 364 - y * 120);
                            Debug.Log(((inventoryPanel.GetComponent<RectTransform>().sizeDelta.x * 4 - 16) / 2 / sizeX) * (sizeX - 1));
                            spawnedItem.GetComponent<RectTransform>().anchoredPosition = vectorNew1;
                            cells[x, y].cellImage.sprite = cells[x, y].cellSingleFilled;
                            var spawnedItemScript = spawnedItem.GetComponent<Item>();
                            spawnedItemScript.occupied_Cells.Add(cells[x, y]);
                            spawnedItemScript.OccupiedCells(false);
                            spawnedItemScript.placedCellX = x;
                            spawnedItemScript.placedCellY = y;
                            spawnedItemScript.prevCell = cells[x, y];
                            spawn = true;
                            break;
                        }
                    }
                }
            }

        }
        inventoryPanel.SetActive(false);
    }
    public void SpawnItem(Item itemPrefub)
    {
        bool spawn = false;
        for (int y = 0; y < sizeY && spawn == false; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if (cells[x, y].isFree)
                {
                    if (itemPrefub.itemSO.itemSizeX > 1)
                    {
                        if (x < sizeX && cells[x + 1, y].isFree)
                        {
                            Item spawnedItem = (Item)Instantiate(itemPrefub, cells[x, y].transform);
                            spawnedItem.transform.SetParent(this.transform);
                            Vector2 vectorNew = new Vector2(-256 + x * 128, 364 - y * 120);
                            spawnedItem.GetComponent<RectTransform>().anchoredPosition = vectorNew;
                            //spawnedItem.positionItem.x += spawnedItem.itemSO.itemSizeX * 8;
                            cells[x, y].cellImage.sprite = cells[x, y].cellLeft;
                            cells[x + 1, y].cellImage.sprite = cells[x + 1, y].cellRight;
                            if (!spawnedItem.occupied_Cells.Contains(cells[x, y]))
                                spawnedItem.occupied_Cells.Add(cells[x, y]);
                            if (!spawnedItem.occupied_Cells.Contains(cells[x + 1, y]))
                                spawnedItem.occupied_Cells.Add(cells[x + 1, y]);
                            spawnedItem.OccupiedCells(false);
                            spawn = true;
                            break;
                        }
                    }
                    else
                    {
                        var spawnedItem = Instantiate(itemPrefub, cells[x, y].transform);
                        cells[x, y].cellImage.sprite = cells[x, y].cellSingleFilled;
                        spawnedItem.occupied_Cells.Add(cells[x, y]);
                        spawnedItem.OccupiedCells(false);
                        spawn = true;
                        break;
                    }
                }
            }
        }
    }
}
