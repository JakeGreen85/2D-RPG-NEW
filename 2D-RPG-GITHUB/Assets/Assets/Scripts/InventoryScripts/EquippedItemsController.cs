using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsController : MonoBehaviour
{
    public GameObject[] equippedItems = new GameObject[10];

    public void EquipItem(GameObject itemToEquip){
        ItemStats stats = itemToEquip.GetComponent<ItemStats>();
        if(GameObject.Find("Player").GetComponent<PlayerController>().level < stats.levelReq){
            Debug.Log("Level too low");
            return;
        }
        if(equippedItems[stats.slot] != null){
            UnequipItem(equippedItems[stats.slot]);
        }
        equippedItems[stats.slot] = itemToEquip;
        GameObject.Find("Player").GetComponent<PlayerController>().atk += stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().attackRange += stats.attackRange;
        GameObject.Find("Player").GetComponent<PlayerController>().mana += stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().maxmana += stats.mana;
        GameObject.Find("Player").GetComponent<PlayerController>().health += stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().maxHealth += stats.health;
        GameObject.Find("Player").GetComponent<PlayerController>().attackSpeed += stats.attackSpeed;
        GameObject.Find("Player").GetComponent<PlayerController>().atk += stats.attack;
        GameObject.Find("Player").GetComponent<PlayerController>().speed += stats.speed;
        GetComponent<PlayerInventoryController>().RemoveItem(itemToEquip);
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().UpdateEquipment();
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().UpdateInventory();
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