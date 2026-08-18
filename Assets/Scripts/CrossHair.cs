using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float aimDistance=5f;
    int layerMask;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCrossHairPos();
        layerMask = LayerMask.GetMask("Enemy","Objects");
    }
    void Update()
    {
        UpdateCrossHairPos();
    }
    private void UpdateCrossHairPos()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right,aimDistance,layerMask);

        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = firePoint.position+ firePoint.right*aimDistance;
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