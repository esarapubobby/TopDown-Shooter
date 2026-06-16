using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot_PC : MonoBehaviour
{
    public GameObject bulletprefab;

    public Transform firePoint;

    Animator animator;

    public Audiomanager audiomanager;

    public UiManager uiManager;

    public float fireRate = 0.35f;

    float nextFireTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (Input.GetMouseButton(1)
            && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            shoot();
        }
    }

    void shoot()
    {
        uiManager.bulletsShot++;

        animator.SetTrigger("IsShoot");

        audiomanager.playBulletSound();

        Instantiate(
            bulletprefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}