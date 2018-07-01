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
                app.model.CameraCurrentlyDragging = false;
            }
                break;
            case "camera.lateUpdate":
            {
                // Get main camera component ref from model
                var mainCameraRef = app.model.Camera.GetComponent<Camera>();

                // If left mouse is pressed and camera is available to drag, start dragging
                if (Input.GetMouseButton(0) && app.model.CameraCanBeDragged)
                {
                    app.model.CameraDifference = (mainCameraRef.ScreenToWorldPoint(Input.mousePosition)) -
                                                 mainCameraRef.transform.position;
                    if (app.model.CameraCurrentlyDragging == false)
                    {
                        app.model.CameraCurrentlyDragging = true;
                        app.model.CameraOrigin = mainCameraRef.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
                // Camera is not available to drag
                else if (!Input.GetMouseButton(0))
                {
                    app.model.CameraCurrentlyDragging = false;
                }

                // Update drag position
                if (app.model.CameraCurrentlyDragging)
                {
                    mainCameraRef.transform.position = app.model.CameraOrigin - app.model.CameraDifference;
                }

                // Clamp camera movement to map bounds
                var vertExtent = mainCameraRef.orthographicSize;
                var horizExtent = vertExtent * Screen.width / Screen.height;
                var linkedCameraPos = mainCameraRef.transform.position;
                var areaBounds = app.model.CameraTilemapBounds.bounds;
                mainCameraRef.transform.position = new Vector3(
                    Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                    Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                    linkedCameraPos.z);
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
                // TODO: HideUI

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
                    ((BuildingView)pTarget).transform.position = orientedPoint;
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
                // TODO: ShowUI

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





            default:
            {
                // Do nothing
            }
                break;
        }
    }
}
