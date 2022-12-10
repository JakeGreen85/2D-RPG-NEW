using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button yourButton;
    GameObject invManager;

	void Start () {
		yourButton = GetComponent<Button>();
		yourButton.onClick.AddListener(TaskOnClick);
        invManager = GameObject.Find("InventoryManager");
	}

    private void Update() {
        if(yourButton == null){
            yourButton = GetComponent<Button>();
            yourButton.onClick.AddListener(TaskOnClick);
            invManager = GameObject.Find("InventoryManager");
        }
    }

	void TaskOnClick(){
        int n = System.Int32.Parse(gameObject.name.Substring(6));
        if(gameObject.CompareTag("Inventory Slot") && invManager.GetComponent<PlayerInventoryController>().inventory[n-1] != null && invManager.GetComponent<PlayerInventoryController>().inventory[n-1].CompareTag("Equipment")){
		    invManager.GetComponent<EquippedItemsController>().EquipItem(invManager.GetComponent<PlayerInventoryController>().inventory[n-1]);
        }
        else if(gameObject.CompareTag("Equipment Slot") && invManager.GetComponent<EquippedItemsController>().equippedItems[n-1] != null){
            invManager.GetComponent<EquippedItemsController>().UnequipItem(invManager.GetComponent<EquippedItemsController>().equippedItems[n-1]);
        }
	}
}
