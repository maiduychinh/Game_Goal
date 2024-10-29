using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    private bool isDragging = false;
    private bool isInteractable = true;
    public GameObject button;
    private PolygonCollider2D polygonCollider; 

    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        UpdateButtonPosition();
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
        UpdateButtonPosition();
    }

    private void UpdateButtonPosition()
    {
        Vector2[] points = polygonCollider.points;

        if (points.Length > 0)
        {
            Vector3 topVertex = transform.TransformPoint(points[0]);
            button.transform.position = topVertex;
        }
    }
}

