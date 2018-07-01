using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public abstract class MapItemView : View<StrategyGameApplication> {

    // Variables
    protected bool _placedOnMap = false;

    // Features
    // None..

    // Events
    public abstract void OnClicked();
}
