using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScaleScript : MonoBehaviour
{
    [SerializeField] float _maxSize = 1f;
    [SerializeField] float _minSize = 0.5f;
    void Awake(){
        foreach (var m_rect in gameObject.GetComponentsInChildren<RectTransform>())
        {
            float m_randomsize = Random.Range(_minSize, _maxSize);
            m_rect.localScale = new Vector3 (m_randomsize,m_randomsize,1);
        }
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }
}
