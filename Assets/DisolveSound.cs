using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveSound : MonoBehaviour
{
    public void PlayDisolveSound(){
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
