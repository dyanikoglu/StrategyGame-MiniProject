using UnityEngine;

public class MapItemFactory {

    private static MapItemFactory instance;

    public  GameObject CreateNewMapItem(GameObject mapItem)
    {
        GameObject newMapItem;

        if (mapItem.GetComponent<BarracksBuildingView>())
        {
            newMapItem = GameObject.Instantiate(mapItem);
            return newMapItem;
        }

        if (mapItem.GetComponent<PowerPlantBuildingView>())
        {
            newMapItem = GameObject.Instantiate(mapItem);
            return newMapItem;
        }

        if (mapItem.GetComponent<SoldierView>())
        {
            newMapItem = GameObject.Instantiate(mapItem);
            return newMapItem;
        }

        return null;
    }

    public static MapItemFactory GetInstance()
    {
        return instance ?? (instance = new MapItemFactory());
    }
}
