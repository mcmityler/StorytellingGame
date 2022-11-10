/*
Created By: Tyler McMillan
Description: This script deals with the credit button and its functions on the start menu
*/
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SheepCreditScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _sheepAnimator;
    [SerializeField] private Animator _titleAnimator;
    [SerializeField] private Animator _creditScreenAnimator;
    [SerializeField] private Animator _creditCheckAnimator;
    [SerializeField] private SettingsScript _settingScript;
    [SerializeField] private GameObject _sheepWool, _sheepHeadWool; //ref to sheep wool and head to change their color

    [SerializeField] private ParticleSystem _shearParticle; //particle system that plays sheep shearing effect on animation
    [SerializeField] private GameObject _buttonBlocker; //blocks buttons from being pressed after this one was already pressed. (gets disabled automatically in chicken Onenable script)
    SoundManager _soundManager; //ref to sound manager to play sheep bah and shearing sound
    private bool _hoveringSound = false; //bool to make sheep bahh after hovering for long enough so its not instant all the time and annoying
    private float _hoverCounter = 0f; //counter for hovering before it makes sheep bah
    [SerializeField] private float _hoverTimer = 0.2f; //how long do you have to hover on sheep before it plays this sound
    [SerializeField] HoverTriggerScript _hoverHighlightScript;
    [SerializeField] CloudSystemScript _cloudSystem;



    void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _sheepAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (_hoveringSound == true)
        {
            _hoverCounter += Time.deltaTime;
            if (_hoverCounter >= _hoverTimer)
            {
                _hoveringSound = false;
                _hoverCounter = 0;
                _soundManager.PlaySound("SheepShort");
            }

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoveringSound = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoveringSound = false;
        _hoverCounter = 0;
    }

    void OnEnable()
    {
        _hoveringSound = false;
        _hoverCounter = 0;
        _hoverHighlightScript.ToggleClickedColour(false);

    }
    public void PlayShearNoise()
    {
        _soundManager.PlaySound("Shears");
    }
    public void PlayShearAnimation() //STARTED ANIMATION!!!
    {
        _buttonBlocker.SetActive(true); //animation started dont allow used to press any more start screen buttons
        _hoverHighlightScript.ToggleClickedColour(true);
        _sheepAnimator.SetTrigger("SheepShear");
    }
    public void PlayShearParticle()
    {
        _shearParticle.Play();
    }
    public void CreditZoomIn()
    {
        _titleAnimator.SetTrigger("OpenCredits");
    }
    public void CreditZoomOut()
    {
        _titleAnimator.SetTrigger("CloseCredits");
    }
    public void CreditScreenOpen()
    {
        _creditScreenAnimator.SetTrigger("OpenCreditScreen");
        _buttonBlocker.SetActive(true);
        _cloudSystem.ToggleCloudSystem(false);

    }
    public void CreditScreenClose()
    {
        _creditScreenAnimator.SetTrigger("CloseCreditScreen");
        _hoverHighlightScript.ToggleClickedColour(false);
        _cloudSystem.ToggleCloudSystem(true);


    }
    public void BackToTitle()
    {
        _sheepAnimator.SetTrigger("BackToTitle");
        SheepColorChange();
        _soundManager.PlaySound("ButtonClick");


    }
    public void CloseButtonBlocker()
    {
        _buttonBlocker.SetActive(false);

    }
    private void SheepColorChange()
    {
        //CHANGE COLOUR OF SHEEPS WOOL AND PARTICLES
        float m_r = Random.Range(0f, 1f);
        float m_g = Random.Range(0f, 1f);
        float m_b = Random.Range(0f, 1f);
        Color m_color = new Color(m_r, m_g, m_b, 1);
        _sheepWool.GetComponent<Image>().color = m_color;
        _sheepHeadWool.GetComponent<Image>().color = m_color;
        ParticleSystem.MainModule m_ma = _shearParticle.main;
        m_ma.startColor = m_color;
    }


    // ----------------------- FUNCTIONS THAT OPEN LINKS -------------------------------
    public void OpenCreditDocument()
    {
        Application.OpenURL("https://docs.google.com/document/d/1OjOGnWkLM-nYdLfWpYDo36uKYdJsHYqqzmlC2kldV5M/edit?usp=sharing");
        _soundManager.PlaySound("ButtonClick");

    }
    public void linkedInURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/tyler-mcmillan-580603216/");
    }
    public void gitHubURL()
    {
        Application.OpenURL("https://github.com/mcmityler");
    }
    public void itchioURL()
    {
        Application.OpenURL("https://mcmityler.itch.io/");
    }
    public void OpenCreditCheck()
    {
        _creditCheckAnimator.SetTrigger("OpenCreditCheck");
        _settingScript.SetCreditCheck(true); //so it knows its open to press escape on
        _soundManager.PlaySound("ButtonClick");

    }
    public void CloseCreditCheck()
    {
        _creditCheckAnimator.SetTrigger("CloseCreditCheck");
        _settingScript.SetCreditCheck(false);//so it knows its closed to press escape to affect setting screen instead of this check box
        _soundManager.PlaySound("ButtonClick");




    }
}
