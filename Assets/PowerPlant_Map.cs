using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant_Map : MonoBehaviour {

    private bool _followMouse = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        if (_followMouse)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Vector2 orientedPoint = new Vector2((int)hit.point.x, (int)hit.point.y);
                this.transform.position = orientedPoint;
            }
        }
    }

    public void SetFollowMouse(bool followMouse)
    {
        _followMouse = followMouse;
    }
}
