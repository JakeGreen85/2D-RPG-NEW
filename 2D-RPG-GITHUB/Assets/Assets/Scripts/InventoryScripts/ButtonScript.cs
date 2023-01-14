using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button yourButton;
    GameObject ToolTipUI;

    [System.Obsolete]
    void Start () {
		yourButton = GetComponent<Button>();
		yourButton.onClick.AddListener(TaskOnClick);
        ToolTipUI = GameManager.Instance.ToolTipUI;
	}

    [System.Obsolete]
    private void Update() {
        if(yourButton == null){
            yourButton = GetComponent<Button>();
            yourButton.onClick.AddListener(TaskOnClick);
        }
    }

    [System.Obsolete]
    void TaskOnClick(){
        int n = System.Int32.Parse(gameObject.name.Substring(6));
        if(gameObject.CompareTag("Inventory Slot") && GameManager.Instance.ShopUI.active && PlayerInventoryController.Instance.inventory[n-1] != null){
            GameManager.Instance.FindClosestShop().Sell(n-1);
        }
        else if(gameObject.CompareTag("ShopButton") && GameManager.Instance.ShopUI.active){
            GameManager.Instance.FindClosestShop().Buy(n-1);
        }
        else if(gameObject.CompareTag("Inventory Slot") && PlayerInventoryController.Instance.inventory[n-1] != null && PlayerInventoryController.Instance.inventory[n-1].CompareTag("Equipment")){
		    EquippedItemsController.Instance.EquipItem(PlayerInventoryController.Instance.inventory[n-1]);
        }
        else if(gameObject.CompareTag("Equipment Slot") && EquippedItemsController.Instance.equippedItems[n-1] != null){
            EquippedItemsController.Instance.UnequipItem(EquippedItemsController.Instance.equippedItems[n-1]);
        }
	}

    public void OnPointerEnter(PointerEventData data){
        int n = System.Int32.Parse(gameObject.name.Substring(6));
        // ToolTipUI.transform.position = Input.mousePosition;
        if(gameObject.CompareTag("ShopButton") && GameManager.Instance.FindClosestShop().Available[n-1] != null && !GameManager.Instance.FindClosestShop().buyback){
            ToolTipUI.SetActive(true);
            ItemStats obj = GameManager.Instance.FindClosestShop().Available[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + "\n" + ConvertSlot(obj.slot) + "\n\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq + "\nPrice: " + obj.price;
        }
        else if(gameObject.CompareTag("ShopButton") && GameManager.Instance.FindClosestShop().BuyBack[n-1] != null && GameManager.Instance.FindClosestShop().buyback){
            ToolTipUI.SetActive(true);
            ItemStats obj = GameManager.Instance.FindClosestShop().BuyBack[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + "\n" + ConvertSlot(obj.slot) + "\n\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq + "\nPrice: " + obj.sellPrice;
        }
        else if(gameObject.CompareTag("Inventory Slot") && PlayerInventoryController.Instance.inventory[n-1] != null && PlayerInventoryController.Instance.inventory[n-1].CompareTag("Equipment")){
            ToolTipUI.SetActive(true);
            ItemStats obj = PlayerInventoryController.Instance.inventory[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + "\n" + ConvertSlot(obj.slot) + "\n\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq + "\nSell Price: " + obj.sellPrice;
        }
        else if(gameObject.CompareTag("Equipment Slot") && EquippedItemsController.Instance.equippedItems[n-1] != null){
            ToolTipUI.SetActive(true);
            ItemStats obj = EquippedItemsController.Instance.equippedItems[n-1].GetComponent<ItemStats>();
            ToolTipUI.GetComponentInChildren<TextMeshProUGUI>().text = obj.title + "\n" + ConvertSlot(obj.slot) + "\n\nDamage: +" + obj.attack + "\nAttack Range: +" + obj.attackRange + "\nAttack Speed: " + obj.attackSpeed + "\nHealth: +" + obj.health + "\nMana: +" + obj.mana + "\nMovement Speed: +" + obj.speed + "\n\nRequired Level: " + obj.levelReq + "\nSell Price: " + obj.sellPrice;
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
