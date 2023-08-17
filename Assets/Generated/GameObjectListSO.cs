using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectList", menuName = "ScriptableObjects/GameObjectList", order = 1)]
public class GameObjectListSO : ScriptableObject
{
    [Tooltip("List of game objects")]
    public GameObject[] gameObjects;
}