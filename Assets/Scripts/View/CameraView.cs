using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class CameraView : View<StrategyGameApplication>
{
    // Camera Features
    // None..

    // Camera Events
    private void LateUpdate()
    {
        Notify("camera.lateUpdate");
    }

    private void Start()
    {
        Notify("camera.start");
    }
}
