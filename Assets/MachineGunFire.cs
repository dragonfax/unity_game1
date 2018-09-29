using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MachineGunFire : MonoBehaviour
{

    public Rigidbody bullet;
    public float bulletSpeed = 10;
    public bool isPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Rigidbody clone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        clone.velocity = transform.forward * bulletSpeed;
    }
}
