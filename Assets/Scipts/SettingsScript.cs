/*
Created By: Tyler McMillan
Description: This script deals with opening and closing the settings panel and anything else to do with the settings 
*/
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] GameObject _settingPanel; //ref to settings panel to open and close
    [SerializeField] private GameObject _shopPanel;//ref to shop screen game obj (not in screen manager because you dont want to close other screens when you open this one)
    [SerializeField] private Animator _ticketAnimator;
    [SerializeField] private Animator _settingsAnimator;
    [SerializeField] private Animator _quitCheckAnimator;
    [SerializeField] private Animator _creditCheckAnimator;
    [SerializeField] private bool _settingsOpen = false;
    [SerializeField] private bool _shopOpen = false;
    [SerializeField] private bool _quitCheckOpen = false;
    [SerializeField] private bool _creditCheckOpen = false;
    [SerializeField] private CowExitScript _cowExitScript;





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape")) //Pressed escape or p (p because of webGl makes escape weird if you are full screen)
        {
            PressedEscape(); //toggle screen on or off
        }
    }
    public void PressedEscape() //slightly different then toggle settings screen function (difference being that if any either shop or setting is open it will close on escape press )
    {
        FindObjectOfType<SoundManager>().PlaySound("ButtonClick");
        
        if (_quitCheckOpen == true)
        {
            _quitCheckAnimator.SetTrigger("Close");
           if (_cowExitScript.GetButtonPressed()) //if it was open because of title cow exit button then close animation
            {
                _cowExitScript.BlockButtons(false);
            }
            _quitCheckOpen = false;
        }
        else if (_creditCheckOpen == true)
        {
            _creditCheckAnimator.SetTrigger("CloseCreditCheck");
            _creditCheckOpen = false;
        }
        else if (_settingsOpen == true)
        {
            _settingsAnimator.SetTrigger("CloseSettings");
            _settingsOpen = false;
        }
        else if (_shopOpen == true) //deactivate settings panel if shop panel was open already. (aka close both shop and settings if one is open and escape pressed)
        {
            _settingsAnimator.SetTrigger("CloseShop");
            _shopOpen = false;
        }
        else if (_settingsOpen == false) //**DO THIS LAST OR ELSE IT WILL OPEN BEFORE IT CLOSES
        {
            _settingsAnimator.SetTrigger("OpenSettings");
            _settingsOpen = true;
        }
    }
    public void ToggleSettingScreen() //toggle settings panel on or off
    {
        FindObjectOfType<SoundManager>().PlaySound("ButtonClick");

        if (_settingsOpen == false) //if you are opening the shop, play open ticket animation to show player their ticket amount
        {
            _shopOpen = false;
            _settingsOpen = true;
            _settingsAnimator.SetTrigger("SlideDown");
        }
        else if (_settingsOpen)
        {
            _shopOpen = true;
            _settingsOpen = false;
            _settingsAnimator.SetTrigger("SlideUp");
        }
    }
    public void ToggleShopScreen() //toggle shop panel on or off
    {
        FindObjectOfType<SoundManager>().PlaySound("ButtonClick");

        if (_shopOpen == false) //if you are opening the shop, play open ticket animation to show player their ticket amount
        {
            _shopOpen = true;
            _settingsOpen = false;
            _settingsAnimator.SetTrigger("SlideUp");
            _ticketAnimator.SetTrigger("OpenTickets");
        }
        else if (_shopOpen)
        {
            _shopOpen = false;
            _settingsOpen = true;
            _settingsAnimator.SetTrigger("SlideDown");
        }
    }
    public void SetQuitCheck(bool m_quitCheck){
        _quitCheckOpen = m_quitCheck;
    }
    public void SetCreditCheck(bool m_creditCheck){
        _creditCheckOpen = m_creditCheck;
    }
}
