﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierView : MapItemView
{
    public override void OnClicked()
    {
        Notify("soldierMapItem.onClicked", GetID());
    }
}
