/*
Created By: Tyler McMillan
Description: This script deals with the current cursor eqipped and its animation
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private MouseCursor _currentCursor; //what is the current cursor
    private int _cursorIndex = 0; //what cursor frame are you currently on
    private float _cursorCounter = 0f; //timer for animation
    private float _animationCooldown = 0f; //how long to wait before animating again
    void Update()
    {
        if (_currentCursor.animated) //check if its an animated cursor
        {
            _cursorCounter += Time.deltaTime; //count up if its an animated cursor
            if (_cursorCounter >= _currentCursor.secondPerFrame && (_animationCooldown <= 0f))
            {
                AnimateCursor(); //change cursors animation
            }
            if (_animationCooldown >= 0f) //cool down for animation
            {
                _animationCooldown -= Time.deltaTime; //subtract from waiting cooldown time
            }
        }
    }
    public void ChangeCursor(MouseCursor m_currentCursor) //NUMBER NEEDS TO MATCH NUMBER ON BUTTON
    {
        _currentCursor = m_currentCursor;
        _cursorIndex = 0;
        Cursor.SetCursor(_currentCursor.cursorTextures[_cursorIndex], _currentCursor.cursorClickLoc, CursorMode.Auto); //change to correct cursor
    }
    public void AnimateCursor()
    {
        
            _cursorCounter = 0; //reset counter for next frame

            if (_cursorIndex >= _currentCursor.cursorTextures.Count)//Reset cursour index back to start if it is over or if its just starting
            {
                _cursorIndex = 0;
                _animationCooldown = _currentCursor.cooldownTime; //how long to wait before repeating animation
            }
            Cursor.SetCursor(_currentCursor.cursorTextures[_cursorIndex++], _currentCursor.cursorClickLoc, CursorMode.Auto);//set cursor texture and increase index count
        }
    }

