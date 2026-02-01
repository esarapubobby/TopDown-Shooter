using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 50;
    void Start()
    {
        Destroy(gameObject,2f);
    }

    
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

}
