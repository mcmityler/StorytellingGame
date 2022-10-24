using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WordObjectTest : MonoBehaviour
{
    [SerializeField] Animator _wordsAnimimator;
    [SerializeField] Animator Word1Animator;

    [SerializeField] GameObject[] _imageObjs;
    [SerializeField] List<Sprite> _SpriteList;
    int counter = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) //escape pressed
        {
            QPressed(); //toggle screen on or off
        }
    }

    private void QPressed()
    {
        _wordsAnimimator.enabled = false;
    }
    public void ChangeImage(int m_imageNum) //0 = normal, 1 = placeholder 1, 2 = placeholder 2
    {
        //Debug.Log("Change image num: " + m_imageNum.ToString());
        int m_random = Random.Range(0, _SpriteList.Count);
        bool m_sameImage = false;
        foreach (var m_spritePlaceholder in _imageObjs) //stop image from being duplicate
        {
            if (_SpriteList[m_random].name == m_spritePlaceholder.GetComponent<Image>().sprite.name)
            {
                m_sameImage = true;
            }
        }
        if (!m_sameImage)
        {
            _imageObjs[m_imageNum].GetComponent<Image>().sprite = _SpriteList[m_random];
        }
        else
        {
            ChangeImage(m_imageNum);
        }

    }
    public void AnimationCompleted()
    {
        counter--;
        Debug.Log(counter.ToString());
        if (counter < 0)
        {
            Debug.Log("Done, setInteger or SetTrigger in animator to play finishing animation and dont start shuffling until button is pressed again");
        }
    }
}
