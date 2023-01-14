using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject player;
    public Sprite noItem;
    DisplayUI dUI;
    public EquippedItemsController EIC;
    public PlayerInventoryController PIC;
    public GameObject[] inventorySlots;
    public GameObject[] equipmentSlots;

    private static InventoryManager _instance;
    public static InventoryManager Instance{
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
    [System.Obsolete]
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        //inventorySlots = GameObject.FindGameObjectsWithTag("Inventory Slot");
        //equipmentSlots = GameObject.FindGameObjectsWithTag("Equipment Slot");

        dUI = GetComponent<DisplayUI>();
        
        dUI.DisplayEquipment();
        dUI.DisplayInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Obsolete]
    public void EnableInventory(){
        UpdateInventory();
        dUI.DisplayInventory();
    }

    [System.Obsolete]
    public void EnableEquipment(){
        UpdateEquipment();
        dUI.DisplayEquipment();
    }

    public void UpdateInventory(){
        for(int i = 0; i < PlayerInventoryController.Instance.sp; i++){
            inventorySlots[i].GetComponent<Image>().sprite = PlayerInventoryController.Instance.inventory[i].GetComponent<SpriteRenderer>().sprite;
        }
        for(int j = PlayerInventoryController.Instance.sp; j < PlayerInventoryController.Instance.inventory.Length; j++){
            inventorySlots[j].GetComponent<Image>().sprite = noItem;
        }
    }

    public void UpdateEquipment(){
        for(int i = 0; i < EquippedItemsController.Instance.equippedItems.Length; i++){
            if(EquippedItemsController.Instance.equippedItems[i] != null){
                equipmentSlots[i].GetComponent<Image>().sprite = EquippedItemsController.Instance.equippedItems[i].GetComponent<SpriteRenderer>().sprite;
            }else{
                equipmentSlots[i].GetComponent<Image>().sprite = noItem;
            }
        }
    }
}
