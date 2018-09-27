using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class RightStickCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig {
	public float rotationBuffer = 1;
	public float pivotRotationSpeed = 10;

	protected override void FollowTarget(float deltaTime) {
		m_Pivot.transform.position = m_Target.transform.position;

		float x = CrossPlatformInputManager.GetAxis("Mouse X");
		float y = CrossPlatformInputManager.GetAxis("Mouse Y");
		// 	float angle = Mathf.Atan2(y,x);

		// if x is positive and y is 0, we want to rotate around UP
		// if y is positive and x is 0, we want to rotate around RIGHT
		// if y and x are both positive, we want to rotate around UP_RIGHT
		// so we take the vecdtor RIGHT, rotate it according to angle of joystick
		// we're rotating it around the forward axis of the camera.

        Quaternion q = Quaternion.Euler(y * rotationBuffer, x * rotationBuffer, 0);

        // Dampen towards the target rotation

/*
		float angle = 0.0F;
		Vector3 axis = Vector3.zero;
		q.ToAngleAxis(out angle, out axis);

		// we want to apply against the local rotation axis.
		// m_Cam.transform.TransformDirection(Vector3.up) and right

		m_Cam.transform.RotateAround(m_Pivot.position, axis, angle);
		*/

		m_Pivot.transform.rotation = m_Pivot.transform.rotation * q;
	}
}