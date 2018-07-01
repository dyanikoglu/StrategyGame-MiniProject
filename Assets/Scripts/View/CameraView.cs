using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class CameraView : View<StrategyGameApplication>
{
    // Variables
    private Vector3 _origin = Vector3.zero;
    private Vector3 _difference = Vector3.zero;
    private bool _currentlyDragging = false;

    // Features
    public void Pan()
    {
        // If left mouse is pressed and camera is available to drag, start dragging
        if (Input.GetMouseButton(0) && app.model.CameraCanBeDragged)
        {
            // Camera component
            var mainCameraRef = GetComponent<Camera>();

            _difference = (mainCameraRef.ScreenToWorldPoint(Input.mousePosition)) -
                          mainCameraRef.transform.position;
            if (!_currentlyDragging)
            {
                _currentlyDragging = true;
                _origin = mainCameraRef.ScreenToWorldPoint(Input.mousePosition);
            }

            // Update drag position
            if (_currentlyDragging)
            {
                mainCameraRef.transform.position = _origin - _difference;
            }

            // Clamp camera movement to map bounds
            var vertExtent = mainCameraRef.orthographicSize;
            var horizExtent = vertExtent * Screen.width / Screen.height;
            var linkedCameraPos = mainCameraRef.transform.position;
            var areaBounds = app.model.Map.GetComponent<BoxCollider2D>().bounds;
            mainCameraRef.transform.position = new Vector3(
                Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                linkedCameraPos.z);
        }

        // Camera is not available to drag
        else if (!Input.GetMouseButton(0))
        {
            _currentlyDragging = false;
        }
    }

    // Events
    private void LateUpdate()
    {
        Pan();
    }

    private void Start()
    {
        app.model.CameraCanBeDragged = true;
    }
}
