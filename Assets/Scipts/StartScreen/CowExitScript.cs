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
    [SerializeField] Image _quitOutline, _buttonBlocker;//image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    bool _buttonPressed = false; //are you currently blocking the buttons (aka button has been pressed dont let it do anything else)
    private bool _mouseinCow = false; //is the mouse hovering on the cow (makes it so animation is played when mouse quickly goes over it)
    [SerializeField] private float _cowTime = 1f; //how long before playing animations (so you dont go over fast and it plays exit quick)
    private float _cowCounter = 0f; //how long have you been hovering over the cow
    [SerializeField] private GameObject _QuitTextObj; //Quit text that gameobject that is the name of the button
    [SerializeField] Color _defaultColor = Color.black; //default colour when its not highlighted and you arent hiding
    [SerializeField] Color _highlightColor = Color.white; //default colour when its  highlighted and you ARE hiding
    private bool _hideText = false; //should you hide the text
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
    public void HideText(Toggle m_hideText) //toggle hide text from settings screen
    {
        _hideText = m_hideText.isOn;
        ChangeText(_defaultColor); //make sure text is correct colour
    }
    private void ChangeText(Color m_color) //change title text for cow
    {
        if (_hideText == false) //if you arent hiding text change colour
        {
            foreach (TMP_Text m_text in _QuitTextObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = m_color;
            }
        }
        else //if you are hiding text hide text object
        {
            foreach (TMP_Text m_text in _QuitTextObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = _highlightColor;
            }
            if (m_color == _highlightColor) //depending on colour of highlight being sent change active of text if hiding text is activated
                _QuitTextObj.SetActive(false);
            if (m_color == _defaultColor)
                _QuitTextObj.SetActive(true);
        }
    }
    public void OnPointerEnter(PointerEventData eventData) //what to do when the mouse enters object
    {
        _mouseinCow = true; //mouse is now hovering over cow (start counter in update)
        _quitOutline.gameObject.SetActive(true); //activate highlight for quit button
        ChangeText(_highlightColor);//change color of text or hide depending on setting
    }

    public void OnPointerExit(PointerEventData eventData)//what to do when the mouse leaves object
    {
        if (_buttonPressed == false) //do this if you havent already clicked button
        {
            _mouseinCow = false; //mouse is no longer hovering on cow (stop counter)
            _cowAnimator.ResetTrigger("CowUp"); //reset head up animation trigger
            _cowAnimator.SetTrigger("CowDown"); //trigger head down animation
            _cowCounter = 0; //reset counter
            _quitOutline.gameObject.SetActive(false); //hide outline
            ChangeText(_defaultColor); //change color of text or hide depending on setting
        }

    }
    void OnEnable() //what to do when this object is enabled
    {
        _cowAnimator.ResetTrigger("CowUp"); //reset both triggers
        _cowAnimator.ResetTrigger("CowDown");
        _cowCounter = 0; //reset counter
        _mouseinCow = false; //mouse not in cow
        _quitOutline.gameObject.SetActive(false); //disable highlight
        ChangeText(_defaultColor);//change color of text or hide depending on setting
    }
    public void BlockButtons(bool m_isbuttonBlocker)
    {
        _buttonBlocker.gameObject.SetActive(m_isbuttonBlocker); //activate or hide button blocker depending if you press quit button or no button on are you sure screen
        _mouseinCow = m_isbuttonBlocker; // set if mouse is still in cow (if you want to play head up anim)
        _buttonPressed = m_isbuttonBlocker; //make sure head animation stays up until option selected
        if (!m_isbuttonBlocker)//trigger cow head down if you press no on are you sure screen
        {
            _cowAnimator.ResetTrigger("CowUp"); 
            _cowAnimator.SetTrigger("CowDown");
            _cowCounter = 0;
            _quitOutline.gameObject.SetActive(false);
            ChangeText(_defaultColor);//change color of text or hide depending on setting
        }
    }
    public void CowMoo() //play cow moo sound
    {
        FindObjectOfType<SoundManager>().PlayAnimal("Cow"); //play animal sound for cow
    }

}
