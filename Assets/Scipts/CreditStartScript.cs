/*
Created By: Tyler McMillan
Description: This script deals with the credit button and its functions on the start menu
*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditStartScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _creditOutline;//image that highlight button when your cursor enters the button
    [SerializeField] GameObject _creditTextObj; //ref to buttons name text
    public void OnPointerEnter(PointerEventData eventData)
    {
        _creditOutline.gameObject.SetActive(true); //enable  button highlight
        _creditTextObj.SetActive(false); //hide name  text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _creditOutline.gameObject.SetActive(false);//Disable  button highlight
        _creditTextObj.SetActive(true);//show name  text

    }
    
     void OnEnable()
    {
        _creditTextObj.SetActive(true);//show name  text
        _creditOutline.gameObject.SetActive(false);//Disable  button highlight
    }
}
