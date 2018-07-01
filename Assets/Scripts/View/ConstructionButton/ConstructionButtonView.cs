using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public abstract class ConstructionButtonView : View<StrategyGameApplication> {

    //Variables
    public int _id;

    // Features
    public int GetID()
    {
        return _id;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    // Events
    public abstract void OnClick();
}
