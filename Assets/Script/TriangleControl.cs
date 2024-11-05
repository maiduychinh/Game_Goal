using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    public Goal goal;
    public War war;
    public GameObject button; 
    public GameObject A; 
    public GameObject B; 

    public BallMovement ballMovement;


    private bool isDragging = false; 
    private bool isInteractable = true; 
    private EdgeCollider2D edgeCollider; 
    private PolygonCollider2D polygonCollider;
    private Vector2 dragOffset; 
    private Vector2 initialPosition; 
    private SpriteRenderer spriteRenderer; 
    private Color originalColor;
    private Vector2 pa; 
    private Vector2 pb;
    private int edgeColliderHitCount = 0; 
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        originalColor = spriteRenderer.color; 
        polygonCollider = GetComponent<PolygonCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>(); 
        SetButtonPosition(); 
        SetB();
        SetA();
        NewinitialPosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("War")) 
        {
            spriteRenderer.color = Color.yellow; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("War")) 
        {
            spriteRenderer.color = originalColor; 
        }
    }
    public void NewinitialPosition()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        if (isInteractable && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            if (IsTouchingTriangle())
            {
                isDragging = true;
                Vector2 touchPosition = GetTouchPosition();
                dragOffset = (Vector2)transform.position - touchPosition; 
                OffEdgeCollider2D(); 
                war.SetBodyType(RigidbodyType2D.Dynamic);
                war.SetTriggerState(true);

                goal.SetBodyType(RigidbodyType2D.Dynamic);
                goal.SetTriggerState(true);
                NewinitialPosition();
            }
        }

        if (isDragging)
        {
            Vector2 touchPosition = GetTouchPosition();
            transform.position = touchPosition + dragOffset; 
            ballMovement.CloseCollider();
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            
            isDragging = false; 
            OffEdgeCollider2D();
            ballMovement.OpenCollider();
            war.SetBodyType(RigidbodyType2D.Dynamic);
            war.SetBodyType(RigidbodyType2D.Dynamic);
            war.SetTriggerState(false);
            goal.SetTriggerState(false);
            goal.SetBodyType(RigidbodyType2D.Dynamic);
            goal.SetBodyType(RigidbodyType2D.Dynamic);

        }
        if (isDragging== false)
        {
            foreach (Collider2D warCollider in war.colliders)
            {
                if (warCollider != null && polygonCollider.IsTouching(warCollider))
                {
                    ResetPosition();
                    break;

                }
            }
            foreach (Collider2D goalCollider in goal.colliders)
            {
                if (goalCollider != null && polygonCollider.IsTouching(goalCollider))
                {
                    ResetPosition();
                    break;

                }
            }

        }

        if (polygonCollider.IsTouching(ballMovement.GetComponent<Collider2D>()) && edgeCollider.enabled == false)
        {
            ResetPosition();
            ballMovement.ResetPosition();
            
           
            ballMovement.rb.velocity = Vector2.zero;
            ballMovement.UpdateinitialPositionBall();
            

            OffEdgeCollider2D();
            
            war.SetBodyType(RigidbodyType2D.Dynamic);
            goal.SetBodyType(RigidbodyType2D.Dynamic);

            NewinitialPosition();
            
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       
        if (edgeCollider.IsTouching(ballMovement.GetComponent<Collider2D>()) && edgeCollider.enabled == true)
        {
            edgeColliderHitCount++;

           
            if (edgeColliderHitCount % 2 != 0)
            {
                ballMovement.UpdateinitialPositionBall();
                ballMovement.rb.velocity = Vector2.zero;
                OffEdgeCollider2D();

                war.SetBodyType(RigidbodyType2D.Dynamic);
                goal.SetBodyType(RigidbodyType2D.Dynamic);

                NewinitialPosition();
                StartCoroutine(ballMovement.ActivateTrianglesAndButtonsWithDelay());
            }
            return;
        }
        if (polygonCollider.IsTouching(ballMovement.GetComponent<Collider2D>()) && edgeCollider.enabled == true)
        {
           
            ballMovement.rb.velocity = Vector2.zero;
            ChangeDirection();
            StartCoroutine(ballMovement.ActivateTrianglesAndButtonsWithDelay());
            ballMovement.UpdateinitialPositionBall();

        }
    }




    public void OnEdgeCollider2D()
    {
        edgeCollider.enabled = true;
    }

    public void OffEdgeCollider2D()
    {
        edgeCollider.enabled = false;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        SetA(); 
        SetB(); 
    }

    private Vector2 GetTouchPosition()
    {
        Vector2 touchPosition = Vector2.zero;

        if (Input.touchCount > 0)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        return touchPosition;
    }

    private bool IsTouchingTriangle()
    {
        Vector2 touchPos = GetTouchPosition();
        Collider2D collider = Physics2D.OverlapPoint(touchPos);
        return collider != null && collider.gameObject == gameObject;
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable; 
    }

    public void OnRotation()
    {

        transform.Rotate(0, 0, 90);
       
    }


    private void SetA()
    {
        Vector2[] points = polygonCollider.points;
        if (points.Length > 0)
        {
            pa = transform.TransformPoint(points[5]); 
        }
    }

    private void SetB()
    {
        Vector2[] points = polygonCollider.points;
        if (points.Length > 0)
        {
            pb = transform.TransformPoint(points[0]); 

        }
    }


    private void SetButtonPosition()
    {
        Vector2[] points = polygonCollider.points;

        if (points.Length > 0)
        {
            Vector3 topVertex = transform.TransformPoint(points[0]); 
            Vector3 offset = new Vector3(0, -button.GetComponent<RectTransform>().rect.height / 2, 0);
            button.transform.position = topVertex + offset; 
        }
    }
   
    public void ChangeDirection()
    {
        SetA();
        SetB();
        Vector2 pa = A.transform.position;
        Vector2 pb = B.transform.position;
        
        if (ballMovement.currentDirection == Vector2.right )
        {
           
            
                if (pa.x > pb.x && pa.y > pb.y && ballMovement.initialPositionBall.y > (pb.y - 17 / 25))
                {

                    ballMovement.currentDirection = Vector2.up;
                    ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;
                }
                else if (pa.x < pb.x && pa.y > pb.y && ballMovement.initialPositionBall.y < (pa.y + 90 / 100))
                {
                    ballMovement.currentDirection = Vector2.down;
                    ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;

                }
        }
        else if (ballMovement.currentDirection == Vector2.left )
        {
            if (pa.x < pb.x && pa.y < pb.y && ballMovement.initialPositionBall.y < pb.y)
            {
                ballMovement.currentDirection = Vector2.down;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;


            }

            else if (pa.x > pb.x && pa.y < pb.y && ballMovement.initialPositionBall.y > (pa.y - 109 / 1000))
            {

                ballMovement.currentDirection = Vector2.up;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;


            }

        }
        else if (ballMovement.currentDirection == Vector2.up  )
        {
            if (pa.x < pb.x && pa.y > pb.y && ballMovement.initialPositionBall.x < pb.x)
            {
                ballMovement.currentDirection = Vector2.left;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;

            }

            else if (pa.x < pb.x && pa.y < pb.y && ballMovement.initialPositionBall.x > pa.x + 109 / 1000)
            {
                ballMovement.currentDirection = Vector2.right;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;
            }

        }
        else if (ballMovement.currentDirection == Vector2.down  )
        {
            if (pa.x > pb.x && pa.y > pb.y && ballMovement.initialPositionBall.x < pa.x)
            {
                ballMovement.currentDirection = Vector2.left;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;
            }

            else if (pa.x > pb.x && pa.y < pb.y && ballMovement.initialPositionBall.x > pb.x)
            {
                ballMovement.currentDirection = Vector2.right;
                ballMovement.rb.velocity = ballMovement.currentDirection * ballMovement.speed;
            }

        }
        

    }


}
