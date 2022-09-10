/*
Created By: Tyler McMillan
Description: This script deals with credit screen functionality
*/
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    // ----------------------- FUNCTIONS THAT OPEN LINKS -------------------------------
     public void OpenCreditDocument(){
        Application.OpenURL("https://docs.google.com/document/d/1OjOGnWkLM-nYdLfWpYDo36uKYdJsHYqqzmlC2kldV5M/edit?usp=sharing");
    }
    public void linkedInURL(){
        Application.OpenURL("https://www.linkedin.com/in/tyler-mcmillan-580603216/");
    }
     public void gitHubURL(){
        Application.OpenURL("https://github.com/mcmityler");
    }
    public void itchioURL(){
        Application.OpenURL("https://mcmityler.itch.io/");
    }
}
