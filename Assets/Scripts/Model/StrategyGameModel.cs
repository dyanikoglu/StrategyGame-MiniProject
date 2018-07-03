﻿using System.Collections;
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
    public GameObject ConstructionButtonPool;
    public Text DetailsPanelText;
    public GameObject DetailsPanelBarracksSprite;
    public GameObject DetailsPanelPowerPlantSprite;
    public GameObject DetailsPanelSoldierSprite;
    public GameObject DetailsPanelSoldierButton;

    // Prefab References
    [Header("Prefab References")]
    public GameObject PowerPlantButton;
    public GameObject BarracksButton;
    public GameObject PowerPlantBuilding;
    public GameObject BarracksBuilding;
    public GameObject SoldierMapItem;

    // Global
    [HideInInspector] public bool CameraCanBeDragged;
    [HideInInspector] public int BarracksID;
    [HideInInspector] public int PowerPlantID;
    [HideInInspector] public int SoldierID;
    [HideInInspector] public MapItemView selectedItem;
}
