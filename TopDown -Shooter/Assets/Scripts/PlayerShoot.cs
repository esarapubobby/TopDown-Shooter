using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletprefab;

    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shoot();
        } 
    }
    void shoot()
    {
        Instantiate(bulletprefab,firePoint.position,firePoint.rotation);
    }
}
