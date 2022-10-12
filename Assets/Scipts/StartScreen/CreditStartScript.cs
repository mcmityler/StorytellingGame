/*
Created By: Tyler McMillan
Description: This script deals with the credit button and its functions on the start menu
*/
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditStartScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _creditOutline;//image that highlight button when your cursor enters the button
    [SerializeField] GameObject _creditTextObj; //ref to buttons name text
    private bool _hideText = false; //are you hiding the text or changing its colour
    [SerializeField] Color _defaultColor = Color.black; //default colour when changing colours
    [SerializeField] Color _highlightColor = Color.white; //highlighted colour and colour when disappearing

    public void OnPointerEnter(PointerEventData eventData)
    {
        _creditOutline.gameObject.SetActive(true); //enable  button highlight
        ChangeText(_highlightColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _creditOutline.gameObject.SetActive(false);//Disable  button highlight
        ChangeText(_defaultColor);

    }
    
     void OnEnable()
    {
        ChangeText(_defaultColor);
        _creditOutline.gameObject.SetActive(false);//Disable  button highlight
    }
    public void HideText(Toggle m_hideText) //toggle hide text from settings screen
    {
        _hideText = m_hideText.isOn;
        ChangeText(_defaultColor); //make sure its the correct colour
    }
    private void ChangeText(Color m_color) //change title text for cow
    {
        if (_hideText == false) //if you arent hiding text change colour
        {
            foreach (TMP_Text m_text in _creditTextObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = m_color;
            }
        }
        else //if you are hiding text hide text object
        {
            foreach (TMP_Text m_text in _creditTextObj.GetComponentsInChildren<TMP_Text>())
            {
                m_text.color = _highlightColor;
            }
            if (m_color == _highlightColor) //depending on colour of highlight being sent change active of text if hiding text is activated
                _creditTextObj.SetActive(false);
            if (m_color == _defaultColor)
                _creditTextObj.SetActive(true);
        }
    }
     // ----------------------- FUNCTIONS THAT OPEN LINKS -------------------------------
     public void OpenCreditDocument(){
        Application.OpenURL("https://docs.google.com/document/d/1OjOGnWkLM-nYdLfWpYDo36uKYdJsHYqqzmlC2kldV5M/edit?usp=sharing");
    }
    public void linkedInURL(){
        Application.OpenURL("https://www.linkedin.com/in/tyler-mcmillan-580603216/");
    }
     public void gitHubURL(){
        Application.OpenURL("https://github.com/mcmityler");
    }
    public void itchioURL(){
        Application.OpenURL("https://mcmityler.itch.io/");
    }
}
