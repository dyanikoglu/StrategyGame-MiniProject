using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks_Construction : MonoBehaviour
{

    public GameObject Barracks2DObject;
    private GameObject _map;

	// Use this for initialization
	void Start () {
		_map = GameObject.FindWithTag("Map");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Clickedd()
    {
        GameObject obj = Instantiate(Barracks2DObject, _map.transform);
        obj.GetComponent<Barracks_Map>().SetFollowMouse(true);
    }
}
