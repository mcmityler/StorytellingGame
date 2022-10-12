/*
Created By: Tyler McMillan
Description: This script deals with the Size Up animation and Sequentially triggering a list of animators to play an animation 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTextScript : MonoBehaviour
{
    // * note you need to play with values in editor to make animation look correct when screen restarts if you are seeing a different anim first
    public float _waitBetween = 0.15f; //how long to wait between playing each individual animation
    public float _waitEnd = 0.5f; //How long to wait before you repeat the animation loop
    public float _waitStart = 0.2f; //How long to wait before you start the animation loop
    List<Animator> _animators = new List<Animator>(); //list of animators on objects you are about to animate

    // Start is called before the first frame update
    void Start()
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>()); //get all children with animators
        StartTitleAnim();
    }
    void OnEnable() //when you enable this script do this. (makes all the letters large)
    {
        StartTitleAnim(); //start animation when the script is enabled
        foreach (Animator m_animator in _animators) //cycle through animators
        {
            m_animator.SetTrigger("SizeUp"); //reset all anims
        }
        
    }
    public void StartTitleAnim()//start loop of animation
    {
        StartCoroutine(LoopAnimation()); 
    }

    IEnumerator LoopAnimation() //loop you want to do
    {
        while (true) //loop forever...
        {
            yield return new WaitForSeconds(_waitStart); //wait before looping
            foreach (Animator m_animator in _animators) //cycle through animators
            {
                m_animator.SetTrigger("SizeUp"); //play animation
                yield return new WaitForSeconds(_waitBetween); //wait before playing next
            }
            yield return new WaitForSeconds(_waitEnd); //wait before looping
        }
    }
}
