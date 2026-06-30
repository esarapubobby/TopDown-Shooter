using UnityEngine;
using PinePie.SimpleJoystick;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Animator animator;

    [SerializeField] float Speed = 4f;

    Vector2 movement;

    public AudioSource audioSource;

    public JoystickController moveJoystick;

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

        bool isMoving = movement.magnitude > 0.1f;

        animator.SetBool("IsMove", isMoving);

        if (isMoving)
        {
            float angle =
                Mathf.Atan2(
                    movement.y,
                    movement.x
                ) * Mathf.Rad2Deg;

            transform.rotation =
                Quaternion.Euler(
                    0,
                    0,
                    angle
                );

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
    }

    void FixedUpdate()
    {
        playerRb.velocity = movement * Speed;
    }
}