using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsPanelView : PanelView
{
    // Variables
    private string _detailsText;
    private string _detailsType;

    // Features
    public void SetDetailsText(string s)
    {
        _detailsText = s;
    }

    public string GetDetailsText()
    {
        return _detailsText;
    }

    public void SetDetailsType(string s)
    {
        _detailsType = s;
    }

    public string GetDetailsType()
    {
        return _detailsType;
    }

    // Events
    public void OnDetailsTextChanged()
    {
        Notify("detailsPanelView.onDetailsTypeChanged");
    }
}
