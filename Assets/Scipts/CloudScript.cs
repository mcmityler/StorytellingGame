/*
Created By: Tyler McMillan
Description: This script deals with individual clouds moving
*/
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f; //what speed the cloud moves at
    Vector3 _cloudMovementAxis = new Vector3(1, 0, 0); //move cloud in the x axis only
    CloudSystemScript _cloudSystem; //ref to cloud system script that randomized and changes components of cloud
    public bool open = true;

    // Start is called before the first frame update
    void Awake()
    {
        _cloudSystem = GetComponentInParent<CloudSystemScript>(); //get reference
        ChangeCloud(); //randomize clouds variables (speed, size, cloud type, position)

        //spawn cloud on the screen whne game starts rather then them all being off screen to start with
        Vector3 m_newPos = new Vector3(Random.Range(-Screen.width / 2, Screen.width / 2), Random.Range(0, Screen.height / 2), 0);
        this.gameObject.transform.localPosition = m_newPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {

            this.gameObject.transform.position += _cloudMovementAxis * _moveSpeed * Time.deltaTime; //move cloud at desired speed
        }

    }
    void OnCollisionEnter2D(Collision2D col) //end of screen colision
    {
        ChangeCloud();
    }
    void ChangeCloud()
    {
        //Change Speed;
        _moveSpeed = _cloudSystem.ChangeCloudSpeed();
        //randomize spawn position
        _cloudSystem.RespawnCloud(this.gameObject);
    }
}
