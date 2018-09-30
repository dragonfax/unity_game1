using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExactFollowCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig
{

    private Quaternion rDiff;
    private Quaternion lastPivotLocalRotation;
    private Quaternion lastPlayerGlobalRotation;

    // We don't move the camera with the ship if their differing view angle is too large.
    private bool tooFar;

    protected override void Start()
    {
        base.Start();

        UpdateRdiff();
        lastPlayerGlobalRotation = m_Target.rotation;
    }

    private void UpdateRdiff()
    {
        tooFar = Quaternion.Angle(m_Target.rotation, m_Pivot.rotation) > 30;
        rDiff = m_Pivot.rotation * Quaternion.Inverse(m_Target.rotation);
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

            if (!pivotLocalRotated)
            {
                // apply rDiff to pivot
                MovePivotRotation();
            }
            else
            {
                UpdateRdiff();
            }

            lastPlayerGlobalRotation = m_Target.rotation;
        }
    }
}
