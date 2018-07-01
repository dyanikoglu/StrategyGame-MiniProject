using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using thelab.mvc;

public class StrategyGameModel : Model<StrategyGameApplication>
{

    // Runtime Gameobject References
    [Header("Runtime Gameobject References")]
    public GameObject Map;
    public GameObject Camera;
    public GameObject ConstructionButtonContent;
    public GameObject DetailsPanel;
    public GameObject ConstructionPanel;
    public ButtonFactoryView ButtonFactory;
    public GameObject MapItemFactory;
    public Text DetailsPanelText;
    public GameObject DetailsPanelBarracksSprite;
    public GameObject DetailsPanelPowerPlantSprite;

    // Prefab References
    [Header("Prefab References")] public GameObject PowerPlantButton;
    public GameObject BarracksButton;
    public GameObject PowerPlantBuilding;
    public GameObject BarracksBuilding;

    // Global
    [HideInInspector] public bool CameraCanBeDragged;
    [HideInInspector] public int BarracksID;
    [HideInInspector] public int PowerPlantID;
}
