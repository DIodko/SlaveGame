using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstantiateDialogue : MonoBehaviour
{
    public TextAsset ta;

    public TMP_Text answerText1;
    public TMP_Text answerText2;
    public TMP_Text answerText3;
    public Button button1;
    public Button button2;
    public Button button3;
    public TMP_Text phrase1;
    public GameObject npcDialPanel;
    public Inventory mcInventory;

    [SerializeField]
    public int currentNode = 0;

    Node[] nd;
    Dialogue dialogue;
    // Start is called before the first frame update
    private void Start()
    {
        dialogue = Dialogue.Load(ta);
        nd = dialogue.nodes;
        npcDialPanel.SetActive(false);
        button1.onClick.AddListener(but1);
        button2.onClick.AddListener(but2);
        button3.onClick.AddListener(but3);
    }
    public void dialPanelAwake()
    {
        npcDialPanel.SetActive(true);
        for (int i = 0; i < mcInventory.itemsList.Count; i++)
        {
            if (mcInventory.itemsList[i].name == dialogue.nodes[2].answers[0].needItem)
            {
                phrase1.text = dialogue.nodes[5].npcText;
                answerText1.text = dialogue.nodes[5].answers[0].text;
                answerText2.text = dialogue.nodes[5].answers[1].text;
                currentNode = 5;
                button3.gameObject.SetActive(false);
                break;
            }
            else
            {
                phrase1.text = dialogue.nodes[currentNode].npcText;
                answerText1.text = dialogue.nodes[currentNode].answers[currentNode].text;
                AnswerClicked(14);
            }
        }
    }
    public void but1()
    {
        AnswerClicked(0);
    }
    public void but2()
    {
        AnswerClicked(1);
    }
    public void but3()
    {
        AnswerClicked(2);
    }
    public void AnswerClicked(int numberOfButton)
    {
        if (numberOfButton == 14)
        {
            currentNode = 0;
        }
        else
        {
            if (dialogue.nodes[currentNode].answers[numberOfButton].end == 1)
            {
                currentNode = 0;
                npcDialPanel.SetActive(false);
                gameObject.GetComponent<ManyaBehavior>().hidingPanel();
            }
            else
            {
                currentNode = dialogue.nodes[currentNode].answers[numberOfButton].nodeId;
            }
        }
        phrase1.text = nd[currentNode].npcText;
        answerText1.text = dialogue.nodes[currentNode].answers[0].text;

        if (dialogue.nodes[currentNode].answers.Length >= 2)
        {
            button2.gameObject.SetActive(true);
            answerText2.text = dialogue.nodes[currentNode].answers[1].text;
        }
        else
        {
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        }
        if (dialogue.nodes[currentNode].answers.Length >= 3)
        {
            button3.gameObject.SetActive(true);
            answerText3.text = dialogue.nodes[currentNode].answers[2].text;
        }
        else
        {
            button3.gameObject.SetActive(false);
        }
    }
}
