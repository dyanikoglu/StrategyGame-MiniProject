using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantBuildingView : BuildingView
{
    public override void OnClicked()
    {
        // Notify only if building is placed on map
        if (GetIsPlacedOnMap())
        {
            Notify("powerplantBuilding.onClicked", GetID());
        }
    }
}
