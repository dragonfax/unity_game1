using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    /* it close to player, circle him.
	 * if far from player, fly towards player
	 * if front frustrum contains player (30 degrees), then fire
	 */

    private Transform m_Player; // Reference to the player's transform.
    public float circleDistance = 100;
    private UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController m_Aeroplane;


    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Aeroplane = GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>();
    }

    private void FixedUpdate()
    {
        m_Aeroplane.Move(0, 0, 0, 1, false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = m_Player.position - transform.position;
        float dot = Vector3.Dot(heading, transform.forward);
        if (dot < 0.5)
        {
            // with 30 degrees in front of us.
            MachineGunFire[] machineGuns = GetComponentsInChildren<MachineGunFire>();
            foreach (MachineGunFire mg in machineGuns)
            {
                mg.Fire();
            }
        }

        if (Vector3.Distance(m_Player.position, transform.position) > circleDistance)
        {
            // move towards player
            Quaternion playerDirection = Quaternion.LookRotation(heading, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerDirection, Time.deltaTime);
            // draw the direction we want to be going.
            Debug.DrawRay(transform.position, heading);
        }
        else
        {
            // circle player

            // get a normal to the vector "towards" the player.
            // then lerp towards that.
            Vector3 perpV = Vector3.Cross(heading, Vector3.up).normalized;
            Quaternion perpQ = Quaternion.LookRotation(perpV, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, perpQ, Time.deltaTime);
            // draw the direction we want to be going.
            Debug.DrawRay(transform.position, perpV * 10);
        }
    }
}
