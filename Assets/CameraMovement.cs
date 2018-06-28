using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    public BoxCollider2D tilemapBounds;
    public bool canBeDragged;

    void Start()
    {
        canBeDragged = false;
        ResetCamera = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && canBeDragged)
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if(!Input.GetMouseButton(0))
        {
            Drag = false;
        }

        if (Drag)
        {
            Camera.main.transform.position = Origin - Diference;
        }

        float vertExtent = this.GetComponent<Camera>().orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        Vector3 linkedCameraPos = this.GetComponent<Camera>().transform.position;
        Bounds areaBounds = tilemapBounds.bounds;

        this.GetComponent<Camera>().transform.position = new Vector3(
            Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
            Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
            linkedCameraPos.z);
    }

    public void SetCanBeDragged(bool canBeDragged)
    {
        this.canBeDragged = canBeDragged;
    }
}