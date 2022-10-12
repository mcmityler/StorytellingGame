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
    [SerializeField] Image _startButtonOutline, _buttonBlocker; //image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    [SerializeField] Animator _chickenAnimator; //animator that controls particles and sprite changed and bawk trigger
    private bool _openStart = false; //animation has started to play, so continue to finish
    private bool _hideText = false; //should you hide text on hover or change colours
    [SerializeField] GameObject _startTitleObj; //object that holds the start button word
    [SerializeField] Color _defaultColor = Color.black; //default colour when you arent hiding the text and when its not highlighted if colour changing
    [SerializeField] Color _highlightColor = Color.white; //default colour when you are hiding text and when its highlighted
 
 
    public void AnimationStarted() //called by chicken start game button so that the animation finishes when clicked and transitions to the next screen
    {
        _openStart = true; //make it so onpointer exit doesnt run if you already clicked

        _buttonBlocker.gameObject.SetActive(true); //enable object that blocks buttons from being pressed
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
        _startButtonOutline.gameObject.SetActive(true); //highlight button to make it more user friendly
        _chickenAnimator.SetTrigger("ChickenHighlight"); //make chicken bawk when you highlight it
        ChangeText(_highlightColor); //change text color or hide

    }
    public void OnPointerExit(PointerEventData eventData) //what to do when mouse exits chicken
    {
        if (_openStart == false) //make sure you havent already clicked start
        {
            _startButtonOutline.gameObject.SetActive(false); //turn off highlight
            _chickenAnimator.SetTrigger("ChickenRest"); //reset animation
            ChangeText(_defaultColor); //change text colour or show text
        }
    }
    void OnEnable() //run when gameobject with script is enabled
    {
        _startButtonOutline.gameObject.SetActive(false); //turn off button highlight
        _openStart = false; //can now see enter and exit animations
        _buttonBlocker.gameObject.SetActive(false); //disable object that blocks buttons from being pressed
        ChangeText(_defaultColor);//change text colour or show text
    }
    public void HideText(Toggle m_hideText) //toggle hide text from settings screen
    {
        _hideText = m_hideText.isOn;
        ChangeText(_defaultColor); //change text colour or show text
    }
    private void ChangeText(Color m_color) //change title text for cow
    {
        if (_hideText == false) //if you arent hiding text change colour
        {
            foreach (TMP_Text m_text in _startTitleObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = m_color;
            }
            
        }
        else //if you are hiding text hide text object
        {
            foreach (TMP_Text m_text in _startTitleObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = _highlightColor;
            }
            if (m_color == _highlightColor) //depending on colour of highlight being sent change active of text if hiding text is activated
                _startTitleObj.SetActive(false);
            if (m_color == _defaultColor)
                _startTitleObj.SetActive(true);
        }
    }
}
