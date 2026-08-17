using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask objectsLayerMask;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCrossHairPos();
    }
    void Update()
    {
        UpdateCrossHairPos();
    }
    private void UpdateCrossHairPos()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right,5f,objectsLayerMask);

        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spriteRenderer.color = Color.red;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spriteRenderer.color = Color.white;
        }
        
    }
}