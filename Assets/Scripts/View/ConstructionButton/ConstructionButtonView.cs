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

    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    // Events
    public abstract void OnClicked();

    public void OnHoverStart()
    {
        Notify("button.onHoverStart");
    }

    public void OnHoverEnd()
    {
        Notify("button.onHoverEnd");
    }
}
