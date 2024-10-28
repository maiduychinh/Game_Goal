using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool hasWon = false; 
    public float speed = 5f;
    public Rigidbody2D rb;
    public int direction = 1; 
    private readonly bool isMoving = true;

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
        if (!isMoving) return;
        _ = Vector2.zero;
        Vector2 moveDirection = direction switch
        {
            1 => Vector2.right,
            2 => Vector2.left,
            3 => Vector2.up,
            4 => Vector2.down,
            _ => Vector2.right,
        };
        rb.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal") && !hasWon)
        {
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();

            hasWon = true;
        }
        else if (other.gameObject.CompareTag("War") && !hasWon)
        {
            UiController.instance.gameOver.OnOpen();
            GameController.instance.DestroyLevel();
            hasWon = true;
        }




    }
}

 