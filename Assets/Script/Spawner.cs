using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform TransformSpawn;
    public Start start;
    public BallMovement ballMovement;
    public TriangleControl triangleRed;
    public BtnRotate btnRotate1;
    public BtnRotate btnRotate2;
    public void OnClickStart()
    {
        start.OnClose();
        ballMovement.Runing();
        triangleRed.SetInteractable(false);
        btnRotate1.OnClose();
        btnRotate2.OnClose();
    }
}
