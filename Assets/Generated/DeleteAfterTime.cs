using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    [Tooltip("Time in seconds before the object is deleted.")]
    public float timeToDelete = 2f;

    private void OnEnable()
    {
        Invoke("DeleteObject", timeToDelete);
    }

    private void DeleteObject()
    {
        Destroy(gameObject);
    }
}