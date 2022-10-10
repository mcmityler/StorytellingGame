using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TooltipScript : MonoBehaviour
{
    public TextMeshProUGUI headerField; //ref to top tmp text aka header
    public TextMeshProUGUI contentField; //ref to bot tmp text aka content 
    public LayoutElement layoutElement; //wraps text

    [SerializeField] private int _characterWrapLimit = 100; //how long to allow text before wrapping text

    public RectTransform rectTransform;
    [SerializeField] private float _closeToRight = 1f;
    [SerializeField] private float _closeToLeft = .15f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string m_content = "", string m_header = "")
    {
        if (string.IsNullOrEmpty(m_header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
        }
        if (string.IsNullOrEmpty(m_content))
        {
            contentField.gameObject.SetActive(false);
            
        }
        else
        {
            contentField.gameObject.SetActive(true);
            
        }
        headerField.text = m_header;
        contentField.text = m_content;
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        //Debug.Log(headerLength + "   >  " + contentLength);
        //is there enough text to turn on layoutelement (which wraps the text)
        layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;

    }
    void Update()
    {
        /* if (headerField != null && contentField != null && Application.isEditor) //PREVIEW TOOLTIP IN ENDITOR OTHERWISE NOT NEEDED
         {
             int headerLength = headerField.text.Length;
             int contentLength = contentField.text.Length;

             //is there enough text to turn on layoutelement (which wraps the text)
             layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;
         }*/
        UpdateTooltipLocation();
    }

    private void UpdateTooltipLocation()
    {
        Vector2 m_position = Input.mousePosition;
        float m_pivotX = (m_position.x / Screen.width) ;
        float m_pivotY = (m_position.y / Screen.height) ;

        if(m_pivotX <= 0.5f){
           m_pivotX -= m_pivotX + _closeToLeft;
        }
        if(m_pivotX > 0.5f){
            m_pivotX = _closeToRight;
        }

//        Debug.Log("y: "+m_pivotY + " x: "+m_pivotX);

        rectTransform.pivot = new Vector2(m_pivotX, m_pivotY);
        transform.position = m_position;
    }
}
