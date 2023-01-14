using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] BuyBack = new GameObject[16];
    public GameObject[] Available = new GameObject[16];
    public GameObject[] buttons;
    public Sprite noItem;
    public bool buyback = false;

    private void Start() {
        buttons = GameManager.Instance.ShopButtons;
    }

    [System.Obsolete]
    private void Update() {
        CheckDistance();
    }

    public void Buy(int n){
        int buyprice = 0;
        // Check if player inventory is full
        if(PlayerInventoryController.Instance.inventory.Length <= PlayerInventoryController.Instance.sp) return;
        if(buyback){
            if(BuyBack[n] == null) return;
            PlayerInventoryController.Instance.AddItem(BuyBack[n]);
            buyprice = BuyBack[n].GetComponent<ItemStats>().sellPrice;
            for(int i = n; i < BuyBack.Length; i++){
                if(i == BuyBack.Length-1){
                    BuyBack[i] = null;
                }
                else{
                    BuyBack[i] = BuyBack[i+1];
                }
            }
        }
        else{
            if(Available[n] == null) return;
            PlayerInventoryController.Instance.AddItem(Available[n]);
            buyprice = Available[n].GetComponent<ItemStats>().price;
        }
        GameManager.Instance.player.GetComponent<PlayerController>().gold -= buyprice;
        UpdateEquipment();
    }

    public void Sell(int n){
        if(PlayerInventoryController.Instance.sp == 0) return;
        GameManager.Instance.player.GetComponent<PlayerController>().gold += PlayerInventoryController.Instance.inventory[n].GetComponent<ItemStats>().sellPrice;
        AddToBuyBack(PlayerInventoryController.Instance.inventory[n]);
        PlayerInventoryController.Instance.RemoveItem(PlayerInventoryController.Instance.inventory[n]);
        UpdateEquipment();
    }

    void AddToBuyBack(GameObject item){
        for(int i = BuyBack.Length-1; i > 0; i--){
            BuyBack[i] = BuyBack[i-1];
        }
        BuyBack[0] = item;
        UpdateEquipment();
    }

    public void OpenShop(){
        buyback = false;
        GameManager.Instance.ShopUI.SetActive(true);
        UpdateEquipment();
        for(int i = 0; i < Available.Length; i++){
            buttons[i].GetComponent<Image>().sprite = Available[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void OpenBuyBack(){
        buyback = true;
        GameManager.Instance.ShopUI.SetActive(true);
        UpdateEquipment();
        for(int i = 0; i < BuyBack.Length; i++){
            if(BuyBack[i] != null){
                buttons[i].GetComponent<Image>().sprite = BuyBack[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    [System.Obsolete]
    void CheckDistance(){
        if(GameManager.Instance.ShopUI.active){
            if(Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position)>3){
                GameManager.Instance.ShopUI.SetActive(false);
            }
        }
    }

    public void UpdateEquipment(){
        GameObject[] displayList;
        if(buyback){
            displayList = BuyBack;
        }
        else{
            displayList = Available;
        }
        for(int i = 0; i < buttons.Length; i++){
            if(displayList[i] != null){
                buttons[i].GetComponent<Image>().sprite = displayList[i].GetComponent<SpriteRenderer>().sprite;
            }else{
                buttons[i].GetComponent<Image>().sprite = noItem;
            }
        }
    }
}
