
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int direction = 1; // 1: phải, 2: trái, 3: lên, 4: xuống


    public void Runing()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        Launch();
    }

    private void Launch()
    {
        Vector2 moveDirection = Vector2.zero;
        switch (direction)
        {
            case 1: 
                moveDirection = Vector2.right;
                break;
            case 2: 
                moveDirection = Vector2.left;
                break;
            case 3: 
                moveDirection = Vector2.up;
                break;
            case 4:
                moveDirection = Vector2.down;
                break;
            default:
                moveDirection = Vector2.right; 
                break;
        }
        rb.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("win");
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();
           

        }
        // Nhớ đổi is trigger
        if (other.gameObject.CompareTag("War"))
        {
            Debug.Log("Lose");
        }
    }
}
