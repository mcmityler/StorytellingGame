/*
Created By: Tyler McMillan
Description: This script deals with the chicken start button and its functions on the start menu
*/
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChickenStartScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ScreenManager _screenManager;
    [SerializeField] ParticleSystem _featherParticles;
    [SerializeField] Image _startButtonOutline, _buttonBlocker; //image that highlight button when your cursor enters the button -- image that blocks buttons when its enabled so you cant press anything you arent intended to
    [SerializeField] Animator _chickenAnimator;
    private bool _openStart = false;
    public void StartGame()
    {
        _screenManager.ScreenSelector("selection");
    }
    public void PoofFeather()
    {
        _featherParticles.Play();
        _openStart = true;

    }
    public void ChickenBawk()
    {
        FindObjectOfType<SoundManager>().PlayAnimal("Chicken");
    }

    //chicken egg enter and exit script animation <------------------------------------------------------------------------NEXT UP! :D
    public void OnPointerEnter(PointerEventData eventData)
    {
        _startButtonOutline.gameObject.SetActive(true); //highlight button to make it more user friendly
        _chickenAnimator.SetTrigger("ChickenHighlight"); //make chicken bawk when you highlight it

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_openStart == false) //make sure you havent already clicked start
        {
            _startButtonOutline.gameObject.SetActive(false); //turn off highlight
            _chickenAnimator.SetTrigger("ChickenRest"); //reset animation
        }


    }
    public void Started() //called by chicken start game button so that the animation finishes when clicked and transitions to the next screen
    {
        _openStart = true; //make it so onpointer exit doesnt run if you already clicked

        _buttonBlocker.gameObject.SetActive(true); //enable object that blocks buttons from being pressed
    }
    void OnEnable() //run when gameobject with script is enabled
    {
        _startButtonOutline.gameObject.SetActive(false); //turn off button highlight
        _openStart = false; //can now see enter and exit animations
        _buttonBlocker.gameObject.SetActive(false); //disable object that blocks buttons from being pressed
    }
}
