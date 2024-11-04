using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Vector2 currentDirection; // Lưu hướng di chuyển hiện tại của bóng
    private bool hasWon = false;
    public float speed = 5f;
    public War war;
    public Goal goal;
    public BtnRotate btnRotate1;
    public BtnRotate btnRotate2;
    public Rigidbody2D rb;
    private Collider2D ballCollider;
    public TriangleControl triangleRed2;
    public TriangleControl triangleRed1;
    public CountdownTimer countdown;
    public GameObject objectToActivate; 
    public float spawnDistance = 2f;
    public float objectDuration = 0.3f;
    public bool OnPosition = false;
    private Vector2 startTouchPosition, endTouchPosition;
    public Vector2 initialPositionBall; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
        initialPositionBall = transform.position;
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
    public void UpdateinitialPositionBall()
    {
        initialPositionBall = transform.position;
    }


    private void HandleSwipe(Vector2 swipeDirection)
    {
        Vector2 moveDirection;
        // Kiểm tra hướng và gán giá trị cho currentDirection
        currentDirection = Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y) ?
                           (swipeDirection.x > 0 ? Vector2.right : Vector2.left) :
                           (swipeDirection.y > 0 ? Vector2.up : Vector2.down);
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            moveDirection = swipeDirection.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            moveDirection = swipeDirection.y > 0 ? Vector2.up : Vector2.down;
        }
        war.SetBodyType(RigidbodyType2D.Kinematic);
        Vector2 newPosition = (Vector2)transform.position - swipeDirection.normalized * spawnDistance;
        objectToActivate.SetActive(true);
        objectToActivate.transform.position = newPosition;
        
        StartCoroutine(DeactivateObjectAndMoveBall(moveDirection));
    }


    private IEnumerator DeactivateObjectAndMoveBall(Vector2 swipeDirection)
    {
        yield return new WaitForSeconds(objectDuration);
        objectToActivate.SetActive(false);

        rb.velocity = swipeDirection.normalized * speed;

        triangleRed2.SetInteractable(false); 
        triangleRed1.SetInteractable(false);
        triangleRed2.OnEdgeCollider2D();
        triangleRed1.OnEdgeCollider2D();
        btnRotate1.OnClose(); 
        btnRotate2.OnClose();
        

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.collider is EdgeCollider2D)
        {
            Debug.Log("EdgeCollider2D");
            rb.velocity = Vector2.zero;
            initialPositionBall = transform.position;

            triangleRed2.OffEdgeCollider2D();
            triangleRed1.OffEdgeCollider2D();
            war.SetBodyType(RigidbodyType2D.Dynamic);
            goal.SetBodyType(RigidbodyType2D.Dynamic);

            triangleRed2.NewinitialPosition();
            triangleRed1.NewinitialPosition();
            StartCoroutine(ActivateTrianglesAndButtonsWithDelay());
        }
        else if (collision.gameObject.CompareTag("Goal") && !hasWon)
        {
            GameController.instance.DoWin();
            GameController.instance.DestroyLevel();
            hasWon = true;
        }
        else if (collision.gameObject.CompareTag("War") && !hasWon)
        {
            Debug.Log("War");
            initialPositionBall = transform.position;
            ResetPosition();
            rb.velocity = Vector2.zero;
           
            triangleRed2.OffEdgeCollider2D();
            triangleRed1.OffEdgeCollider2D();
            triangleRed2.NewinitialPosition();
            triangleRed1.NewinitialPosition();
            war.SetBodyType(RigidbodyType2D.Dynamic);
            goal.SetBodyType(RigidbodyType2D.Dynamic);
            StartCoroutine(ActivateTrianglesAndButtonsWithDelay());
        }
       
    }
    public void ResetPosition()
    {
        transform.position = initialPositionBall;
        
    }
    public IEnumerator ActivateTrianglesAndButtonsWithDelay()
    {
        yield return new WaitForSeconds(0.2f);

        triangleRed2.SetInteractable(true);
        triangleRed1.SetInteractable(true);
        
        btnRotate1.OnOpen();
        btnRotate2.OnOpen();
    }

}
