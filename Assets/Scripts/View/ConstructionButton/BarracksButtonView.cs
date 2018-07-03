using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarracksButtonView : ConstructionButtonView {

    // Features
    // None..

    // Events
    // Bound scroll event to construction panel's OnScroll function.
    private void Start()
    {
        var trigger = GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Scroll
        };
        entry.callback.AddListener((eventData) => { app.view.ConstructionPanel.OnScroll(); });
        trigger.triggers.Add(entry);
    }

    public override void OnClicked()
    {
        Notify("barracksButton.onClicked", GetID());
    }
}
