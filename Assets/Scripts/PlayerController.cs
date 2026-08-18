using UnityEngine;
using PinePie.SimpleJoystick;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Animator animator;

    [SerializeField] float Speed = 4f;

    Vector2 movement;
    Vector2 aim;


    public AudioSource audioSource;

    public JoystickController moveJoystick;
    public JoystickController AimJoystick;

    
    [SerializeField] float smoothSpeed =12f;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = moveJoystick.InputDirection;
        aim =  AimJoystick.InputDirection;

        bool isMoving = movement.magnitude > 0.1f;



        bool isAiming = aim.magnitude>0.01f;

        animator.SetBool("IsMove", isMoving);

        if (!isAiming && isMoving)
        {
            float angle =
                Mathf.Atan2(
                    movement.y,
                    movement.x
                ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,angle),smoothSpeed*Time.deltaTime);


            if (!audioSource.isPlaying)
            {
                audioSource.pitch = 2f;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if ( aim.magnitude>0.01f)
        {
            float angle =
                Mathf.Atan2(
                    aim.y,
                    aim.x
                ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,angle),smoothSpeed*Time.deltaTime);

        }
    }

    void FixedUpdate()
    {
        playerRb.velocity = movement * Speed;
    }
}