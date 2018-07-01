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
                var obj = app.view.MapItemFactory.CreateNewMapItem("Barracks", (int) pData[0]);
                obj.GetComponent<BarracksBuildingView>().OnBuildModeStart();
            }
                break;

            case "powerplantButton.onClick":
            {
                var obj = app.view.MapItemFactory.CreateNewMapItem("PowerPlant", (int) pData[0]);
                obj.GetComponent<PowerPlantBuildingView>().OnBuildModeStart();
            }
                break;
            // Construction Button Notifications End

            // Map Item Notifications Start
            case "building.onBuildModeStart":
            {
                // Hide UI
                app.model.ConstructionPanel.GetComponent<PanelView>().HidePanel();
                app.model.DetailsPanel.GetComponent<PanelView>().HidePanel();
            }
                break;
            case "building.onBuildModeEnd":
            {
                // Show UI
                app.model.ConstructionPanel.GetComponent<PanelView>().ShowPanel();
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
                app.view.DetailsPanel.SetDetailsText("Barracks #" + (int) pData[0]);
                app.view.DetailsPanel.SetDetailsType("Barracks");
                app.view.DetailsPanel.OnDetailsTextChanged();
                app.view.DetailsPanel.ShowPanel();
            }
                break;

            case "powerplantBuilding.onClicked":
            {
                // Set details panel information from clicked object
                    app.view.DetailsPanel.SetDetailsText("PowerPlant #" + (int) pData[0]);
                app.view.DetailsPanel.SetDetailsType("PowerPlant");
                app.view.DetailsPanel.OnDetailsTextChanged();
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
                var constructionPanelRef = ((ConstructionPanelView) pTarget);

                for (var i = 0; i < constructionPanelRef.RowCount; i++)
                {
                    var obj = app.view.ButtonFactory.CreateNewButton("Barracks");
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + constructionPanelRef.XOffset,
                        obj.transform.localPosition.y - constructionPanelRef.YOffset -
                        constructionPanelRef.YSpacing * i, obj.transform.localPosition.z);

                    obj = app.view.ButtonFactory.CreateNewButton("PowerPlant");
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + constructionPanelRef.XOffset + constructionPanelRef.XSpacing,
                        obj.transform.localPosition.y - constructionPanelRef.YOffset -
                        constructionPanelRef.YSpacing * i, obj.transform.localPosition.z);
                }
            }
                break;

            case "detailsPanelView.onDetailsTypeChanged":
            {
                app.model.DetailsPanelText.text = app.view.DetailsPanel.GetDetailsText();

                if (app.view.DetailsPanel.GetDetailsType() == "Barracks")
                {
                    app.model.DetailsPanelBarracksSprite.SetActive(true);
                    app.model.DetailsPanelPowerPlantSprite.SetActive(false);
                }
                else if (app.view.DetailsPanel.GetDetailsType() == "PowerPlant")
                {
                    app.model.DetailsPanelBarracksSprite.SetActive(false);
                    app.model.DetailsPanelPowerPlantSprite.SetActive(true);
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
