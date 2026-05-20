using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip 
    bulletSound,
    hitSound,
    enemyAttackSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playBulletSound()
    {
        audioSource.PlayOneShot(bulletSound);
    }

    public void playHitObjectSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void playAttackSound()
    {
        audioSource.PlayOneShot(enemyAttackSound);
    }
    
}
