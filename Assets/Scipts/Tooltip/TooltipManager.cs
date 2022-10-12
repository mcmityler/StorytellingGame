/*
Created By: Tyler McMillan
Description: This script deals with displaying and hiding the tool tip bar
*/
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _current;
    public TooltipScript tooltip;

    public void Awake() //set tooltipmanager obj
    {
        _current = this;
    }
    public static void Show(string m_content, string m_header) //show tool bar and fill its contents
    {
        _current.tooltip.SetText(m_content, m_header); //set text on tool tip
        _current.tooltip.gameObject.SetActive(true); //show tool tip object
        _current.tooltip.gameObject.GetComponent<Animator>().SetTrigger("FadeIn"); //fade tooltip in
    }
    public static void Hide()
    {
        if (_current.tooltip.gameObject != null) //hide gameobject
        {
            _current.tooltip.gameObject.SetActive(false); //hide tool tip object
        }
    }
}
