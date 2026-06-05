using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_PC : MonoBehaviour
{
    Rigidbody2D playerRb;
    Animator animator;

    [SerializeField] float Speed = 4f;
    Vector2 movement;

    public AudioSource audioSource;

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
        Vector2 moveDirection =
            (Vector2)transform.right * movement.y +
            (Vector2)transform.up * movement.x;

        playerRb.velocity = moveDirection * Speed;
    }

    public void OnMove(InputValue inputvalue)
    {
        movement = inputvalue.Get<Vector2>();

        bool ismoving = movement.magnitude > 0.1f;

        animator.SetBool("IsMove", ismoving);

        if (ismoving)
        {
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
}