using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public abstract class MapItemView : View<StrategyGameApplication> {

    // Variables
    private bool _isPlacedOnMap = false;
    public int _id;

    // Features
    public bool GetIsPlacedOnMap()
    {
        return _isPlacedOnMap;
    }

    public int GetID()
    {
        return _id;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetIsPlacedOnMap(bool b)
    {
        _isPlacedOnMap = b;
    }

    // Events
    public abstract void OnClicked();
}
