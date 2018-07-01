using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thelab.mvc;

public class PanelView : View<StrategyGameApplication> {

    // Variables
    public bool IsLeftPanel;
    private bool _isAnimationOnGoing = false;
    private bool _isHidden = false;
    private float _targetX = 0;

    // Features
    public void ShowPanel()
    {
        if (!_isHidden) return;

        _isHidden = false;
        _targetX = 0;
        _isAnimationOnGoing = true;
    }

    public void HidePanel()
    {
        if (_isHidden) return;

        _isHidden = true;
        if (IsLeftPanel)
        {
            _targetX = -GetComponent<RectTransform>().rect.width;
        }
        else
        {
            _targetX = GetComponent<RectTransform>().rect.width;
        }

        _isAnimationOnGoing = true;
    }

    public float GetTargetX()
    {
        return _targetX;
    }

    public void SetIsAnimationOngoing(bool b)
    {
        _isAnimationOnGoing = b;
    }

    // Events
    private void Update()
    {
        if (_isAnimationOnGoing)
        {
            Notify("panel.onAnimationPlaying");
        }
    }
}
