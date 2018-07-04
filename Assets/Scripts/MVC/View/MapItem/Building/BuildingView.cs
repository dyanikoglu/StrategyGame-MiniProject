using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingView : MapItemView
{

    // Variables
    private bool _isOnBuildMode = false;
    private bool _canBeBuilt = true;
    public SpriteRenderer BackgroundSpriteRef;

    // Features
    public void SetIsOnBuildMode(bool b)
    {
        _isOnBuildMode = b;
    }

    public void SetCanBeBuilt(bool b)
    {
        _canBeBuilt = b;
    }

    public bool GetCanBeBuilt()
    {
        return _canBeBuilt;
    }

    public void SetBackgroundColor(Color c)
    {
        BackgroundSpriteRef.color = c;
    }

    public void BuildToCurrentPlace()
    {
        SetIsPlacedOnMap(true);
    }

    public void CancelBuild()
    {
        Destroy(this.gameObject);
    }

    // Events
    private void Start()
    {
        if (!_isOnBuildMode)
        {
            Notify("building.onBuildModeStart");
            SetIsOnBuildMode(true);
        }
    }

    private void Update()
    {
        if (!GetIsPlacedOnMap() && _isOnBuildMode)
        {
            // Get main camera component ref from model
            var mainCameraRef = app.model.Camera.GetComponent<Camera>();

            // Raytrace to map from mouse position
            var hit = Physics2D.Raycast(mainCameraRef.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                var orientedPoint = new Vector2((int)hit.point.x, (int)hit.point.y);
                transform.position = new Vector3(orientedPoint.x, orientedPoint.y, -1);
            }

            // Destroy the item with right click
            if (Input.GetMouseButtonDown(1))
            {
                OnBuildModeEnd(false);
            }

            else if (_canBeBuilt && Input.GetMouseButtonDown(0))
            {
                OnBuildModeEnd(true);
            }
        }
    }

    public void OnBuildModeEnd(bool buildToCurrentPlace)
    {
        Notify("building.onBuildModeEnd", buildToCurrentPlace);

        SetIsOnBuildMode(false);

        // If buildToCurrentPlace == true, complete build action
        if (buildToCurrentPlace)
        {
            BuildToCurrentPlace();
        }
        // Else, destroy the object
        else
        {
            CancelBuild();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!GetIsPlacedOnMap() && collider.GetComponent<MapItemView>())
        {
            Notify("building.onCollisionWithAnotherBuildingStay");
            SetCanBeBuilt(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!GetIsPlacedOnMap() && collider.GetComponent<MapItemView>())
        {
            Notify("building.onCollisionWithAnotherBuildingEnd");
            SetCanBeBuilt(true);
        }
    }
}
