using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public GameObject[] inventory;
    public InventoryManager invManager;
    public int sp = 0;

    private static PlayerInventoryController _instance;
    public static PlayerInventoryController Instance{
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
    // Start is called before the first frame update
    void Start()
    {
        invManager = InventoryManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(GameObject item){
        if(sp<inventory.Length){
            inventory[sp] = item;
            sp++;
            InventoryManager.Instance.UpdateInventory();
            return true;
        }
        else{
            return false;
        }
    }

    public void RemoveItem(GameObject item){
        for(int i = 0; i<inventory.Length; i++){
            if(inventory[i] == item){
                for(int j = i; j<inventory.Length; j++){
                    if(j < inventory.Length-1){
                        inventory[j] = inventory[j+1];
                    }else{
                        inventory[j] = null;
                    }
                }
                sp--;
                InventoryManager.Instance.UpdateInventory();
                return;
            }
        }
    }
}
