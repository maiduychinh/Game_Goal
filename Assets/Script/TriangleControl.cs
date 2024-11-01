﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    public Goal goal;
    public War war;
    public GameObject button; // Nút trên hình tam giác
    public BallMovement ballMovement;
    private bool isDragging = false; // Biến kiểm tra trạng thái kéo thả
    private bool isInteractable = true; // Biến kiểm tra có thể tương tác không
    private EdgeCollider2D edgeCollider; // Thêm biến cho EdgeCollider2D
    private PolygonCollider2D polygonCollider;
    private Vector2 dragOffset; // Độ lệch khi kéo thả
    private Vector2 initialPosition; // Vị trí ban đầu của tam giác
    private SpriteRenderer spriteRenderer; // Thêm biến để lưu SpriteRenderer của tam giác
    private Color originalColor; // Lưu màu ban đầu của tam giác
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer của tam giác
        originalColor = spriteRenderer.color; // Lưu màu ban đầu
        polygonCollider = GetComponent<PolygonCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>(); // Khởi tạo EdgeCollider2D
        SetButtonPosition(); // Đặt vị trí của nút
        NewinitialPosition();




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("War")) // Kiểm tra nếu va chạm với tường
        {
            spriteRenderer.color = Color.yellow; // Đổi màu 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("War")) // Kiểm tra khi không còn va chạm với tường
        {
            spriteRenderer.color = originalColor; // Trở lại màu ban đầu
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
                dragOffset = (Vector2)transform.position - touchPosition; // Tính độ lệch kéo
                OffEdgeCollider2D(); // Tắt EdgeCollider2D khi bắt đầu kéo
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
            transform.position = touchPosition + dragOffset; // Cập nhật vị trí của tam giác
            ballMovement.CloseCollider();
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            
            isDragging = false; // Dừng kéo khi thả chuột hoặc kết thúc chạm
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

                    // Ngừng kiểm tra sau khi đã tìm thấy va chạm
                }
            }
            foreach (Collider2D goalCollider in goal.colliders)
            {
                if (goalCollider != null && polygonCollider.IsTouching(goalCollider))
                {
                    ResetPosition();
                    break;

                    // Ngừng kiểm tra sau khi đã tìm thấy va chạm
                }
            }

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
