using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
    public void OnclickLevel1()
    {
        GameController.instance.CurrentID = 1;
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        this.OnClose(); 
    }
    public void OnclickLevel2()
    {
        GameController.instance.CurrentID = 2;
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        this.OnClose();
    }

    public void OnclickLevel3()
    {
        GameController.instance.CurrentID = 3;
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        this.OnClose();
    }
    public void OnclickLevel4()
    {
        GameController.instance.CurrentID = 4;
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        this.OnClose();
    }
    public void OnclickLevel5()
    {
        GameController.instance.CurrentID = 5;
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        this.OnClose();
    }
}
