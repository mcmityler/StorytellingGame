/*
Created By: Tyler McMillan
Description: This script deals with updating tooltip text fields and positioning
*/
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TooltipScript : MonoBehaviour
{
    public TextMeshProUGUI headerField; //ref to top tmp text aka header
    public TextMeshProUGUI contentField; //ref to bot tmp text aka content 
    public LayoutElement layoutElement; //ref to object on tool tip that will wrap text if enable if disabled it will make text box fit text perfectly if its not large enough to wrap

    [SerializeField] private int _characterWrapLimit = 100; //how long to allow text before wrapping text

    public RectTransform rectTransform; //reference to rect transform to change the size of the tool tip bar
    [SerializeField] private float _closeToRight = 1f; //how close should it be to cursor on right of the screen
    [SerializeField] private float _closeToLeft = .15f; //how close should it be to cursor on left of the screen

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //get rect transform on gameobject so you can resize 
    }
    public void SetText(string m_content = "", string m_header = "")//set text in contents and header text boxes on tool tip
    {
        if (string.IsNullOrEmpty(m_header)) //hide header box if empty string or null
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true); //show header since there is text to fill it
        }
        if (string.IsNullOrEmpty(m_content))//hide content box if empty string or null
        {
            contentField.gameObject.SetActive(false);
        }
        else
        {
            contentField.gameObject.SetActive(true); //show content since there is text to fill it
        }
        headerField.text = m_header; //fill header with text
        contentField.text = m_content; //fill content with text 
        int headerLength = headerField.text.Length; //get header text box length
        int contentLength = contentField.text.Length;//get content text box length

        //is there enough text to turn on layoutelement (which wraps the text)
        layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false; //determin if you should wrap text or not (deactivate layout element or enable)

    }
    void Update()
    {
        /* if (headerField != null && contentField != null && Application.isEditor) //---------PREVIEW TOOLTIP IN ENDITOR OTHERWISE NOT NEEDED------------ (also needs editorrun tag at top)
         {
             int headerLength = headerField.text.Length;
             int contentLength = contentField.text.Length;

             //is there enough text to turn on layoutelement (which wraps the text)
             layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;
         }*/
        UpdateTooltipLocation(); //update location of the text box to follow the mouse pos 
    }

    private void UpdateTooltipLocation()//update location of the text box to follow the mouse pos 
    {
        Vector2 m_position = Input.mousePosition;
        float m_pivotX = (m_position.x / Screen.width);
        float m_pivotY = (m_position.y / Screen.height);

        if (m_pivotX <= 0.5f) //if mouse is on the left side of screen make text box more right of mouse
        {
            m_pivotX -= m_pivotX + _closeToLeft;
        }
        if (m_pivotX > 0.5f)//if mouse is on the right side of screen make text box more left of mouse
        {
            m_pivotX = _closeToRight;
        }

        rectTransform.pivot = new Vector2(m_pivotX, m_pivotY);
        transform.position = m_position; //change tooltip box pos
    }
}
