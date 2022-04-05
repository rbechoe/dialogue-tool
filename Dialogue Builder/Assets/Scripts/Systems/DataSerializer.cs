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

    public void WriteSave(List<Node> nodesToSave)
    {
        nodes.collection.Clear();

        foreach(GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            nodes.collection.Add(node.GetComponent<Node>());
        }

        string jsonData = JsonUtility.ToJson(nodes); 
        File.WriteAllText(savePath, jsonData);
        print("Saved data");
    }

    public List<Node> ReadSave()
    {
        string jsonString = File.ReadAllText(savePath);
        nodes = JsonUtility.FromJson<SerializableList<Node>>(jsonString);
        return nodes.collection;
    }
}
