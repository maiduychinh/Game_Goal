
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f; 
    public Rigidbody2D rb;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        Launch(); 
    }
    private void Launch()
    {
        
        rb.velocity = new Vector2(speed, 0);
    }
   


}
