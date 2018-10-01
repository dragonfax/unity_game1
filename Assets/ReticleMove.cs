using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMove : MonoBehaviour
{

    public Transform ship;
    public float reticleTargetDistance = 1000;

    // Update is called once per frame
    void Update()
    {
        // extend the ships aim out for a distance
        Vector3 d = Vector3.forward * reticleTargetDistance;
        // Debug.DrawRay(ship.transform.position, d, Color.red);
        Vector3 a = ship.transform.rotation * d;
        // Debug.DrawRay(ship.transform.position, a, Color.green);
        Vector3 reticleAimPosition = ship.position + a;
        // Debug.DrawLine(ship.transform.position, reticleAimPosition, Color.blue);

        // get screen coords of this position
        Vector3 hudPosition = Camera.main.WorldToScreenPoint(reticleAimPosition);

        RectTransform CanvasRect = transform.parent.GetComponent<RectTransform>();


        // move the recticle there.
        GetComponent<RectTransform>().anchoredPosition = new Vector2(hudPosition.x, hudPosition.y) - CanvasRect.sizeDelta / 2f;
    }
}
