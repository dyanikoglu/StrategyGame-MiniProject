using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapItem : MonoBehaviour
{

    protected bool _isOnBuildMode = false;
    protected bool _canBeBuilt = true;
    protected bool _placedOnMap = false;
    protected GameManager GameManager;

    public SpriteRenderer background;

    void Start () {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
	
	void Update () {
	    if (!_placedOnMap && _isOnBuildMode)
	    {
	        BuildMode();
	    }
	}

    private void BuildMode()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Vector2 orientedPoint = new Vector2((int) hit.point.x, (int) hit.point.y);
            this.transform.position = orientedPoint;
        }

        // Destroy the item with right click
        if (Input.GetMouseButtonDown(1))
        {
            CancelBuildAction();
        }

        else if (_canBeBuilt && Input.GetMouseButtonDown(0))
        {
            Build();
        }
    }

    private void Build()
    {
        GameManager.ShowConstructionPanel();
        _placedOnMap = true;
        _isOnBuildMode = false;
    }

    private void CancelBuildAction()
    {
        GameManager.ShowConstructionPanel();
        Destroy(this.gameObject);
    }

    public void SetToBuildMode()
    {
        if (!_placedOnMap)
        {
            GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            GameManager.HideUI();
            _isOnBuildMode = true;
        }
    }

    public void SetCanBeBuilt(bool canBeBuilt)
    {
        _canBeBuilt = canBeBuilt;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!_placedOnMap && collider.GetComponent<MapItem>())
        {
            background.color = new Color(0.5f, 0, 0, 0.5f);
            _canBeBuilt = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!_placedOnMap && collider.GetComponent<MapItem>())
        {
            background.color = new Color(1, 1, 1, 0.5f);
            _canBeBuilt = true;
        }
    }
}
