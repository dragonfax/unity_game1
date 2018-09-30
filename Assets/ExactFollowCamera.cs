using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExactFollowCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig
{

    private Quaternion rDiff;
    private Quaternion lastPivotLocalRotation;
    private Quaternion lastPlayerGlobalRotation;
    private bool tooFar;

    protected override void Start()
    {
        base.Start();

        UpdateRdiff();
        lastPlayerGlobalRotation = m_Target.rotation;
    }

    private void UpdateRdiff()
    {
        if (Quaternion.Angle(m_Target.rotation, m_Pivot.rotation) > 30)
        {
            // rotating too far
            // Debug.Log("rotated too far");
            tooFar = true;
        }
        else
        {
            tooFar = false;
            rDiff = m_Pivot.rotation * Quaternion.Inverse(m_Target.rotation);
        }
        lastPivotLocalRotation = m_Pivot.localRotation;
    }

    private void MovePivotRotation()
    {
        // Debug.Log(Time.frameCount + ": updating pivot");
        if (!tooFar)
        {
            m_Pivot.rotation = m_Target.rotation * rDiff;
            lastPivotLocalRotation = m_Pivot.localRotation;
        }
    }

    protected override void FollowTarget(float deltaTime)
    {

        bool pivotLocalRotated = m_Pivot.localRotation != lastPivotLocalRotation;
        bool playerRotated = m_Target.rotation != lastPlayerGlobalRotation;

        if (pivotLocalRotated)
        {
            // player is moving the camera

            UpdateRdiff();
        }
        if (playerRotated)
        {
            // player is moving the ship

            // apply rDiff to pivot
            if (!pivotLocalRotated)
            {
                MovePivotRotation();
            }

            lastPlayerGlobalRotation = m_Target.rotation;
        }
    }
}
