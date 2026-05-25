using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiomanager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource BackGroundmusicSource;

    public AudioClip 
    bulletSound,
    hitSound,
    enemyAttackSound,
    DeathSound,
    wavesound,
    HoverSound,
    clickSound,
    VictorySound,
    healthPickUpSound;

    public Slider musicSlider;

    public Slider sfxSlider;
    void Start()
    {
        

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

    public void playVictorySound()
    {
        audioSource.PlayOneShot(VictorySound);
    }

    public void playHealthPickUpSound()
    {
        audioSource.PlayOneShot(healthPickUpSound);
    }

    public void playHoverSound()
    {
        audioSource.PlayOneShot(HoverSound);
    }

    public void playClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void playWaveSound()
    {
        audioSource.PlayOneShot(wavesound);
    }



    void SetMusicVolume(float value)
    {
        BackGroundmusicSource.volume = value;
    }

    void SetSFXVolume(float value)
    {
        audioSource.volume = value;
    }
    
}
