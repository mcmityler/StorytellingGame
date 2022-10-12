/*
Created By: Tyler McMillan
Description: This script deals with opening and closing the settings panel and anything else to do with the settings 
*/
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] GameObject _settingPanel; //ref to settings panel to open and close
    [SerializeField] private GameObject _shopPanel;//ref to shop screen game obj (not in screen manager because you dont want to close other screens when you open this one)
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) //escape pressed
        {
            PressedEscape(); //toggle screen on or off
        }
    }
    public void PressedEscape() //slightly different then toggle settings screen function (difference being that if any either shop or setting is open it will close on escape press )
    {
        _settingPanel.SetActive(!_settingPanel.activeInHierarchy);
        if (_shopPanel.activeInHierarchy)
        {
            _settingPanel.SetActive(false);

        }
        _shopPanel.SetActive(false);
    }
    public void ToggleSettingScreen() //toggle settings panel on or off
    {
        _settingPanel.SetActive(!_settingPanel.activeInHierarchy);
        _shopPanel.SetActive(false);
    }
    public void ToggleShopScreen() //toggle shop panel on or off
    {
        _shopPanel.SetActive(!_shopPanel.activeInHierarchy); //depending on whether the panel is active in the hierarchy open or close it and change settings on variable
        _settingPanel.SetActive(false);
    }
}
