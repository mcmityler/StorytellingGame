using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture; //default cursor texture
    [SerializeField] private Texture2D _pumpkinPieCursorTexture; //pumpkin pie cursor  
    [SerializeField] private List<Texture2D> _pumpkinCursorTexture; //pumpkin texture (animated)
    [SerializeField] private List<Texture2D> _pumpkinPieAnimatedCursorTexture;//pumpkin pie texture (animated)


    private bool _pumpkin = false;
    private bool _pumpkinPie = false;

    private int _pumpkinCursorIndex = 0;
    private float _pumpkinCursorCounter = 0f;
    [SerializeField] private float _pumpkinTime, _pumpkinPieTime = 1.0f;

    

    private Vector2 _cursorClickLoc;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DefaultCursor();
    }
    void Update(){
        if(_pumpkin){
            //update cursor form
            _pumpkinCursorCounter += Time.deltaTime;
            if(_pumpkinCursorCounter >= _pumpkinTime){
                _pumpkinCursorCounter = 0f;
                PumpkinCursor();
            }
        }
        if(_pumpkinPie){
            //update cursor form
            _pumpkinCursorCounter += Time.deltaTime;
            if(_pumpkinCursorCounter >= _pumpkinPieTime){
                _pumpkinCursorCounter = 0f;
                PumpkinPieAnimatedCursor();
            }
        }
    }
    
    public void DefaultCursor(){
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_cursorTexture,_cursorClickLoc, CursorMode.Auto);
        _pumpkin = false;
        _pumpkinPie = false;
    }
    public void PumpkinPieCursor(){
        
        _cursorClickLoc = new Vector2(0, 0);
        Cursor.SetCursor(_pumpkinPieCursorTexture,_cursorClickLoc, CursorMode.Auto);
        _pumpkin = false;
        _pumpkinPie = false;
    }
    public void PumpkinPieAnimatedCursor(){
        if(_pumpkinPie == false){
            _cursorClickLoc = new Vector2(0, 0);
            _pumpkinCursorIndex = 0;
        }
        _pumpkinPie = true;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_pumpkinPieAnimatedCursorTexture[_pumpkinCursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_pumpkinCursorIndex >= _pumpkinPieAnimatedCursorTexture.Count){
            _pumpkinCursorIndex = 0;
        }
        _pumpkin = false;
    }
    public void PumpkinCursor(){
        if(_pumpkin == false){
            _cursorClickLoc = new Vector2(0, 0);
            _pumpkinCursorIndex = 0;
        }
        _pumpkin = true;
        //set cursor texture and increse index to next
        Cursor.SetCursor(_pumpkinCursorTexture[_pumpkinCursorIndex++],_cursorClickLoc, CursorMode.Auto);
        //Reset cursour index back to start if it is over
        if(_pumpkinCursorIndex >= _pumpkinCursorTexture.Count){
            _pumpkinCursorIndex = 0;
        }
        _pumpkinPie = false;
    }
}
