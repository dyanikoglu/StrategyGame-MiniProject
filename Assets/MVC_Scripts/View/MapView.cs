using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class MapView : View<StrategyGameApplication>
{

    public void OnPointerEnter()
    {
        Notify("map.onPointerEnter");
    }

    public void OnPointerExit()
    {
        Notify("map.onPointerExit");
    }

}
