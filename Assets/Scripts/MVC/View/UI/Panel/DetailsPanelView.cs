using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsPanelView : PanelView
{
    // Variables
    private string _detailsText;
    private EMapItem.Type _detailsType;

    // Features
    public string GetDetailsText()
    {
        return _detailsText;
    }

    public void SetDetails(EMapItem.Type detailsType, int id)
    {
        _detailsType = detailsType;

        switch (detailsType)
        {
            case EMapItem.Type.Barracks:
                _detailsText = "Barracks #" + id;
                break;
            case EMapItem.Type.PowerPlant:
                _detailsText = "PowerPlant #" + id;
                break;
            case EMapItem.Type.Soldier:
                _detailsText = "Soldier #" + id;
                break;
            default:
                _detailsText = "";
                break;
        }

        OnDetailsChanged();
    }

    public EMapItem.Type GetDetailsType()
    {
        return _detailsType;
    }

    // Events
    public void OnDetailsChanged()
    {
        Notify("detailsPanelView.onDetailsChanged");
    }
}
