using UnityEngine;

public class enemymovement : MonoBehaviour
{
    Rigidbody2D enemyRb;

    Transform player;

    Animator animator;

    [SerializeField] float patrolSpeed = 2f;

    [SerializeField] float chaseSpeed = 4f;

    [SerializeField] float detectionDistance = 5f;

    [SerializeField] float stopDistance = 2f;

    [SerializeField] float patrolChangeTime = 3f;

    Vector2 moveDirection;

    Vector2 currentDirection;

    float patrolTimer;

    float damageCooldown = 2f;

    float damageTimer;

    bool hasSeenPlayer = false;
    Audiomanager audiomanager;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        
        audiomanager = FindObjectOfType<Audiomanager>();

        ChooseRandomDirection();
    }

    void Update()
    {
        damageTimer += Time.deltaTime;

        patrolTimer += Time.deltaTime;

        float distance =
            Vector2.Distance(transform.position, player.position);

        
        if (distance <= detectionDistance)
        {
            hasSeenPlayer = true;
        }

        PlayerHealth playerHealth =
            player.GetComponent<PlayerHealth>();

        
        if (playerHealth != null)
        {
            if (playerHealth.isdead)
            {
                currentDirection = Vector2.zero;

                animator.SetBool("IsMove", false);

                animator.SetBool("IsAttack", false);

                return;
            }
        }

        
        if (hasSeenPlayer)
        {
            if (distance > stopDistance)
            {
                currentDirection =
                    (player.position - transform.position).normalized;

                animator.SetBool("IsMove", true);

                animator.SetBool("IsAttack", false);

                RotateEnemy(currentDirection);
            }
            else
            {
                currentDirection = Vector2.zero;

                animator.SetBool("IsMove", false);

                animator.SetBool("IsAttack", true);

                
                if (damageTimer >= damageCooldown)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(20);

                        damageTimer = 0f;
                    }
                }
            }
        }

        
        else
        {
            currentDirection = moveDirection;

            animator.SetBool("IsMove", true);

            animator.SetBool("IsAttack", false);

            RotateEnemy(moveDirection);

            if (patrolTimer >= patrolChangeTime)
            {
                ChooseRandomDirection();

                patrolTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        float currentSpeed =
            hasSeenPlayer ? chaseSpeed : patrolSpeed;

        enemyRb.MovePosition(
            enemyRb.position +
            currentDirection * currentSpeed * Time.fixedDeltaTime
        );
    }

    void ChooseRandomDirection()
    {
        moveDirection =
            Random.insideUnitCircle.normalized;
    }

    void RotateEnemy(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation =
            Quaternion.Euler(0, 0, angle);

        transform.rotation =
            Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                4f * Time.deltaTime
            );
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            ChooseRandomDirection();
        }
    }
    void playAttack()
    {
        audiomanager.playAttackSound();
    }
}