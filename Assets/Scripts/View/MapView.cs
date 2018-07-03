using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class MapView : View<StrategyGameApplication>
{
    // Variables
    private bool _focused = false;

    // Features
    public void SetFocused(bool b)
    {
        _focused = b;
    }

    // Events
    public void OnHoverStart()
    {
        Notify("map.onHoverStart");
    }

    public void OnHoverEnd()
    {
        Notify("map.onHoverEnd");
    }

    private void Update()
    {
        if (!_focused) return;

        if (Input.GetMouseButtonDown(1))
        {
            Notify("map.onRightClicked");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Notify("map.onLeftClicked");
        }
    }
}
