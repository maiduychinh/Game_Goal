using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }
    public Transform TransformSpawn;
    public Start start;
    public BallMovement ballMovement;
    public void OnclickPause()
    {
        UiController.instance.Pause.OnClickPause();
        OnClose();
    }
    public void OnclickContinue()
    {
        UiController.instance.Pause.OnClickContinue();
        OnOpen();
    }



   
}
