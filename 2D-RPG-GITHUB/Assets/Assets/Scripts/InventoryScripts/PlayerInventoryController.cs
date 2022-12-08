using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public struct item{

    }
    public struct weapon{
        int attack;
        int levelReq;
        int attackRange;
    }
    public struct equipment{
        int health;
        int armor;
        int levelReq;
        int mana;
    }
    public GameObject[] inventory = new GameObject[16];
    public GameObject invManager;
    public int sp = 0;
    // Start is called before the first frame update
    void Start()
    {
        invManager = GameObject.Find("InventoryManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item){
        inventory[sp] = item;
        sp++;
        invManager.GetComponent<InventoryManager>().UpdateInventory();
    }

    public void RemoveItem(GameObject item){
        sp--;
        for(int i = 0; i<inventory.Length; i++){
            if(inventory[i] == item){
                for(int j = i; j<inventory.Length; j++){
                    if(j < inventory.Length-1){
                        inventory[j] = inventory[j+1];
                    }else{
                        inventory[j] = null;
                    }
                }
                invManager.GetComponent<InventoryManager>().UpdateInventory();
                return;
            }
        }
    }
}
