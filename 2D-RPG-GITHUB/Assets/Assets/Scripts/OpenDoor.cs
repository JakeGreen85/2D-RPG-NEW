using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Sprite openDoor;
    public void DoorOpen(){
        GetComponent<SpriteRenderer>().sprite = openDoor;
    }
}
