using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant_Construction : MonoBehaviour {

    public GameObject PowerPlant2DObject;
    private GameObject _map;

    // Use this for initialization
    void Start()
    {
        _map = GameObject.FindWithTag("Map");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clickedd()
    {
        GameObject obj = Instantiate(PowerPlant2DObject, _map.transform);
        obj.GetComponent<PowerPlant_Map>().SetFollowMouse(true);
    }
}
