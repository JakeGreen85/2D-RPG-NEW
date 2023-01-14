using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// MOVE THIS CODE TO GAME MANAGER IN THE FUTURE
public class DisplayUI : MonoBehaviour
{
    public GameObject InvUI;
    public GameObject equipUI;
    // Start is called before the first frame update
    void Start()
    {
        InvUI = GameObject.Find("InventoryUI");
        equipUI = GameObject.Find("EquipmentUI");
    }

    [System.Obsolete]
    public void DisplayEquipment(){
        if(equipUI.active){
            equipUI.SetActive(false);
        }else{
            equipUI.SetActive(true);
        }
    }

    [System.Obsolete]
    public void DisplayInventory(){
        if(InvUI.active){
            InvUI.SetActive(false);
        }else{
            InvUI.SetActive(true);
        }
    }
}
