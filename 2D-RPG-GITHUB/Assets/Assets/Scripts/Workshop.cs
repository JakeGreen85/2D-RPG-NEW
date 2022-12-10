using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public GameObject UI;
    public GameObject selectedItem;
    public GameObject[] craftables;
    private void Start() {
        UI = GameObject.Find("WorkshopUI");
    }
    public void ToggleUI(){
        UI.SetActive(!UI.activeInHierarchy);
    }

    public void CraftItem(){

    }

    public void ChooseItem(){

    }
}
