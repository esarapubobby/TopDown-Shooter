using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject[] ImpactPrefab;
    public float speed = 10f;
    public int damage = 50;
    GameObject impact;

    Audiomanager audiomanager;
    void Start()
    {
        Destroy(gameObject,2f);
        audiomanager = FindObjectOfType<Audiomanager>();
    }

    
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
         if(collision.gameObject.tag == "Enemy" || collision.CompareTag("Boss"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                int randomNumber = Random.Range(0,2);
                impact =Instantiate(ImpactPrefab[randomNumber],collision.gameObject.transform.position,Quaternion.identity);
                enemyHealth.TakeDamage(damage);
            }
            audiomanager.playHitObjectSound();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Objects" )
        {
            audiomanager.playHitObjectSound();
            Destroy(gameObject);
        }
    }


}
