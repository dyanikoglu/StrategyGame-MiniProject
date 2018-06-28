using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPanel : MonoBehaviour
{

    public GameObject BarracksMenuObject;
    public GameObject PowerPlantMenuObject;
    public GameObject ConstructionObjectsContent;

    public float XOffset;
    public float YOffset;
    public float XSpacing;
    public float YSpacing;

    public int RowCount;

	// Use this for initialization
	void Start ()
	{
	    FillConstructionPanel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FillConstructionPanel()
    {
        for (int i = 0; i < RowCount; i++)
        {
            GameObject obj = Instantiate(BarracksMenuObject, ConstructionObjectsContent.transform);
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x + XOffset, obj.transform.localPosition.y - YOffset - YSpacing * i, obj.transform.localPosition.z);

            obj = Instantiate(PowerPlantMenuObject, ConstructionObjectsContent.transform);
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x + XOffset + XSpacing, obj.transform.localPosition.y - YOffset - YSpacing * i, obj.transform.localPosition.z);
        }
    }
}