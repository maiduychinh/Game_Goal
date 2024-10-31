/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    public GameObject button; // Nút trên hình tam giác
    public BallMovement ballMovement;
    private bool isDragging = false; // Biến kiểm tra trạng thái kéo thả
    private bool isInteractable = true; // Biến kiểm tra có thể tương tác không
    private EdgeCollider2D edgeCollider; // Thêm biến cho EdgeCollider2D
    private PolygonCollider2D polygonCollider;
    private Vector2 dragOffset; // Độ lệch khi kéo thả
    private Vector2 initialPosition; // Vị trí ban đầu của tam giác
    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>(); // Khởi tạo EdgeCollider2D
        SetButtonPosition(); // Đặt vị trí của nút
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
                dragOffset = (Vector2)transform.position - touchPosition; // Tính độ lệch kéo
                // Tắt EdgeCollider2D khi bắt đầu kéo
                OffEdgeCollider2D();
            }
        }

        if (isDragging)
        {
            Vector2 touchPosition = GetTouchPosition();
            transform.position = touchPosition + dragOffset; // Cập nhật vị trí của tam giác
            ballMovement.CloseCollider();
            

        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            isDragging = false; // Dừng kéo khi thả chuột hoặc kết thúc chạm
            OffEdgeCollider2D();
            ballMovement.OpenCollider();
           
        }
        // Kiểm tra nếu tam giác va chạm với quả bóng
        if (polygonCollider.IsTouching(ballMovement.GetComponent<Collider2D>()) && edgeCollider.enabled == false)
        {
            ballMovement.ResetPosition();
            ResetPosition();

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
        isInteractable = interactable; // Đặt trạng thái có thể tương tác

    }

    public void OnRotation()
    {
        transform.Rotate(0, 0, 90); // Xoay tam giác 90 độ
    }

    private void SetButtonPosition()
    {
        Vector2[] points = polygonCollider.points;

        if (points.Length > 0)
        {
            Vector3 topVertex = transform.TransformPoint(points[0]); // Đỉnh trên của tam giác
            Vector3 offset = new Vector3(0, -button.GetComponent<RectTransform>().rect.height / 2, 0);
            button.transform.position = topVertex + offset; // Đặt nút ở vị trí tính toán
        }
    }
}

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    public War war;
    public GameObject button; // Nút trên hình tam giác
    public BallMovement ballMovement;
    private bool isDragging = false; // Biến kiểm tra trạng thái kéo thả
    private bool isInteractable = true; // Biến kiểm tra có thể tương tác không
    private EdgeCollider2D edgeCollider; // Thêm biến cho EdgeCollider2D
    private PolygonCollider2D polygonCollider;
    private Vector2 dragOffset; // Độ lệch khi kéo thả
    private Vector2 initialPosition; // Vị trí ban đầu của tam giác

    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>(); // Khởi tạo EdgeCollider2D
        SetButtonPosition(); // Đặt vị trí của nút
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
                dragOffset = (Vector2)transform.position - touchPosition; // Tính độ lệch kéo
                OffEdgeCollider2D(); // Tắt EdgeCollider2D khi bắt đầu kéo
            }
        }

        if (isDragging)
        {
            Vector2 touchPosition = GetTouchPosition();
            transform.position = touchPosition + dragOffset; // Cập nhật vị trí của tam giác
            ballMovement.CloseCollider();
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            isDragging = false; // Dừng kéo khi thả chuột hoặc kết thúc chạm
            OffEdgeCollider2D();
            ballMovement.OpenCollider();
        }

        // Kiểm tra nếu tam giác va chạm với quả bóng
        if (polygonCollider.IsTouching(ballMovement.GetComponent<Collider2D>()) && edgeCollider.enabled == false)
        {
            ballMovement.ResetPosition();
            ResetPosition();
        }
       /* Collider2D warCollider = GameObject.FindGameObjectWithTag("War")?.GetComponent<Collider2D>();
        if (warCollider != null && polygonCollider.IsTouching(warCollider))
        {
            ResetPosition();
        }*/
        /*if (polygonCollider.IsTouching(war.GetComponent<Collider2D>()) )
        {
           
            ResetPosition();
            Debug.Log("RSPTS");
        }*/


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
        isInteractable = interactable; // Đặt trạng thái có thể tương tác
    }

    public void OnRotation()
    {
        transform.Rotate(0, 0, 90); // Xoay tam giác 90 độ
    }

    private void SetButtonPosition()
    {
        Vector2[] points = polygonCollider.points;

        if (points.Length > 0)
        {
            Vector3 topVertex = transform.TransformPoint(points[0]); // Đỉnh trên của tam giác
            Vector3 offset = new Vector3(0, -button.GetComponent<RectTransform>().rect.height / 2, 0);
            button.transform.position = topVertex + offset; // Đặt nút ở vị trí tính toán
        }
    }
}
