using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierButtonView : ConstructionButtonView
{

    public override void OnClicked()
    {
        Notify("soldierButton.onClicked");
    }
}
