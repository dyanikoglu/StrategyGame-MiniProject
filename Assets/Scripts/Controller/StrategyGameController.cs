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
            // Camera Notifications
            case "camera.start":
            {
                app.model.CameraCanBeDragged = true;
            }
                break;

            case "camera.lateUpdate":
            {
                ((CameraView)pTarget).Pan();
            }
                break;
            // Camera Notifications End


            // Map Notifications Start
            case "map.onPointerEnter":
            {
                app.model.CameraCanBeDragged = true;
            }
                break;

            case "map.onPointerExit":
            {
                app.model.CameraCanBeDragged = false;
            }
                break;
            // Map Notifications End


            // Construction Button Notifications Start
            case "barracksButton.onClick":
            {
                var obj = Instantiate(app.model.BarracksBuilding, app.model.Map.transform);
                obj.GetComponent<BarracksBuildingView>().OnBuildModeStart();
            }
                break;

            case "powerplantButton.onClick":
            {
                var obj = Instantiate(app.model.PowerPlantBuilding, app.model.Map.transform);
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

                ((BuildingView) pTarget).SetIsOnBuildMode(true);
            }
                break;
            case "building.onBuildModeStay":
            {
                // Get main camera component ref from model
                var mainCameraRef = app.model.Camera.GetComponent<Camera>();

                // Raytrace to map from mouse position
                var hit = Physics2D.Raycast(mainCameraRef.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    var orientedPoint = new Vector2((int) hit.point.x, (int) hit.point.y);
                    ((BuildingView) pTarget).transform.position = orientedPoint;
                }

                // Destroy the item with right click
                if (Input.GetMouseButtonDown(1))
                {
                    ((BuildingView) pTarget).OnBuildModeEnd(false);
                }

                else if (((BuildingView) pTarget).GetCanBeBuilt() && Input.GetMouseButtonDown(0))
                {
                    ((BuildingView) pTarget).OnBuildModeEnd(true);
                }
            }
                break;
            case "building.onBuildModeEnd":
            {
                // Show UI
                app.model.ConstructionPanel.GetComponent<PanelView>().ShowPanel();

                ((BuildingView) pTarget).SetIsOnBuildMode(false);

                // If buildToCurrentPlace == true, complete build action
                if ((bool) pData[0])
                {
                    ((BuildingView) pTarget).BuildToCurrentPlace();
                }
                // Else, destroy the object
                else
                {
                    ((BuildingView) pTarget).CancelBuild();
                }
            }
                break;

            case "building.onCollisionWithAnotherBuildingStay":
            {
                ((BuildingView) pTarget).SetBackgroundColor(new Color(0.5f, 0, 0, 0.5f));
                ((BuildingView) pTarget).SetCanBeBuilt(false);
            }
                break;

            case "building.onCollisionWithAnotherBuildingEnd":
            {
                ((BuildingView) pTarget).SetBackgroundColor(new Color(1, 1, 1, 0.5f));
                ((BuildingView) pTarget).SetCanBeBuilt(true);
            }
                break;
            // Map Item Notifications End

            // Panel Notifications Start
            case "panel.onAnimationPlaying":
            {
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
                var constructionPanelRef = ((ConstructionPanelView) pTarget);

                for (var i = 0; i < ((ConstructionPanelView) pTarget).RowCount; i++)
                {
                    var obj = Instantiate(app.model.BarracksButton, app.model.ConstructionButtonContent.transform);
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + constructionPanelRef.XOffset,
                        obj.transform.localPosition.y - constructionPanelRef.YOffset -
                        constructionPanelRef.YSpacing * i, obj.transform.localPosition.z);

                    obj = Instantiate(app.model.PowerPlantButton, app.model.ConstructionButtonContent.transform);
                    obj.transform.localPosition = new Vector3(
                        obj.transform.localPosition.x + constructionPanelRef.XOffset + constructionPanelRef.XSpacing,
                        obj.transform.localPosition.y - constructionPanelRef.YOffset -
                        constructionPanelRef.YSpacing * i, obj.transform.localPosition.z);
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
