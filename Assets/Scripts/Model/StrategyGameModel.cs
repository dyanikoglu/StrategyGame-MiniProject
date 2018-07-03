using System.Collections;
using System.Collections.Generic;
using AStar;
using UnityEngine;
using UnityEngine.UI;
using thelab.mvc;

public class StrategyGameModel : Model<StrategyGameApplication>
{
    // Define Path Finder
    public PathFinder PathFinder;

    // Runtime Gameobject References
    [Header("Runtime Gameobject References")]
    public GameObject Map;
    public GameObject MapItemsContainer;
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

    // Global Model Variables
    [HideInInspector] public bool CameraCanBeDragged;
    [HideInInspector] public int BarracksID;
    [HideInInspector] public int PowerPlantID;
    [HideInInspector] public int SoldierID;
    [HideInInspector] public MapItemView SelectedItem;
    [HideInInspector] public bool CameraIsCurrentlyDragging;

    // Game Settings
    [Header("Soldier Preferences")]
    public float SoldierMovementSpeed;
    public bool SoldierCanMoveDiagonal;
}
