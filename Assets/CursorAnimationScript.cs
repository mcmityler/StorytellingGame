using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimationScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> _cursorButtonObjs;

    [SerializeField] private List<GameObject> _cursorPlaceholderLeftOrder;
    [SerializeField] private List<GameObject> _cursorPlaceholderRightOrder;


    private int _cursorCounterLeft = 0;
    private int _cursorCounterRight = -1;

    private bool _animationDone = true;
    private bool _goingRight = false;
    [SerializeField] private Animator _cursorAnimator;
    void Update()
    {
        if (_animationDone == false) //update cursor buttons location if the animation is still playing
        {
            PlaySlideAnimation();
        }

    }
    void PlaySlideAnimation()
    {
        for (int i = 0; i < _cursorPlaceholderLeftOrder.Count; i++)
        {
            if (_goingRight)
            {
                if (_cursorCounterRight + i < 0)
                {
                    _cursorButtonObjs[_cursorCounterRight + i + _cursorButtonObjs.Count].transform.position = _cursorPlaceholderRightOrder[i].transform.position;
                }
                else
                {
                    _cursorButtonObjs[_cursorCounterRight + i].transform.position = _cursorPlaceholderRightOrder[i].transform.position;
                }
            }
            else if (!_goingRight)
            {
                if (_cursorCounterLeft + i >= _cursorButtonObjs.Count)
                {
                    _cursorButtonObjs[(_cursorCounterLeft + i) - _cursorButtonObjs.Count].transform.position = _cursorPlaceholderLeftOrder[i].transform.position; //counter - how many total gives remains
                }
                else
                {
                    _cursorButtonObjs[_cursorCounterLeft + i].transform.position = _cursorPlaceholderLeftOrder[i].transform.position;
                }
            }
        }
    }

    public void SlidenimationDone()
    {
        _animationDone = true; //let script know animation is now complete
        //add or subtract from counters depending if going left or right
        if (_goingRight)
        {
            _cursorCounterRight--;
            _cursorCounterLeft--;
        }
        else
        {
            _cursorCounterLeft++;
            _cursorCounterRight++;
        }
        //keep counters in bounds
        if (_cursorCounterLeft >= _cursorButtonObjs.Count)
        {
            _cursorCounterLeft = 0;
        }
        if (_cursorCounterRight > -1)
        {
            _cursorCounterRight = -_cursorButtonObjs.Count;
        }
        if (_cursorCounterRight < -_cursorButtonObjs.Count)
        {
            _cursorCounterRight = -1;
        }
        if (_cursorCounterLeft < 0)
        {
            _cursorCounterLeft = _cursorButtonObjs.Count - 1;
        }
    }
    public void SlideAnimationStarted(bool m_goingRight)
    {
        if (_animationDone == false) //if you double press button
        {
            return;
        }
        //play correct animation depending on direction clicked
        if (m_goingRight)
        {
            _cursorAnimator.SetTrigger("SlideRight");
        }
        else if (!m_goingRight)
        {
            _cursorAnimator.SetTrigger("SlideLeft");
        }
        _animationDone = false; //let script know animation in progress
        _goingRight = m_goingRight; //let script know direction clicked
    }
}
