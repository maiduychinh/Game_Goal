using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRotate : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
    
}
