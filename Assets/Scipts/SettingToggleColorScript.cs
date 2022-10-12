/*
Created By: Tyler McMillan
Description: This script deals with changing the toggle text colour in the settings
*/
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SettingToggleColorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _defaultColour = Color.black; //default colour of the text
    [SerializeField] private Color _highlightColour = Color.white; //default colour when highlighting / hovering toggle text

    public void OnPointerEnter(PointerEventData eventData) //what to do when the mouse enters object
    {
        gameObject.transform.Find("Label (TMP)").GetComponent<TMP_Text>().color = _highlightColour; //highlight colour when hover
    }

    public void OnPointerExit(PointerEventData eventData)//what to do when the mouse leaves object
    {
        gameObject.transform.Find("Label (TMP)").GetComponent<TMP_Text>().color = _defaultColour; //change back to default when exit hover

    }
    void OnEnable() //what to do when this object is enabled
    {
        gameObject.transform.Find("Label (TMP)").GetComponent<TMP_Text>().color = _defaultColour;//change back to default when enabled
        
    }
}
