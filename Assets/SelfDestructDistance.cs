using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructDistance : MonoBehaviour
{

    private Vector3 startPosition;

    public float maxDistance;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(this);
        }
    }
}
