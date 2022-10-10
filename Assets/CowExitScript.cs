/*
Created By: Tyler McMillan
Description: This script deals with the cow quit button and its functions on the start menu
*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CowExitScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _cowAnimator;
    [SerializeField] private float _cowTime = 1f; //how long before playing animations (so you dont go over fast and it plays exit quick)
    [SerializeField] Image _quitOutline, _buttonBlocker;//image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    

    bool _buttonsblocked = false;
    private bool _mouseinCow = false;
    private float _cowCounter = 0f;
    [SerializeField] private GameObject _QuitTextObj;
    void Update()
    {
        if (_mouseinCow)
        {
            _cowCounter += Time.deltaTime;
            if (_cowCounter >= _cowTime)
            {
                _mouseinCow = false;
                _cowAnimator.ResetTrigger("CowDown");
                _cowAnimator.SetTrigger("CowUp");
                CowMoo();

            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseinCow = true;
        _quitOutline.gameObject.SetActive(true);
        _QuitTextObj.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_buttonsblocked == false)
        {


            _mouseinCow = false;
            _cowAnimator.ResetTrigger("CowUp");
            _cowAnimator.SetTrigger("CowDown");
            _cowCounter = 0;
            _quitOutline.gameObject.SetActive(false);
            _QuitTextObj.SetActive(true);
        }

    }
    void OnEnable()
    {
        _cowAnimator.ResetTrigger("CowUp");
        _cowAnimator.ResetTrigger("CowDown");
        _cowCounter = 0;
        _mouseinCow = false;
        _quitOutline.gameObject.SetActive(false);
        _QuitTextObj.SetActive(true);
    }
    public void BlockButtons(bool m_isbuttonBlocker)
    {
        _buttonBlocker.gameObject.SetActive(m_isbuttonBlocker);
        _mouseinCow = m_isbuttonBlocker;
        _buttonsblocked = m_isbuttonBlocker;
        if (!m_isbuttonBlocker)
        {
            _cowAnimator.ResetTrigger("CowUp");
            _cowAnimator.SetTrigger("CowDown");
            _cowCounter = 0;
            _quitOutline.gameObject.SetActive(false);
            _QuitTextObj.SetActive(true);
        }
    }
    public void CowMoo()
    {
        FindObjectOfType<SoundManager>().PlayAnimal("Cow");
    }

}
