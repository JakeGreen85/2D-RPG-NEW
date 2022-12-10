using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject[] activeQuests = new GameObject[10];
    public GameObject[] completedQuests = new GameObject[20];
    public GameObject player;


    public GameObject questGiven;
    public GameObject questComplete;
    public GameObject givenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveQuest(GameObject quest){
        Debug.Log("Quest given");
        givenPrefab.gameObject.SetActive(true);
        questGiven = quest;
        GameObject.Find("QuestInfoText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().questInfo;
        GameObject.Find("GoalInfoText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goalInfo;
        GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goldReward.ToString();
        GameObject.Find("ExpText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().expReward.ToString();
        GameObject.Find("ItemRewardSprite").GetComponent<Image>().sprite = quest.GetComponent<Quest>().rewards[0].GetComponent<SpriteRenderer>().sprite;
    }

    public void CloseQuest(){
        if(givenPrefab.gameObject.active){
            givenPrefab.gameObject.SetActive(false);
        }else{
            questComplete.gameObject.SetActive(false);
        }
    }

    public void AcceptQuest(){
        givenPrefab.SetActive(false);
        addQuest();
    }

    public bool CompleteQuest(GameObject quest){
        questComplete.gameObject.SetActive(true);
        questGiven = quest;
        if(quest.GetComponent<Quest>().killGoal == 0){
            GameObject.Find("CompleteGoalInfoText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().gathers.ToString() + "/" + quest.GetComponent<Quest>().gatherGoal.ToString() + " collected";
        }else{
            GameObject.Find("CompleteGoalInfoText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().kills.ToString() + "/" + quest.GetComponent<Quest>().killGoal.ToString() + " killed";
        }
        GameObject.Find("CompleteGoldText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goldReward.ToString();
        GameObject.Find("CompleteExpText").GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().expReward.ToString();
        GameObject.Find("ItemCompleteSprite").GetComponent<Image>().sprite = quest.GetComponent<Quest>().rewards[0].GetComponent<SpriteRenderer>().sprite;
        if(quest.GetComponent<Quest>().kills >= quest.GetComponent<Quest>().killGoal && quest.GetComponent<Quest>().gathers >= quest.GetComponent<Quest>().gatherGoal){
            return true;
        }
        else{
            return false;
        }
    }

    public void ClickComplete(){
        if(questGiven.GetComponent<Quest>().complete){
            player.GetComponent<PlayerController>().gold += questGiven.GetComponent<Quest>().goldReward;
            player.GetComponent<PlayerController>().experience += questGiven.GetComponent<Quest>().expReward;
            for(int i = 0; i < questGiven.GetComponent<Quest>().gatherGoal; i++){
                GameObject.Find("InventoryManager").GetComponent<PlayerInventoryController>().RemoveItem(GameObject.FindGameObjectWithTag(questGiven.GetComponent<Quest>().gatherType));
            }
            foreach(GameObject reward in questGiven.GetComponent<Quest>().rewards){
                GameObject.Find("InventoryManager").GetComponent<PlayerInventoryController>().AddItem(reward);
            }
            //completedQuests[0] = questGiven;
            RemoveQuest();
            CloseQuest();
            // Give rewards
            // Give the next quest ?
        }
    }

public void RemoveQuest(){
    for(int i = 0; i < activeQuests.Length; i++){
        if(activeQuests[i] == questGiven){
            activeQuests[i] = null;
            for(int j = i; j < activeQuests.Length-1; j++){
                activeQuests[j] = activeQuests[j+1];
            }
            activeQuests[activeQuests.Length-1] = null;
        }
    }
}

    public void addQuest(){
        for(int i = 0; i < activeQuests.Length; i++){
            if(activeQuests[i] == null){
                activeQuests[i] = questGiven;
                return;
            }
        }
    }
}
