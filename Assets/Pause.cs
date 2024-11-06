using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickPause()
    {
        GameController.instance.PauseGame();
        OnOpen();
    }
    public void OnClickHome()
    {
        GameController.instance.DestroyLevel();
        OnClose();
        UiController.instance.mainMenu.OnOpen();
    }
    public void OnClickContinue()
    {

        GameController.instance.ContinueGame();
        OnClose();
        GameController.instance.SpawnerCurrent.OnOpen();
    }
    public void OnClickLevel()
    {
        GameController.instance.DestroyLevel();
        OnClose();
        UiController.instance.selectlevel.OnOpen();
    }



}
