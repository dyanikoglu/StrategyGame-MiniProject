using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public bool IsLeftPanel;

    private bool _isAnimationOnGoing = false;
    private bool _isHidden = false;
    private float _targetX = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (_isAnimationOnGoing)
	    {
            AnimationHandler();
	    }
	}

    public void Hide()
    {
        if (!_isHidden)
        {
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
    }

    public void Show()
    {
        if (_isHidden)
        {
            _isHidden = false;
            _targetX = 0;
            _isAnimationOnGoing = true;
        }
    }

    private void AnimationHandler()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, _targetX, 0.1f), rectTransform.anchoredPosition.y);

        if (Mathf.Abs(_targetX - rectTransform.anchoredPosition.x) < 0.1f)
        {
            _isAnimationOnGoing = false;
        }
    }
}
