using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLevelCamera : UnityStandardAssets.Cameras.PivotBasedCameraRig {

	private Quaternion lastPivotLocalRotation;
	private float lastPivotRotationTime;
	public float idleTimeToRotate = 2;

	protected override void FollowTarget(float deltaTime) {
		/* wait until pivot hasn't changed in a second, and then lerp towards global up
		 *
		 * We verify change against the local rotation
		 * but when we actually rotate, we have to use global rotations.
		 * we're rotating to match the global UP vector.
		 */

		Quaternion currentPivotLocalRotation = m_Pivot.transform.localRotation;
		Quaternion currentPivotGlobalRotation = m_Pivot.transform.rotation;

		if ( lastPivotLocalRotation != currentPivotLocalRotation	) {
			lastPivotRotationTime = Time.time;
			lastPivotLocalRotation = currentPivotLocalRotation;
		} else {
			// calculate preferred rotation (closes to world UP)
			Vector3 pivotForwardGlobal = currentPivotGlobalRotation * Vector3.forward;
			Quaternion toRotation = Quaternion.LookRotation(pivotForwardGlobal, Vector3.up);

			if ( currentPivotGlobalRotation != toRotation && Time.time - lastPivotRotationTime > idleTimeToRotate ) {
				m_Pivot.transform.rotation = Quaternion.Lerp(currentPivotGlobalRotation, toRotation, deltaTime);

				// update the pivot rotation so we don't mistake it for our own rotation
				lastPivotLocalRotation = m_Pivot.transform.localRotation;
			}
		}
	}
}
