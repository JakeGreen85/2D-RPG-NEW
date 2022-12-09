using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public GameObject UI;
    public GameObject selectedItem;
    public GameObject[] craftables;
    public void ToggleUI(){
        UI.SetActive(!UI.activeInHierarchy);
    }

    public void CraftItem(){

    }

    public void ChooseItem(){

    }
}
