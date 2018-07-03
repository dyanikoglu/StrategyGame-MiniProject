using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class ButtonFactoryView : View<StrategyGameApplication> {

    // Features
    public GameObject CreateNewButton(string buttonType)
    {
        if (buttonType.Equals("Barracks"))
        {
            var newObject = Instantiate(app.model.BarracksButton, app.model.ConstructionButtonPool.transform);
            newObject.GetComponent<ConstructionButtonView>().SetID(app.model.BarracksID++);
            return newObject;
        }
        else if (buttonType.Equals("PowerPlant"))
        {
            var newObject = Instantiate(app.model.PowerPlantButton, app.model.ConstructionButtonPool.transform);
            newObject.GetComponent<ConstructionButtonView>().SetID(app.model.PowerPlantID++);
            return newObject;
        }
        else
        {
            return null;
        }
    }

    // Events
    private void Start()
    {
        app.model.BarracksID = 0;
        app.model.PowerPlantID = 0;
    }
}
