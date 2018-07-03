using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionPanelView : PanelView
{
    // Variables
    public float XOffset;
    public float YOffset;
    public float XSpacing;
    public float YSpacing;
    public int RowCount;

    // Features
    // None..

    // Events

    private void Start()
    {
        Notify("constructionPanel.start");
    }

    public void OnScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Notify("constructionPanel.scrollUp");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Notify("constructionPanel.scrollDown");
        }
    }

}
