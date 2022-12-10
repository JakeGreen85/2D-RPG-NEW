using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    //public Sprite openDoor;
    public string EnterScene;
    public void DoorOpen(){
        // GetComponent<SpriteRenderer>().sprite = openDoor;
    }

    public void Enter(){
        GameObject.Find("GameManager").GetComponent<GameManager>().EnterRoom(EnterScene);
    }
}
