using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

   
  
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit ");
    }
}
