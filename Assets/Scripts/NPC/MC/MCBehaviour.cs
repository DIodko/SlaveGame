using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Equip_Inventory;
    public GameObject Bag_Inventory;
    public GameObject uiInventory;
    private Inventory inventoryScript;
    public Animator anim;

    public Item someItem;

    public float speed;
    public bool moveY;
    public bool moveX;
    // Start is called before the first frame update
    void Start()
    {
        Equip_Inventory.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        inventoryScript = uiInventory.GetComponent<Inventory>();
        uiInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            if (Input.GetKey("a"))
            {
                anim.SetTrigger("Walk_Left_Behind");
                rb.velocity = new Vector2(-speed / (Mathf.Sqrt(2)), speed / (Mathf.Sqrt(2)));
            }
            else if (Input.GetKey("d"))
            {
                anim.SetTrigger("Walk_Right_Behind");
                rb.velocity = new Vector2(speed / (Mathf.Sqrt(2)), speed / (Mathf.Sqrt(2)));
            }
            else
            {
                anim.SetTrigger("Walk_Behind");
                rb.velocity = new Vector2(0, speed);
            }
        }
        else if (Input.GetKey("s"))
        {
            if (Input.GetKey("a"))
            {
                anim.SetTrigger("Walk_Left_Forward");
                rb.velocity = new Vector2(-speed / (Mathf.Sqrt(2)), -speed / (Mathf.Sqrt(2)));
            }
            else if (Input.GetKey("d"))
            {
                anim.SetTrigger("Walk_Right_Forward");
                rb.velocity = new Vector2(speed / (Mathf.Sqrt(2)), -speed / (Mathf.Sqrt(2)));
            }
            else
            {
                anim.SetTrigger("Walk_Forward");
                rb.velocity = new Vector2(0, -speed);
            }
        }
        else if (Input.GetKey("a"))
        {
            anim.SetTrigger("Walk_Left");
            rb.velocity = new Vector2(-speed, 0);
        }
        else if (Input.GetKey("d"))
        {
            anim.SetTrigger("Walk_Right");
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }


        if ((Input.GetKeyUp("f")))
        {
            if (Equip_Inventory.activeInHierarchy == true)
            {
                Equip_Inventory.SetActive(false);
            }
            else
            {
                Equip_Inventory.SetActive(true);
            }
        }
        if ((Input.GetKeyUp("g")))
        {
            if (Bag_Inventory.activeInHierarchy == true)
            {
                uiInventory.SetActive(false);
                Bag_Inventory.SetActive(false);
            }
            else
            {
                uiInventory.SetActive(true);
                Bag_Inventory.SetActive(true);
            }
        }
        //if ((Input.GetKeyUp("1")))
        //{
        //    inventoryScript.SpawnItem(someItem);
        //}
    }
    public void AddItem()
    {

    }
}
