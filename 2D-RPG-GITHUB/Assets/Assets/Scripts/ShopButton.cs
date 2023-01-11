using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject Shop;

    public void TaskOnClick(){
        int n = System.Int32.Parse(gameObject.name.Substring(6));
        Shop.GetComponent<Shop>().Buy(n);
    }
}
