using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject player;
    public Sprite noItem;
    DisplayUI dUI;
    EquippedItemsController EIC;
    PlayerInventoryController PIC;
    public GameObject[] inventorySlots;
    public GameObject[] equipmentSlots;

    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        inventorySlots = GameObject.FindGameObjectsWithTag("Inventory Slot");
        equipmentSlots = GameObject.FindGameObjectsWithTag("Equipment Slot");
        dUI = GetComponent<DisplayUI>();
        EIC = GameManager.Instance.EIC;
        PIC = GameManager.Instance.PIC;
        dUI.DisplayEquipment();
        dUI.DisplayInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableInventory(){
        UpdateInventory();
        dUI.DisplayInventory();
    }

    public void EnableEquipment(){
        UpdateEquipment();
        dUI.DisplayEquipment();
    }

    public void UpdateInventory(){
        for(int i = 0; i < PIC.sp; i++){
            inventorySlots[i].GetComponent<Image>().sprite = PIC.inventory[i].GetComponent<SpriteRenderer>().sprite;
        }
        for(int j = PIC.sp; j < PIC.inventory.Length; j++){
            inventorySlots[j].GetComponent<Image>().sprite = noItem;
        }
    }

    public void UpdateEquipment(){
        for(int i = 0; i < EIC.equippedItems.Length; i++){
            if(EIC.equippedItems[i] != null){
                equipmentSlots[i].GetComponent<Image>().sprite = EIC.equippedItems[i].GetComponent<SpriteRenderer>().sprite;
            }else{
                equipmentSlots[i].GetComponent<Image>().sprite = noItem;
            }
        }
    }
}
