using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsPanelView : PanelView
{
    // Variables
    private string _detailsText;
    private MapItem.Type _detailsType;

    // Features
    public string GetDetailsText()
    {
        return _detailsText;
    }

    public void SetDetails(MapItem.Type detailsType, int id)
    {
        _detailsType = detailsType;

        switch (detailsType)
        {
            case MapItem.Type.Barracks:
                _detailsText = "Barracks #" + id;
                break;
            case MapItem.Type.PowerPlant:
                _detailsText = "PowerPlant #" + id;
                break;
            case MapItem.Type.Soldier:
                _detailsText = "Soldier #" + id;
                break;
            default:
                _detailsText = "";
                break;
        }

        OnDetailsChanged();
    }

    public MapItem.Type GetDetailsType()
    {
        return _detailsType;
    }

    // Events
    public void OnDetailsChanged()
    {
        Notify("detailsPanelView.onDetailsChanged");
    }
}
