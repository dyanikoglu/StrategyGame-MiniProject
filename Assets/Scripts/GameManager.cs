using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Map;
    public ConstructionPanel ConstructionPanel;
    public DetailsPanel DetailsPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetMap()
    {
        return Map;
    }

    public void HideConstructionPanel()
    {
        ConstructionPanel.Hide();
    }

    public void ShowConstructionPanel()
    {
        ConstructionPanel.Show();
    }

    public void HideDetailsPanel()
    {
        DetailsPanel.Hide();
    }

    public void ShowDetailsPanel()
    {
        DetailsPanel.Show();
    }

    public void HideUI()
    {
        HideDetailsPanel();
        HideConstructionPanel();
    }

    public void ShowUI()
    {
        ShowConstructionPanel();
        ShowDetailsPanel();
    }
}
