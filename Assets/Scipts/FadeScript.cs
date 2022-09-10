/*
Created By: Tyler McMillan
Description: This script deals with making the screen fade black and back
*/
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] Animator _fadeAnimator;
    [SerializeField] ScreenManager _screenManager;
    public void FadeBlack() //fade screen black when you press a button to change screens
    {
        _fadeAnimator.SetBool("IsFaded", true); //change animations
    }
    public void ClearFade() //called at the end of fade black animation to change screens and fade back to clear
    {
        _fadeAnimator.SetBool("IsFaded", false); //animate
        _screenManager.ChangeScreens(); //change screen actives
    }
}
