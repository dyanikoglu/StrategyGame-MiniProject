using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksBuildingView : BuildingView
{
    public override void OnClicked()
    {
        // Notify only if building is placed on map
        if (GetIsPlacedOnMap())
        {
            Notify("barracksBuilding.onClicked", GetID());
        }
    }
}
