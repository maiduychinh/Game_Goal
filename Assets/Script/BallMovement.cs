using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool hasWon = false;
    public float speed = 5f;

    public BtnRotate btnRotate1;
    public BtnRotate btnRotate2;
    private Rigidbody2D rb;
    private Collider2D ballCollider;
    public TriangleControl triangleRed;
    public TriangleControl triangleRed1;
    public CountdownTimer countdown;
    public GameObject objectToActivate; // Đối tượng cần kích hoạt
    public float spawnDistance = 2f; // Khoảng cách để di chuyển đối tượng
    public float objectDuration = 0.3f; // Thời gian đối tượng tồn tại

    private Vector2 startTouchPosition, endTouchPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (ballCollider.OverlapPoint(touchPosition))
            {
                startTouchPosition = touchPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0) && startTouchPosition != Vector2.zero)
        {
            endTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            HandleSwipe(swipeDirection); 

            startTouchPosition = Vector2.zero;
            countdown.OnStart();

        }
    }

    private void HandleSwipe(Vector2 swipeDirection)
    {
        
        Vector2 newPosition = (Vector2)transform.position - swipeDirection.normalized * spawnDistance;
        objectToActivate.SetActive(true);
        objectToActivate.transform.position = newPosition; 

        // SetActive
        StartCoroutine(DeactivateObjectAfterDelay(objectDuration));

        // V ball
        rb.velocity = swipeDirection.normalized * speed;

        triangleRed.SetInteractable(false);
        triangleRed1.SetInteractable(false);
        btnRotate1.OnClose();
        btnRotate2.OnClose();
    }

    private IEnumerator DeactivateObjectAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration); //Time anim
        objectToActivate.SetActive(false); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
        {
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Goal") && !hasWon)
        {
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();
            hasWon = true;
        }
        else if (collision.gameObject.CompareTag("War") && !hasWon)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
