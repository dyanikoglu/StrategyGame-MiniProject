using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public abstract class MapItemView : View<StrategyGameApplication> {

    // Variables
    private bool _isPlacedOnMap = false;

    // Features
    public bool GetIsPlacedOnMap()
    {
        return _isPlacedOnMap;
    }

    public void SetIsPlacedOnMap(bool b)
    {
        _isPlacedOnMap = b;
    }

    // Events
    public abstract void OnClicked();
}
