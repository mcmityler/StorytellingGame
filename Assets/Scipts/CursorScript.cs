/*
Created By: Tyler McMillan
Description: This script deals with cursors and changing them
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Texture2D _defaultCursorTexture; //default cursor texture
    [SerializeField] private Texture2D _pumpkinPieCursorTexture; //pumpkin pie cursor  
    [SerializeField] private Texture2D _swirlCursorDefault; //Swirl default cursor
    [SerializeField] private Texture2D _pumpkinDefaultCursorTexture; //pumpkin default cursor 
    [SerializeField] private Texture2D _iceCreamCursorDefault; //icecream default cursor 
    [SerializeField] private Texture2D _candyCornCursorTexture; //candy Corn  cursor 





    [SerializeField] private List<Texture2D> _swirlAnimatedCursorTexture; //swirl texture (animated)
    [SerializeField] private List<Texture2D> _jackoAnimatedCursorTexture; //jackolantern texture (animated)
    [SerializeField] private List<Texture2D> _pumpkinPieAnimatedCursorTexture;//pumpkin pie texture (animated)
    [SerializeField] private List<Texture2D> _iceCreamAnimatedCursorTexture;//ice cream texture (animated)


    public enum cursorType
    {
        Default,
        Swirl,
        SwirlAnimated,
        Pumpkin,
        PumpkinPie,
        PumpkinPieAnimated,
        IceCream,
        CandyCorn,
        Jackolantern,
        IceCreamAnimated
    }
    private cursorType _cursorType = cursorType.Default;


    private int _cursorIndex = 0;
    private float _cursorCounter = 0f;
    [SerializeField] private float _swirlTime, _pumpkinPieTime, _jackoTime, _iceCreamTime = 1.0f;
    [SerializeField] private int _swirlResetAmount = 10;
    private int _swirlResetCounter = 0; //how many revolutions should it do before it resets



    private Vector2 _cursorClickLoc;
    // Start is called before the first frame update
    void Awake()
    {
        ChangeCursors("Default"); //set cursor to default cursor
    }
    void Update()
    {
        //if cursor is an animated cursor, update cursors frame when '_cursorCounter' value becomes greater then respected time per frame of cursor animation
        switch (_cursorType)
        {
            case cursorType.SwirlAnimated:
                if (_cursorCounter >= _swirlTime)
                {
                    _cursorCounter = 0f;
                    ChangeCursors("SwirlAnimated");
                }
                break;
            case cursorType.PumpkinPieAnimated:
                if (_cursorCounter >= _pumpkinPieTime)
                {
                    _cursorCounter = 0f;
                    ChangeCursors("PumpkinPieAnimated");
                }
                break;
            case cursorType.Jackolantern:
                if (_cursorCounter >= _jackoTime)
                {
                    _cursorCounter = 0f;
                    ChangeCursors("Jackolantern");
                }
                break;
            case cursorType.IceCreamAnimated:
                if (_cursorCounter >= _iceCreamTime)
                {
                    _cursorCounter = 0f;
                    ChangeCursors("IceCreamAnimated");
                }
                break;
        }
        if (_cursorType == cursorType.SwirlAnimated || _cursorType == cursorType.PumpkinPieAnimated || _cursorType == cursorType.Jackolantern || _cursorType == cursorType.IceCreamAnimated)
        {
            _cursorCounter += Time.deltaTime; //count up if its an animated cursor
        }
    }
    public void ChangeCursors(string m_cursorName) //function to change cursor type or change certain cursors animation
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _cursorClickLoc = new Vector2(0, 0);
        switch (m_cursorName)
        {
            case "Default":
                Cursor.SetCursor(_defaultCursorTexture, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.Default;
                break;
            case "Swirl":
                Cursor.SetCursor(_swirlCursorDefault, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.Swirl;
                break;
            case "CandyCorn":
                Cursor.SetCursor(_candyCornCursorTexture, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.CandyCorn;
                break;
            case "Pumpkin":
                Cursor.SetCursor(_pumpkinDefaultCursorTexture, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.Pumpkin;
                break;
            case "PumpkinPie":
                Cursor.SetCursor(_pumpkinDefaultCursorTexture, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.PumpkinPie;
                break;
            case "IceCream":
                Cursor.SetCursor(_iceCreamCursorDefault, _cursorClickLoc, CursorMode.Auto);
                _cursorType = cursorType.IceCream;
                break;
            case "Jackolantern":
                if (_cursorIndex >= _jackoAnimatedCursorTexture.Count || _cursorType != cursorType.Jackolantern) //Reset cursour index back to start if it is over or if its just starting
                    _cursorIndex = 0;
                _cursorType = cursorType.Jackolantern;
                Cursor.SetCursor(_jackoAnimatedCursorTexture[_cursorIndex++], _cursorClickLoc, CursorMode.Auto);//set cursor texture and increase index count
                break;
            case "PumpkinPieAnimated":
                if (_cursorIndex >= _pumpkinPieAnimatedCursorTexture.Count || _cursorType != cursorType.PumpkinPieAnimated) //Reset cursour index back to start if it is over or if its just starting
                    _cursorIndex = 0;
                _cursorType = cursorType.PumpkinPieAnimated;
                Cursor.SetCursor(_pumpkinPieAnimatedCursorTexture[_cursorIndex++], _cursorClickLoc, CursorMode.Auto);//set cursor texture and increase index count
                break;
            case "IceCreamAnimated":
                if (_cursorIndex >= _iceCreamAnimatedCursorTexture.Count || _cursorType != cursorType.IceCreamAnimated) //Reset cursour index back to start if it is over or if its just starting
                    _cursorIndex = 0;
                _cursorType = cursorType.IceCreamAnimated;
                Cursor.SetCursor(_iceCreamAnimatedCursorTexture[_cursorIndex++], _cursorClickLoc, CursorMode.Auto);//set cursor texture and increase index count
                break;
            case "SwirlAnimated":
                if (_cursorIndex >= _swirlAnimatedCursorTexture.Count || _cursorType != cursorType.SwirlAnimated) //Reset cursour index back to start if it is over or if its just starting
                    _cursorIndex = 0;
                _cursorType = cursorType.SwirlAnimated;
                Cursor.SetCursor(_swirlAnimatedCursorTexture[_cursorIndex++], _cursorClickLoc, CursorMode.Auto);//set cursor texture and increase index count
                break;
            default:
                Debug.Log("not a real tag: " + m_cursorName);
                break;
        }
    }

}
