/*
Created By: Tyler McMillan
Description: This script deals with buttons functionality in the title screen menu and some other menus
*/

using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    //screens
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _selectionScreen;
    [SerializeField] private GameObject _helpScreen;
    [SerializeField] private GameObject _creditsScreen;
    [SerializeField] private GameObject _gameScreen;

    
    [SerializeField] private Animator QuitCheckAnimator;
    [SerializeField] private FadeScript _fadeScript;

    private bool _isTitle, _isHelp, _isSelection, _isCredit, _isGame = false;
    GameScript _gameScript;
    void Awake()
    {
        _gameScript = gameObject.GetComponent<GameScript>();
    }
    public void ChangeScreens()
    {
        //close all open screens
        _titleScreen.SetActive(_isTitle);
        _selectionScreen.SetActive(_isSelection);
        _helpScreen.SetActive(_isHelp);
        _creditsScreen.SetActive(_isCredit);
        _gameScreen.SetActive(_isGame);
        if (_isGame)
        {
            _gameScript.StartGame();
        }
        _isSelection = _isTitle = _isGame = _isCredit = _isHelp = false;
    }
    public void QuitGame()
    {
        Application.Quit(); //Quit the game in the application
        Debug.Log("Quit game");
    }

    public void ScreenSelector(string m_screenToOpen) //Select with buttons what screen to open (sets variable to true that opens screen after its faded black)
    {
        if (!_isSelection && !_isTitle && !_isGame && !_isCredit && !_isHelp) //make sure you cant click 2 buttons before the screen actually changes
        {
            _isSelection = _isTitle = _isGame = _isCredit = _isHelp = false; //make sure you haven't selected two screens (or else the one on top will be the one that shows)
            bool m_nonExistent = false;
            switch (m_screenToOpen)
            {
                case "title":
                    _isTitle = true;
                    break;
                case "selection":
                    _isSelection = true;
                    break;
                case "help":
                    _isHelp = true;
                    break;
                case "credits":
                    _isCredit = true;
                    break;
                case "game":
                    _isGame = true;
                    break;
                default:
                    Debug.Log(m_screenToOpen + " not existent");
                    m_nonExistent = true;
                    break;
            }
            if (m_nonExistent == false) //if screen exists fade to black 
            {
                _fadeScript.FadeBlack();
            }
        }
    }
    public void QuitCheck(bool m_open){
        Debug.Log("should you open: " + m_open );
        if(m_open){
            QuitCheckAnimator.SetTrigger("Open");
            QuitCheckAnimator.ResetTrigger("Close");
        }else{
            QuitCheckAnimator.SetTrigger("Close");
            QuitCheckAnimator.ResetTrigger("Open");
        }
    }
}
