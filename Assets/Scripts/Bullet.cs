using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject[] ImpactPrefab;
    [SerializeField] private GameObject ObjectsImpactPrefab;

    public float speed = 10f;
    public int damage = 50;
    GameObject impactPrefab;
    GameObject objectsImpactPrefab;

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
                int randomNumber = Random.Range(0,ImpactPrefab.Length);
                impactPrefab =Instantiate(ImpactPrefab[randomNumber],collision.gameObject.transform.position - collision.gameObject.transform.right*-0.5f,Quaternion.identity);
                Invoke("HideimpactPrefab",1.5f);

                enemyHealth.TakeDamage(damage);
            }
            audiomanager.playHitObjectSound();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Objects"))
        {
            audiomanager.playHitObjectSound();

            Vector2 hitPoint = collision.ClosestPoint(transform.position);

            objectsImpactPrefab = Instantiate(
                ObjectsImpactPrefab,
                hitPoint,
                Quaternion.identity
            );

            Invoke("HideobjectsImpactPrefabPrefab", 1.5f);

            Destroy(gameObject);
        }
    }
    void HideimpactPrefab()
    {
        impactPrefab.SetActive(false);
    }
        void HideobjectsImpactPrefabPrefab()
    {
        objectsImpactPrefab.SetActive(false);
    }

}
