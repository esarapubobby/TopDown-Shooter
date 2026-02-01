using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlller : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
       
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}


