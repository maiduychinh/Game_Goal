using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
       
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
    public void OnClickOverReTry()
    {


        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        OnClose();
    }
    public void OnClickOverMenu()
    {

        OnClose();
        UiController.instance.mainMenu.OnOpen();
        GameController.instance.DestroyLevel();
    }
}
