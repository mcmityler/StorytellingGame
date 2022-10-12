/*
Created By: Tyler McMillan
Description: This script deals with buttons functionality in the title screen menu and some other menus
*/

using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    //screens
    [SerializeField] private GameObject _titleScreen; //ref to title screen game obj
    [SerializeField] private GameObject _selectionScreen; //ref to selection screen game obj
    [SerializeField] private GameObject _helpScreen; //ref to help screen game obj
    [SerializeField] private GameObject _creditsScreen; //ref to credit screen game obj
    [SerializeField] private GameObject _gameScreen; //ref to game screen game obj
    [SerializeField] private Animator QuitCheckAnimator; //animator that opens and closes quit check on quit button
    [SerializeField] private FadeScript _fadeScript; //reference to the fade screen script to fade screen in and out on button press

    private bool _isTitle, _isHelp, _isSelection, _isCredit, _isGame = false; //what screen button was pressed & what one to change to
    GameScript _gameScript; //reference to game script to start game on start game press
    void Awake()
    {
        _gameScript = gameObject.GetComponent<GameScript>(); //get ref to game manager from same game object as this one
    }
    public void ChangeScreens() //called when screen is fully faded black
    {
        //close all open screens && open desired screen
        _titleScreen.SetActive(_isTitle);
        _selectionScreen.SetActive(_isSelection);
        _helpScreen.SetActive(_isHelp);
        _creditsScreen.SetActive(_isCredit);
        _gameScreen.SetActive(_isGame);
        if (_isGame) //if it was the game screen, start game too
        {
            _gameScript.StartGame();
        }
        _isSelection = _isTitle = _isGame = _isCredit = _isHelp = false; //reset all screen variables for next time
    }
    public void QuitGame()
    {
        Application.Quit(); //Quit the game in the application
        Debug.Log("Quit game"); // for testing to know when you quit in editor
    }

    public void ScreenSelector(string m_screenToOpen) //Called to change screens (send what screen to open and after it fades to black it will open that screen and fade into it)
    {
        if (!_isSelection && !_isTitle && !_isGame && !_isCredit && !_isHelp) //make sure you cant click 2 buttons before the screen actually changes
        {
            _isSelection = _isTitle = _isGame = _isCredit = _isHelp = false; //make sure you haven't selected two screens (or else the one on top will be the one that shows)
            bool m_nonExistent = false; //does the screen exist
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
                default: //if screen doesnt exist print to log name and it doesn't exist
                    Debug.Log(m_screenToOpen + " not existent");
                    m_nonExistent = true;
                    break;
            }
            if (m_nonExistent == false) //if screen name exists fade to black 
            {
                _fadeScript.FadeBlack();
            }
        }
    }
    public void QuitCheck(bool m_open) //called by buttons to open quit check screen
    {
        // Debug.Log("should you open: " + m_open);
        if (m_open) //open quit check (cow quit button // Setting quit button)
        {
            QuitCheckAnimator.ResetTrigger("Close");
            QuitCheckAnimator.SetTrigger("Open");
        }
        else //close quit check (no button on quit check)
        {
            QuitCheckAnimator.ResetTrigger("Open");
            QuitCheckAnimator.SetTrigger("Close");
            if (_titleScreen.activeInHierarchy) //if you are on the title screen and you press no lower cow head and disable blocker
            {
                _titleScreen.transform.Find("CowQuit").GetComponent<CowExitScript>().BlockButtons(false);
            }
        }
    }
}
