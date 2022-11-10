using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordColourScript : MonoBehaviour
{
    [SerializeField] GameObject _wordObjBackground;
    [SerializeField] Toggle _bgWordToggle;
    [SerializeField] Color _usedColor;
    [SerializeField] Color _newColor;


    public void ToggleBGColour(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _wordObjBackground.GetComponent<Image>().color = _usedColor;

        }
        else
        {
            _wordObjBackground.GetComponent<Image>().color = _newColor;
        }
        FindObjectOfType<SoundManager>().PlaySound("ButtonClick"); //button click sound when toggle is pressed

    }
    public void ResetToggle()
    {
        _bgWordToggle.isOn = false;
        _wordObjBackground.GetComponent<Image>().color = _newColor;
        _bgWordToggle.interactable = false;
    }
    public void EnableToggle()
    {
        _bgWordToggle.interactable = true;

    }

}
