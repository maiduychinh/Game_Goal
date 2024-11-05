using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    public Collider2D[] colliders;
    private Vector2 initialPosition; 
    private Quaternion initialRotation; 
    private Vector2[] initialChildPositions; 
    private Quaternion[] initialChildRotations; 

    void Start()
    {
        
        colliders = GetComponentsInChildren<Collider2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        
        Transform[] children = GetComponentsInChildren<Transform>();
        initialChildPositions = new Vector2[children.Length - 1]; 
        initialChildRotations = new Quaternion[children.Length - 1]; 

        for (int i = 1; i < children.Length; i++) 
        {
            
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
        
        
        transform.rotation = initialRotation;

        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i++) 
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
