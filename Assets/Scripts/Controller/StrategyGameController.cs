using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            case "map.onHoverStart":
            {
                app.model.CameraCanBeDragged = true;
                app.view.Map.SetFocused(true);
            }
                break;

            case "map.onHoverEnd":
            {
                app.model.CameraCanBeDragged = false;
                app.view.Map.SetFocused(false);
            }
                break;

            case "map.onLeftClicked":
            {
                // Hide Details Panel
                app.view.DetailsPanel.HidePanel();
            }
                break;

            case "map.onRightClicked":
            {
                // Move soldier unit
            }
                break;
            // Map Notifications End


            // Construction Button Notifications Start
            case "button.onHoverStart":
            {
                ((ConstructionButtonView) pTarget).SetColor(Color.red);
            }
                break;

            case "button.onHoverEnd":
            {
                ((ConstructionButtonView) pTarget).SetColor(Color.white);
            }
                break;

            case "barracksButton.onClicked":
            {
                // Spawn new barracks building
                app.view.MapItemFactory.CreateNewMapItem(MapItem.Type.Barracks, (int) pData[0]);
            }
                break;

            case "powerplantButton.onClicked":
            {
                // Spawn new powerplant building
                app.view.MapItemFactory.CreateNewMapItem(MapItem.Type.PowerPlant, (int) pData[0]);
            }
                break;

            case "soldierButton.onClicked":
            {
                var newSoldier = app.view.MapItemFactory.CreateNewMapItem(MapItem.Type.Soldier, app.model.SoldierID++);

                // If a barracks map item is still selected, spawn this new soldier near it on closest available tile. Otherwise, destroy it back.
                if ((BarracksBuildingView) app.model.selectedItem)
                {
                    var barracksPosition = app.model.selectedItem.GetComponent<RectTransform>().anchoredPosition;
                    // If we can't find a tile to spawn this soldier, destroy it back
                    if (!newSoldier.GetComponent<SoldierView>().FindATileToSpawn(barracksPosition))
                    {
                        Destroy(newSoldier);
                    }
                }
                else
                {
                    Destroy(newSoldier);
                }
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
                app.model.selectedItem = (MapItemView) pTarget;
                app.model.DetailsPanelSoldierButton.SetActive(true);
                app.view.DetailsPanel.ShowPanel();
            }
                break;

            case "powerplantBuilding.onClicked":
            {
                // Set details panel information from clicked object

                app.view.DetailsPanel.SetDetails(MapItem.Type.PowerPlant, (int) pData[0]);
                app.model.selectedItem = (MapItemView) pTarget;
                app.model.DetailsPanelSoldierButton.SetActive(false);
                app.view.DetailsPanel.ShowPanel();
            }
                break;

            case "soldierMapItem.onClicked":
            {
                app.view.DetailsPanel.SetDetails(MapItem.Type.Soldier, (int) pData[0]);
                app.model.selectedItem = (MapItemView) pTarget;
                app.model.DetailsPanelSoldierButton.SetActive(false);
                app.view.DetailsPanel.ShowPanel();
                // TODO
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

            // Move buttons in button pool up
            case "constructionPanel.scrollUp":
            {
                // Check if scrollUp is available (Check if upper button has id 0)
                foreach (var buttonView in app.model.ConstructionButtonPool.GetComponentsInChildren<ConstructionButtonView>())
                {
                    if (buttonView.GetID() == 0 && buttonView.GetComponent<RectTransform>().anchoredPosition.y == -app.view.ConstructionPanel.YSpacing)
                    {
                        return;
                    }
                }

                var contentRecTransform = app.model.ConstructionButtonPool.GetComponent<RectTransform>();
                var oldY = contentRecTransform.anchoredPosition.y;

                // Move each button down
                foreach (var buttonRectTransform in
                    app.model.ConstructionButtonPool.GetComponentsInChildren<RectTransform>())
                {
                    buttonRectTransform.anchoredPosition = new Vector2(buttonRectTransform.anchoredPosition.x,
                        buttonRectTransform.anchoredPosition.y - 5);

                    // End of the panel reached, send it back to top (Object Pooling)
                    if (buttonRectTransform.anchoredPosition.y <= oldY - app.view.ConstructionPanel.YOffset / 2)
                    {
                        // Check if reused object is going to be out of ID range
                        if (buttonRectTransform.GetComponent<ConstructionButtonView>().GetID() -
                            app.view.ConstructionPanel.RowCount >= 0)
                        {
                            buttonRectTransform.anchoredPosition =
                                new Vector2(buttonRectTransform.anchoredPosition.x, 0);

                            // Roll back the id of the reused object
                            buttonRectTransform.GetComponent<ConstructionButtonView>().SetID(
                                buttonRectTransform.GetComponent<ConstructionButtonView>().GetID() -
                                app.view.ConstructionPanel.RowCount);
                        }
                    }

                    // Fix for content canvas self-movement
                    contentRecTransform.anchoredPosition = new Vector2(contentRecTransform.anchoredPosition.x, oldY);
                }
            }
                break;
            
            // Move buttons in button pool down
            case "constructionPanel.scrollDown":
            {
                var contentRecTransform = app.model.ConstructionButtonPool.GetComponent<RectTransform>();
                var oldY = contentRecTransform.anchoredPosition.y;

                // Move each button up
                foreach (var buttonRectTransform in
                    app.model.ConstructionButtonPool.GetComponentsInChildren<RectTransform>())
                {
                    buttonRectTransform.anchoredPosition = new Vector2(buttonRectTransform.anchoredPosition.x,
                        buttonRectTransform.anchoredPosition.y + 5);

                    // End of the panel reached, send it back to bottom (Object Pooling)
                    if (buttonRectTransform.anchoredPosition.y >= 0)
                    {
                        buttonRectTransform.anchoredPosition =
                            new Vector2(buttonRectTransform.anchoredPosition.x, contentRecTransform.anchoredPosition.y - app.view.ConstructionPanel.YOffset / 2);

                        // Advance the id of the reused object
                        buttonRectTransform.GetComponent<ConstructionButtonView>().SetID(buttonRectTransform.GetComponent<ConstructionButtonView>().GetID() + app.view.ConstructionPanel.RowCount);
                    }

                    // Fix for content canvas self-movement
                    contentRecTransform.anchoredPosition = new Vector2(contentRecTransform.anchoredPosition.x, oldY);
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
                        app.model.DetailsPanelSoldierSprite.SetActive(false);
                        break;
                    case MapItem.Type.PowerPlant:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelPowerPlantSprite.SetActive(true);
                        app.model.DetailsPanelSoldierSprite.SetActive(false);
                        break;
                    case MapItem.Type.Soldier:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelPowerPlantSprite.SetActive(false);
                        app.model.DetailsPanelSoldierSprite.SetActive(true);
                        break;
                    default:
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelBarracksSprite.SetActive(false);
                        app.model.DetailsPanelSoldierSprite.SetActive(false);
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
