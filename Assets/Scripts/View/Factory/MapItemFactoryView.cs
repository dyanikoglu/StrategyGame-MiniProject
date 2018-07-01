using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class MapItemFactoryView : View<StrategyGameApplication> {

    // Features
    public GameObject CreateNewMapItem(string mapItemType, int id)
    {
        if (mapItemType.Equals("Barracks"))
        {
            var newObject = Instantiate(app.model.BarracksBuilding, app.model.Map.transform);
            newObject.GetComponent<MapItemView>().SetID(id);
            return newObject;
        }
        else if (mapItemType.Equals("PowerPlant"))
        {
            var newObject = Instantiate(app.model.PowerPlantBuilding, app.model.Map.transform);
            newObject.GetComponent<MapItemView>().SetID(id);
            return newObject;
        }
        else
        {
            return null;
        }
    }
}
