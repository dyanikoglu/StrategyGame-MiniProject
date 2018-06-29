using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConstructionButton : MonoBehaviour
{
    public GameObject MapBuildingObject;
    protected GameManager GameManager;

    void Start ()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
	
	void Update () {
		
	}

    public abstract void OnClick();
}
