using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string[] _header = { "" };
    [SerializeField] private string[] _content = { "" };
    [SerializeField] private float _timeBeforeTooltip = 0.2f;
    private float _counter = 0f;
    private bool _showToolTip = false;
    private bool _toolTipOn = false;
    private bool _hideToolTip = false;

    [SerializeField] private string _content2; //alternate content text
    [SerializeField] private string _header2; //alternate header text
    private string _originalContent;//holds first content so you can reset it back if you change the text to another 
    private string _originalHeader; //holds first header so you can reset it back if you change the text to another 


    void Awake()
    {
        _originalContent = _content[0];
        _originalHeader = _header[0];
    }
    void Update()
    {
        if (_showToolTip)
        {
            _counter += Time.deltaTime;
        }
        if (_showToolTip && _counter > _timeBeforeTooltip)
        {
            _showToolTip = false;
            _toolTipOn = true;
            MultiToolTip();
        }
        if (_hideToolTip && _toolTipOn)
        {
            _hideToolTip = false;
            _toolTipOn = false;
            TooltipManager.Hide();
        }
    }
    private int _contentAmount, _headerAmount = 0;
    public void MultiToolTip() //change tool tip if more then one in the array, changes everytime you reenter tool tip if more then 1
    {
        if (_content.Length >= 2 || _header.Length >= 2) //if you have more then one line
        {
            TooltipManager.Show(_content[_contentAmount++], _header[_headerAmount++]);
            if (_contentAmount >= _content.Length)
            {
                _contentAmount = 0;
            }
            if (_headerAmount >= _header.Length)
            {
                _headerAmount = 0;
            }

        }
        else
        {
//            Debug.Log(_content[0] + "   =    " + _header[0]);
            TooltipManager.Show(_content[0], _header[0]);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _showToolTip = true;
        _hideToolTip = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _hideToolTip = true;
        _showToolTip = false;
        _counter = 0;
    }
    public void OnDisable()
    {
        if (_toolTipOn)
        {
            TooltipManager.Hide();
            _showToolTip = false;
            _hideToolTip = false;
            _toolTipOn = false;
            _counter = 0;
        }
    }
    public void OnDestroy()
    {
        if (_toolTipOn)
        {
            TooltipManager.Hide();
            _showToolTip = false;
            _hideToolTip = false;
            _toolTipOn = false;
            _counter = 0;
        }
    }

    public void ChangeContentTip() //if you have want to change to _content2 and _content[0] add this(used for change real/cartoon tool tip)
    {
        string m_contentHold = _content[0];
        _content[0] = _content2;
        _content2 = m_contentHold;
        TooltipManager.Show(_content[0], _header[0]);

    }
    public void ChangeHeaderTip() //if you have want to change to _header2 and _header[0] add this
    {
        string m_headerHold = _header[0];
        _header[0] = _header2;
        _header2 = m_headerHold;
        TooltipManager.Show(_content[0], _header[0]);
    }
    public void ResetTiptext() //reset tool tips back to original if they were changed to content2 (used for change real/cartoon tool tip)
    {
        if (_originalContent == _content2)
        {
            _content2 = _content[0];
        }
        if (_originalHeader == _header2)
        {
            _header2 = _header[0];
        }
        _content[0] = _originalContent;
        _header[0] = _originalHeader;
    }

}
