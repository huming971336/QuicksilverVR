using UnityEngine;

using System.Collections;
using Photon.Pun;

public class GrowThorns : MonoBehaviour
{
    public Vector3 targetScale = Vector3.one; // The scale you want to reach.
    public float duration = 2.0f; // The duration of the scaling animation.

    private Vector3 initialScale;
    private float startTime;

    private void Start()
    {
        // Store the initial scale and start time.
        initialScale = transform.localScale;
        startTime = Time.time;

        // Start the scaling animation.
        StartCoroutine(ScaleOverTime());
    }

    private IEnumerator ScaleOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current scale based on the interpolation factor.
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // Update the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly the target scale.
        transform.localScale = targetScale;
    }
}