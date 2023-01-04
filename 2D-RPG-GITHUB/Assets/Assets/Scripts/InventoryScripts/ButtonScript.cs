using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button yourButton;
    GameObject invManager;
    GameObject ToolTipUI;

	void Start () {
		yourButton = GetComponent<Button>();
		yourButton.onClick.AddListener(TaskOnClick);
        invManager = GameObject.Find("InventoryManager");
        ToolTipUI = GameManager.Instance.ToolTipUI;
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

    public void OnPointerEnter(PointerEventData data){
        int n = System.Int32.Parse(gameObject.name.Substring(6));
        if(gameObject.CompareTag("Inventory Slot") && invManager.GetComponent<PlayerInventoryController>().inventory[n-1] != null && invManager.GetComponent<PlayerInventoryController>().inventory[n-1].CompareTag("Equipment")){
            ToolTipUI.SetActive(true);
            ItemStats obj = invManager.GetComponent<PlayerInventoryController>().inventory[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + "\n" + ConvertSlot(obj.slot) + "\n\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq;
        }
        else if(gameObject.CompareTag("Equipment Slot") && invManager.GetComponent<EquippedItemsController>().equippedItems[n-1] != null){
            ToolTipUI.SetActive(true);
            ItemStats obj = invManager.GetComponent<EquippedItemsController>().equippedItems[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + ConvertSlot(obj.slot) + "\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq;
        }
    }

    string ConvertSlot(int slot){
        switch (slot)
        {
            case 0:
                return "Helmet";
            case 1:
                return "Chest";
            case 2:
                return "Pants";
            case 3:
                return "Feet";
            case 4:
                return "Off Hand";
            case 5:
                return "Primary Weapon";
            case 6:
                return "Belt";
            case 7:
                return "Shoulders";
            case 8:
                return "Hands";
            case 9:
                return "Neck";
            default:
                Debug.Log("Invalid slot");
                return "null";
        }
    }

    public void OnPointerExit(PointerEventData data) {
        ToolTipUI.SetActive(false);
    }
}
