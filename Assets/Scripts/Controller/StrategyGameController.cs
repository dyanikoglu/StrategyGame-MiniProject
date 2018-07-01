using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class StrategyGameController : Controller<StrategyGameApplication>
{

    /// <inheritdoc />
    /// Handle notifications from the application.
    public override void OnNotification(string pEvent, Object pTarget, params object[] pData)
    {
        switch (pEvent)
        {
            // Map Notifications Start
            case "map.onClicked":
            {
                // Hide Details Panel
                app.view.DetailsPanel.HidePanel();
            }
                break;
            // Map Notifications End


            // Construction Button Notifications Start
            case "barracksButton.onClick":
            {
                // Spawn new barracks building
                app.view.MapItemFactory.CreateNewMapItem(MapItem.Type.Barracks, (int) pData[0]);
            }
                break;

            case "powerplantButton.onClick":
            {
                // Spawn new powerplant building
                app.view.MapItemFactory.CreateNewMapItem(MapItem.Type.PowerPlant, (int) pData[0]);
            }
                break;
            // Construction Button Notifications End

            // Map Item Notifications Start
            case "building.onBuildModeStart":
            {
                // Hide UI
                app.view.ConstructionPanel.HidePanel();
                app.view.DetailsPanel.HidePanel();
            }
                break;
            case "building.onBuildModeEnd":
            {
                // Show UI
                app.view.ConstructionPanel.ShowPanel();
            }
                break;

            case "building.onCollisionWithAnotherBuildingStay":
            {
                // Give feedback to player by changing background color to red
                ((BuildingView) pTarget).SetBackgroundColor(new Color(0.5f, 0, 0, 0.5f));
            }
                break;

            case "building.onCollisionWithAnotherBuildingEnd":
            {
                // Give feedback to player by changing background color to white
                ((BuildingView) pTarget).SetBackgroundColor(new Color(1, 1, 1, 0.5f));
            }
                break;

            case "barracksBuilding.onClicked":
            {
                // Set details panel information from clicked object
                app.view.DetailsPanel.SetDetails(MapItem.Type.Barracks, (int) pData[0]);
                app.view.DetailsPanel.ShowPanel();
            }
                break;

            case "powerplantBuilding.onClicked":
            {
                // Set details panel information from clicked object

                app.view.DetailsPanel.SetDetails(MapItem.Type.PowerPlant, (int) pData[0]);
                app.view.DetailsPanel.ShowPanel();
            }
                break;
            // Map Item Notifications End

            // Panel Notifications Start
            case "panel.onAnimationPlaying":
            {
                // Keep animating the panel
                var rectTransform = ((PanelView) pTarget).GetComponent<RectTransform>();
                rectTransform.anchoredPosition =
                    new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, ((PanelView) pTarget).GetTargetX(), 0.1f),
                        rectTransform.anchoredPosition.y);

                // Destination reached, stop animation
                if (Mathf.Abs(((PanelView) pTarget).GetTargetX() - rectTransform.anchoredPosition.x) < 0.1f)
                {
                    ((PanelView) pTarget).SetIsAnimationOngoing(false);
                }
            }
                break;

            case "constructionPanel.start":
            {
                // Spawn buttons on construction panel

                for (var i = 0; i < app.view.ConstructionPanel.RowCount; i++)
                {
                    var obj = app.view.ButtonFactory.CreateNewButton("Barracks");
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + app.view.ConstructionPanel.XOffset,
                        obj.transform.localPosition.y - app.view.ConstructionPanel.YOffset -
                        app.view.ConstructionPanel.YSpacing * i, obj.transform.localPosition.z);

                    obj = app.view.ButtonFactory.CreateNewButton("PowerPlant");
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + app.view.ConstructionPanel.XOffset +
                        app.view.ConstructionPanel.XSpacing,
                        obj.transform.localPosition.y - app.view.ConstructionPanel.YOffset -
                        app.view.ConstructionPanel.YSpacing * i, obj.transform.localPosition.z);
                }
            }
                break;

            case "detailsPanelView.onDetailsChanged":
            {
                // Variables in details panel changed, reflect changes to UI

                app.model.DetailsPanelText.text = app.view.DetailsPanel.GetDetailsText();

                switch (app.view.DetailsPanel.GetDetailsType())
                {
                    case MapItem.Type.Barracks:
                        app.model.DetailsPanelBarracksSprite.SetActive(true);
                        app.model.DetailsPanelPowerPlantSprite.SetActive(false);
                        break;
                    case MapItem.Type.PowerPlant:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelPowerPlantSprite.SetActive(true);
                        break;
                    case MapItem.Type.Soldier:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelPowerPlantSprite.SetActive(false);
                        break;
                    default:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        break;
                }
            }
                break;
            // Panel Notifications End



            default:
            {
                // Do nothing
            }
                break;
        }
    }
}
