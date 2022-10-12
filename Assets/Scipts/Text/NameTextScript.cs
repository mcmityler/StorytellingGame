/*
Created By: Tyler McMillan
Description: This script deals with automatically changing text game object names to whatever the text box has within editor mode (so i dont have to change contents and title everytime)
*/
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
