using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterEvent : MonoBehaviour
{
    public GameObject current_player; // CURRENT PLAYER
    public GameObject current_camera; //CURRENT CAMERA
    public GameObject switch_player; //THE PLAYER YOU ARE SWITCHING TO
    public GameObject free_look;
    public GameObject switch_camera; //CAMERA WE ARE SWITCHING TO

    public GameObject[] updateDoors; //DOORS THAT NEED TO ACKNOWLEDGE NEW PLAYER

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        switch_camera.SetActive(true); //CAMERA WE ARE SWITCHING TO ACTIVATED
        free_look.SetActive(true); //GAMEOBJECT ACTIVE
        current_camera.SetActive(false); //CURRENT CAMERA DEACTIVATED
        current_player.SetActive(false); //CURRENT PLAYER IS DEACTIVATED
        switch_player.SetActive(true); //PLAYER WE ARE SWITCHING TO IS 
        alertObjects();
        Destroy(this.gameObject);
    }

    void alertObjects()
    {
        for(int index = 0; index<updateDoors.Length; index++)
        {
            updateDoors[index].GetComponent<DoorController>().updatePlayer(switch_player); //ALL AFFECTED OBJECTS SHOULD HAVE THIS METHOD
        }
    }
}
