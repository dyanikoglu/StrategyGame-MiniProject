using UnityEngine;

public class ButtonFactory
{
    private static ButtonFactory instance;

    public GameObject CreateNewButton(GameObject buttonPrefab)
    {
        GameObject newButton;

        if (buttonPrefab.GetComponent<BarracksButtonView>())
        {
            newButton = GameObject.Instantiate(buttonPrefab);
            return newButton;
        }

        if (buttonPrefab.GetComponent<PowerPlantButtonView>())
        {
            newButton = GameObject.Instantiate(buttonPrefab);
            return newButton;
        }

        if (buttonPrefab.GetComponent<SoldierButtonView>())
        {
            newButton = GameObject.Instantiate(buttonPrefab);
            return newButton;
        }

        return null;

    }

    public static ButtonFactory GetInstance()
    {
        return instance ?? (instance = new ButtonFactory());
    }
}
