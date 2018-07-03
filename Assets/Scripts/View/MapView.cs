using System.Collections;
using System.Collections.Generic;
using AStar;
using UnityEngine;
using thelab.mvc;
using UnityEngine.EventSystems;

public class MapView : View<StrategyGameApplication>
{
    // Variables
    private bool _focused = false; // Mouse is on map

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
        // If mouse is not on map, return
        if (!_focused) return;

        if (Input.GetMouseButtonUp(1))
        {
            Notify("map.onRightClicked");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Notify("map.onLeftClicked");
        }
    }

    private void Start()
    {
        // Initialize pathfinder
        app.model.PathFinder = new PathFinder(AStar.Tools.CreateMatrix(128), new PathFinderOptions() {Diagonals = app.model.SoldierCanMoveDiagonal});
    }
}
