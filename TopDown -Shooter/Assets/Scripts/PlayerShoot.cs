using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletprefab;

    public Transform firePoint;

    Animator animator;
    public Audiomanager audiomanager;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shoot();
        } 
    }
    void shoot()
    {
        animator.SetTrigger("IsShoot");
        audiomanager.playBulletSound();
        Instantiate(bulletprefab,firePoint.position,firePoint.rotation);
    }
}
