using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataSerializer : MonoBehaviour
{
    [SerializeField] SerializableList<Node> nodes;

    private string savePath;
    private string exportPath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/dialogue-save.json";
    }

    public void WriteSave(List<Node> nodesToSave, string path = null)
    {
        nodes.collection.Clear();

        foreach(GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            nodes.collection.Add(node.GetComponent<NodeObject>().GetNode());
        }

        string jsonData = JsonUtility.ToJson(nodes);
        if (path != null)
        {
            path = path.Replace("\\", "/");
            File.WriteAllText(path, jsonData);
        }
        else
        {
            File.WriteAllText(savePath, jsonData);
        }
        print("Saved data");
    }

    public List<Node> ReadSave(string path = null)
    {
        string jsonString;

        if (path != null)
        {
            path = path.Replace("\\", "/");
            jsonString = File.ReadAllText(path);
        }
        else
        {
            jsonString = File.ReadAllText(savePath);
        }

        nodes = JsonUtility.FromJson<SerializableList<Node>>(jsonString);
        return nodes.collection;
    }
}
