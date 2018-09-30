using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchPad : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Quaternion q1 = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Quaternion q2 = Quaternion.LookRotation(Vector3.up, Vector3.forward);
        Debug.Log(Vector3.Angle(q1 * Vector3.forward, q2 * Vector3.forward));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
