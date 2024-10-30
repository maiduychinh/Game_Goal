using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    public GameObject button;



    private bool isDragging = false;
    private bool isInteractable = true;

    private PolygonCollider2D polygonCollider;
    private Vector2 dragOffset;
    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        SetButtonPosition();
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
            }
        }

        if (isDragging)
        {
            Vector2 touchPosition = GetTouchPosition();

            transform.position = touchPosition + dragOffset;
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            isDragging = false;
        }
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
}
