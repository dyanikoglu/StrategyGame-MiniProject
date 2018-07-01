using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPanelView : PanelView
{
    // Variables
    public float XOffset;
    public float YOffset;
    public float XSpacing;
    public float YSpacing;
    public int RowCount;

    // Features

    // Events

    private void Start()
    {
        Notify("constructionPanel.start");
    }

}
