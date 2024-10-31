using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    private Collider2D warCollider;
    private void Start()
    {
        warCollider = GetComponent<Collider2D>();
        
    }
}
