using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using ES3Internal;
using UnityEditor;

public class LoadTransforms : MonoBehaviour
{
    [Tooltip("List of Game Objects to load transforms into.")]
    public List<GameObject> gameObjectsList;

    public void LoadTransformsFromES3()
    {
        List<Transform> transformsList = ES3.Load<List<Transform>>("savedTransforms");

        if (transformsList != null)
        {
            for (int i = 0; i < gameObjectsList.Count; i++)
            {
                gameObjectsList[i].transform.position = transformsList[i].position;
                gameObjectsList[i].transform.rotation = transformsList[i].rotation;
                gameObjectsList[i].transform.localScale = transformsList[i].localScale;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LoadTransforms))]
    public class LoadTransformsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LoadTransforms loadTransforms = (LoadTransforms)target;

            if (GUILayout.Button("Save Transforms"))
            {
                List<Transform> transformsList = new List<Transform>();

                foreach (GameObject gameObject in loadTransforms.gameObjectsList)
                {
                    transformsList.Add(gameObject.transform);
                }

                ES3.Save<List<Transform>>("savedTransforms", transformsList);
            }

            if (GUILayout.Button("Load Transforms"))
            {
                loadTransforms.LoadTransformsFromES3();
            }
        }
    }
#endif
}