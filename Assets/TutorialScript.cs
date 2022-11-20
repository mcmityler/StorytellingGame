using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] GameObject _background;
    public void CloseTutorial()
    {

        GameObject.FindGameObjectWithTag("manager").GetComponent<GameScript>().SetTutorialDone(true);
        GameObject.FindGameObjectWithTag("manager").GetComponent<GameScript>().StartGame();
        _background.SetActive(false);

        this.gameObject.SetActive(false);


    }
}
