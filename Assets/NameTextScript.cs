using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode()]
public class NameTextScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying) return; //if this isnt the editor return
        if(string.IsNullOrEmpty(this.gameObject.GetComponent<TMP_Text>().text) == false) //change name of bouncing text game object to match text box if its not blank or null
            this.gameObject.name = this.gameObject.GetComponent<TMP_Text>().text + " (TMP)";
    }
}
