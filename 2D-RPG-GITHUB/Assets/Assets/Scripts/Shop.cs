using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] BuyBack;
    public GameObject[] Available;
    public GameObject[] buttons;
    bool buyback = false;


    private void Start() {
        buttons = GameManager.Instance.ShopButtons;
    }

    public void Buy(int n){
        GameManager.Instance.player.GetComponent<PlayerController>().gold -= GameManager.Instance.PIC.inventory[n].GetComponent<ItemStats>().price;
        if(buyback){
            GameManager.Instance.PIC.AddItem(BuyBack[n]);
        }
        else{
            GameManager.Instance.PIC.AddItem(Available[n]);
        }
    }

    public void Sell(int n){
        GameManager.Instance.player.GetComponent<PlayerController>().gold += GameManager.Instance.PIC.inventory[n].GetComponent<ItemStats>().sellPrice;
        GameManager.Instance.PIC.RemoveItem(GameManager.Instance.PIC.inventory[n]);
    }

    public void OpenShop(){
        for(int i = 0; i < Available.Length; i++){
            buttons[i].GetComponent<Image>().sprite = Available[i].GetComponent<SpriteRenderer>().sprite;
        }
        buyback = false;
        GameManager.Instance.ShopUI.SetActive(true);
    }

    public void OpenBuyBack(){
        for(int i = 0; i < BuyBack.Length; i++){
            buttons[i].GetComponent<Image>().sprite = BuyBack[i].GetComponent<SpriteRenderer>().sprite;
        }
        buyback = true;
        GameManager.Instance.ShopUI.SetActive(true);
    }

    public void ToggleShop(){
        GameManager.Instance.ShopUI.SetActive(!GameManager.Instance.ShopUI.active);
    }
}
