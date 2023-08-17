using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using ES3Internal;

public class SavePosition : MonoBehaviour
{
    [Tooltip("The button to save the position")]
    public Button saveButton;

    [Tooltip("List of game objects to save positions for")]
    public List<GameObject> gameObjects;

    
    [Tooltip("Serialized list of saved positions")]
    private List<Transform> savedTransforms = new List<Transform>();

    private void Start()
    {
        saveButton.onClick.AddListener(SavePositionsToES);
    }

    private void SavePositionsToES()
    {
        savedTransforms.Clear();
        foreach (GameObject obj in gameObjects)
        {
            savedTransforms.Add(obj.transform);
        }
        ES3.Save<List<Transform>>("savedTransforms", savedTransforms);
    }

}
