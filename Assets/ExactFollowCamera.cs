using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ExactFollowCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig
{

    private Vector3 rDiff;
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
        tooFar = HeadingAngle(m_Target.rotation, m_Pivot.rotation) > controlAngle;
        rDiff = Heading(m_Pivot.rotation) - Heading(m_Target.rotation);
        lastPivotLocalRotation = m_Pivot.localRotation;
    }

    public static bool Approximately(Quaternion val, Quaternion about, float range)
    {
        return Quaternion.Dot(val, about) > 1f - range;
    }

    private static float HeadingAngle(Quaternion a, Quaternion b)
    {
        return Vector3.Angle(a * Vector3.forward, b * Vector3.forward);
    }

    private static Vector3 Heading(Quaternion q)
    {
        return q * Vector3.forward;
    }

    private void MovePivotRotation()
    {
        // Debug.Log(Time.frameCount + ": updating pivot");
        if (!tooFar)
        {
            Vector3 heading = Heading(m_Target.rotation) + rDiff;
            Vector3 upwards = m_Pivot.rotation * Vector3.up;
            m_Pivot.rotation = Quaternion.LookRotation(heading, upwards);
            lastPivotLocalRotation = m_Pivot.localRotation;
        }
    }

    protected override void FollowTarget(float deltaTime)
    {

        bool pivotLocalRotated = m_Pivot.localRotation != lastPivotLocalRotation;
        bool playerRotated = m_Target.rotation != lastPlayerGlobalRotation;
        // bool pivotLocalRotated = !Approximately(m_Pivot.localRotation, lastPivotLocalRotation, quatEllipsis);
        // bool playerRotated = !Approximately(m_Target.rotation, lastPlayerGlobalRotation, quatEllipsis);

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
