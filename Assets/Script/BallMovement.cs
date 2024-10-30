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
    public void OpenCollider()
    {
        ballCollider.enabled = true;
    }
    public void CloseCollider()
    {
        ballCollider.enabled = false;

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
    /* private void HandleSwipe(Vector2 swipeDirection)
     {
         // Di chuyển đối tượng kích hoạt đến vị trí mới
         Vector2 newPosition = (Vector2)transform.position - swipeDirection.normalized * spawnDistance;
         objectToActivate.SetActive(true);
         objectToActivate.transform.position = newPosition;

         // Bắt đầu quá trình tắt đối tượng và sau đó di chuyển bóng
         StartCoroutine(DeactivateObjectAndMoveBall(swipeDirection));
     }*/
    private void HandleSwipe(Vector2 swipeDirection)
    {
        // Xác định hướng di chuyển chính
        Vector2 moveDirection;

        // Kiểm tra thành phần lớn nhất của swipeDirection để xác định hướng di chuyển
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            // Di chuyển theo trục X (trái hoặc phải)
            moveDirection = swipeDirection.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            // Di chuyển theo trục Y (trên hoặc dưới)
            moveDirection = swipeDirection.y > 0 ? Vector2.up : Vector2.down;
        }

        // Di chuyển đối tượng kích hoạt đến vị trí mới
        Vector2 newPosition = (Vector2)transform.position + moveDirection * spawnDistance;
        objectToActivate.SetActive(true);
        objectToActivate.transform.position = newPosition;

        // Bắt đầu quá trình tắt đối tượng và sau đó di chuyển bóng
        StartCoroutine(DeactivateObjectAndMoveBall(moveDirection));
    }


    private IEnumerator DeactivateObjectAndMoveBall(Vector2 swipeDirection)
    {
        // Đợi thời gian tắt đối tượng
        yield return new WaitForSeconds(objectDuration);
        objectToActivate.SetActive(false);

        // Sau khi tắt objectToActivate, bóng mới di chuyển
        rb.velocity = swipeDirection.normalized * speed;

        triangleRed.SetInteractable(false); // Vô hiệu hóa tương tác với tam giác
        triangleRed1.SetInteractable(false);
        btnRotate1.OnClose(); // Đóng nút xoay
        btnRotate2.OnClose();
        ballCollider.enabled = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
        {
            rb.velocity = Vector2.zero;
            triangleRed.SetInteractable(true); 
            triangleRed1.SetInteractable(true);
            btnRotate1.OnOpen();
            btnRotate2.OnOpen();
            

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
            triangleRed.SetInteractable(true);
            triangleRed1.SetInteractable(true);
            btnRotate1.OnOpen();
            btnRotate2.OnOpen();
           

        }
    }
}
