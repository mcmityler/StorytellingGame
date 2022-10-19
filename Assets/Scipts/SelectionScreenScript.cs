/*
Created By: Tyler McMillan
Description: This script deals with animating the selection screen buttons
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenScript : MonoBehaviour
{
    private int _currentDiceNum = 2;
    private bool _isEasyAnim = false;
    private Difficulty _difficulty = Difficulty.easy;
    public enum Difficulty //difficulty num has to match number pressed on button and be in that numbers pos in the array to work
    {
        easy = 0,
        medium = 1,
        hard = 2,
    }

    [SerializeField] private Animator[] _wordNumAnimators;
    [SerializeField] private Animator[] _difficultyAnimators;
    [SerializeField] private GameScript _gameScript;
    [SerializeField] private List<HoverTriggerScript> _difficultyHighlightTriggers;
    void OnEnable() //when you enable object set its animations to what they should be
    {
        PlayDiceAnimation(_currentDiceNum);
        PlayDifficultyAnim((int)_difficulty);

    }
    public void PlayDiceAnimation(int m_diceNum) //change dice animation on selection screen
    {
        // Debug.Log(_diceNum + " "+ _difficutly);
        //*warning* cant call close trigger on all anims or else if it didnt already call it it will wait for an opportunity to call it (use reset trigger to cancel triggering it)
        for (int i = 0; i < _wordNumAnimators.Length; i++) //cycle through all dice animators
        {
            if (_isEasyAnim == true && m_diceNum != _currentDiceNum) //if easy animation on and dice clicked was different then the one already selected
            {
                if (m_diceNum == (i + 2)) //if dice num passed (2,3,4) equals the correct word object # (i + 2 cause its 0,1,2 instead of 2,3,4)
                {
                    _wordNumAnimators[i].SetTrigger("OpenEasy");
                }
                else if (_currentDiceNum == i + 2)
                {
                    _wordNumAnimators[i].SetTrigger("CloseEasy");
                }
            }
            else if (_isEasyAnim == false && m_diceNum != _currentDiceNum) //if 'hard' animation on and dice clicked was different then the one already selected
            {
                if (m_diceNum == (i + 2))//if dice num passed (2,3,4) equals the correct word object # (i + 2 cause its 0,1,2 instead of 2,3,4)
                {
                    _wordNumAnimators[i].SetTrigger("OpenHard");
                }
                else if (_currentDiceNum == i + 2)
                {
                    _wordNumAnimators[i].SetTrigger("CloseHard");
                }
            }
        }
        _currentDiceNum = m_diceNum;
    }
    public void Toggle(Toggle _wordNumAnimToggle) //toggle between 'hard' and easy animation for dice
    {
        _isEasyAnim = _wordNumAnimToggle.isOn;
    }

    public void PlayDifficultyAnim(int m_difficulty) //change difficulty animation on selection screen  (0 = easy, 1= medium, 2 = hard same as enum)
    {
        int m_oldDifficulty = (int)_difficulty;
        if (m_difficulty != m_oldDifficulty) //only change animation if they clicked a button that isnt already highlighted
        {
            _difficultyAnimators[m_difficulty].SetTrigger("OpenFade"); //open fade of button num sent (match array number)
            _difficultyAnimators[m_oldDifficulty].SetTrigger("CloseFade"); //close fade of last button that was clicked (match array number)
            _difficultyHighlightTriggers[m_difficulty].ToggleClickedColour(true);
            _difficultyHighlightTriggers[m_oldDifficulty].ToggleClickedColour(false);

            _difficulty = (Difficulty)m_difficulty; //update difficulty
            _gameScript.SetWordDifficulty(_difficulty.ToString()); //update difficulty in gamescript
        }
    }
}
