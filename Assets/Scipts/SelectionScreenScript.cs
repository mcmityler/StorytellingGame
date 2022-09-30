using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenScript : MonoBehaviour
{
    [SerializeField] private GameScript _gameScript;
    private int _numOfWords = 2;
    private bool _isEasyAnim = false;
    private string _difficutly = "easy";

    [SerializeField] private Animator[] _wordNumAnimators;
    [SerializeField] private Animator[] _difficultyAnimators;

    void OnEnable()
    {

        //_numOfWords = _gameScript.GetWordCount();
        PlayDiceAnimation(_numOfWords);
        PlayDifficultyAnim(_difficutly);
    }
    public void PlayDiceAnimation(int _diceNum)
    {
        Debug.Log(_diceNum + " "+ _difficutly);
        //cant call close trigger on all anims or else if it didnt already call it it will wait for an opportunity to call it (use reset trigger to cancel triggering it)
        for (int i = 0; i < _wordNumAnimators.Length; i++)
        {
            if (_isEasyAnim == true && _diceNum != _numOfWords)
            {
                if (_diceNum == (i + 2))
                {
                    _wordNumAnimators[i].SetTrigger("OpenEasy");
                }
                else if (_numOfWords == i + 2)
                {
                    _wordNumAnimators[i].SetTrigger("CloseEasy");
                }
            }
            else if (_isEasyAnim == false && _diceNum != _numOfWords)
            {
                if (_diceNum == (i + 2))
                {
                    _wordNumAnimators[i].SetTrigger("OpenHard");
                }
                else if (_numOfWords == i + 2)
                {
                    _wordNumAnimators[i].SetTrigger("CloseHard");
                }
            }
        }
        _numOfWords = _diceNum;
    }
    public void Toggle(Toggle _wordNumAnimToggle)
    {
        _isEasyAnim = _wordNumAnimToggle.isOn;
    }

    public void PlayDifficultyAnim(string m_difficulty)
    {
        if (m_difficulty != _difficutly)
        {
            switch (m_difficulty)
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
            switch (_difficutly)
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
            _difficutly = m_difficulty;
        }
    }
}
