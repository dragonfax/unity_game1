using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLevelShip : MonoBehaviour
{

    private Quaternion lastLocalRotation;
    private float lastRotationTime;
    public float idleTimeToRotate = 2;

    void Update(float deltaTime)
    {
        /* wait until pivot hasn't changed in a second, and then lerp towards global up
		 *
		 * We verify change against the local rotation
		 * but when we actually rotate, we have to use global rotations.
		 * we're rotating to match the global UP vector.
		 */

        Quaternion currentLocalRotation = transform.localRotation;
        Quaternion currentGlobalRotation = transform.rotation;

        if (lastLocalRotation != currentLocalRotation)
        {
            lastRotationTime = Time.time;
            lastLocalRotation = currentLocalRotation;
        }
        else
        {
            // calculate preferred rotation (closes to world UP)
            Vector3 ForwardGlobal = currentGlobalRotation * Vector3.forward;
            Quaternion toRotation = Quaternion.LookRotation(ForwardGlobal, Vector3.up);

            if (currentGlobalRotation != toRotation && Time.time - lastRotationTime > idleTimeToRotate)
            {
                transform.rotation = Quaternion.Lerp(currentGlobalRotation, toRotation, deltaTime);

                // update the pivot rotation so we don't mistake it for our own rotation
                lastLocalRotation = transform.localRotation;
            }
        }
    }
}
