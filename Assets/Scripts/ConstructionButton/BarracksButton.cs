using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksButton : ConstructionButton
{
    public override void OnClick()
    {
        GameObject obj = Instantiate(MapBuildingObject, GameManager.GetMap().transform);
        obj.GetComponent<BarracksItem>().SetToBuildMode();
    }
}
