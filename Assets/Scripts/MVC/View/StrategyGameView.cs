using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;


/// Root class for all views.
public class StrategyGameView : View<StrategyGameApplication> {

    /// Reference to the Camera view.
    public CameraView Camera { get { return _camera = Assert<CameraView>(_camera); } }
    private CameraView _camera;

    /// Reference to the Map view.
    public MapView Map { get { return _map = Assert<MapView>(_map); } }
    private MapView _map;

    /// Reference to the ConstructionButton view.
    public ConstructionButtonView ConstructionButton { get { return _constructionButton = Assert<ConstructionButtonView>(_constructionButton); } }
    private ConstructionButtonView _constructionButton;

    /// Reference to the BarracksButton view.
    public BarracksButtonView BarracksButton { get { return _barracksButton = Assert<BarracksButtonView>(_barracksButton); } }
    private BarracksButtonView _barracksButton;

    /// Reference to the PowerPlantButton view.
    public PowerPlantButtonView PowerPlantButton { get { return _powerPlantButton = Assert<PowerPlantButtonView>(_powerPlantButton); } }
    private PowerPlantButtonView _powerPlantButton;

    /// Reference to the Panel view.
    public PanelView Panel { get { return _panel = Assert<PanelView>(_panel); } }
    private PanelView _panel;

    /// Reference to the ConstructionPanel view.
    public ConstructionPanelView ConstructionPanel { get { return _constructionPanel = Assert<ConstructionPanelView>(_constructionPanel); } }
    private ConstructionPanelView _constructionPanel;

    /// Reference to the DetailsPanel view.
    public DetailsPanelView DetailsPanel { get { return _detailsPanel = Assert<DetailsPanelView>(_detailsPanel); } }
    private DetailsPanelView _detailsPanel;
}
