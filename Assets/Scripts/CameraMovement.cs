using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;
    private bool _drag = false;
    private bool _canBeDragged;

    public BoxCollider2D TilemapBounds;
 
    private void Start()
    {
        _canBeDragged = false;
    }

    private void LateUpdate()
    {
        // If left mouse is pressed and camera is available to drag, start dragging
        if (Input.GetMouseButton(0) && _canBeDragged)
        {
            _difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (_drag == false)
            {
                _drag = true;
                _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        // Camera is not available to drag
        else if(!Input.GetMouseButton(0))
        {
            _drag = false;
        }

        // Update drag position
        if (_drag)
        {
            Camera.main.transform.position = _origin - _difference;
        }

        // Clamp camera movement to map bounds
        float vertExtent = this.GetComponent<Camera>().orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;
        Vector3 linkedCameraPos = this.GetComponent<Camera>().transform.position;
        Bounds areaBounds = TilemapBounds.bounds;
        this.GetComponent<Camera>().transform.position = new Vector3(
            Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
            Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
            linkedCameraPos.z);
    }

    public void SetCanBeDragged(bool canBeDragged)
    {
        this._canBeDragged = canBeDragged;
    }
}