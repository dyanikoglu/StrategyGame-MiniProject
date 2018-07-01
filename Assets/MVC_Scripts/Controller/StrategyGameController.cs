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
                obj.GetComponent<BarracksItem>().SetToBuildMode();
            }
                break;

            case "powerplantButton.onClick":
            {
                var obj = Instantiate(app.model.PowerPlantBuilding, app.model.Map.transform);
                obj.GetComponent<PowerPlantItem>().SetToBuildMode();
            }
                break;
            // Construction Button Notifications End




            default:
            {
                // Do nothing
            }
                break;
        }
    }
}
