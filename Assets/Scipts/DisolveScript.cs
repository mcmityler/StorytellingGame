using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DisolveScript : MonoBehaviour
{

    [SerializeField] private float _amountDissolved = -0.1f;
    [SerializeField] private float _dissolveSpeed = 1f;
    [SerializeField] private float _dissolveSize = 150f;

    [SerializeField] private bool _isDissolving = true;

    [SerializeField] private bool _changeScene = false;


    [SerializeField] private Material _mat;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            _isDissolving = true;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            _isDissolving = false;
        }
        if(_isDissolving){
            if(_amountDissolved >= 0.1f){
                _amountDissolved -= Time.deltaTime * _dissolveSpeed;
            }
            
        }
        if(!_isDissolving){
            if(_amountDissolved < 1){
                _amountDissolved += Time.deltaTime * _dissolveSpeed;
            }
        }
        _mat.SetFloat("_DissolveValue", _amountDissolved);
        _mat.SetFloat("_DissolveSize", _dissolveSize);
        if(_changeScene){
            SceneManager.LoadScene("GameScene");
        }
    }
}
