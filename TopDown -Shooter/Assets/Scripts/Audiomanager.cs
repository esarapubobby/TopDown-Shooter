using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiomanager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioSource musicSource;

    public AudioClip 
    bulletSound,
    hitSound,
    enemyAttackSound;

    public Slider musicSlider;

    public Slider sfxSlider;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

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



    void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    void SetSFXVolume(float value)
    {
        audioSource.volume = value;
    }
    
}
