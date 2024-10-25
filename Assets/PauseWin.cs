using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWin : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaa");
      
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
    public void OnClickNextLevel()
    {
        GameController.instance.NextData();
        GameController.instance.NextLevel();
        UiController.instance.pauseWin.OnClose();
    }
    public void OnClickRetry()
    {
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
        UiController.instance.pauseWin.OnClose();
    }
}
