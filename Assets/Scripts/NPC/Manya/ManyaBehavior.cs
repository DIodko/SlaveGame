using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManyaBehavior : MonoBehaviour
{
    public Transform player;
    public GameObject button;
    public float reaction_distance;
    public float current_distance;

    public TMP_Text nameTMP;
    public string name;
    public GameObject dialoguePanel;
    public Image portrait;
    public Sprite portraitSprite;
    private InstantiateDialogue instDial;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
        button.SetActive(false);
        instDial = GetComponent<InstantiateDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        current_distance = Vector3.Distance(transform.position, player.position);
        if (Vector3.Distance(transform.position, player.position) < reaction_distance)
        {
            button.SetActive(true);
            if (Input.GetKeyUp("e"))
            {
                dialoguePanel.SetActive(true);
                button.SetActive(false);
                nameTMP.text = name;
                portrait.sprite = portraitSprite;
                instDial.dialPanelAwake();
            }
        }
        else
        {
            button.SetActive(false);
        }
    }
    public void hidingPanel()
    {
        dialoguePanel.SetActive(false);
    }
}
