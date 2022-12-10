using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public GameObject UI;
    public GameObject selectedItem;
    public GameObject[] craftables;
    private void Start() {
    }

    private void Update(){
        if(UI.activeInHierarchy){
            if(Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position)>5){
                ToggleUI();
            }
        }
    }
    public void ToggleUI(){
        UI.SetActive(!UI.activeInHierarchy);
    }

    public void CraftItem(){

    }

    public void ChooseItem(){

    }
}
