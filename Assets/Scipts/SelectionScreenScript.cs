/*
Created By: Tyler McMillan
Description: This script deals with animating the selection screen buttons
*/
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenScript : MonoBehaviour
{
    private int _currentDiceNum = 2;
    private bool _isEasyAnim = false;
    private string _difficutly = "easy";

    [SerializeField] private Animator[] _wordNumAnimators;
    [SerializeField] private Animator[] _difficultyAnimators;

    void OnEnable() //when you enable object set its animations to what they should be
    {
        PlayDiceAnimation(_currentDiceNum);
        PlayDifficultyAnim(_difficutly);
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

    public void PlayDifficultyAnim(string m_difficulty) //change difficulty animation on selection screen
    {
        if (m_difficulty != _difficutly) //only change animation if they clicked a button that isnt already highlighted
        {
            switch (m_difficulty) //animate open of what was pressed
            {
                case "easy":
                    _difficultyAnimators[0].SetTrigger("OpenFade");
                    break;
                case "medium":
                    _difficultyAnimators[1].SetTrigger("OpenFade");
                    break;
                case "hard":
                    _difficultyAnimators[2].SetTrigger("OpenFade");
                    break;
            }
            switch (_difficutly) //close animation of what was just open
            {
                case "easy":
                    _difficultyAnimators[0].SetTrigger("CloseFade");
                    break;
                case "medium":
                    _difficultyAnimators[1].SetTrigger("CloseFade");
                    break;
                case "hard":
                    _difficultyAnimators[2].SetTrigger("CloseFade");
                    break;
            }
            _difficutly = m_difficulty; //update current difficulty
        }
    }
}
