using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsController : MonoBehaviour
{
    public GameObject[] equippedItems = new GameObject[10];
    private static EquippedItemsController _instance;
    public static EquippedItemsController Instance{
        get{
            return _instance;
        }
    }

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }

    public void EquipItem(GameObject itemToEquip){
        ItemStats stats = itemToEquip.GetComponent<ItemStats>();
        if(GameObject.Find("Player").GetComponent<PlayerController>().level < stats.levelReq){
            Debug.Log("Level too low");
            return;
        }
        GetComponent<PlayerInventoryController>().RemoveItem(itemToEquip);
        GameObject.Find("Player").GetComponent<PlayerController>().atk += stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().attackRange += stats.attackRange;
        GameObject.Find("Player").GetComponent<PlayerController>().mana += stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().maxmana += stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().health += stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().maxHealth += stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().attackSpeed += stats.attackSpeed;
        GameObject.Find("Player").GetComponent<PlayerController>().atk += stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().speed += stats.speed;

        if(equippedItems[stats.slot] != null){
            UnequipItem(equippedItems[stats.slot]);
        }
        equippedItems[stats.slot] = itemToEquip;
        GetComponent<InventoryManager>().UpdateEquipment();
        GetComponent<InventoryManager>().UpdateInventory();
    }

    public void UnequipItem(GameObject itemToUnequip){
        ItemStats stats = itemToUnequip.GetComponent<ItemStats>();
        GetComponent<PlayerInventoryController>().AddItem(itemToUnequip);
        
        equippedItems[stats.slot] = null;

        // Subtract stats from the removed item
        GameObject.Find("Player").GetComponent<PlayerController>().atk -= stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().attackRange -= stats.attackRange;
        GameObject.Find("Player").GetComponent<PlayerController>().mana -= stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().maxmana -= stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().health -= stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().maxHealth -= stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().attackSpeed -= stats.attackSpeed;
        GameObject.Find("Player").GetComponent<PlayerController>().atk -= stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().speed -= stats.speed;
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().UpdateEquipment();
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().UpdateInventory();
        
    }


}