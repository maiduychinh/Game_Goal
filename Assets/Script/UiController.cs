using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    public MainMenu mainMenu;
    public Spawner spawner;
    public PauseWin pauseWin;
    public GameOver gameOver;
    public Pause Pause;
    public SelectLevel selectlevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
