using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class RightStickCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig {
	protected override void FollowTarget(float deltaTime) {
		m_Pivot.transform.position = m_Target.transform.position;
	}
}