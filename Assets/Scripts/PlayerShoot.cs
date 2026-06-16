using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
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

    public void Shoot()
    {
        if (Time.timeScale == 0f)
            return;

        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;

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