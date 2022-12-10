using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEnemy : MonoBehaviour
{
    System.Random rand = new System.Random();
    public GameObject[] drops;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLoot(){
        GameObject drop = drops[rand.Next(drops.Length)];
        Debug.Log(drop.ToString());
        
        if(GameObject.Find("InventoryManager").GetComponent<PlayerInventoryController>().AddItem(drop)){
            foreach(GameObject q in GameObject.Find("QuestManager").GetComponent<QuestManager>().activeQuests){
                if(q != null){
                    q.GetComponent<Quest>().CheckGather(drop);
                }
            }
            GameObject.Destroy(gameObject);
        }
        else{
            Debug.Log("Inventory Full");
        }
    }
}
