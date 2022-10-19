/*
Created By: Tyler McMillan
Description: This script deals with the chicken start button and its functions on the start menu
*/
using TMPro;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChickenStartScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ScreenManager _screenManager; //reference to screen manager to change screens after animation is done playing
    [SerializeField] ParticleSystem _featherParticles; //feather poof particles
    [SerializeField] Image _buttonBlocker; //image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    [SerializeField] Animator _chickenAnimator; //animator that controls particles and sprite changed and bawk trigger
    private bool _openStart = false; //animation has started to play, so continue to finish
    [SerializeField] HoverTriggerScript _hoverHighlightScript;


    public void AnimationStarted() //called by chicken start game button so that the animation finishes when clicked and transitions to the next screen
    {
        _openStart = true; //make it so onpointer exit doesnt run if you already clicked

        _buttonBlocker.gameObject.SetActive(true); //enable object that blocks buttons from being pressed
        _hoverHighlightScript.ToggleClickedColour(true);
    }
    public void StartGame() //called by animator when animation is done so that you can move to the next screen (selection screen)
    {
        _screenManager.ScreenSelector("selection");
    }
    public void PoofFeather() //play animation on chickent o show feathers poofing
    {
        _featherParticles.Play();

    }
    public void ChickenBawk() //play chicken animal noise
    {
        FindObjectOfType<SoundManager>().PlayAnimal("Chicken");
    }
    public void OnPointerEnter(PointerEventData eventData) //what to do when mouse is hovering chicken button
    {
        _chickenAnimator.SetTrigger("ChickenHighlight"); //make chicken bawk when you highlight it

    }
    public void OnPointerExit(PointerEventData eventData) //what to do when mouse exits chicken
    {
        if (_openStart == false) //make sure you havent already clicked start
        {
            _chickenAnimator.SetTrigger("ChickenRest"); //reset animation

        }
    }
    public void ResetChickenButton()
    {
        _openStart = false; //can now see enter and exit animations
        _buttonBlocker.gameObject.SetActive(false); //disable object that blocks buttons from being pressed
        _hoverHighlightScript.ToggleClickedColour(false);
        _chickenAnimator.SetTrigger("ChickenRest"); //reset animation

    }
    void OnEnable() //run when gameobject with script is enabled
    {
        ResetChickenButton();

    }
}
