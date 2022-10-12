/*
Created By: Tyler McMillan
Description: This script deals with triggering show and hide tool bar
*/
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string[] _header = { "" }; //array of header strings, if multiple strings it will rotate through them everytime you enable tooltip it will rotate to the next one ** CANT BE EMPTY
    [SerializeField] private string[] _content = { "" };//array of content strings, if multiple strings it will rotate through them everytime you enable tooltip it will rotate to the next one ** CANT BE EMPTY
    [SerializeField] private float _timeBeforeTooltip = 0.2f; //how long to wait before displaying tool tip, so it doesnt instantly show if you dont want it to
    private float _counter = 0f; //counter for tool tip bar to show
    private bool _showToolTip = false; //should you show tool tip
    private bool _toolTipOn = false; //is the tool tip on
    private bool _hideToolTip = false; //should you hide tool tip

    [SerializeField] private string _content2; //alternate content text (changes on calling "ChangeContentText" to whatever you want)
    [SerializeField] private string _header2; //alternate content text (changes on calling "ChangeHeaderText" to whatever you want)
    private string _originalContent;//holds first content so you can reset it back if you change the text to another 
    private string _originalHeader; //holds first header so you can reset it back if you change the text to another 
    private int _contentAmount, _headerAmount = 0; //how many headers or contents strings there are


    void Awake()
    {
        _originalContent = _content[0]; //hold original content
        _originalHeader = _header[0]; //hold original header
    }
    void Update()
    {
        if (_showToolTip) //are you hovering something that has a tool tip
        {
            _counter += Time.deltaTime; //start counter
            //when counter is greater then time before tool tip then show tool tip
            if (_counter > _timeBeforeTooltip) 
            {
                _showToolTip = false;
                _toolTipOn = true;
                MultiToolTip();
            }
        }
        if (_hideToolTip && _toolTipOn)
        {
            _hideToolTip = false;
            _toolTipOn = false;
            TooltipManager.Hide();
        }
    }
    public void MultiToolTip() //change tool tip if more then one in the array, changes everytime you reenter tool tip if more then 1
    {
        if (_content.Length >= 2 || _header.Length >= 2) //if you have more then one line ... switch between them every time you enter and exit tool tip
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
        else //show tool tip if only one header and one content
        {
            TooltipManager.Show(_content[0], _header[0]);
        }
    }


    public void OnPointerEnter(PointerEventData eventData) //mouse hovering obj with tool tip trigger script on it
    {
        _showToolTip = true; //show tool tip (start counter in update)
        _hideToolTip = false; //no longer hide tool tip
    }
    public void OnPointerExit(PointerEventData eventData)//mouse no longer hovering obj with tool tip trigger script on it
    {
        _hideToolTip = true; //hide tool tip
        _showToolTip = false; //no longer show tool tip
        _counter = 0; //reset tool tip counter
    }
    public void OnDisable()
    {
        if (_toolTipOn) //if tool tip is on and object with trigger gets disabled hide tool tip && reset counter
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
        if (_toolTipOn) //if tool tip is on and object with trigger gets destroyed hide tool tip && reset counter
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
        string m_contentHold = _content[0]; //temp string to hold what it currently is
        _content[0] = _content2; //set content to second message
        _content2 = m_contentHold; //set content 2 to temp string
        TooltipManager.Show(_content[0], _header[0]); //reshow tool tip with new word

    }
    public void ChangeHeaderTip() //if you have want to change to _header2 and _header[0] add this
    {
        string m_headerHold = _header[0];
        _header[0] = _header2;
        _header2 = m_headerHold;
        TooltipManager.Show(_content[0], _header[0]);
    }
    public void ResetTiptext() //reset tool tips back to original if they were changed to content2/header2 (used for change real/cartoon tool tip back to original if you shuffle while its on the second tool tip)
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
