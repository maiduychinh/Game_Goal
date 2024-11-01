using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    public Collider2D[] colliders;
    private Vector2 initialPosition; // Vị trí ban đầu của đối tượng chính
    private Quaternion initialRotation; // Rotation ban đầu của đối tượng chính
    private Vector2[] initialChildPositions; // Lưu vị trí ban đầu của các đối tượng con
    private Quaternion[] initialChildRotations; // Lưu rotation ban đầu của các đối tượng con

    void Start()
    {
        // Lấy tất cả Collider2D của đối tượng hiện tại và các đối tượng con của nó
        colliders = GetComponentsInChildren<Collider2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Lưu vị trí và rotation ban đầu của các đối tượng con
        Transform[] children = GetComponentsInChildren<Transform>();
        initialChildPositions = new Vector2[children.Length - 1]; // Không lưu vị trí của chính đối tượng War
        initialChildRotations = new Quaternion[children.Length - 1]; // Không lưu rotation của chính đối tượng War

        for (int i = 1; i < children.Length; i++) // Bắt đầu từ 1 để bỏ qua chính đối tượng War
        {
            // Lưu vị trí và rotation ban đầu của mỗi đối tượng con
            initialChildPositions[i - 1] = children[i].position;
            initialChildRotations[i - 1] = children[i].rotation;
        }
    }

    private void Update()
    {
        ResetInitialPosition();
    }

    public void OpenColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }

    public void CloseColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void SetBodyType(RigidbodyType2D bodyType)
    {
        foreach (var collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = bodyType;
            }
        }
    }

    public void ResetInitialPosition()
    {
        // Đặt lại vị trí và rotation của đối tượng chính
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Đặt lại vị trí và rotation của tất cả các đối tượng con về giá trị ban đầu
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i++) // Bắt đầu từ 1 để bỏ qua chính đối tượng War
        {
            children[i].position = initialChildPositions[i - 1];
            children[i].rotation = initialChildRotations[i - 1];
        }
    }
    public void SetTriggerState(bool isTrigger)
    {
        foreach (var collider in colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }

}
