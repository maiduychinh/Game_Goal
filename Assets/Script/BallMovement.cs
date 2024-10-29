/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool hasWon = false;
    public float speed = 5f;
    private bool isMoving = true;

    private Rigidbody2D rb;
    private Collider2D ballCollider;

    private Vector2 startTouchPosition, endTouchPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
        ballCollider.enabled = false;
    }


    private void Update()
    {
        ballCollider.enabled = true;
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && isMoving)
        {
            endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            rb.velocity = swipeDirection.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D && !hasWon)
        {
            rb.velocity = Vector2.zero;
        }
        else if (other.gameObject.CompareTag("Goal") && !hasWon)
        {
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();
            hasWon = true;
        }
        else if (other.gameObject.CompareTag("War") && !hasWon)
        {
            rb.velocity = Vector2.zero;
            isMoving = false;
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool hasWon = false;
    public float speed = 5f;
    private bool isMoving = true;

    private Rigidbody2D rb;
    private Collider2D ballCollider;

    private Vector2 startTouchPosition, endTouchPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
        ballCollider.enabled = false;
    }

    private void Update()
    {
        ballCollider.enabled = true;
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && isMoving)
        {
            endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;
            rb.velocity = swipeDirection.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D && !hasWon)
        {
            rb.velocity = Vector2.zero;
        }
        else if (other.gameObject.CompareTag("Goal") && !hasWon)
        {
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();
            hasWon = true;
        }
        else if (other.gameObject.CompareTag("War") && !hasWon)
        {
            rb.velocity = Vector2.zero;
            isMoving = false;
        }
    }
}
