using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Animator animator;

    [SerializeField] float Speed =4f;
    Vector2 movement;

    // [SerializeField] float rotateSpeed=5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        playerRb.velocity = movement*Speed;
  
    }

    void OnMove(InputValue inputvalue)
    {
        movement = inputvalue.Get<Vector2>();
        bool ismoving = movement.magnitude>0.1f;
        animator.SetBool("IsMove",ismoving);
    }
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            animator.SetBool("IsShoot", true);
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            animator.SetBool("IsShoot", false);
        }
    }
   
}
