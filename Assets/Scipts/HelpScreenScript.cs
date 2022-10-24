/*
Created By: Tyler McMillan
Description: This script deals with the help screen (on credit screen) and everything to do with it
*/
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpScreenScript : MonoBehaviour
{
    [SerializeField] Animator _helpScreenAnimator;
    [SerializeField] HoverTriggerScript _hoverTrigger;
    [SerializeField] TMP_Text _helpBodyText;

    [SerializeField] [TextArea(5, 4)] List<string> _helpBodyStrings;
    private int _currentPage = 0;
    [SerializeField] TMP_Text _pageCountText;
    void Awake()
    {
        ChangeText(); //set to first text page
    }
    void Update()
    {
        if (Input.GetKeyDown("p")) //escape pressed
        {
            PPressed(); //toggle screen on or off
        }
    }
    void PPressed()
    {
        if (_hoverTrigger.isActiveAndEnabled == false)
        {
            _helpScreenAnimator.SetTrigger("EraseFast");
        }
    }
    public void ChangePage(bool m_nextPage)
    {
        if (m_nextPage) //next page if positive
        {
            _currentPage++;
            if (_currentPage >= _helpBodyStrings.Count)
            {
                _currentPage = 0;
            }
        }
        else //previous page if false
        {
            _currentPage--;
            if (_currentPage < 0)
            {
                _currentPage = _helpBodyStrings.Count - 1;
            }
        }
        _helpScreenAnimator.SetTrigger("EraseFast");

    }
    public void ChangeText()
    {
        _helpBodyText.text = _helpBodyStrings[_currentPage];
        _pageCountText.text = (_currentPage + 1).ToString() + "/" + _helpBodyStrings.Count.ToString();
    }
    public void PlayEraseSound(){
        FindObjectOfType<SoundManager>().PlaySound("Erase");
    }
    public void OpenHelpScreen()
    {
        _helpScreenAnimator.SetTrigger("OpenHelp");
        _hoverTrigger.enabled = false;
    }
    public void CloseHelpScreen()
    {
        _helpScreenAnimator.SetTrigger("CloseHelp");
        _hoverTrigger.enabled = true;
    }
}
