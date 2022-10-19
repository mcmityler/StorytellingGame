/*
Created By: Tyler McMillan
Description: This script deals with the cow quit button and its functions on the start menu
*/
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CowExitScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _cowAnimator; //animator that controls cows head animation and other cow exit button animations
    [SerializeField] Image _buttonBlocker;//image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    bool _buttonPressed = false; //are you currently blocking the buttons (aka button has been pressed dont let it do anything else)
    private bool _mouseinCow = false; //is the mouse hovering on the cow (makes it so animation is played when mouse quickly goes over it)
    [SerializeField] private float _cowTime = 1f; //how long before playing animations (so you dont go over fast and it plays exit quick)
    private float _cowCounter = 0f; //how long have you been hovering over the cow
    [SerializeField] HoverTriggerScript _hoverHighlightScript;

    void Update()
    {
        if (_mouseinCow) //if hovering count up timer
        {
            _cowCounter += Time.deltaTime;
            if (_cowCounter >= _cowTime) //if timer is above how long you want play head up animation
            {
                _mouseinCow = false; //set mouse in cow to false so this doesnt retrigger forever
                _cowAnimator.ResetTrigger("CowDown"); //make sure head down animation isnt triggered
                _cowAnimator.SetTrigger("CowUp"); //trigger head up animation
                CowMoo(); //play cow moo sound

            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData) //what to do when the mouse enters object
    {
        _mouseinCow = true; //mouse is now hovering over cow (start counter in update)
    }

    public void OnPointerExit(PointerEventData eventData)//what to do when the mouse leaves object
    {
        if (_buttonPressed == false) //do this if you havent already clicked button
        {
            _mouseinCow = false; //mouse is no longer hovering on cow (stop counter)
            _cowAnimator.ResetTrigger("CowUp"); //reset head up animation trigger
            _cowAnimator.SetTrigger("CowDown"); //trigger head down animation
            _cowCounter = 0; //reset counter
        }

    }
    void OnEnable() //what to do when this object is enabled
    {
        _cowAnimator.ResetTrigger("CowUp"); //reset both triggers
        _cowAnimator.ResetTrigger("CowDown");
        _cowCounter = 0; //reset counter
        _mouseinCow = false; //mouse not in cow
    }
    public void BlockButtons(bool m_isbuttonBlocker)
    {
        _buttonBlocker.gameObject.SetActive(m_isbuttonBlocker); //activate or hide button blocker depending if you press quit button or no button on are you sure screen
        _mouseinCow = m_isbuttonBlocker; // set if mouse is still in cow (if you want to play head up anim)
        _buttonPressed = m_isbuttonBlocker; //make sure head animation stays up until option selected
        _hoverHighlightScript.ToggleClickedColour(m_isbuttonBlocker);

        if (!m_isbuttonBlocker)//trigger cow head down if you press no on are you sure screen or pressed escape
        {
            _cowAnimator.ResetTrigger("CowUp"); 
            _cowAnimator.SetTrigger("CowDown");
            _cowCounter = 0;
        }
    }
    public bool GetButtonPressed(){
        return _buttonPressed;
    }
    public void CowMoo() //play cow moo sound
    {
        FindObjectOfType<SoundManager>().PlayAnimal("Cow"); //play animal sound for cow
    }

}
