using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class StrategyGameModel : Model<StrategyGameApplication> {

    // Runtime Gameobject References
    [Header("Runtime Gameobject References")]
    public GameObject Map;
    public GameObject Camera;
    public GameObject ConstructionButtonContent;
    public GameObject DetailsPanel;
    public GameObject ConstructionPanel;

    // Prefab References
    [Header("Prefab References")]
    public GameObject PowerPlantButton;
    public GameObject BarracksButton;
    public GameObject PowerPlantBuilding;
    public GameObject BarracksBuilding;

    // Global
    [HideInInspector]
    public bool CameraCanBeDragged;
}
