using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksButtonView : ConstructionButtonView {

    // Features
    // None..

    // Events
    public override void OnClick()
    {
        Notify("barracksButton.onClick");
    }
}
