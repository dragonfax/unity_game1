using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ExactFollowCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig
{

    private Quaternion rDiff;
    private Quaternion lastPivotLocalRotation;
    private Quaternion lastPlayerGlobalRotation;

    // We don't move the camera with the ship if their differing view angle is too large.
    private bool tooFar;
    public float controlAngle = 30;
    public float quatEllipsis = 0.01f;

    protected override void Start()
    {
        base.Start();

        UpdateRdiff();
        lastPlayerGlobalRotation = m_Target.rotation;
    }

    private void UpdateRdiff()
    {
        tooFar = Quaternion.Angle(m_Target.rotation, m_Pivot.rotation) > controlAngle;
        rDiff = m_Pivot.rotation * Quaternion.Inverse(m_Target.rotation);
        Assert.IsTrue(Approximately(m_Target.rotation * rDiff, m_Pivot.rotation, quatEllipsis));
        lastPivotLocalRotation = m_Pivot.localRotation;
    }

    public static bool Approximately(Quaternion val, Quaternion about, float range)
    {
        return Quaternion.Dot(val, about) > 1f - range;
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

        bool pivotLocalRotated = !Approximately(m_Pivot.localRotation, lastPivotLocalRotation, quatEllipsis);
        bool playerRotated = !Approximately(m_Target.rotation, lastPlayerGlobalRotation, quatEllipsis);

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
