/*
Created By: Tyler McMillan
Description: This script deals with randomly changing how the clouds look / act
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudSystemScript : MonoBehaviour
{

    //max and min speed of clouds for randomizing speed
    [SerializeField] float _minMoveSpeed = 0.3f;
    [SerializeField] float _maxMoveSpeed = 2f;
    //max and min size of clouds for randomizing size
    [SerializeField] float _minSize = 150f;
    [SerializeField] float _maxSize = 400f;

    //two positions that create the vector for clouds to spawn on
    [SerializeField] GameObject _spawnTransform1;
    [SerializeField] GameObject _spawnTransform2;


    //different types of cloud sprites
    [SerializeField] List<Sprite> _normalCloudSprites;
    [SerializeField] List<Sprite> _semiRareCloudSprites;
    [SerializeField] List<Sprite> _rareCloudSprites;



    public void RespawnCloud(GameObject m_cloudObj) //change where cloud is, and its sprite
    {
        //spawn in rectangle made by 2 transforms
        Vector3 m_newPos = new Vector3(Random.Range(_spawnTransform1.transform.localPosition.x, _spawnTransform2.transform.localPosition.x),Random.Range(_spawnTransform1.transform.localPosition.y, _spawnTransform2.transform.localPosition.y), 0);
        m_cloudObj.transform.localPosition = m_newPos; //set cloud to its new position

        m_cloudObj.GetComponent<RectTransform>().sizeDelta = new Vector2(Random.Range(_minSize, _maxSize), Random.Range(_minSize, _maxSize)); //resize the cloud

        ShuffleCloudImage(m_cloudObj);
    }
    public float ChangeCloudSpeed() //randomize cloud speed
    {
        return Random.Range(_minMoveSpeed, _maxMoveSpeed);
    }
    public void ShuffleCloudImage(GameObject _cloudObj) //change sprite of cloud
    {
        int m_randomNum = Random.Range(0, 100); //randomize sprite depending on how rare it is
        if (m_randomNum < 75)
        {
            _cloudObj.GetComponent<Image>().sprite = _normalCloudSprites[Random.Range(0, _normalCloudSprites.Count)];
        }
        else if (m_randomNum < 94)
        {
            _cloudObj.GetComponent<Image>().sprite = _semiRareCloudSprites[Random.Range(0, _semiRareCloudSprites.Count)];
        }
        else if (m_randomNum < 101)
        {
            _cloudObj.GetComponent<Image>().sprite = _rareCloudSprites[Random.Range(0, _rareCloudSprites.Count)];
        }
    }
    public void ToggleCloudSystem(bool m_isOn){
        this.gameObject.SetActive(m_isOn);
    }
}
