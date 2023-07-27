using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlacer : MonoBehaviour
{
    // Start is called before the first frame update

    Transform t;
    void Start()
    {
        t = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("CubeAnchor") != null)
        {
            gameObject.transform.position = GameObject.FindGameObjectWithTag("CubeAnchor").transform.position;

        }

    }
}
