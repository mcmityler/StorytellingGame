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
    [SerializeField] private Texture2D _candyCornCursor; //candy Corn  cursor 
    


    

    [SerializeField] private List<Texture2D> _swirlAnimatedCursorTexture; //swirl texture (animated)
    [SerializeField] private List<Texture2D> _jackoAnimatedCursorTexture; //jackolantern texture (animated)
    [SerializeField] private List<Texture2D> _pumpkinPieAnimatedCursorTexture;//pumpkin pie texture (animated)
    [SerializeField] private List<Texture2D> _iceCreamAnimatedCursorTexture;//ice cream texture (animated)


    public enum cursorType {
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DefaultCursor();
    }
    void Update(){
        if(_cursorType == cursorType.SwirlAnimated){
            //update cursor form
            _cursorCounter += Time.deltaTime;
            if(_cursorCounter >= _swirlTime){
                _cursorCounter = 0f;
                SwirlAnimatedCursor();
            }
        }
        if(_cursorType == cursorType.PumpkinPieAnimated){
            //update cursor form
            _cursorCounter += Time.deltaTime;
            if(_cursorCounter >= _pumpkinPieTime){
                _cursorCounter = 0f;
                PumpkinPieAnimatedCursor();
            }
        }
         if(_cursorType == cursorType.Jackolantern){
            //update cursor form
            _cursorCounter += Time.deltaTime;
            if(_cursorCounter >= _jackoTime){
                _cursorCounter = 0f;
                JackoAnimatedCursor();
            }
        }
        if(_cursorType == cursorType.IceCreamAnimated){
            //update cursor form
            _cursorCounter += Time.deltaTime;
            if(_cursorCounter >= _iceCreamTime){
                _cursorCounter = 0f;
                IceCreamAnimatedCursor();
            }
        }
    }
    
    public void DefaultCursor(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_defaultCursorTexture,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.Default;
    }
    public void PumpkinPieCursor(){
        
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_pumpkinPieCursorTexture,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.PumpkinPie;
    }
    public void PumpkinCursor(){
       _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_pumpkinDefaultCursorTexture,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.Pumpkin;
    }
    public void PumpkinPieAnimatedCursor(){
        if(_cursorType != cursorType.PumpkinPieAnimated){
            _cursorClickLoc = new Vector2(0, 0);
            _cursorIndex = 0;
        }
        _cursorType = cursorType.PumpkinPieAnimated;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_pumpkinPieAnimatedCursorTexture[_cursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_cursorIndex >= _pumpkinPieAnimatedCursorTexture.Count){
            _cursorIndex = 0;
        }
    }
    
    public void SwirlAnimatedCursor(){
        if(_cursorType != cursorType.SwirlAnimated){
            _cursorClickLoc = new Vector2(0, 0);
            _cursorIndex = 0;
        }
        _cursorType = cursorType.SwirlAnimated;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_swirlAnimatedCursorTexture[_cursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_cursorIndex >= _swirlAnimatedCursorTexture.Count){
            _swirlResetCounter ++; //counter reset when this runs enough
            if(_swirlResetCounter >= _swirlResetAmount){
                
                _cursorIndex = 0;
                _swirlResetCounter = 0;
            }else{
                _cursorIndex = _swirlAnimatedCursorTexture.Count - 1;
            }
        }
    }
    
    public void DefaultSwirlCursor(){
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_swirlCursorDefault,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.Swirl;
    }
    public void DefaultIceCreamCursor(){
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_iceCreamCursorDefault,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.IceCream;
    }
    public void CandyCornCursor(){
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_candyCornCursor,_cursorClickLoc, CursorMode.Auto);
        _cursorType = cursorType.CandyCorn;
    }
    public void JackoAnimatedCursor(){
        if(_cursorType != cursorType.Jackolantern){
            _cursorClickLoc = new Vector2(0, 0);
            _cursorIndex = 0;
        }
        _cursorType = cursorType.Jackolantern;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_jackoAnimatedCursorTexture[_cursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_cursorIndex >= _jackoAnimatedCursorTexture.Count){
            _cursorIndex = 0;
        }
    }
    public void IceCreamAnimatedCursor(){
        if(_cursorType != cursorType.IceCreamAnimated){
            _cursorClickLoc = new Vector2(0, 0);
            _cursorIndex = 0;
        }
        _cursorType = cursorType.IceCreamAnimated;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_iceCreamAnimatedCursorTexture[_cursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_cursorIndex >= _iceCreamAnimatedCursorTexture.Count){
            _cursorIndex = 0;
        }
    }
    
}
