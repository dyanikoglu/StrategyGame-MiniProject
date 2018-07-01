using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantButtonView : ConstructionButtonView
{
    // Features
    // None..

    // Events
    public override void OnClick()
    {
        Notify("powerplantButton.onClick", GetID());
    }
}
