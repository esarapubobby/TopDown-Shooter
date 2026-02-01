using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    Rigidbody2D enemyRb;

    [SerializeField] float enemySpeed;

    [SerializeField] float EnemyStopDistance=2f;

    [SerializeField] float ditectiondistance=5f;
    Transform player;
    float damageCooldown = 2f;   
    float timer = 0f;
    bool hasSeenPlayer = false;
    Animator animator;
    

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer+=Time.deltaTime;
    }
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position,player.position);
        
        //don't follow if player is far way
       if (!hasSeenPlayer)
    {
        if (distance <= ditectiondistance)
        {
            hasSeenPlayer = true;
        }
        else
        {
            return;
        }
    }

        
        //stop enemy if Enemy is close enough

        
        if (EnemyStopDistance>= distance)
        {
            Debug.Log("Hi Badri");
            animator.SetBool("IsMove", false);

            animator.SetBool("IsAttack", true);

            return;
        }

        //move Enemy

        animator.SetBool("IsMove", true); 

        animator.SetBool("IsAttack", false);
 


        Vector2 direction = (player.position-transform.position).normalized;
        enemyRb.MovePosition(enemyRb.position+direction*enemySpeed*Time.fixedDeltaTime);
        
        //rotate Enemy
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        enemyRb.rotation = angle;
        
    }
    

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(timer >= damageCooldown)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10);
                    timer=0f;
                }
            }
        }
        
    }
}
