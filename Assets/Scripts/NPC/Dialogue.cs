using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dialogue")]
public class Dialogue
{
    [XmlElement("node")]
    public Node[] nodes;

    public static Dialogue Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialogue));
        StringReader reader = new StringReader(_xml.text);
        Dialogue dial = serializer.Deserialize(reader) as Dialogue;
        return dial;
    }
}
[System.Serializable]
public class Node
{
    [XmlElement("npctext")]
    public string npcText;

    [XmlArray("answers")]
    [XmlArrayItem("answer")]
    public Answer[] answers;
}
[System.Serializable]
public class Answer
{
    [XmlAttribute("tonode")]
    public int nodeId;
    [XmlElement("text")]
    public string text;
    [XmlAttribute("dialend")]
    public int end;
    [XmlAttribute("needItem")]
    public string needItem;
}