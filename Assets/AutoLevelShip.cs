using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLevelShip : MonoBehaviour
{

    private Quaternion lastLocalRotation;
    private float lastRotationTime;
    public float idleTimeToRotate = 2;
    private float closeAngle = 5;

    bool QuaternionsClose(Quaternion a, Quaternion b)
    {
        return a == b || Quaternion.Angle(a, b) < closeAngle;
    }

    void Update()
    {
        Quaternion currentLocalRotation = transform.localRotation;
        Quaternion currentGlobalRotation = transform.rotation;

        if (!QuaternionsClose(lastLocalRotation, currentLocalRotation))
        {
            lastRotationTime = Time.time;
            lastLocalRotation = currentLocalRotation;
            Debug.Log("manual rotated");
        }
        else
        {
            Debug.Log("not manual rotated");
            // calculate preferred rotation (closes to world UP)
            Vector3 ForwardGlobal = currentGlobalRotation * Vector3.forward;
            Quaternion toRotation = Quaternion.LookRotation(ForwardGlobal, Vector3.up);

            if (currentGlobalRotation != toRotation && Time.time - lastRotationTime > idleTimeToRotate)
            {
                Debug.Log("auto-rotated");
                transform.rotation = Quaternion.Lerp(currentGlobalRotation, toRotation, Time.deltaTime);

                // update the pivot rotation so we don't mistake it for our own rotation
                lastLocalRotation = transform.localRotation;
            }
        }
    }
}
