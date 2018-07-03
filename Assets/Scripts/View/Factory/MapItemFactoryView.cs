using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class MapItemFactoryView : View<StrategyGameApplication> {

    // Features
    public GameObject CreateNewMapItem(MapItem.Type mapItemType, int id)
    {
        switch (mapItemType)
        {
            case MapItem.Type.Barracks:
                var newBarracks = Instantiate(app.model.BarracksBuilding, app.model.MapItemsContainer.transform);
                newBarracks.GetComponent<MapItemView>().SetID(id);
                newBarracks.GetComponent<BuildingView>().OnBuildModeStart();
                return newBarracks;
            case MapItem.Type.PowerPlant:
                var newPowerPlant = Instantiate(app.model.PowerPlantBuilding, app.model.MapItemsContainer.transform);
                newPowerPlant.GetComponent<MapItemView>().SetID(id);
                newPowerPlant.GetComponent<BuildingView>().OnBuildModeStart();
                return newPowerPlant;
            case MapItem.Type.Soldier:
                var newSoldier = Instantiate(app.model.SoldierMapItem, app.model.MapItemsContainer.transform);
                newSoldier.GetComponent<MapItemView>().SetID(id);
                return newSoldier;
            default:
                return null;
        }
    }
}
