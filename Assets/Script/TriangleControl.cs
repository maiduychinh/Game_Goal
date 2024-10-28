/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    private bool isDragging = false;
    private bool isInteractable = true; // Biến để kiểm tra có thể tương tác

    private void Update()
    {
        if (isDragging && isInteractable)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    private void OnMouseDown()
    {
        if (isInteractable && Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
    }
    public void OnRotation()
    {
        transform.Rotate(0, 0, 90);
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleControl : MonoBehaviour
{
    private bool isDragging = false;
    private bool isInteractable = true; 
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
    }
}


