using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    private bool isDragging = false;
    private bool isInteractable = true;
    public GameObject button; // Tham chiếu đến Button trên hình tam giác
    private PolygonCollider2D polygonCollider; // Collider của tam giác

    private void Start()
    {
        // Lấy PolygonCollider2D
        polygonCollider = GetComponent<PolygonCollider2D>();
        UpdateButtonPosition(); // Đặt vị trí nút ban đầu
    }

    private void Update()
    {
        if (isInteractable && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            if (IsTouchingTriangle())
            {
                isDragging = true;
            }
        }

        if (isDragging)
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

            transform.position = touchPosition;

            // Cập nhật vị trí button sau khi di chuyển tam giác
            UpdateButtonPosition();
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            isDragging = false;
        }
    }

    private bool IsTouchingTriangle()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.touchCount > 0)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

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
        // Cập nhật vị trí button sau khi xoay tam giác
        UpdateButtonPosition();
    }

    private void UpdateButtonPosition()
    {
        // Lấy các đỉnh từ PolygonCollider2D
        Vector2[] points = polygonCollider.points;

        if (points.Length > 0)
        {
            // Tính toán vị trí đỉnh trên (đỉnh thứ nhất trong PolygonCollider)
            Vector3 topVertex = transform.TransformPoint(points[0]);
            button.transform.position = topVertex; // Cập nhật vị trí của button
        }
    }
}

