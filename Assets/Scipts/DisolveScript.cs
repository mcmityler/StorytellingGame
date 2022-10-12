/*
Created By: Tyler McMillan
Description: This script deals with displaying intro disolve texture material (done through animations)
*/
using UnityEngine;
using UnityEngine.SceneManagement;


public class DisolveScript : MonoBehaviour
{

    [SerializeField] private float _amountDissolved = 0.01f; //how much of the material has disolved 
    [SerializeField] private float _dissolveSpeed = 1f; //how fast to disolve the material
    [SerializeField] private float _dissolveSize = 150f; //size of disolve 'bubbles'

    [SerializeField] private bool _isDissolving = true; //is the object disolving or undisolving

    [SerializeField] private bool _changeScene = false; //change scene by triggering variable change in animation


    [SerializeField] private Material _mat;

    // Update is called once per frame
    void Update()
    {
        /*// testing buttons
        if(Input.GetKeyDown(KeyCode.W)){
            _isDissolving = true;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            _isDissolving = false;
        }*/
        if (_isDissolving) //if disolving subtract from amount disolved until it is disolved
        {
            if (_amountDissolved >= 0.1f)
            {
                _amountDissolved -= Time.deltaTime * _dissolveSpeed;
            }

        }
        if (!_isDissolving)//if disolving add from amount disolved until it is back
        {
            if (_amountDissolved < 1)
            {
                _amountDissolved += Time.deltaTime * _dissolveSpeed;
            }
        }
        //change values in mat texture on object
        _mat.SetFloat("_DissolveValue", _amountDissolved);
        _mat.SetFloat("_DissolveSize", _dissolveSize);
        if (_changeScene) //change scene once its done disolving through animation
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
