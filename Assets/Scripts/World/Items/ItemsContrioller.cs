using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsContrioller : MonoBehaviour
{
    public Transform player;
    public GameObject button;
    public float reaction_distance;
    public float current_distance;

    public GameObject objectInventoryPanel;
    public GameObject objectBagInventory;
    public List<Item> itemsList = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        objectInventoryPanel.SetActive(false);
    }

    void Update()
    {
        current_distance = Vector3.Distance(transform.position, player.position);
        if (Vector3.Distance(transform.position, player.position) < reaction_distance)
        {
            button.SetActive(true);
            if (Input.GetKeyUp("e"))
            {
                Debug.Log("Ты подобрал " + name);
                objectInventoryPanel.SetActive(true);
                objectInventoryPanel.SetActive(true);
                objectBagInventory.SetActive(true);
                button.SetActive(false);
            }
        }
        else
        {
            button.SetActive(false);
        }
    }
}
