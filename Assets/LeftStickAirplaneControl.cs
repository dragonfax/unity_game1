using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles;
using UnityStandardAssets.CrossPlatformInput;

public class LeftStickAirplaneControl : MonoBehaviour
{

    public float rotationSpeed = 1;

    private UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController m_Aeroplane;

    private void Awake()
    {
        m_Aeroplane = GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    private void FixedUpdate()
    {
        m_Aeroplane.Move(0, 0, 0, 1, false);
    }

    // Update is called once per frame
    void Update()
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float y = CrossPlatformInputManager.GetAxis("Vertical");
        Quaternion q = Quaternion.Euler(y * rotationSpeed, x * rotationSpeed, 0);

        transform.rotation = transform.rotation * q;
    }
}
