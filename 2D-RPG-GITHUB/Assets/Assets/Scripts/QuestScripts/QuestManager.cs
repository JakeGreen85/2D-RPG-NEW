using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject[] activeQuests = new GameObject[10];
    public GameObject[] completedQuests;
    public GameObject player;


    public GameObject questGiven;
    public GameObject questComplete;
    public GameObject givenPrefab;
    public GameObject givenQuestInfoText;
    public GameObject givenGoalInfoText;
    public GameObject givenGoldRewardsText;
    public GameObject GivenExpRewardsText;
    public GameObject GivenItemRewardSprite;
    public GameObject completeQuestInfoText;
    public GameObject completeGoalInfoText;
    public GameObject completeGoldRewardsText;
    public GameObject completeExpRewardsText;
    public GameObject completeItemRewardSprite;
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
        givenQuestInfoText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().questInfo;
        givenGoalInfoText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goalInfo;
        givenGoldRewardsText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goldReward.ToString();
        GivenExpRewardsText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().expReward.ToString();
        GivenItemRewardSprite.GetComponent<Image>().sprite = quest.GetComponent<Quest>().rewards[0].GetComponent<SpriteRenderer>().sprite;
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
            completeGoalInfoText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().gathers.ToString() + "/" + quest.GetComponent<Quest>().gatherGoal.ToString() + " collected";
        }else{
            completeGoalInfoText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().kills.ToString() + "/" + quest.GetComponent<Quest>().killGoal.ToString() + " killed";
        }
        completeGoldRewardsText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().goldReward.ToString();
        completeExpRewardsText.GetComponent<TextMeshProUGUI>().text = quest.GetComponent<Quest>().expReward.ToString();
        completeItemRewardSprite.GetComponent<Image>().sprite = quest.GetComponent<Quest>().rewards[0].GetComponent<SpriteRenderer>().sprite;
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
            foreach(GameObject reward in questGiven.GetComponent<Quest>().rewards){
                GameObject.Find("InventoryManager").GetComponent<PlayerInventoryController>().AddItem(reward);
            }
            CloseQuest();
            // Give rewards
            // Give the next quest ?
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
