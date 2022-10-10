using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _current;
    public TooltipScript tooltip;

    public void Awake(){
        _current = this;
    }
    public static void Show(string m_content, string m_header){
        _current.tooltip.SetText(m_content,m_header);
        _current.tooltip.gameObject.SetActive(true);
        _current.tooltip.gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
    }
    public static void Hide(){
        if(_current.tooltip.gameObject!= null){
            
            _current.tooltip.gameObject.SetActive(false);
        }
    }
}
