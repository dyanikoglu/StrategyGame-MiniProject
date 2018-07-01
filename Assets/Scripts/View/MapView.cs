using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class MapView : View<StrategyGameApplication>
{

    // Events
    public void OnPointerEnter()
    {
        app.model.CameraCanBeDragged = true;
    }

    public void OnPointerExit()
    {
        app.model.CameraCanBeDragged = false;
    }

    public void OnClicked()
    {
        Notify("map.onClicked");
    }

}
