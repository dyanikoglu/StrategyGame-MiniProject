using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class BuildingView : MapItemView
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
        _placedOnMap = true;
    }

    public void CancelBuild()
    {
        Destroy(this.gameObject);
    }

    // Events
    private void Update()
    {
        if (!_placedOnMap && _isOnBuildMode)
        {
            Notify("building.onBuildModeStay");
        }
    }

    public void OnBuildModeStart()
    {
        Notify("building.onBuildModeStart");
    }

    public void OnBuildModeEnd(bool buildToCurrentPlace)
    {
        Notify("building.onBuildModeEnd", buildToCurrentPlace);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!_placedOnMap && collider.GetComponent<MapItemView>())
        {
            Notify("building.onCollisionWithAnotherBuildingStay");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!_placedOnMap && collider.GetComponent<MapItemView>())
        {
            Notify("building.onCollisionWithAnotherBuildingEnd");
        }
    }

    public override void OnClicked() 
    {

    }

}
