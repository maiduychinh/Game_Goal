using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform TransformSpawn;
    public Start start;
    public BallMovement ballMovement;
    public TriangleControl triangleYellow;
    public TriangleControl triangleRed;
    public void OnClickStart()
    {
        start.OnClose();
        ballMovement.Runing();
        triangleRed.SetInteractable(false);
        triangleYellow.SetInteractable(false);
    }
}
